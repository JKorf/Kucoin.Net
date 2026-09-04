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

namespace Kucoin.Net.Clients.FuturesApi
{
    internal partial class KucoinRestClientFuturesSharedApi
    {

        #region Place Futures Order

        async Task<ICallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.Leverage), typeof(decimal), "The leverage for opening the position", 3m)
            }
        };

        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                GetOrderSide(request.Side, request.PositionSide),
                request.OrderType == SharedOrderType.Limit ? Enums.NewOrderType.Limit : Enums.NewOrderType.Market,
                request.Leverage!.Value,
                quantity: (int?)request.Quantity?.QuantityInContracts,
                quantityInBaseAsset: request.Quantity?.QuantityInContracts == null ? request.Quantity?.QuantityInBaseAsset : null,
                price: request.Price,
                postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                reduceOnly: request.ReduceOnly,
                timeInForce: GetTimeInForce(request.TimeInForce),
                clientOrderId: request.ClientOrderId,
                marginMode: request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Isolated ? FuturesMarginMode.Isolated : FuturesMarginMode.Cross,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        #endregion

        #region Get Futures Order

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.CancelExist),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.ExecutedValue, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                Leverage = order.Data.Leverage,
                ReduceOnly = order.Data.ReduceOnly,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                TriggerPrice = order.Data.StopPrice,
                IsTriggerOrder = order.Data.StopPrice > 0,
                IsCloseOrder = order.Data.CloseOrder,
                PositionSide = ParsePositionSide(order.Data.PositionSide)
            });
        }

        #endregion

        #region Get Open Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var ordersTask = _api.Trading.GetOrdersAsync(symbol, OrderStatus.Active, ct: ct);
            var stopOrdersTask = _api.Trading.GetUntriggeredStopOrdersAsync(symbol, ct: ct);
            await Task.WhenAll(ordersTask, stopOrdersTask).ConfigureAwait(false);
            if (!ordersTask.Result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(ordersTask.Result);
            if (!stopOrdersTask.Result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(stopOrdersTask.Result);

            var orders = ordersTask.Result;
            var stopOrders = stopOrdersTask.Result;

            var result = orders.Data.Items.Concat(stopOrders.Data.Items).OrderByDescending(x => x.CreateTime);
            return HttpResult.Ok(orders, result.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.Id.ToString(),
                x.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                SharedOrderStatus.Open,
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ExecutedValue, contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                Leverage = x.Leverage,
                ReduceOnly = x.ReduceOnly,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                TriggerPrice = x.StopPrice,
                IsTriggerOrder = x.StopPrice > 0,
                IsCloseOrder = x.CloseOrder,
                PositionSide = ParsePositionSide(x.PositionSide)
            }).ToArray());
        }

        #endregion

        #region Get Closed Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetOrdersAsync(request.Symbol!.GetSymbol(FormatSymbol),
                OrderStatus.Done,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        x.Id.ToString(),
                        x.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(x.Type),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status, x.CancelExist),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ExecutedValue, contractQuantity: x.QuantityFilled),
                        TimeInForce = ParseTimeInForce(x.TimeInForce),
                        UpdateTime = x.UpdateTime,
                        Leverage = x.Leverage,
                        ReduceOnly = x.ReduceOnly,
                        AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                        TriggerPrice = x.StopPrice,
                        IsTriggerOrder = x.StopPrice > 0,
                        IsCloseOrder = x.CloseOrder,						
						PositionSide = ParsePositionSide(x.PositionSide)
                    }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Get Futures Order Trades

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var orders = await _api.Trading.GetUserTradesAsync(orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Items.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.Id,
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(contractQuantity: x.Quantity),
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Liquidity == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        #endregion

        #region Get Futures User Trade History

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesUserTradeHistory.GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFuturesUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        x.OrderId.ToString(),
                        x.Id,
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        Role = x.Liquidity == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel Futures Order

        async Task<ICallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Get Positions

        async Task<ICallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var result = await _api.Account.GetPositionsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            IEnumerable<KucoinPosition> data = result.Data;
            if (symbol != null)
                data = data.Where(x => x.Symbol == symbol);

            return HttpResult.Ok(result, data.Select(x =>
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                    x.Symbol,
                    new SharedOrderQuantity(contractQuantity: Math.Abs(x.CurrentQuantity)),
                    x.OpenTime)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    Leverage = x.RealLeverage,
                    AverageOpenPrice = x.AverageEntryPrice,
                    PositionMode = x.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                    PositionSide = x.CurrentQuantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short,
                }).ToArray());
        }

        #endregion

        #region Close Position

        async Task<ICallResult<SharedId>> IClosePosition.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
            => await ClosePositionAsync(request, ct).ConfigureAwait(false);

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.PositionSide), typeof(SharedPositionSide), "Position side to close", SharedPositionSide.Short)
            }
        };
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                NewOrderType.Market,
                0,
                0,
                closeOrder: true,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        #endregion

        private OrderSide GetOrderSide(SharedOrderSide side, SharedPositionSide? posSide)
        {
            if (posSide == null) return side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell;

            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy)
                    return OrderSide.Buy;
                return OrderSide.Sell;
            }

            if (side == SharedOrderSide.Buy)
                return OrderSide.Sell;
            return OrderSide.Buy;
        }

        private TimeInForce? GetTimeInForce(SharedTimeInForce? tif)
        {
            if (tif == null)
                return null;

            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status, bool cancelExists)
        {
            if (status == OrderStatus.Active) return SharedOrderStatus.Open;
            if (cancelExists) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Done) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? tif)
        {
            if (tif == null)
                return null;

            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private SharedPositionSide? ParsePositionSide(PositionSide? positionSide)
            => positionSide switch
            {
                PositionSide.Long => SharedPositionSide.Long,
                PositionSide.Short => SharedPositionSide.Short,
                _ => null
            };

        #region Get Futures Order By Client Order Id

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrderByClientOrderId.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderByClientOrderIdAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.CancelExist),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.ExecutedValue, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                Leverage = order.Data.Leverage,
                ReduceOnly = order.Data.ReduceOnly,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                TriggerPrice = order.Data.StopPrice,
                IsTriggerOrder = order.Data.StopPrice > 0,
                IsCloseOrder = order.Data.CloseOrder,
                PositionSide = ParsePositionSide(order.Data.PositionSide),
            });
        }

        #endregion

        #region Cancel Futures Order By Client Order Id

        async Task<ICallResult<SharedId>> ICancelFuturesOrderByClientOrderId.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.CanceledOrderId));
        }

        #endregion
    }
}
