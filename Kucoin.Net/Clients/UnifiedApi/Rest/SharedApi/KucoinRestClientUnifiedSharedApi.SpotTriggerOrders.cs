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

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Place Spot Trigger Order

        async Task<IExchangeCallResult<SharedId>> IPlaceSpotTriggerOrder.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
            => await PlaceSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderPrice == null ? OrderType.Market : OrderType.Limit,
                request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInQuoteAsset ?? 0,
                request.OrderPrice,
                quantityUnit: request.Quantity?.QuantityInBaseAsset != null ? QuantityUnit.BaseAsset : QuantityUnit.QuoteAsset,
                timeInForce: GetTimeInForce(request.TimeInForce),
                triggerDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopType.Up : StopType.Down,
                triggerPrice: request.TriggerPrice,
                triggerPriceType: StopPriceType.TradePrice,
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            
        }

        #endregion

        #region Get Spot Trigger Order

        async Task<IExchangeCallResult<SharedSpotTriggerOrder>> IGetSpotTriggerOrder.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);
                       
            var order = await _api.Trading.GetOrderAsync(
                UnifiedSimpleAccountType.Spot,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                request.Symbol,
                order.Data.Symbol,
                order.Data.OrderId,
                order.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Data.Side == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.Status),
                order.Data.TriggerPrice ?? 0,
                order.Data.OrderTime
                ) 
            {
                PlacedOrderId = order.Data.TriggerOrderId,
                UpdateTime = order.Data.UpdateTime
            });
            
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(UnifiedOrderStatus status)
        {
            if (status == UnifiedOrderStatus.Triggered)
                return SharedTriggerOrderStatus.Triggered;
            if (status == UnifiedOrderStatus.Canceled)
                return SharedTriggerOrderStatus.CanceledOrRejected;
            if (status == UnifiedOrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;
            if (status == UnifiedOrderStatus.Live)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Unknown;
        }

        #endregion

        #region Cancel Spot Trigger Order

        async Task<IExchangeCallResult<SharedId>> ICancelSpotTriggerOrder.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
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

    }
}
