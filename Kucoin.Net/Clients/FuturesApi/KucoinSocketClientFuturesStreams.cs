using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Linq;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinSocketClientFuturesStreams" />
    public class KucoinSocketClientFuturesStreams : SocketApiClient, IKucoinSocketClientFuturesStreams
    {
        private readonly KucoinSocketClient _baseClient;
        private readonly KucoinSocketClientOptions _options;

        internal KucoinSocketClientFuturesStreams(Log log, KucoinSocketClient baseClient, KucoinSocketClientOptions options)
            : base(log, options, options.FuturesStreamsOptions)
        {
            _baseClient = baseClient;
            _options = options;

            SendPeriodic("Ping", TimeSpan.FromSeconds(30), (connection) => new KucoinPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                InvokeHandler(tokenData.As(GetData<KucoinStreamFuturesMatch>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/execution:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                InvokeHandler(tokenData.As(GetData<KucoinStreamFuturesTick>(tokenData), symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", "/contractMarket/tickerV2:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<JToken>(tokenData);
                var change = data["change"]?.ToString();
                var sequence = data["sequence"]?.ToString();
                if (string.IsNullOrEmpty(change) || string.IsNullOrEmpty(sequence))
                    return;

                var items = change!.Split(',');
                var result = new KucoinFuturesOrderBookChange
                {
                    Sequence = long.Parse(sequence),
                    Price = decimal.Parse(items[0], CultureInfo.InvariantCulture),
                    Side = items[1] == "sell" ? OrderSide.Sell : OrderSide.Buy,
                    Quantity = decimal.Parse(items[2], CultureInfo.InvariantCulture)
                };

                InvokeHandler(tokenData.As(result, symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 5, 50);

            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var book = GetData<KucoinStreamOrderBookChanged>(tokenData);
                InvokeHandler(tokenData.As(book, symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/level2Depth{limit}:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                if (tokenData.Data["subject"]?.ToString() == "mark.index.price")
                {
                    var data = GetData<KucoinStreamFuturesMarkIndexPrice>(tokenData);
                    InvokeHandler(tokenData.As(data, symbol), onMarkIndexPriceUpdate);
                }
                else
                {
                    var data = GetData<KucoinStreamFuturesFundingRate>(tokenData);
                    InvokeHandler(tokenData.As(data, symbol), onFundingRateUpdate);
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/instrument:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinContractAnnouncement>(tokenData);
                data.Event = tokenData.Data["subject"]?.ToString() ?? "";
                InvokeHandler(tokenData.As(data), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/announcement", false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinStreamTransactionStatisticsUpdate>(tokenData);
                InvokeHandler(tokenData.As(data, symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/snapshot:" + symbol, false);
            return await SubscribeAsync("futures", request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinStreamFuturesOrderUpdate>(tokenData);
                InvokeHandler(tokenData.As(data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/tradeOrders" + (symbol == null ? "" : ":" + symbol), true);
            return await SubscribeAsync("futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var data = GetData<KucoinStreamFuturesStopOrderUpdate>(tokenData);
                InvokeHandler(tokenData.As((KucoinStreamStopOrderUpdateBase)data, data.Symbol), onData);
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractMarket/advancedOrders", true);
            return await SubscribeAsync("futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData =>
            {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "orderMargin.change")
                {
                    var data = GetData<KucoinStreamOrderMarginUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data, data.Asset), onOrderMarginUpdate);
                }
                else if (subject == "availableBalance.change")
                {
                    var data = GetData<KucoinStreamFuturesBalanceUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data, data.Asset), onBalanceUpdate);
                }
                else if (subject == "withdrawHold.change")
                {
                    var data = GetData<KucoinStreamFuturesWithdrawableUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data, data.Asset), onWithdrawableUpdate);
                }
                else
                {
                    _log.Write(LogLevel.Warning, "Unknown update: " + subject);
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contractAccount/wallet", true);
            return await SubscribeAsync("futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinPositionUpdate>> onPositionUpdate,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>> onMarkPriceUpdate,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>> onFundingSettlementUpdate,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>> onRiskAdjustUpdate,
            CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<JToken>>(tokenData => {
                var subject = tokenData.Data["subject"]?.ToString();
                if (subject == "position.change")
                {
                    var changeReason = tokenData.Data["data"]?["changeReason"]?.ToString();
                    if (changeReason == null || changeReason == "markPriceChange")
                    {
                        var data = GetData<KucoinPositionMarkPriceUpdate>(tokenData);
                        InvokeHandler(tokenData.As(data, symbol), onMarkPriceUpdate);
                    }
                    else
                    {
                        var data = GetData<KucoinPositionUpdate>(tokenData);
                        InvokeHandler(tokenData.As(data, symbol), onPositionUpdate);
                    }
                }
                else if (subject == "position.settlement")
                {
                    var data = GetData<KucoinPositionFundingSettlementUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data, symbol), onFundingSettlementUpdate);
                }
                else if (subject == "position.adjustRiskLimit")
                {
                    var data = GetData<KucoinPositionRiskAdjustResultUpdate>(tokenData);
                    InvokeHandler(tokenData.As(data, symbol), onRiskAdjustUpdate);
                }
                else
                {
                    _log.Write(LogLevel.Warning, $"Unknown update: {subject}, data: {tokenData}");
                }
            });

            var request = new KucoinRequest(NextId().ToString(CultureInfo.InvariantCulture), "subscribe", $"/contract/position:" + symbol, true);
            return await SubscribeAsync("futures", request, null, true, innerHandler, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(string address, bool authenticated)
        {
            var apiCredentials = (KucoinApiCredentials?)(Options.ApiCredentials ?? _baseClient.ClientOptions.ApiCredentials ?? KucoinSocketClientOptions.Default.ApiCredentials ?? KucoinClientOptions.Default.ApiCredentials);

            var clientOptions = new KucoinClientOptions(new KucoinClientOptions
            {
                ApiCredentials = apiCredentials,
                LogLevel = _options.LogLevel,
                SpotApiOptions = new KucoinRestApiClientOptions
                {
                    BaseAddress = KucoinClientOptions.Default.FuturesApiOptions.BaseAddress
                }
            });

            using (var restClient = new KucoinClient(clientOptions))
            {
                WebCallResult<KucoinToken> tokenResult = await ((KucoinClientFuturesApiAccount)restClient.FuturesApi.Account).GetWebsocketToken(authenticated).ConfigureAwait(false);
                if (!tokenResult)
                    return tokenResult.As<string?>(null);

                return new CallResult<string?>(tokenResult.Data.Servers.First().Endpoint + "?token=" + tokenResult.Data.Token);
            }
            
        }

        /// <inheritdoc />
        public override async Task<Uri?> GetReconnectUriAsync(SocketConnection connection)
        {
            var result = await GetConnectionUrlAsync(connection.ConnectionUri.ToString(), connection.Subscriptions.Any(s => s.Authenticated)).ConfigureAwait(false);
            if (!result)
                return null;

            return new Uri(result.Data);
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
            await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(5), null, message =>
            {
                var id = message["id"];
                if (id == null)
                    return false;

                if (id!.ToString() != request.Id)
                    return false;

                var result = Deserialize<KucoinSubscribeResponse>(message);
                if (!result)
                {
                    _log.Write(LogLevel.Warning, "Failed to unsubscribe: " + result.Error);
                    success = false;
                    return true;
                }

                if (result.Data.Type != "ack")
                {
                    _log.Write(LogLevel.Warning, "Failed to unsubscribe: " + new ServerError(result.Data.Code, result.Data.Data));
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
                _log.Write(LogLevel.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
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
    }
}
