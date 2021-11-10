using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.Rest.Spot
{
    public class KucoinClientSpot : KucoinBaseClient, IExchangeClient, IKucoinClientSpot
    {
        public IKucoinClientSpotAccount Account { get; }

        public IKucoinClientSpotExchangeData ExchangeData { get; }

        public IKucoinClientSpotTrading Trading { get; }

        public KucoinClientSpot() : this(KucoinClientSpotOptions.Default)
        {

        }

        public KucoinClientSpot(KucoinClientSpotOptions options) : base("Kucoin[Spot]", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            Account = new KucoinClientSpotAccount(this);
            ExchangeData = new KucoinClientSpotExchangeData(this);
            Trading = new KucoinClientSpotTrading(this);
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string apiPass)
        {
            SetAuthenticationProvider(new KucoinAuthenticationProvider(new KucoinApiCredentials(apiKey, apiSecret, apiPass)));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(KucoinClientSpotOptions options)
        {
            KucoinClientSpotOptions.Default = options;
        }

        #region common interface

        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

#pragma warning disable 1066

        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var symbols = await ExchangeData.GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var result = await ExchangeData.GetTickerAsync(symbol).ConfigureAwait(false);
            return result.As<ICommonTicker>((ICommonTicker?)result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var symbols = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonTicker>>(symbols.Data?.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (limit != null)
                return WebCallResult<IEnumerable<ICommonKline>>.CreateErrorResult(new ArgumentError(
                    $"Kucoin doesn't support the {nameof(limit)} parameter for the method {nameof(IExchangeClient.GetKlinesAsync)}"));

            var symbols = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime).ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonKline>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await ExchangeData.GetOrderBookAsync(symbol).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var book = await ExchangeData.GetTradeHistoryAsync(symbol).ConfigureAwait(false);
            return book.As<IEnumerable<ICommonRecentTrade>>(book.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var order = await Trading.PlaceOrderAsync(symbol,
                side == IExchangeClient.OrderSide.Sell ? OrderSide.Sell : OrderSide.Buy,
                type == IExchangeClient.OrderType.Limit ? NewOrderType.Limit : NewOrderType.Market,
                price, quantity).ConfigureAwait(false);
            return order.As<ICommonOrderId>(order.Data);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol)
        {
            var order = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
            return order.As<ICommonOrder>(order.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var trades = await Trading.GetUserTradesAsync(orderId: orderId).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonTrade>>(trades.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol)
        {
            var orders = await Trading.GetOrdersAsync(status: OrderStatus.Active).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol)
        {
            var orders = await Trading.GetOrdersAsync(status: OrderStatus.Done).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol)
        {
            var result = await Trading.CancelOrderAsync(orderId).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var result = await Account.GetAccountsAsync().ConfigureAwait(false);
            return result.As<IEnumerable<ICommonBalance>>(result.Data);
        }
#pragma warning restore 1066

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
            if (timeSpan == TimeSpan.FromHours(12)) return KlineInterval.TwelfHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;

            throw new ArgumentException("Unsupported timespan for Kucoin kline interval, check supported intervals using Kucoin.Net.Objects.KucoinKlineInterval");
        }
        #endregion
    }
}
