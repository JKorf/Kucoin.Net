using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot.Socket;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotSharedApi
    {

        #region Subscribe Spot Orders

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType(new[] { ParseOrder(update.Data) })),
                update => handler(update.ToType(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedSpotOrderUpdate ParseOrder(KucoinStreamOrderBaseUpdate orderUpdate)
        {
            if (orderUpdate is KucoinStreamOrderNewUpdate update)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                            update.Symbol,
                            update.OrderId.ToString(),
                            update.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(update.Status, update.UpdateType),
                            update.OrderTime)
                {
                    ClientOrderId = update.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(update.OriginalQuantity == 0 ? null : update.OriginalQuantity, update.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(0, 0),
                    OrderPrice = update.Price == 0 ? null : update.Price,
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = 0,
#pragma warning restore CS0618 // Type or member is obsolete
                    IsTriggerOrder = update.OrderType == OrderType.Stop || update.OrderType == OrderType.MarketStop || update.OrderType == OrderType.LimitStop
                };
            }
            if (orderUpdate is KucoinStreamOrderMatchUpdate matchUpdate)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, matchUpdate.Symbol),
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : matchUpdate.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            matchUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(matchUpdate.Status, matchUpdate.UpdateType),
                            matchUpdate.OrderTime)
                {
                    ClientOrderId = matchUpdate.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(matchUpdate.OriginalQuantity == 0 ? null : matchUpdate.OriginalQuantity, matchUpdate.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(matchUpdate.QuantityFilled, matchUpdate.OriginalValue - (matchUpdate.QuoteQuantityRemaining + matchUpdate.ValueCanceled)),
                    OrderPrice = matchUpdate.Price == 0 ? null : matchUpdate.Price,
                    UpdateTime = matchUpdate.Timestamp,
                    IsTriggerOrder = matchUpdate.OrderType == OrderType.Stop || matchUpdate.OrderType == OrderType.MarketStop || matchUpdate.OrderType == OrderType.LimitStop,
                    LastTrade = new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, matchUpdate.Symbol), 
                        matchUpdate.Symbol, 
                        matchUpdate.OrderId, 
                        matchUpdate.TradeId, 
                        matchUpdate.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(matchUpdate.MatchQuantity), 
                        matchUpdate.MatchPrice, 
                        matchUpdate.Timestamp)
                        {
                            ClientOrderId = matchUpdate.ClientOrderid,
                            Role = matchUpdate.Liquidity == LiquidityType.Taker ? SharedRole.Taker : SharedRole.Maker
                        }
                };
            }
            if (orderUpdate is KucoinStreamOrderUpdate upd)
            {
                return new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, upd.Symbol),
                            upd.Symbol,
                            upd.OrderId.ToString(),
                            upd.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : upd.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            upd.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(upd.Status, upd.UpdateType),
                            upd.OrderTime)
                {
                    ClientOrderId = upd.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(upd.OriginalQuantity == 0 ? null : upd.OriginalQuantity, upd.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(upd.QuantityFilled, upd.OriginalValue - (upd.QuoteQuantityRemaining + upd.ValueCanceled)),
                    OrderPrice = upd.Price == 0 ? null : upd.Price,
                    UpdateTime = upd.Timestamp,
                    IsTriggerOrder = upd.OrderType == OrderType.Stop || upd.OrderType == OrderType.MarketStop || upd.OrderType == OrderType.LimitStop,
                };
            }

            throw new Exception("Unknown order update type");
        }

        private SharedOrderStatus ParseOrderStatus(ExtendedOrderStatus? status, MatchUpdateType? updateType)
        {
            if (status == ExtendedOrderStatus.New || status == ExtendedOrderStatus.Open || updateType == MatchUpdateType.Open || updateType == MatchUpdateType.Received) return SharedOrderStatus.Open;
            if (updateType == MatchUpdateType.Canceled) return SharedOrderStatus.Canceled;
            if (updateType == MatchUpdateType.Filled) return SharedOrderStatus.Filled;
            return SharedOrderStatus.Unknown;
        }
    }
}
