using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Clients.FuturesApi;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinSocketClient" />
    public class KucoinSocketClient : BaseSocketClient, IKucoinSocketClient
    {
        #region Api clients

        /// <inheritdoc />
        public IKucoinSocketClientSpotStreams SpotStreams { get; }
        /// <inheritdoc />
        public IKucoinSocketClientFuturesStreams FuturesStreams { get; }

        #endregion

        /// <summary>
        /// Create a new instance of KucoinSocketClient using the default options
        /// </summary>
        public KucoinSocketClient() : this(KucoinSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public KucoinSocketClient(KucoinSocketClientOptions options) : base("Kucoin", options)
        {
            MaxSocketConnections = 10;

            SendPeriodic(TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });

            SpotStreams = new KucoinSocketClientSpotStreams(log, this, options);
            FuturesStreams = new KucoinSocketClientFuturesStreams(log, this, options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            KucoinSocketClientOptions.Default = options;
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, url, request, identifier, authenticated, dataHandler, ct);

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, request, identifier, authenticated, dataHandler, ct);

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(SocketApiClient apiClient, string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
        {
            SocketConnection? socketConnection;
            SocketSubscription subscription;
            var released = false;
            try
            {
                await semaphoreSlim.WaitAsync(ct).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return new CallResult<UpdateSubscription>(null, new CancellationRequestedError());
            }

            try
            {
                socketConnection = GetSocketConnection(apiClient, url, authenticated);
                if (socketConnection == null)
                {
                    var clientOptions = new KucoinClientOptions();
                    KucoinApiCredentials? thisCredentials = (KucoinApiCredentials?)apiClient.AuthenticationProvider?.Credentials;

                    // Create new socket
                    IWebsocket socket;
                    if (SocketFactory is WebsocketFactory)
                    {
                        KucoinToken token;
                        
                        if (url == "futures")
                        {
                            if (thisCredentials != null)
                            {
                                clientOptions.FuturesApiOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                                    thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                            }

                            using (var restClient = new KucoinClient(clientOptions))
                            {
                                WebCallResult<KucoinToken> tokenResult = await ((KucoinClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketToken(authenticated, ct).ConfigureAwait(false);
                                if (!tokenResult)
                                    return new CallResult<UpdateSubscription>(null, tokenResult.Error);

                                token = tokenResult.Data;
                            }
                        }
                        else
                        {
                            if (thisCredentials != null)
                            {
                                clientOptions.SpotApiOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                                    thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                            }

                            using (var restClient = new KucoinClient(clientOptions))
                            {
                                WebCallResult<KucoinToken> tokenResult = await ((KucoinClientSpotApiAccount)restClient.SpotApi.Account).GetWebsocketToken(authenticated, ct).ConfigureAwait(false);
                                if (!tokenResult)
                                    return new CallResult<UpdateSubscription>(null, tokenResult.Error);

                                token = tokenResult.Data;
                            }
                        }
                        

                        socket = CreateSocket(token.Servers.First().Endpoint + "?token=" + token.Token);
                    }
                    else
                        socket = CreateSocket("test");

                    socketConnection = new SocketConnection(this, apiClient, socket);
                    socketConnection.Tag = url;
                    foreach (var kvp in genericHandlers)
                        socketConnection.AddSubscription(SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value));
                }

                subscription = AddSubscription(request, identifier, true, socketConnection, dataHandler);
                if (ClientOptions.SocketSubscriptionsCombineTarget == 1)
                {
                    // Can release early when only a single sub per connection
                    semaphoreSlim.Release();
                    released = true;
                }

                var connectResult = await ConnectIfNeededAsync(socketConnection, authenticated).ConfigureAwait(false);
                if (!connectResult)
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
                var subResult = await SubscribeAndWaitAsync(socketConnection, request, subscription).ConfigureAwait(false);
                if (!subResult)
                {
                    await socketConnection.CloseAsync(subscription).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, subResult.Error);
                }

            }
            else
            {
                subscription.Confirmed = true;
            }

            socketConnection.ShouldReconnect = true;
            if (ct != default)
            {
                subscription.CancellationTokenRegistration = ct.Register(async () =>
                {
                    log.Write(LogLevel.Debug, $"Socket {socketConnection.Socket.Id} Cancellation token set, closing subscription");
                    await socketConnection.CloseAsync(subscription).ConfigureAwait(false);
                }, false);
            }
            return new CallResult<UpdateSubscription>(new UpdateSubscription(socketConnection, subscription), null);
        }

        /// <inheritdoc />
        protected override SocketConnection GetSocketConnection(SocketApiClient apiClient, string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => s.Value.Tag == address
                                                 && s.Value.ApiClient.GetType() == apiClient.GetType()
                                                 && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < ClientOptions.SocketSubscriptionsCombineTarget || sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= ClientOptions.SocketSubscriptionsCombineTarget))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            return null!;
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var kRequest = (KucoinRequest)request;
            if (id!.ToString() != kRequest.Id)
                return false;

            var result = Deserialize<KucoinSubscribeResponse>(message);
            if (!result)
            {
                callResult = new CallResult<object>(null, result.Error);
                return true;
            }

            if (result.Data.Type != "ack")
            {
                callResult = new CallResult<object>(null, new ServerError(result.Data.Code, result.Data.Data));
                return true;
            }

            callResult = new CallResult<object>(result.Data, null);
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            if (message["type"] == null || message["type"]?.ToString() != "message")
                return false;

            if (message["topic"] == null)
                return false;

            var kRequest = (KucoinRequest)request;
            var topic = message["topic"]?.ToString();
            if (kRequest.Topic == topic)
                return true;

            var subject = message["subject"]?.ToString();
            if (topic != null)
            {
                if (kRequest.Topic.StartsWith("/market/ticker:") && subject != null && subject == "trade.ticker"
                || kRequest.Topic.StartsWith("/market/level2:") && topic.StartsWith("/market/level2")
                || kRequest.Topic.StartsWith("/spotMarket/level3:") && topic.StartsWith("/spotMarket/level3")
                || kRequest.Topic.StartsWith("/spotMarket/level2Depth5:") && topic.StartsWith("/spotMarket/level2Depth5")
                || kRequest.Topic.StartsWith("/spotMarket/level2Depth20:") && topic.StartsWith("/spotMarket/level2Depth20")
                || kRequest.Topic.StartsWith("/indicator/index:") && topic.StartsWith("/indicator/index")
                || kRequest.Topic.StartsWith("/indicator/markPrice:") && topic.StartsWith("/indicator/markPrice")
                || kRequest.Topic.StartsWith("/market/snapshot:") && topic.StartsWith("/market/snapshot"))
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
            }

            if (kRequest.Topic == "/account/balance" && subject != null && subject == "account.balance")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            if (message["type"] != null)
            {
                var type = message["type"]!.ToString();
                if (type == "pong" && identifier == "Ping")
                    return true;

                if (type == "welcome" && identifier == "Welcome")
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            var kRequest = (KucoinRequest)s.Request!;
            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "unsubscribe", kRequest.Topic, false);

            var success = false;
            await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(5), message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if (id!.ToString() != request.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message);
                if (!result)
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    log.Write(LogLevel.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
                    success = false;
                    return true;
                }

                success = true;
                return true;
            }).ConfigureAwait(false);

            return success;
        }


        internal static void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)!))
                return;

            handler?.Invoke(data!);
        }

        internal T GetData<T>(DataEvent<JToken> tokenData)
        {
            var desResult = Deserialize<KucoinUpdateMessage<T>>(tokenData.Data);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default!;
            }
            return desResult.Data.Data;
        }

        internal string? TryGetSymbolFromTopic(DataEvent<JToken> data)
        {
            string? symbol = null;
            var topic = data.Data["topic"]?.ToString();
            if (topic != null && topic.Contains(':'))
                symbol = topic.Split(':').Last();
            return symbol;
        }

        internal CallResult<T> DeserializeInternal<T>(JToken data, JsonSerializer? serializer = null, int? requestId = null)
            => Deserialize<T>(data, serializer, requestId);

        internal int NextIdInternal() => NextId();

        /// <inheritdoc />
        public override void Dispose()
        {
            SpotStreams.Dispose();
            FuturesStreams.Dispose();
            base.Dispose();
        }
    }
}
