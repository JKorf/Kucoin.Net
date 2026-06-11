using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kucoin.Net.SymbolOrderBooks
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinSpotSymbolOrderBook: CryptoExchange.Net.OrderBook.SymbolOrderBook
    {
        private readonly IKucoinRestClient _restClient;
        private readonly IKucoinSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;
        private readonly bool _clientOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsFunc">The options for the order book</param>
        public KucoinSpotSymbolOrderBook(string symbol, Action<KucoinOrderBookOptions>? optionsFunc = null)
            : this(symbol, optionsFunc, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsFunc">The options for the order book</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        [ActivatorUtilitiesConstructor]
        public KucoinSpotSymbolOrderBook(
            string symbol,
            Action<KucoinOrderBookOptions>? optionsFunc,
            ILoggerFactory? logger,
            IKucoinRestClient? restClient,
            IKucoinSocketClient? socketClient) : base(logger, "Kucoin", "Spot", symbol)
        {
            var options = KucoinOrderBookOptions.Default.Copy();
            if (optionsFunc != null)
                optionsFunc(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = true;

            Levels = options.Limit;
            _initialDataTimeout = options.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new KucoinSocketClient(x =>
            {
                x.ApiCredentials = (KucoinCredentials?)options.ApiCredentials?.Copy() ?? (KucoinCredentials?)KucoinSocketOptions.Default.ApiCredentials?.Copy();
            });
            _restClient = restClient ?? new KucoinRestClient(x =>
            {
                x.ApiCredentials = (KucoinCredentials?)options.ApiCredentials?.Copy() ?? (KucoinCredentials?)KucoinRestOptions.Default.ApiCredentials?.Copy();
            });
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            WebSocketResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await _socketClient.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);
                if (!subResult.Success)
                    return CallResult.Fail<UpdateSubscription>(subResult.Error);

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;

                // Wait up to 1s until the first update has been received
                await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);

                var bookResult = await _restClient.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult.Success)
                {
                    _logger.Log(LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book: " + bookResult.Error);
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(bookResult.Error!);
                }

                SetSnapshot(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult.Success)
                    return CallResult.Fail<UpdateSubscription>(subResult.Error);

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                if (!setResult.Success)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(setResult.Error);
                }
            }

            if (!subResult.Success)
                return CallResult.Fail<UpdateSubscription>(subResult.Error!);

            return CallResult.Ok(subResult.Data);
        }

        /// <inheritdoc />
        protected override async Task<CallResult> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            // Wait up to 1s until the first update has been received
            await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);

            var bookResult = await _restClient.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult.Success)
                return CallResult.Fail(bookResult.Error!);

            SetSnapshot(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            return CallResult.Ok();
        }

        private void HandleFullUpdate(DataEvent<KucoinStreamOrderBook> data)
        {
            UpdateOrderBook(data.Data.SequenceStart, data.Data.SequenceEnd, data.Data.Changes.Bids, data.Data.Changes.Asks, data.DataTime, data.DataTimeLocal);
        }

        private void HandleUpdate(DataEvent<KucoinStreamOrderBookChanged> data)
        {
            SetSnapshot(data.Data.Timestamp!.Value.Ticks, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _socketClient?.Dispose();
                _restClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
