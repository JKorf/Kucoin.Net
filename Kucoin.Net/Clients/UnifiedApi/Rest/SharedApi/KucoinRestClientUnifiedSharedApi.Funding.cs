using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Get Funding Info

        async Task<IExchangeCallResult<SharedFundingInfo>> IGetFundingInfo.GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
            => await GetFundingInfoAsync(request, ct).ConfigureAwait(false);

        public GetFundingInfoOptions GetFundingInfoOptions { get; } = new GetFundingInfoOptions(_exchangeName, false, false, false, 0, false);

        public async Task<HttpResult<SharedFundingInfo>> GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
        {
            var validationError = GetFundingInfoOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingInfo>(Exchange, validationError);

            // Get data
            var result = await _api.ExchangeData.GetFundingRatesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingInfo>(result);

            var fundingInfo = result.Data.SingleOrDefault();
            if (fundingInfo == null)
                return HttpResult.Fail<SharedFundingInfo>(Exchange, new ServerError(ErrorType.UnknownSymbol, "No funding info returned"));

            // Return
            return HttpResult.Ok(result, new SharedFundingInfo(
                fundingInfo.NextFundingRate,
                fundingInfo.FundingTime,
                (int)(fundingInfo.FundingInterval / 1000)));
        }

        #endregion

        #region Get Funding Rate History

        async Task<IExchangeCallResult<SharedFundingRate[]>> IGetFundingRateHistory.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFundingRateHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 100, false);

        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime ?? DateTime.UtcNow.AddDays(-7),
                endTime: pageParams.EndTime ?? DateTime.UtcNow,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingRate(x.FundingRate, x.Timestamp))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Get User Funding History

        async Task<IExchangeCallResult<SharedFundingFee[]>> IGetUserFundingHistory.GetUserFundingHistoryAsync(GetUserFundingHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetUserFundingHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetUserFundingHistoryOptions GetUserFundingHistoryOptions { get; } = new GetUserFundingHistoryOptions(_exchangeName, false, true, true, 200, false);

        public async Task<HttpResult<SharedFundingFee[]>> GetUserFundingHistoryAsync(GetUserFundingHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetUserFundingHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingFee[]>(Exchange, validationError);

            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetFundingFeeHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime ?? DateTime.UtcNow.AddDays(-7),
                endTime: pageParams.EndTime ?? DateTime.UtcNow,
                limit: limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingFee[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Items.Min(x => x.SettlementTime)),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.SettlementTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.SettlementTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingFee(x.Symbol, x.FundingFee, x.SettlementTime))
                    .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
