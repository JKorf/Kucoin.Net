using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects;
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
                x.ApiCredentials = (KucoinApiCredentials?)options.ApiCredentials?.Copy() ?? (KucoinApiCredentials?)KucoinSocketOptions.Default.ApiCredentials?.Copy();
            });
            _restClient = restClient ?? new KucoinRestClient(x =>
            {
                x.ApiCredentials = (KucoinApiCredentials?)options.ApiCredentials?.Copy() ?? (KucoinApiCredentials?)KucoinRestOptions.Default.ApiCredentials?.Copy();
            });
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
            {
                subResult = await _socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleFullUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var bookResult = await _restClient.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
                if (!bookResult)
                {
                    await _socketClient.UnsubscribeAllAsync().ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetInitialOrderBook(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                subResult = await _socketClient.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);
                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                if (!setResult)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return setResult.As<UpdateSubscription>(default);
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
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            var bookResult = await _restClient.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync(Symbol).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetInitialOrderBook(bookResult.Data.Sequence!.Value, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
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
