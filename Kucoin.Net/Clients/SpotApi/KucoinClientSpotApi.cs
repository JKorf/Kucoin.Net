using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinClientSpotApi" />
    public class KucoinClientSpotApi : RestApiClient, IKucoinClientSpotApi, ISpotClient
    {
        private readonly KucoinClient _baseClient;
        private readonly KucoinClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Spot Api");

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public string ExchangeName => "Kucoin";

        /// <inheritdoc />
        public IKucoinClientSpotApiAccount Account { get; }

        /// <inheritdoc />
        public IKucoinClientSpotApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IKucoinClientSpotApiTrading Trading { get; }

        internal KucoinClientSpotApi(Log log, KucoinClient baseClient, KucoinClientOptions options)
            : base(options, options.SpotApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new KucoinClientSpotApiAccount(this);
            ExchangeData = new KucoinClientSpotApiExchangeData(this);
            Trading = new KucoinClientSpotApiTrading(this);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        #region common interface

        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(d => new Symbol
            {
                SourceObject = d,
                Name = d.Symbol,
                MinTradeQuantity = d.BaseMinQuantity,
                PriceStep = d.PriceIncrement,
                QuantityStep = d.QuoteIncrement
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            var symbols = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<Ticker>(null);

            var ticker = symbols.Data.Data.SingleOrDefault(s => s.Symbol == symbol);
            if (ticker == null)
                return symbols.AsError<Ticker>(new ArgumentError("Symbol not found"));

            return symbols.As(new Ticker
            {
                SourceObject = ticker,
                HighPrice = ticker.HighPrice,
                LastPrice = ticker.LastPrice,
                LowPrice = ticker.LowPrice,
                Price24H = ticker.LastPrice - ticker.ChangePrice,
                Symbol = ticker.Symbol,
                Volume = ticker.Volume
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Ticker>>(null);

            return symbols.As(symbols.Data.Data.Select(t => new Ticker
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                LastPrice = t.LastPrice,
                LowPrice = t.LowPrice,
                Price24H = t.LastPrice - t.ChangePrice,
                Symbol = t.Symbol,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (limit != null)
                throw new ArgumentException($"Kucoin doesn't support the {nameof(limit)} parameter for the method {nameof(IBaseRestClient.GetKlinesAsync)}", nameof(limit));

            var symbols = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, ct: ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Kline>>(null);

            return symbols.As(symbols.Data.Select(k => new Kline
            {
                SourceObject = k,
                ClosePrice = k.ClosePrice,
                HighPrice = k.HighPrice,
                LowPrice = k.LowPrice,
                OpenPrice = k.OpenPrice,
                OpenTime = k.OpenTime,
                Volume = k.Volume
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            var book = await ExchangeData.GetOrderBookAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!book)
                return book.As<OrderBook>(null);

            return book.As(new OrderBook
            {
                SourceObject = book.Data,
                Asks = book.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = book.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            var trades = await ExchangeData.GetTradeHistoryAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(t => new Trade
            {
                SourceObject = t,
                Price = t.Price,
                Quantity = t.Quantity,
                Symbol = symbol,
                Timestamp = t.Timestamp
            }));
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            var order = await Trading.PlaceOrderAsync(symbol,
                side == CommonOrderSide.Sell ? OrderSide.Sell : OrderSide.Buy,
                type == CommonOrderType.Limit ? NewOrderType.Limit : NewOrderType.Market,
                quantity,
                price,
                clientOrderId: clientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = order.Data.Id
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var order = await Trading.GetOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order.Data,
                Id = order.Data.Id,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Timestamp = order.Data.CreateTime,
                Symbol = order.Data.Symbol,
                Side = order.Data.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Type = order.Data.Type == OrderType.Market ? CommonOrderType.Market : order.Data.Type == OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                Status = order.Data.IsActive == true ? CommonOrderStatus.Active : order.Data.CancelExist ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var trades = await Trading.GetUserTradesAsync(orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<UserTrade>>(null);

            return trades.As(trades.Data.Items.Select(t => new UserTrade
            {
                SourceObject = t,
                Fee = t.Fee,
                FeeAsset = t.FeeAsset,
                Id = t.Id,
                OrderId = t.OrderId,
                Price = t.Price,
                Quantity = t.Quantity,
                Symbol = t.Symbol,
                Timestamp = t.Timestamp
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await Trading.GetOrdersAsync(status: Enums.OrderStatus.Active, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            return orders.As(orders.Data.Items.Select(d => new Order
            {
                SourceObject = d,
                Id = d.Id,
                Price = d.Price,
                Quantity = d.Quantity,
                QuantityFilled = d.QuantityFilled,
                Timestamp = d.CreateTime,
                Symbol = d.Symbol,
                Side = d.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Type = d.Type == OrderType.Market ? CommonOrderType.Market : d.Type == OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                Status = d.IsActive == true ? CommonOrderStatus.Active : d.CancelExist ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await Trading.GetOrdersAsync(status: Enums.OrderStatus.Done, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            return orders.As(orders.Data.Items.Select(d => new Order
            {
                SourceObject = d,
                Id = d.Id,
                Price = d.Price,
                Quantity = d.Quantity,
                QuantityFilled = d.QuantityFilled,
                Timestamp = d.CreateTime,
                Symbol = d.Symbol,
                Side = d.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Type = d.Type == OrderType.Market ? CommonOrderType.Market : d.Type == OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                Status = d.IsActive == true ? CommonOrderStatus.Active : d.CancelExist ? CommonOrderStatus.Canceled : CommonOrderStatus.Filled
            }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var result = await Trading.CancelOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);

            if (!result.Data.CancelledOrderIds.Any())
                return result.AsError<OrderId>(new ServerError("Order not canceled"));

            return result.As(new OrderId
            {
                SourceObject = result.Data,
                Id = result.Data.CancelledOrderIds.First()
            });
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var result = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Balance>>(null);

            return result.As(result.Data.Select(b => new Balance
            {
                SourceObject = b,
                Asset = b.Asset,
                Available = b.Available,
                Total = b.Total
            }));
        }

        private static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return KlineInterval.TwoHours;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromHours(6)) return KlineInterval.SixHours;
            if (timeSpan == TimeSpan.FromHours(8)) return KlineInterval.EightHours;
            if (timeSpan == TimeSpan.FromHours(12)) return KlineInterval.TwelveHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;

            throw new ArgumentException("Unsupported timespan for Kucoin kline interval, check supported intervals using Kucoin.Net.Objects.KucoinKlineInterval");
        }
        #endregion

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        internal Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
         => _baseClient.Execute(this, uri, method, ct, parameters, signed);

        internal Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false)
         => _baseClient.Execute<T>(this, uri, method, ct, parameters, signed, weight, ignoreRatelimit: ignoreRatelimit);

        internal Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(BaseAddress.AppendPath("v" + apiVersion, path));
        }


        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.SpotApiOptions.AutoTimestamp, _options.SpotApiOptions.TimestampRecalculationInterval, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;
    }
}
