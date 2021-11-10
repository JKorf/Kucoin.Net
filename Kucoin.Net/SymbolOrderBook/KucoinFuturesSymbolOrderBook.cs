using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Clients.Rest.Futures;
using Kucoin.Net.Clients.Socket;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Interfaces.Clients.Socket;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures.Socket;
using Kucoin.Net.Objects.Spot;
using Kucoin.Net.Objects.Spot.Socket;

namespace Kucoin.Net.SymbolOrderBooks
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly IKucoinClientFutures restClient;
        private readonly IKucoinSocketClientFutures socketClient;
        private readonly bool _restOwner;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public KucoinFuturesSymbolOrderBook(string symbol, KucoinOrderBookFuturesOptions? options = null) : base("Kucoin[Futures]", symbol, options ?? new KucoinOrderBookFuturesOptions())
        {
            strictLevels = false;
            sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit;
            socketClient = options?.SocketClient ?? new KucoinSocketClientFutures();
            restClient = options?.RestClient ?? new KucoinClientFutures();
            _restOwner = options?.RestClient == null;
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            if (KucoinClientFuturesOptions.Default.ApiCredentials == null)
                return new CallResult<UpdateSubscription>(null, new ArgumentError("No API credentials provided for the default KucoinClient. Make sure API credentials are set using KucoinClient.SetDefaultOptions."));
           
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await socketClient.SubscribeToOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);

                Status = OrderBookStatus.Syncing;
                var bookResult = await restClient.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult)
                {
                    await socketClient.UnsubscribeAllAsync().ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, bookResult.Error);
                }

                SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await socketClient.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                Status = OrderBookStatus.Syncing;
                await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
            }

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);
            
            return new CallResult<UpdateSubscription>(subResult.Data, null);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);

            var bookResult = await restClient.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(false, bookResult.Error);

            SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true, null);
        }

        private void HandleFullUpdate(DataEvent<KucoinFuturesOrderBookChange> data)
        {
            var entry = new KucoinOrderBookEntry()
            {
                Price = data.Data.Price,
                Quantity = data.Data.Quantity
            };

            if (data.Data.Side == OrderSide.Buy)
                UpdateOrderBook(data.Data.Sequence, new List<ISymbolOrderBookEntry> { entry }, new List<ISymbolOrderBookEntry>());
            else
                UpdateOrderBook(data.Data.Sequence, new List<ISymbolOrderBookEntry>(), new List<ISymbolOrderBookEntry> { entry });
        }

        private void HandleUpdate(DataEvent<KucoinStreamOrderBookChanged> data)
        {
            SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            if (_restOwner)
                restClient?.Dispose();
            if (_socketOwner)
                socketClient?.Dispose();
        }
    }
}
