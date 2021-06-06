using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Kucoin socket client interface
    /// </summary>
    public interface IKucoinSocketClient: ISocketClient
    {
        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTickerUpdates(string symbol, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTickerUpdates(IEnumerable<string> symbols, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAllTickerUpdates(Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(string symbolOrMarket, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(
            IEnumerable<string> symbolOrMarkets, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string symbol, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(IEnumerable<string> symbols, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTradeUpdates(string symbol, Action<KucoinStreamMatch> onData);

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<KucoinStreamMatch> onData);

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, KucoinKlineInterval interval,
            Action<KucoinStreamCandle> onData);

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KucoinKlineInterval interval, Action<KucoinStreamCandle> onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int limit,
            Action<KucoinStreamOrderBookChanged> onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(IEnumerable<string> symbols, int limit,
            Action<KucoinStreamOrderBookChanged> onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<KucoinStreamOrderBookChanged> onData);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<KucoinStreamOrderBookChanged> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string symbol, Action<KucoinStreamMatchEngineUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<KucoinStreamMatchEngineUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(IEnumerable<string> symbols, Action<KucoinStreamMatchEngineUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamMatchEngineUpdate> onData);

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOrderUpdates(Action<KucoinStreamOrderBaseUpdate> onOrderData, Action<KucoinStreamOrderMatchUpdate> onTradeData);

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// </summary>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<KucoinStreamOrderBaseUpdate> onOrderData, Action<KucoinStreamOrderMatchUpdate> onTradeData);

        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToBalanceChanges(Action<KucoinBalanceUpdate> onBalanceChange);

        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceChangesAsync(Action<KucoinBalanceUpdate> onBalanceChange);

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToStopOrderUpdates(Action<KucoinStreamStopOrderUpdate> onData);

        /// <summary>
        /// Subscribe to updates for stop orders
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<KucoinStreamStopOrderUpdate> onData);
    }
}