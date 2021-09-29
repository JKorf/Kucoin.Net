using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Spot.Socket;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinSpotSymbolOrderBook: SymbolOrderBook
    {
        private readonly IKucoinClient restClient;
        private readonly IKucoinSocketClient socketClient;
        private readonly bool _restOwner;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public KucoinSpotSymbolOrderBook(string symbol, KucoinOrderBookOptions? options = null) : base(symbol, options ?? new KucoinOrderBookOptions())
        {
            Levels = options?.Limit;
            socketClient = options?.SocketClient ?? new KucoinSocketClient();
            restClient = options?.RestClient ?? new KucoinClient();
            _restOwner = options?.RestClient == null;
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            if (!KucoinClient.CredentialsDefaultSet)            
                return new CallResult<UpdateSubscription>(null, new ArgumentError("No API credentials provided for the default KucoinClient. Make sure API credentials are set using KucoinClient.SetDefaultOptions."));
           
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await socketClient.Spot.SubscribeToAggregatedOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                Status = OrderBookStatus.Syncing;
                var bookResult = await restClient.Spot.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult)
                {
                    log.Write(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Id} order book {Symbol} failed to retrieve initial order book");
                    await socketClient.Spot.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, bookResult.Error);
                }

                SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await socketClient.Spot.SubscribeToOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

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

            var bookResult = await restClient.Spot.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(false, bookResult.Error);

            SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true, null);
        }

        private void HandleFullUpdate(DataEvent<KucoinStreamOrderBook> data)
        {
            UpdateOrderBook(data.Data.SequenceStart, data.Data.SequenceEnd, data.Data.Changes.Bids, data.Data.Changes.Asks);
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

            if(_restOwner)
                restClient?.Dispose();
            if(_socketOwner)
                socketClient?.Dispose();
        }
    }
}
