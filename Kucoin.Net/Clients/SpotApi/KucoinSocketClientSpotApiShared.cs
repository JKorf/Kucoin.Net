using Kucoin.Net;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotApi : IKucoinSocketClientSpotApiShared
    {
        public string Exchange => KucoinExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToSnapshotUpdatesAsync(symbol, update => handler(update.As(new SharedTicker(symbol, update.Data.LastPrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0)))).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As<IEnumerable<SharedTrade>>(new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.Timestamp) })), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAsk.Price, update.Data.BestAsk.Quantity, update.Data.BestBid.Price, update.Data.BestBid.Quantity))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.As<IEnumerable<SharedBalance>>(new[] { new SharedBalance(update.Data.Asset, update.Data.Available, update.Data.Total) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToOrderUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedSpotOrder ParseOrder(KucoinStreamOrderBaseUpdate orderUpdate)
        {
            if (orderUpdate is KucoinStreamOrderNewUpdate update)
            {
                return new SharedSpotOrder(
                            update.Symbol,
                            update.OrderId.ToString(),
                            update.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(update.Status, update.UpdateType),
                            update.OrderTime)
                {
                    ClientOrderId = update.ClientOrderid?.ToString(),
                    Quantity = update.OriginalQuantity,
                    QuantityFilled = 0,
                    QuoteQuantity = update.OriginalValue,
                    QuoteQuantityFilled = 0,
                    Price = update.Price,
                    Fee = 0
                };
            }
            if (orderUpdate is KucoinStreamOrderMatchUpdate matchUpdate)
            {
                return new SharedSpotOrder(
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : matchUpdate.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            matchUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(matchUpdate.Status, matchUpdate.UpdateType),
                            matchUpdate.OrderTime)
                {
                    ClientOrderId = matchUpdate.ClientOrderid?.ToString(),
                    Quantity = matchUpdate.OriginalQuantity,
                    QuantityFilled = matchUpdate.QuantityFilled,
                    QuoteQuantity = matchUpdate.OriginalValue,
                    Price = matchUpdate.Price,
                    UpdateTime = matchUpdate.Timestamp,
                    LastTrade = new SharedUserTrade(matchUpdate.OrderId, matchUpdate.TradeId, matchUpdate.MatchQuantity, matchUpdate.MatchPrice, matchUpdate.Timestamp)
                    {
                        Role = matchUpdate.Liquidity == LiquidityType.Taker ? SharedRole.Taker : SharedRole.Maker
                    }
                };
            }
            if (orderUpdate is KucoinStreamOrderUpdate upd)
            {
                return new SharedSpotOrder(
                            upd.Symbol,
                            upd.OrderId.ToString(),
                            upd.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : upd.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            upd.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(upd.Status, upd.UpdateType),
                            upd.OrderTime)
                {
                    ClientOrderId = upd.ClientOrderid?.ToString(),
                    Quantity = upd.OriginalQuantity,
                    QuantityFilled = upd.QuantityFilled,
                    QuoteQuantity = upd.OriginalValue,
                    Price = upd.Price,
                    UpdateTime = upd.Timestamp
                };
            }

            throw new Exception("Unknown order update type");
        }

        private SharedOrderStatus ParseStatus(ExtendedOrderStatus? status, MatchUpdateType? updateType)
        {
            if (status == ExtendedOrderStatus.New)
                return SharedOrderStatus.Open;

            if (updateType == MatchUpdateType.Canceled)
                return SharedOrderStatus.Canceled;

            if (updateType == MatchUpdateType.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Open;
        }
    }
}
