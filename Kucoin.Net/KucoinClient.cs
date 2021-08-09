using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Interfaces;
using Kucoin.Net.SubClients;

namespace Kucoin.Net
{
    /// <summary>
    /// Client to interact with the Kucoin REST API
    /// </summary>
    public class KucoinClient: RestClient, IKucoinClient, IExchangeClient
    {
        private static KucoinClientOptions defaultOptions = new KucoinClientOptions();
        internal static KucoinClientOptions DefaultOptions => defaultOptions.Copy();
        internal static bool CredentialsDefaultSet => DefaultOptions.ApiCredentials != null;


        private readonly string? _baseAddressFutures;

        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is cancelled via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public IKucoinClientSpot Spot { get; }
        /// <inheritdoc />
        public IKucoinClientFutures Futures { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinClient using the default options
        /// </summary>
        public KucoinClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinClient with the provided options
        /// </summary>
        public KucoinClient(KucoinClientOptions options) : base("Kucoin", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            _baseAddressFutures = options.FuturesBaseAddress;

            Spot = new KucoinClientSpot(this);
            Futures = new KucoinClientFutures(this);
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinClientOptions options)
        {
            defaultOptions = options;
        }

        

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
            {
                var errorBody = error.ToString();
                return new ServerError(string.IsNullOrEmpty(errorBody) ? "Unknown error": errorBody);
            }

            if (error["code"] != null && error["msg"] != null)
            {
                var result = error.ToObject<KucoinResult<object>>();
                return new ServerError(result.Code, result.Message!);
            }

            return new ServerError(error.ToString());
        }

        internal static long ToUnixTimestamp(DateTime time)
        {
	        return ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        }

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        internal async Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<KucoinResult<object>>(uri, method, ct, parameters, signed).ConfigureAwait(false);
            if (!result)
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }

        internal async Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<KucoinResult<T>>(uri, method, ct, parameters, signed).ConfigureAwait(false);
            if (!result)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As<T>(result.Data.Data);
        }

        internal Uri GetSpotUri(string path, int apiVersion = 1)
        {
            return new Uri(Path.Combine(BaseAddress, "v" + apiVersion, path));
        }

        internal Uri GetFuturesUri(string path, int apiVersion = 1)
        {
            return new Uri(Path.Combine(_baseAddressFutures, "v" + apiVersion, path));
        }
        #endregion

        #region common interface

        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var symbols = await Spot.GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var result = await Spot.GetTickerAsync(symbol).ConfigureAwait(false);
            return result.As<ICommonTicker>((ICommonTicker?)result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var symbols = await Spot.GetTickersAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonTicker>>(symbols.Data?.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (limit != null)
                return WebCallResult<IEnumerable<ICommonKline>>.CreateErrorResult(new ArgumentError(
                    $"Kucoin doesn't support the {nameof(limit)} parameter for the method {nameof(IExchangeClient.GetKlinesAsync)}"));

            var symbols = await Spot.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime).ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonKline>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await Spot.GetOrderBookAsync(symbol).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var book = await Spot.GetSymbolTradesAsync(symbol).ConfigureAwait(false);
            return book.As<IEnumerable<ICommonRecentTrade>>(book.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var order = await Spot.PlaceOrderAsync(symbol,
                Guid.NewGuid().ToString(),
                side == IExchangeClient.OrderSide.Sell? KucoinOrderSide.Sell: KucoinOrderSide.Buy, 
                type == IExchangeClient.OrderType.Limit ? KucoinNewOrderType.Limit: KucoinNewOrderType.Market,
                price, quantity).ConfigureAwait(false);
            return order.As<ICommonOrderId>(order.Data);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol)
        {
            var order = await Spot.GetOrderAsync(orderId).ConfigureAwait(false);
            return order.As<ICommonOrder>(order.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var trades = await Spot.GetFillsAsync(orderId: orderId).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonTrade>>(trades.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol)
        {
            var orders = await Spot.GetOrdersAsync(status: KucoinOrderStatus.Active).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol)
        {
            var orders = await Spot.GetOrdersAsync(status: KucoinOrderStatus.Done).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol)
        {
            var result = await Spot.CancelOrderAsync(orderId).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var result = await Spot.GetAccountsAsync().ConfigureAwait(false);
            return result.As<IEnumerable<ICommonBalance>>(result.Data);
        }

        private static KucoinKlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KucoinKlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KucoinKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KucoinKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KucoinKlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KucoinKlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return KucoinKlineInterval.TwoHours;
            if (timeSpan == TimeSpan.FromHours(4)) return KucoinKlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromHours(6)) return KucoinKlineInterval.SixHours;
            if (timeSpan == TimeSpan.FromHours(8)) return KucoinKlineInterval.EightHours;
            if (timeSpan == TimeSpan.FromHours(12)) return KucoinKlineInterval.TwelfHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KucoinKlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KucoinKlineInterval.OneWeek;

            throw new ArgumentException("Unsupported timespan for Kucoin kline interval, check supported intervals using Kucoin.Net.Objects.KucoinKlineInterval");
        }
        #endregion
    }
}
