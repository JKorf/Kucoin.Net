using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinSocketClientUnifiedSharedApi
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
        public async Task<QueryResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.PlaceOrderAsync(
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
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
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

        #endregion

        #region Edit Spot Order

        async Task<IExchangeCallResult<SharedId>> IEditSpotOrder.EditSpotOrderAsync(EditOrderRequest request, CancellationToken ct)
            => await EditSpotOrderAsync(request, ct).ConfigureAwait(false);

        public EditSpotOrderOptions EditSpotOrderOptions { get; } = new EditSpotOrderOptions(_exchangeName);
        public async Task<QueryResult<SharedId>> EditSpotOrderAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.EditOrderAsync(
                request.OrderId,
                null,
                symbol: request.SymbolName(FormatSymbol),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                quantityUnit: request.Quantity == null ? null : request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion

        #region Edit Spot Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> IEditSpotOrderByClientOrderId.EditSpotOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
            => await EditSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public EditSpotOrderByClientOrderIdOptions EditSpotOrderByClientOrderIdOptions { get; } = new EditSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> EditSpotOrderByClientOrderIdAsync(EditOrderRequest request, CancellationToken ct)
        {
            var validationError = EditSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.EditOrderAsync(
                null,
                request.OrderId,
                symbol: request.SymbolName(FormatSymbol),
                quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                quantityUnit: request.Quantity == null ? null : request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                ct: ct
                ).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data.OrderId?.ToString()));

        }

        #endregion

        #region Cancel Spot Order

        async Task<IExchangeCallResult<SharedId>> ICancelSpotOrder.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.CancelOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Cancel Spot Order By Client Order Id

        async Task<IExchangeCallResult<SharedId>> ICancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderByClientOrderIdOptions CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<QueryResult<SharedId>> CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.CancelOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                clientOrderId: request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Subscribe Spot Order

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                UnifiedAccountType.Unified,
                update => {
                    if (update.Data.TradeType != UnifiedAccountType.Spot)
                        return;

                    var sharedSymbol = ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, update.Data.Symbol);
                    handler(update.ToType<SharedSpotOrderUpdate[]>(
                        [new SharedSpotOrderUpdate(
                            sharedSymbol,
                            update.Data.Symbol,
                            update.Data.OrderId,
                            ParseOrderType(update.Data.OrderType, update.Data.PostOnly),
                            update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(update.Data.Status),
                            update.Data.CreateTime
                            )
                        {
                            TimeInForce = ParseTimeInForce(update.Data.TimeInForce),
                            AveragePrice = update.Data.AveragePrice,
                            ClientOrderId = update.Data.ClientOrderId,
                            IsTriggerOrder = update.Data.TriggerPrice != null,
                            OrderPrice = update.Data.Price,
#warning check quantity unit
                            OrderQuantity = new SharedOrderQuantity(update.Data.Quantity),
                            QuantityFilled = new SharedOrderQuantity(update.Data.TotalQuantityFilled),
                            TriggerPrice = update.Data.TriggerPrice,
                            UpdateTime = update.Data.UpdateTime,
                            LastTrade = update.Data.LastTradeId == null ? null :
                                new SharedUserTrade(
                                    sharedSymbol,
                                    update.Data.Symbol,
                                    update.Data.OrderId,
                                    update.Data.LastTradeId!.ToString()!,
                                    update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
#warning check quantity unit
                                    new SharedOrderQuantity(update.Data.LastTradeQuantity),
                                    update.Data.LastTradePrice!.Value,
                                    update.Data.UpdateTime)
                                {
                                    Role = update.Data.LastTradeRole == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker,
                                    ClientOrderId = update.Data.ClientOrderId
                                }
                        }]));
                    }, ct).ConfigureAwait(false);

            return result;
        }

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


        #endregion
    }
}
