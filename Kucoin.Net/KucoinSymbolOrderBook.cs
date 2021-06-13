using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinSymbolOrderBook: SymbolOrderBook
    {
        private readonly KucoinClient restClient;
        private readonly KucoinSocketClient socketClient;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public KucoinSymbolOrderBook(string symbol, KucoinOrderBookOptions? options = null) : base(symbol, options ?? new KucoinOrderBookOptions())
        {
            Levels = options?.Limit;
            restClient = new KucoinClient();
            socketClient = new KucoinSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            if (!KucoinClient.CredentialsDefaultSet)            
                return new CallResult<UpdateSubscription>(null, new ArgumentError("No API credentials provided for the default KucoinClient. Make sure API credentials are set using KucoinClient.SetDefaultOptions."));
           
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await socketClient.SubscribeToAggregatedOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);

                Status = OrderBookStatus.Syncing;
                var bookResult = await restClient.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult)
                {
                    await socketClient.UnsubscribeAll().ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, bookResult.Error);
                }

                SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await socketClient.SubscribeToOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                Status = OrderBookStatus.Syncing;
                await WaitForSetOrderBook(10000).ConfigureAwait(false);
            }

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);
            
            return new CallResult<UpdateSubscription>(subResult.Data, null);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResync()
        {
            if (Levels != null)
                return await WaitForSetOrderBook(10000).ConfigureAwait(false);

            var bookResult = await restClient.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
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

            restClient?.Dispose();
            socketClient?.Dispose();
        }
    }
}
