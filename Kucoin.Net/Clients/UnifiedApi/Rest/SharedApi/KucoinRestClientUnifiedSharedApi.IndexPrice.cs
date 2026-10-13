using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net;
using Kucoin.Net.Objects.Models.Futures;
using CryptoExchange.Net.Objects.Errors;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Get Index Price

        async Task<IExchangeCallResult<SharedIndexPrice>> IGetIndexPrice.GetIndexPriceAsync(GetIndexPriceRequest request, CancellationToken ct)
            => await ((IGetIndexPrice)this).GetIndexPriceAsync(request, ct).ConfigureAwait(false);

        public GetIndexPriceOptions GetIndexPriceOptions { get; } = new GetIndexPriceOptions(_exchangeName, false);
        public async Task<HttpResult<SharedIndexPrice>> GetIndexPriceAsync(GetIndexPriceRequest request, CancellationToken ct)
        {
            var validationError = GetIndexPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedIndexPrice>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetTickersAsync(
                productType,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedIndexPrice>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedIndexPrice>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedIndexPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, ticker.Symbol),
                    ticker.Symbol,
                    ticker.IndexPrice ?? 0));
        }


        #endregion

        #region Get All Index Prices

        async Task<IExchangeCallResult<SharedIndexPrice[]>> IGetAllIndexPrices.GetAllIndexPricesAsync(GetAllIndexPricesRequest request, CancellationToken ct)
            => await ((IGetAllIndexPrices)this).GetAllIndexPricesAsync(request, ct).ConfigureAwait(false);

        public GetAllIndexPricesOptions GetAllIndexPricesOptions { get; } = new GetAllIndexPricesOptions(_exchangeName, false);
        public async Task<HttpResult<SharedIndexPrice[]>> GetAllIndexPricesAsync(GetAllIndexPricesRequest request, CancellationToken ct)
        {
            var validationError = GetAllIndexPricesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedIndexPrice[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(
                ProductType.Futures,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedIndexPrice[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedIndexPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.IndexPrice ?? 0)
            ).ToArray());
        }

        #endregion

        #region Get Index Price Klines

        async Task<IExchangeCallResult<SharedFuturesKline[]>> IGetIndexPriceKlines.GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetIndexPriceKlinesAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetIndexPriceKlinesOptions GetIndexPriceKlinesOptions { get; } = new GetIndexPriceKlinesOptions(_exchangeName, false, true, true, 200, false);

        public async Task<HttpResult<SharedFuturesKline[]>> GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;

            var validationError = GetIndexPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetKlinesAsync(
                productType,
                symbol,
                interval,
                type: KlineType.IndexPrice,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime).Add(TimeSpan.FromSeconds(-(int)interval))),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFuturesKline(
                            request.Symbol,
                            symbol,
                            x.OpenTime,
                            x.ClosePrice,
                            x.HighPrice,
                            x.LowPrice,
                            x.OpenPrice))
                    .ToArray(), nextPageRequest);
        }

        #endregion

    }
}
