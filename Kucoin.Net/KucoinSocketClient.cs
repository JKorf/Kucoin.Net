using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kucoin.Net.Interfaces;

namespace Kucoin.Net
{
    /// <summary>
    /// Client for interacting with the Kucoin websocket API
    /// </summary>
    public class KucoinSocketClient: SocketClient, IKucoinSocketClient
    {
        #region fields
        private static KucoinSocketClientOptions defaultOptions = new KucoinSocketClientOptions();
        private static KucoinSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the default options
        /// </summary>
        public KucoinSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the provided options
        /// </summary>
        public KucoinSocketClient(KucoinSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            MaxSocketConnections = 10;

            SendPeriodic(TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(),
                Type = "ping"
            });

            AddGenericHandler("Ping", (con, msg) => { });
            AddGenericHandler("Welcome", (con, msg) => { });
        }
        #endregion

        #region methods    
        #region public
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="market">The market to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTickerUpdates(string market, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(new[] { market }, onData).Result;

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="market">The market to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string market, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(new[] { market }, onData);

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="markets">The markets to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTickerUpdates(string[] markets, Action<KucoinStreamTick> onData) => SubscribeToTickerUpdatesAsync(markets, onData).Result;

        /// <summary>
        /// Subscribe to updates for a market ticker
        /// </summary>
        /// <param name="markets">The markets to subscribe to</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string[] markets, Action<KucoinStreamTick> onData)
        {
            var innerHandler = new Action<JToken>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = ((string)tokenData["topic"]).Split(':').Last();
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/ticker:" + string.Join(",", markets), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates for all market tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAllTickerUpdates(Action<KucoinStreamTick> onData) => SubscribeToAllTickerUpdatesAsync(onData).Result;

        /// <summary>
        /// Subscribe to updates for all market tickers
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<KucoinStreamTick> onData)
        {
            var innerHandler = new Action<JToken>(tokenData =>
            {
                var data = GetData<KucoinStreamTick>(tokenData);
                if (data == null)
                    return;

                data.Symbol = (string)tokenData["subject"];
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/ticker:all", false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Subscribe to updates for market snapshots
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToSnapshotUpdates(string market, Action<KucoinStreamSnapshot> onData) => SubscribeToSnapshotUpdatesAsync(market, onData).Result;

        /// <summary>
        /// Subscribe to updates for market snapshots
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string market, Action<KucoinStreamSnapshot> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamSnapshotWrapper>(tokenData)?.Data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/snapshot:" + market, false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="market">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string market, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { market }, onData).Result;

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="market">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string market, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(new[] { market }, onData);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedOrderBookUpdates(string[] markets, Action<KucoinStreamOrderBook> onData) => SubscribeToAggregatedOrderBookUpdatesAsync(markets, onData).Result;

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// </summary>
        /// <param name="markets">The markets to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string[] markets, Action<KucoinStreamOrderBook> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamOrderBook>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/level2:" + string.Join(",", markets), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToTradeUpdates(string market, Action<KucoinStreamMatch> onData) => SubscribeToTradeUpdatesAsync(market, onData).Result;

        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="market">The market to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string market, Action<KucoinStreamMatch> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                InvokeHandler(GetData<KucoinStreamMatch>(tokenData), onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/match:" + market, false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

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
        public CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string market, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(new[] { market }, onData).Result;

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
        public Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string market, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(new[] { market }, onData);

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
        public CallResult<UpdateSubscription> SubscribeToMatchEngineUpdates(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToMatchEngineUpdatesAsync(markets, onData).Result;

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
        public async Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                KucoinStreamOrderBaseUpdate data = null;
                var subject = (string)tokenData["subject"];
                if (subject == "trade.l3received")
                    data = GetData<KucoinStreamOrderReceivedUpdate>(tokenData);
                if (subject == "trade.l3open")
                    data = GetData<KucoinStreamOrderOpenUpdate>(tokenData);
                if (subject == "trade.l3done")
                    data = GetData<KucoinStreamOrderDoneUpdate>(tokenData);
                if (subject == "trade.l3match")
                    data = GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                if (subject == "trade.l3change")
                    data = GetData<KucoinStreamOrderChangeUpdate>(tokenData);
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/level3:" + string.Join(",", markets), false);
            return await Subscribe(request, null, false, innerHandler).ConfigureAwait(false);
        }

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
        public CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(string market, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToOwnMatchEngineUpdatesAsync(new[] { market }, onData).Result;

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
        public Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(string market, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToOwnMatchEngineUpdatesAsync(new[] { market }, onData);

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
        public CallResult<UpdateSubscription> SubscribeToOwnMatchEngineUpdates(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData) => SubscribeToOwnMatchEngineUpdatesAsync(markets, onData).Result;

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
        public async Task<CallResult<UpdateSubscription>> SubscribeToOwnMatchEngineUpdatesAsync(string[] markets, Action<KucoinStreamOrderBaseUpdate> onData)
        {
            var innerHandler = new Action<JToken>(tokenData => {
                KucoinStreamOrderBaseUpdate data = null;
                var subject = (string)tokenData["subject"]["data"]["type"];
                var type = (string)tokenData["subject"];
                if (subject == "trade.l3received")
                {
                    if (type == "stop" || type == "activate")
                        data = GetData<KucoinStreamOrderStopUpdate>(tokenData);
                    else
                        data = GetData<KucoinStreamOrderReceivedUpdate>(tokenData);                    
                }
                if (subject == "trade.l3open")
                    data = GetData<KucoinStreamOrderOpenUpdate>(tokenData);
                if (subject == "trade.l3done")
                    data = GetData<KucoinStreamOrderDoneUpdate>(tokenData);
                if (subject == "trade.l3match")
                    data = GetData<KucoinStreamOrderMatchUpdate>(tokenData);
                if (subject == "trade.l3change")
                    data = GetData<KucoinStreamOrderChangeUpdate>(tokenData);
                InvokeHandler(data, onData);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/market/level3:" + string.Join(",", markets), true);
            return await Subscribe(request, null, true, innerHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public CallResult<UpdateSubscription> SubscribeToBalanceChanges(Action<KucoinBalanceUpdate> onBalanceChange) => SubscribeToBalanceChangesAsync(onBalanceChange).Result;
        
        /// <summary>
        /// Subscribe to balance updates
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceChangesAsync(Action<KucoinBalanceUpdate> onBalanceChange)
        {
            var innerHandler = new Action<JToken>(data =>
            {
                var desResult = Deserialize<KucoinUpdateMessage<KucoinBalanceUpdate>>(data, false);
                if (!desResult.Success)
                {
                    log.Write(LogVerbosity.Warning, "Failed to deserialize balance update: " + desResult.Error);
                    return;
                }
                onBalanceChange(desResult.Data.Data);
            });

            var request = new KucoinRequest(NextId().ToString(), "subscribe", "/account/balance", true);
            return await Subscribe(request, null, true, innerHandler).ConfigureAwait(false);
        }
        #endregion
        #region private

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, object request, string identifier, bool authenticated, Action<T> dataHandler)
        {
            SocketConnection socket;
            SocketSubscription handler;
            bool released = false;
            await semaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                socket = GetWebsocket(url, authenticated);
                if (socket == null)
                {
                    KucoinToken token;
                    var clientOptions = KucoinClient.DefaultOptions.Copy();
                    var thisCredentials = (KucoinApiCredentials)authProvider?.Credentials;
                    if (thisCredentials != null)
                        clientOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key.GetString(),
                            thisCredentials.Secret.GetString(), thisCredentials.PassPhrase.GetString());
                    using (var restClient = new KucoinClient(clientOptions))
                    {
                        var tokenResult = restClient.GetWebsocketToken(authenticated).Result;
                        if (!tokenResult.Success)
                            return new CallResult<UpdateSubscription>(null, tokenResult.Error);
                        token = tokenResult.Data;
                    }

                    // Create new socket
                    var s = CreateSocket(token.Servers[0].Endpoint + "?token=" + token.Token);
                    socket = new SocketConnection(this, s);
                    foreach (var kvp in genericHandlers)
                        socket.AddHandler(kvp.Key, false, kvp.Value);
                }

                handler = AddHandler(request, identifier, true, socket, dataHandler);
                if (SocketCombineTarget == 1)
                {
                    // Can release early when only a single sub per connection
                    semaphoreSlim.Release();
                    released = true;
                }

                var connectResult = await ConnectIfNeeded(socket, authenticated).ConfigureAwait(false);
                if (!connectResult.Success)
                    return new CallResult<UpdateSubscription>(null, connectResult.Error);
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                if (!released)
                    semaphoreSlim.Release();
            }


            if (request != null)
            {
                var subResult = await SubscribeAndWait(socket, request, handler).ConfigureAwait(false);
                if (!subResult.Success)
                {
                    await socket.Close(handler).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, subResult.Error);
                }

            }
            else
                handler.Confirmed = true;

            socket.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(socket, handler), null);
        }

        /// <inheritdoc />
        protected override SocketConnection GetWebsocket(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.HandlerCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.HandlerCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.HandlerCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            return null;
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var kRequest = (KucoinRequest)request;
            if ((string)id != kRequest.Id)
                return false;

            var result = Deserialize<KucoinSubscribeResponse>(message, false);
            if(!result.Success)
            {
                callResult = new CallResult<object>(null, result.Error);
                return true;
            }

            if(result.Data.Type != "ack")
            {
                callResult = new CallResult<object>(null, new ServerError(result.Data.Code, result.Data.Data));
                return true;
            }

            callResult = new CallResult<object>(result.Data, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (message["type"] == null || (string)message["type"] != "message")
                return false;

            if (message["topic"] == null)
                return false;

            var kRequest = (KucoinRequest)request;
            var topic = (string)message["topic"];
            if (kRequest.Topic == topic)
                return true;

            if((kRequest.Topic.StartsWith("/market/ticker:") && (string)message["subject"] == "trade.ticker")
            || (kRequest.Topic.StartsWith("/market/level2:") && ((string)message["topic"]).StartsWith("/market/level2"))
            || (kRequest.Topic.StartsWith("/market/level3:") && ((string)message["topic"]).StartsWith("/market/level3")))
            {
                var marketSplit = topic.Split(':');
                if (marketSplit.Length > 1)
                {
                    var market = marketSplit[1];
                    var subMarkets = kRequest.Topic.Split(':').Last().Split(',');
                    if (subMarkets.Contains(market))
                        return true;
                }
            }

            if (kRequest.Topic == "/account/balance" && (string)message["subject"] == "account.balance")
                return true;
            
            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message["type"] != null)
            {
                var type = (string)message["type"];
                if (type == "pong" && identifier == "Ping")
                    return true;

                if (type == "welcome" && identifier == "Welcome")
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        /// <inheritdoc />
        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            var kRequest = (KucoinRequest)s.Request;
            var request = new KucoinRequest(NextId().ToString(), "unsubscribe", kRequest.Topic, false);

            bool success = false;
            await connection.SendAndWait(request, TimeSpan.FromSeconds(5), message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if ((string)id != kRequest.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message, false);
                if (!result.Success)
                {
                    log.Write(LogVerbosity.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    log.Write(LogVerbosity.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
                    success = false;
                    return true;
                }

                success = true;
                return true;
            }).ConfigureAwait(false);

            return success;
        }

        private void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)))
                return;

            handler(data);
        }

        private T GetData<T>(JToken tokenData)
        {
            var desResult = Deserialize<KucoinUpdateMessage<T>>(tokenData, false);
            if (!desResult.Success)
            {
                log.Write(LogVerbosity.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default(T);
            }
            return desResult.Data.Data;
        }
        #endregion
        #endregion
    }
}
