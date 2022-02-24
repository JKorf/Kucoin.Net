using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Models.Spot.Socket;

namespace Kucoin.Net.SymbolOrderBooks
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinSpotSymbolOrderBook: CryptoExchange.Net.OrderBook.SymbolOrderBook
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
        public KucoinSpotSymbolOrderBook(string symbol, KucoinOrderBookOptions? options = null) : base("Kucoin", symbol, options ?? new KucoinOrderBookOptions())
        {
            strictLevels = false;
            sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit;
            socketClient = options?.SocketClient ?? new KucoinSocketClient();
            restClient = options?.RestClient ?? new KucoinClient();
            _restOwner = options?.RestClient == null;
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            if (KucoinClientOptions.Default.ApiCredentials == null && KucoinClientOptions.Default.SpotApiOptions.ApiCredentials == null)
                return new CallResult<UpdateSubscription>(new ArgumentError("No API credentials provided for the default KucoinClient. Make sure API credentials are set using KucoinClient.SetDefaultOptions."));

            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await socketClient.SpotStreams.SubscribeToAggregatedOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var bookResult = await restClient.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult)
                {
                    log.Write(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Id} order book {Symbol} failed to retrieve initial order book: " + bookResult.Error);
                    await socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await socketClient.SpotStreams.SubscribeToOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var setResult = await WaitForSetOrderBookAsync(10000, ct).ConfigureAwait(false);
                if(!setResult)
                {

                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return setResult.As(subResult.Data);
                }    
            }

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);
            
            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(10000, ct).ConfigureAwait(false);

            var bookResult = await restClient.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetInitialOrderBook(bookResult.Data.Sequence, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        private void HandleFullUpdate(DataEvent<KucoinStreamOrderBook> data)
        {
            UpdateOrderBook(data.Data.SequenceStart, data.Data.SequenceEnd, data.Data.Changes.Bids, data.Data.Changes.Asks);
        }

        private void HandleUpdate(DataEvent<KucoinStreamOrderBookChanged> data)
        {
            SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.Bids, data.Data.Asks);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_socketOwner)
                socketClient?.Dispose();
            if (_restOwner)
                restClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}
