using System.Collections.Generic;
using System.Linq;
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
        public KucoinSymbolOrderBook(string symbol, KucoinOrderBookOptions options = null) : base(symbol, options ?? new KucoinOrderBookOptions())
        {
            restClient = new KucoinClient();
            socketClient = new KucoinSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            var subResult = await socketClient.SubscribeToAggregatedOrderBookUpdatesAsync(Symbol, HandleUpdate);
            if(!subResult.Success)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            var bookResult = await restClient.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult.Success)
            {
                await socketClient.UnsubscribeAll().ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(null, bookResult.Error);
            }

            SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Asks, bookResult.Data.Bids);
            return new CallResult<UpdateSubscription>(subResult.Data, null);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResync()
        {
            var bookResult = await restClient.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult.Success)
                return new CallResult<bool>(false, bookResult.Error);

            SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Asks, bookResult.Data.Bids);
            return new CallResult<bool>(true, null);
        }

        private void HandleUpdate(KucoinStreamOrderBook data)
        {
            var updates = new List<ProcessEntry>();
            updates.AddRange(data.Changes.Asks.Select(a => new ProcessEntry(OrderBookEntryType.Ask, a)));
            updates.AddRange(data.Changes.Bids.Select(b => new ProcessEntry(OrderBookEntryType.Bid, b)));
            UpdateOrderBook(data.SequenceStart, data.SequenceEnd, updates);
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
