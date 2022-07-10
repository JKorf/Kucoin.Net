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
            SendPeriodic("Ping", TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });

            SpotStreams = AddApiClient(new KucoinSocketClientSpotStreams(log, this, options));
            FuturesStreams = AddApiClient(new KucoinSocketClientFuturesStreams(log, this, options));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            KucoinSocketClientOptions.Default = options;
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            ((KucoinSocketClientSpotStreams)SpotStreams).SetApiCredentials(credentials);
            ((KucoinSocketClientFuturesStreams)FuturesStreams).SetApiCredentials(credentials);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, url, request, identifier, authenticated, dataHandler, ct);

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, request, identifier, authenticated, dataHandler, ct);

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(SocketApiClient apiClient, string address, bool authenticated)
        {
            var clientOptions = new KucoinClientOptions(KucoinClientOptions.Default);
            KucoinApiCredentials? thisCredentials = (KucoinApiCredentials?)apiClient.AuthenticationProvider?.Credentials;
            if (apiClient is KucoinSocketClientSpotStreams)
            {
                if (thisCredentials != null)
                {
                    clientOptions.SpotApiOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                        thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                }

                using (var restClient = new KucoinClient(clientOptions))
                {
                    WebCallResult<KucoinToken> tokenResult = await ((KucoinClientSpotApiAccount)restClient.SpotApi.Account).GetWebsocketToken(authenticated).ConfigureAwait(false);
                    if (!tokenResult)
                        return tokenResult.As<string?>(null);                    

                    return new CallResult<string?>(tokenResult.Data.Servers.First().Endpoint + "?token=" + tokenResult.Data.Token);
                }
            }
            else
            {
                if (thisCredentials != null)
                {
                    clientOptions.FuturesApiOptions.ApiCredentials = new KucoinApiCredentials(thisCredentials.Key!.GetString(),
                        thisCredentials.Secret!.GetString(), thisCredentials.PassPhrase.GetString());
                }

                using (var restClient = new KucoinClient(clientOptions))
                {
                    WebCallResult<KucoinToken> tokenResult = await ((KucoinClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketToken(authenticated).ConfigureAwait(false);
                    if (!tokenResult)
                        return tokenResult.As<string?>(null);

                    return new CallResult<string?>(tokenResult.Data.Servers.First().Endpoint + "?token=" + tokenResult.Data.Token);
                }
            }
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
                callResult = new CallResult<object>(result.Error!);
                return true;
            }

            if (result.Data.Type != "ack")
            {
                callResult = new CallResult<object>(new ServerError(result.Data.Code, result.Data.Data));
                return true;
            }

            callResult = new CallResult<object>(result.Data);
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
            return Task.FromResult(new CallResult<bool>(true));
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
    }
}
