using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Sockets;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Interface for the Kucoin socket client
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
        /// Subscribe to updates for symbol snapshots
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(string symbol, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to updates for symbol snapshots
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbol, Action<KucoinStreamSnapshot> onData);

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
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string symbol, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(IEnumerable<string> symbols, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(string symbol, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbol">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(string symbol, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(IEnumerable<string> symbols, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<KucoinStreamOrderBaseUpdate> onData);

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
    }
}