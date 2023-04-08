using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{

    /// <summary>
    /// Spot streams
    /// </summary>
    public interface IKucoinSocketClientSpotStreams : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para><a href="https://docs.kucoin.com/#symbol-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para><a href="https://docs.kucoin.com/#symbol-ticker" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// <para><a href="https://docs.kucoin.com/#all-symbols-ticker" /></para>
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// <para><a href="https://docs.kucoin.com/#symbol-snapshot" /></para>
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// <para><a href="https://docs.kucoin.com/#symbol-snapshot" /></para>
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// <para><a href="https://docs.kucoin.com/#level-2-market-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// <para><a href="https://docs.kucoin.com/#level-2-market-data" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://docs.kucoin.com/#match-execution-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://docs.kucoin.com/#klines" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://docs.kucoin.com/#level2-5-best-ask-bid-orders" /></para>
        /// <para><a href="https://docs.kucoin.com/#level2-50-best-ask-bid-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://docs.kucoin.com/#level2-5-best-ask-bid-orders" /></para>
        /// <para><a href="https://docs.kucoin.com/#level2-50-best-ask-bid-orders" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in quantity</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in quantity</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// <para><a href="https://docs.kucoin.com/#private-order-change-events" /></para>
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<KucoinStreamOrderBaseUpdate>> onOrderData, Action<DataEvent<KucoinStreamOrderMatchUpdate>> onTradeData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://docs.kucoin.com/#account-balance-notice" /></para>
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for stop orders
        /// <para><a href="https://docs.kucoin.com/#margin-order-done-event" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding book updates
        /// <para><a href="https://docs.kucoin.com/#order-book-change" /></para>
        /// </summary>
        /// <param name="currency">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(string currency, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding book updates
        /// <para><a href="https://docs.kucoin.com/#order-book-change" /></para>
        /// </summary>
        /// <param name="currencies">Currencies to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(IEnumerable<string> currencies, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://docs.kucoin.com/#index-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://docs.kucoin.com/#index-price" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://docs.kucoin.com/#mark-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://docs.kucoin.com/#mark-price" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);
    }
}