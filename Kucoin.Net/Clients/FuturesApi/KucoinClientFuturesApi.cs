using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinClientFuturesApi" />
    public class KucoinClientFuturesApi : RestApiClient, IKucoinClientFuturesApi, IFuturesClient
    {
        private readonly KucoinClient _baseClient;
        private readonly KucoinClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

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
        public IKucoinClientFuturesApiAccount Account { get; }

        /// <inheritdoc />
        public IKucoinClientFuturesApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IKucoinClientFuturesApiTrading Trading { get; }

        internal KucoinClientFuturesApi(Log log, KucoinClient baseClient, KucoinClientOptions options)
            : base(options, options.FuturesApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new KucoinClientFuturesApiAccount(this);
            ExchangeData = new KucoinClientFuturesApiExchangeData(this);
            Trading = new KucoinClientFuturesApiTrading(this);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        internal Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
         => _baseClient.Execute(this, uri, method, ct, parameters, signed);

        internal Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1)
         => _baseClient.Execute<T>(this, uri, method, ct, parameters, signed);

        internal Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(BaseAddress.AppendPath("v" + apiVersion, path));
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.FuturesApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();
        
        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync()
        {
            var symbols = await ExchangeData.GetOpenContractsAsync().ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(d => new Symbol
            {
                SourceObject = d,
                Name = d.Symbol,
                MinTradeQuantity = d.LotSize,
                QuantityStep = d.LotSize,
                PriceStep = d.TickSize
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol)
        {
            var ticker = await ExchangeData.GetContractAsync(symbol).ConfigureAwait(false);
            if (!ticker)
                return ticker.As<Ticker>(null);

            return ticker.As(new Ticker
            {
                SourceObject = ticker,
                HighPrice = ticker.Data.HighPrice,
                LowPrice = ticker.Data.LowPrice,
                Symbol = ticker.Data.Symbol,
                Volume = ticker.Data.Volume24H
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync()
        {
            var symbols = await ExchangeData.GetOpenContractsAsync().ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Ticker>>(null);

            return symbols.As(symbols.Data.Select(t => new Ticker
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,                
                Symbol = t.Symbol,
                Volume = t.Volume24H
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit)
        {
            if (limit != null)
                throw new ArgumentException($"Kucoin doesn't support the {nameof(limit)} parameter for the method {nameof(IBaseRestClient.GetKlinesAsync)}", nameof(limit));

            var symbols = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime).ConfigureAwait(false);
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

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol)
        {
            var book = await ExchangeData.GetAggregatedPartialOrderBookAsync(symbol, 100).ConfigureAwait(false);
            if (!book)
                return book.As<OrderBook>(null);

            return book.As(new OrderBook
            {
                SourceObject = book.Data,
                Asks = book.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = book.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol)
        {
            var trades = await ExchangeData.GetTradeHistoryAsync(symbol).ConfigureAwait(false);
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

        async Task<WebCallResult<OrderId>> IFuturesClient.PlaceOrderAsync(string symbol, CryptoExchange.Net.CommonObjects.OrderSide side, CryptoExchange.Net.CommonObjects.OrderType type, decimal quantity, decimal? price, int? leverage, string? accountId)
        {
            if (!leverage.HasValue)
                throw new ArgumentException($"Kucoin required the {nameof(leverage)} parameter for {nameof(IFuturesClient.PlaceOrderAsync)}");

            var order = await Trading.PlaceOrderAsync(symbol,
                side == CryptoExchange.Net.CommonObjects.OrderSide.Sell ? Enums.OrderSide.Sell : Enums.OrderSide.Buy,
                type == CryptoExchange.Net.CommonObjects.OrderType.Limit ? NewOrderType.Limit : NewOrderType.Market,
                leverage.Value,
                quantity,
                price
                ).ConfigureAwait(false);
            return order.As(new OrderId
            {
                SourceObject = order.Data,
                Id = order.Data.Id
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol)
        {
            var order = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
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
                Side = order.Data.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                Type = order.Data.Type == Enums.OrderType.Market ? CryptoExchange.Net.CommonObjects.OrderType.Market : order.Data.Type == Enums.OrderType.Limit ? CryptoExchange.Net.CommonObjects.OrderType.Limit : CryptoExchange.Net.CommonObjects.OrderType.Other,
                Status = order.Data.IsActive == true ? CryptoExchange.Net.CommonObjects.OrderStatus.Active : order.Data.CancelExist ? CryptoExchange.Net.CommonObjects.OrderStatus.Canceled : CryptoExchange.Net.CommonObjects.OrderStatus.Filled
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol)
        {
            var trades = await Trading.GetUserTradesAsync(orderId: orderId).ConfigureAwait(false);
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

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol)
        {
            var orders = await Trading.GetOrdersAsync(status: Enums.OrderStatus.Active).ConfigureAwait(false);
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
                Side = d.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                Type = d.Type == Enums.OrderType.Market ? CryptoExchange.Net.CommonObjects.OrderType.Market : d.Type == Enums.OrderType.Limit ? CryptoExchange.Net.CommonObjects.OrderType.Limit : CryptoExchange.Net.CommonObjects.OrderType.Other,
                Status = d.IsActive == true ? CryptoExchange.Net.CommonObjects.OrderStatus.Active : d.CancelExist ? CryptoExchange.Net.CommonObjects.OrderStatus.Canceled : CryptoExchange.Net.CommonObjects.OrderStatus.Filled
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol)
        {
            var orders = await Trading.GetOrdersAsync(status: Enums.OrderStatus.Done).ConfigureAwait(false);
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
                Side = d.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                Type = d.Type == Enums.OrderType.Market ? CryptoExchange.Net.CommonObjects.OrderType.Market : d.Type == Enums.OrderType.Limit ? CryptoExchange.Net.CommonObjects.OrderType.Limit : CryptoExchange.Net.CommonObjects.OrderType.Other,
                Status = d.IsActive == true ? CryptoExchange.Net.CommonObjects.OrderStatus.Active : d.CancelExist ? CryptoExchange.Net.CommonObjects.OrderStatus.Canceled : CryptoExchange.Net.CommonObjects.OrderStatus.Filled
            }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol)
        {
            var result = await Trading.CancelOrderAsync(orderId).ConfigureAwait(false);
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

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId)
        {
            var result = await Account.GetAccountOverviewAsync().ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Balance>>(null);

            return result.As<IEnumerable<Balance>>(new List<Balance> { new Balance
            {
                Asset = result.Data.Asset,
                Available = result.Data.AvailableBalance,
                Total = result.Data.FrozenFunds + result.Data.AvailableBalance,
                SourceObject = result.Data
            } });
        }

        async Task<WebCallResult<IEnumerable<Position>>> IFuturesClient.GetPositionsAsync()
        {
            var positions = await Account.GetPositionsAsync().ConfigureAwait(false);
            if (!positions)
                return positions.As<IEnumerable<Position>>(null);

            return positions.As(positions.Data.Select(p => new Position
            {
                SourceObject = p,
                Id = p.Id,
                AutoMargin = p.AutoDeposit,
                Leverage = p.RealLeverage,
                Quantity = p.CurrentQuantity,
                Symbol = p.Symbol,
                LiquidationPrice = p.LiquidationPrice,
                MaintananceMargin = p.MaintenanceMargin,
                PositionMargin = p.PositionMargin,
                UnrealizedPnl = p.UnrealizedPnl,
                RealizedPnl = p.RealizedPnl,
                MarkPrice = p.MarkPrice,
                Isolated = !p.CrossMode,
                EntryPrice = p.AverageEntryPrice
            }));
        }

        /// <inheritdoc />
        public IFuturesClient ComonFuturesClient => this;

        private static FuturesKlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return FuturesKlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return FuturesKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return FuturesKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return FuturesKlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return FuturesKlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return FuturesKlineInterval.TwoHours;
            if (timeSpan == TimeSpan.FromHours(4)) return FuturesKlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromHours(8)) return FuturesKlineInterval.EightHours;
            if (timeSpan == TimeSpan.FromHours(12)) return FuturesKlineInterval.TwelveHours;
            if (timeSpan == TimeSpan.FromDays(1)) return FuturesKlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return FuturesKlineInterval.OneWeek;

            throw new ArgumentException("Unsupported timespan for Kucoin kline interval, check supported intervals using Kucoin.Net.Objects.KucoinKlineInterval");
        }

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
    }
}
