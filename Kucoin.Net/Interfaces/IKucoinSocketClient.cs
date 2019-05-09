using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Sockets;

namespace Kucoin.Net.Interfaces
{
    public interface IKucoinSocketClient
    {
        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="market">The market to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTickerUpdates(string market, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="market">The market to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string market, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="markets">The markets to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTickerUpdates(string[] markets, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="markets">The markets to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string[] markets, Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for all market tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAllTickerUpdates(Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for all market tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<KucoinStreamTick> onData);

        /// <summary>
        /// Subscribe to updates for market snapshots
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(string market, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to updates for market snapshots
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string market, Action<KucoinStreamSnapshot> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="market">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string market, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="market">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string market, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string[] markets, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string[] markets, Action<KucoinStreamOrderBook> onData);

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToTradeUpdates(string market, Action<KucoinStreamMatch> onData);

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<KucoinStreamMatch> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string market, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string market, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(string market, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(string market, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData);

        /// <summary>
        /// <para>Subscribe to match engine updates for your own orders. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>
        /// <para><see cref="KucoinStreamOrderReceivedUpdate" />: A valid order is received by the matching engine</para>
        /// <para><see cref="KucoinStreamOrderOpenUpdate" />: A limit order is opened on the order book</para>
        /// <para><see cref="KucoinStreamOrderDoneUpdate" />: An order is no longer on the order book</para>
        /// <para><see cref="KucoinStreamOrderMatchUpdate" />: An order is matched with another order</para>
        /// <para><see cref="KucoinStreamOrderChangeUpdate" />: An order is changed (decreased) in size</para>
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData);

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
        /// The factory for creating sockets. Used for unit testing
        /// </summary>
        IWebsocketFactory SocketFactory { get; set; }

        /// <inheritdoc cref="SocketClientOptions.ReconnectInterval"/>
        TimeSpan ReconnectInterval { get; }

        /// <inheritdoc cref="SocketClientOptions.AutoReconnect"/>
        bool AutoReconnect { get; }

        /// <inheritdoc cref="SocketClientOptions.SocketResponseTimeout"/>
        TimeSpan ResponseTimeout { get; }

        /// <inheritdoc cref="SocketClientOptions.SocketNoDataTimeout"/>
        TimeSpan SocketNoDataTimeout { get; }

        /// <summary>
        /// The max amount of concurrent socket connections
        /// </summary>
        int MaxSocketConnections { get; }

        /// <inheritdoc cref="SocketClientOptions.SocketSubscriptionsCombineTarget"/>
        int SocketCombineTarget { get; }

        string BaseAddress { get; }

        /// <summary>
        /// Periodically sends an object to a socket
        /// </summary>
        /// <param name="interval">How often</param>
        /// <param name="objGetter">Method returning the object to send</param>
        void SendPeriodic(TimeSpan interval, Func<SocketConnection, object> objGetter);

        /// <summary>
        /// Unsubscribe from a stream
        /// </summary>
        /// <param name="subscription">The subscription to unsubscribe</param>
        /// <returns></returns>
        Task Unsubscribe(UpdateSubscription subscription);

        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        /// <returns></returns>
        Task UnsubscribeAll();

        /// <summary>
        /// Dispose the client
        /// </summary>
        void Dispose();
    }
}