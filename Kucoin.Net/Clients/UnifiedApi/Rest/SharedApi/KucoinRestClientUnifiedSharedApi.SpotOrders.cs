using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects.Models.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static CryptoExchange.Net.SharedApis.SharedCapabilities;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Place Spot Order

        async Task<IExchangeCallResult<SharedId>> IPlaceSpotOrder.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
            => await PlaceSpotOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceSpotOrderOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

                var result = await _api.Trading.PlaceOrderAsync(
                    UnifiedSimpleAccountType.Spot,
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    GetPlaceOrderType(request.OrderType),
                    request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                    request.Price,
                    quantityUnit: request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                    timeInForce: GetTimeInForce(request.TimeInForce),
                    postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                    clientOrderId: request.ClientOrderId).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            
        }

        #endregion

        #region Get Spot Order

        async Task<IExchangeCallResult<SharedSpotOrder>> IGetSpotOrder.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderOptions GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);
                       
            var order = await _api.Trading.GetOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data.OrderType, order.Data.PostOnly),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.OrderTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
#warning how is quote quantity handled?
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                TriggerPrice = order.Data.TriggerPrice,
                IsTriggerOrder = order.Data.TriggerPrice > 0,
                AveragePrice = order.Data.AveragePrice,
                UpdateTime = order.Data.UpdateTime
            });
            
        }

        #endregion


        #region Get Spot Order By Client Order Id

        async Task<IExchangeCallResult<SharedSpotOrder>> IGetSpotOrderByClientOrderId.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderByClientOrderIdOptions GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                clientOrderId: request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                   ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
                   order.Data.Symbol,
                   order.Data.OrderId.ToString(),
                   ParseOrderType(order.Data.OrderType, order.Data.PostOnly),
                   order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                   ParseOrderStatus(order.Data.Status),
                   order.Data.OrderTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
#warning how is quote quantity handled?
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                TriggerPrice = order.Data.TriggerPrice,
                IsTriggerOrder = order.Data.TriggerPrice > 0,
                AveragePrice = order.Data.AveragePrice,
                UpdateTime = order.Data.UpdateTime
            });
        }

        #endregion

        #region Get Open Spot Orders

        async Task<IExchangeCallResult<SharedSpotOrder[]>> IGetOpenSpotOrders.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenSpotOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenSpotOrdersOptions GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder[]>> GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);
                        
            if (request.Symbol == null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, ArgumentError.Missing("Symbol", "Symbol parameter is required for HfTrading account"));

            var symbol = request.Symbol.GetSymbol(FormatSymbol);
            var order = await _api.Trading.GetOpenOrdersAsync(
                UnifiedSimpleAccountType.Spot,
                symbol).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(order);

            return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedSpotOrder(
                   ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                   x.Symbol,
                   x.OrderId.ToString(),
                   ParseOrderType(x.OrderType, x.PostOnly),
                   x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                   ParseOrderStatus(x.Status),
                   x.OrderTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
#warning how is quote quantity handled?
                OrderQuantity = new SharedOrderQuantity(x.Quantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                TriggerPrice = x.TriggerPrice,
                IsTriggerOrder = x.TriggerPrice > 0,
                AveragePrice = x.AveragePrice,
                UpdateTime = x.UpdateTime
            }).ToArray());            
        }

        #endregion

        #region Get Closed Spot Orders

        async Task<IExchangeCallResult<SharedSpotOrder[]>> IGetClosedSpotOrders.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedSpotOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetSpotClosedOrdersOptions GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 200);
        public async Task<HttpResult<SharedSpotOrder[]>> GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);
                       
            // Determine page token
            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetOrderHistoryAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                pageSize: pageParams.Limit,
                lastId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                        () => result.Data.LastId == null ? null : Pagination.NextPageFromId(result.Data.LastId.Value),
                        result.Data.Items.Length,
                        result.Data.Items.Select(x => x.OrderTime),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams,
                        TimeSpan.FromDays(7));

            return HttpResult.Ok(result, result.Data.Items.Select(x => new SharedSpotOrder(
               ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
               x.Symbol,
               x.OrderId.ToString(),
               ParseOrderType(x.OrderType, x.PostOnly),
               x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
               ParseOrderStatus(x.Status),
               x.OrderTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
#warning how is quote quantity handled?
                OrderQuantity = new SharedOrderQuantity(x.Quantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                TriggerPrice = x.TriggerPrice,
                IsTriggerOrder = x.TriggerPrice > 0,
                AveragePrice = x.AveragePrice,
                UpdateTime = x.UpdateTime
            }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Get Spot Order Trades

        async Task<IExchangeCallResult<SharedUserTrade[]>> IGetSpotOrderTrades.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetSpotOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderTradesOptions GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);
                        
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var order = await _api.Trading.GetUserTradesAsync(
                UnifiedSimpleAccountType.Spot,
                symbol,
                orderId: request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedUserTrade[]>(order);

            return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantity, x.Value),
                x.Price,
                x.ExecutionTime)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.LiquidityRole == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());            
        }

        #endregion

        #region Get Spot User Trade History

        async Task<IExchangeCallResult<SharedUserTrade[]>> IGetSpotUserTradeHistory.GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetSpotUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetSpotUserTradeHistoryOptions GetSpotUserTradeHistoryOptions { get; } = new GetSpotUserTradeHistoryOptions(_exchangeName, false, true, true, 200);
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetSpotUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);
                        
            // Determine page token
            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                pageSize: pageParams.Limit,
                lastId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                        () => result.Data.LastId == null ? null : Pagination.NextPageFromId(result.Data.LastId.Value),
                        result.Data.Items.Length,
                        result.Data.Items.Select(x => x.ExecutionTime),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams,
                        TimeSpan.FromDays(7));

            return HttpResult.Ok(result, result.Data.Items.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId.ToString(),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantity, x.Value),
                x.Price,
                x.ExecutionTime)
                {
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.LiquidityRole == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
                }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel Spot Order

        async Task<IExchangeCallResult<SharedId>> ICancelSpotOrder.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);
                        
            var order = await _api.Trading.CancelOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(UnifiedOrderStatus status)
        {
            if (status == UnifiedOrderStatus.Live || status == UnifiedOrderStatus.NotTriggered || status == UnifiedOrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == UnifiedOrderStatus.Canceled || status == UnifiedOrderStatus.PartiallyCanceled) return SharedOrderStatus.Canceled;
            if (status == UnifiedOrderStatus.Filled) return SharedOrderStatus.Filled;
            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type, bool? postOnly)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.Limit && postOnly == true) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? tif)
        {
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;
            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private OrderType GetPlaceOrderType(SharedOrderType type)
        {
            if (type == SharedOrderType.Market) return OrderType.Market;

            return OrderType.Limit;
        }

        private TimeInForce? GetTimeInForce(SharedTimeInForce? tif)
        {
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;

            return null;
        }

        #region Cancel Spot Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> ICancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderByClientOrderIdOptions CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);
                        
            var order = await _api.Trading.CancelOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                clientOrderId: request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel All Spot Symbol Orders

        async Task<IExchangeCallResult> ICancelAllSpotSymbolOrders.CancelAllSpotSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
            => await CancelAllSpotSymbolOrdersAsync(request, ct).ConfigureAwait(false);

        public CancelAllSpotSymbolOrdersOptions CancelAllSpotSymbolOrdersOptions { get; } = new CancelAllSpotSymbolOrdersOptions(_exchangeName, true);
        public async Task<HttpResult> CancelAllSpotSymbolOrdersAsync(CancelAllSymbolOrdersRequest request, CancellationToken ct)
        {
            var validationError = CancelAllSpotSymbolOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelSymbolOrdersAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order);
        }

        #endregion

        #region Edit Spot Order

        async Task<IExchangeCallResult<SharedId>> IEditSpotOrder.EditSpotOrderAsync(EditOrderRequest request, CancellationToken ct)
            => await EditSpotOrderAsync(request, ct).ConfigureAwait(false);

        public EditSpotOrderOptions EditSpotOrderOptions { get; } = new EditSpotOrderOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> EditSpotOrderAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.EditOrderAsync(
                request.OrderId,
                null,
                symbol: request.SymbolName(FormatSymbol),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                quantityUnit: request.Quantity == null ? null : request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion

        #region Edit Spot Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> IEditSpotOrderByClientOrderId.EditSpotOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
            => await EditSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public EditSpotOrderByClientOrderIdOptions EditSpotOrderByClientOrderIdOptions { get; } = new EditSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> EditSpotOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.EditOrderAsync(
                null,
                request.OrderId,
                symbol: request.SymbolName(FormatSymbol),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                quantityUnit: request.Quantity == null ? null : request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion
    }
}
