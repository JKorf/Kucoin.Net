using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kucoin.Net.SymbolOrderBooks
{
    /// <summary>
    /// Kucoin order book implementation
    /// </summary>
    public class KucoinFuturesSymbolOrderBook : CryptoExchange.Net.OrderBook.SymbolOrderBook
    {
        private readonly IKucoinRestClient _restClient;
        private readonly IKucoinSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;
        private readonly bool _clientOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public KucoinFuturesSymbolOrderBook(string symbol, Action<KucoinOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        [ActivatorUtilitiesConstructor]
        public KucoinFuturesSymbolOrderBook(
            string symbol,
            Action<KucoinOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger = null,
            IKucoinRestClient? restClient = null,
            IKucoinSocketClient? socketClient = null) : base(logger, "Kucoin", "Futures", symbol)
        {
            var options = KucoinOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options.Limit == null;

            Levels = options.Limit;
            _initialDataTimeout = options.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
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
                subResult = await _socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);
                if (!subResult.Success)
                    return CallResult.Fail<UpdateSubscription>(subResult.Error);

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(new CancellationRequestedError());
                }


                // Wait up to 1s until the first update has been received
                await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);

                Status = OrderBookStatus.Syncing;
                var bookResult = await _restClient.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult.Success)
                {
                    await _socketClient.UnsubscribeAllAsync().ConfigureAwait(false);
                    return CallResult.Fail<UpdateSubscription>(bookResult.Error!);
                }

                SetSnapshot(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await _socketClient.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
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
            await WaitUntilFirstUpdateBufferedAsync(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000), ct).ConfigureAwait(false);

            var bookResult = await _restClient.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult.Success)
                return CallResult.Fail(bookResult.Error!);

            SetSnapshot(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            return CallResult.Ok();
        }

        private void HandleFullUpdate(DataEvent<KucoinFuturesOrderBookChange> data)
        {
            var entry = new KucoinOrderBookEntry()
            {
                Price = data.Data.Price,
                Quantity = data.Data.Quantity
            };

            if (data.Data.Side == OrderSide.Buy)
                UpdateOrderBook(data.Data.Sequence, new ISymbolOrderBookEntry[] { entry }, Array.Empty<ISymbolOrderBookEntry>(), data.DataTime, data.DataTimeLocal);
            else
                UpdateOrderBook(data.Data.Sequence, Array.Empty<ISymbolOrderBookEntry>(), new ISymbolOrderBookEntry[] { entry }, data.DataTime, data.DataTimeLocal);
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
