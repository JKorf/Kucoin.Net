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
        #region Place Futures Trigger Order

        async Task<IExchangeCallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                UnifiedSimpleAccountType.Futures,
                request.Symbol!.GetSymbol(FormatSymbol),
                (request.OrderDirection == SharedTriggerOrderDirection.Enter && request.PositionSide == SharedPositionSide.Long) 
                    || (request.OrderDirection == SharedTriggerOrderDirection.Exit && request.PositionSide == SharedPositionSide.Short) ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderPrice == null ? OrderType.Market : OrderType.Limit,
                request.Quantity?.QuantityInContracts ?? 0,
                request.OrderPrice,
                timeInForce: GetTimeInForce(request.TimeInForce),
                triggerDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopType.Up : StopType.Down,
                triggerPrice: request.TriggerPrice,
                triggerPriceType: 
                    request.TriggerPriceType == SharedTriggerPriceType.LastPrice ? StopPriceType.TradePrice
                    : request.TriggerPriceType == SharedTriggerPriceType.IndexPrice ? StopPriceType.IndexPrice
                    : StopPriceType.MarkPrice,
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
            
        }

        #endregion

        #region Get Futures Trigger Order

        async Task<IExchangeCallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);
                       
            var order = await _api.Trading.GetOrderAsync(
                UnifiedSimpleAccountType.Futures,
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                request.Symbol,
                order.Data.Symbol,
                order.Data.OrderId,
                order.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Data.Side == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.Status),
                order.Data.TriggerPrice ?? 0,
                order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : order.Data.PositionSide == PositionSide.Short ? SharedPositionSide.Short : null,
                order.Data.OrderTime
                ) 
            {
                PlacedOrderId = order.Data.TriggerOrderId,
                UpdateTime = order.Data.UpdateTime
            });
            
        }

        #endregion

        #region Cancel Futures Trigger Order

        async Task<IExchangeCallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);
                        
            var order = await _api.Trading.CancelOrderAsync(
                UnifiedSimpleAccountType.Futures,
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
