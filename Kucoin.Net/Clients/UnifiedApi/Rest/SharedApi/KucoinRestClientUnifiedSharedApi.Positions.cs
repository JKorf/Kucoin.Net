using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
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
        #region Get Position History

        async Task<IExchangeCallResult<SharedPositionHistory[]>> IGetPositionHistory.GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetPositionHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetPositionHistoryOptions GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, true, 200);
        public async Task<HttpResult<SharedPositionHistory[]>> GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetPositionHistoryAsync(
                request.Symbol?.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                lastId: pageParams.FromId == null ? null: long.Parse(pageParams.FromId),
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => result.Data.LastId == null ? null : Pagination.NextPageFromId(result.Data.LastId.Value),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(7));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedPositionHistory(
                            request.Symbol,
                            x.Symbol,
                            x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                            x.EntryPrice,
                            x.ClosePrice,
                            new SharedOrderQuantity(contractQuantity: x.MaxQuantity),
                            x.RealizedPnL,
                            x.CloseTime
                            )
                        {
                            Leverage = x.Leverage
                        }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Get All Positions

        async Task<IExchangeCallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await ((IGetPositionsRest)this).GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.Trading.GetPositionsAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    new SharedOrderQuantity(contractQuantity: x.Quantity),
                    x.UpdateTime
                    )
                {
                    AverageOpenPrice = x.EntryPrice,
                    Id = x.Id,
                    Leverage = x.Leverage,
                    LiquidationPrice = x.LiquidationPrice,
                    PositionSizes = new SharedOrderQuantity(contractQuantity: x.Quantity),
                    UnrealizedPnl = x.UnrealizedPnL
                }
            ).ToArray());
        }

        #endregion
    }
}
