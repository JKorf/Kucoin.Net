using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinUnifiedSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly string _channel;
        private readonly string _type;
        private readonly string? _symbol;
        private readonly string? _interval;
        private readonly string? _depth;
        private readonly Action<DateTime, string?, KucoinUnifiedSocketUpdate<T>> _handler;

        public KucoinUnifiedSubscription(
            ILogger logger, 
            SocketApiClient client, 
            string channel,
            UnifiedAccountType type,
            string? symbol,
            Action<DateTime, string?, KucoinUnifiedSocketUpdate<T>> handler,
            bool authenticated,
            KlineInterval? interval = null,
            OrderBookDepth? depth = null)
            : base(logger, authenticated)
        {
            _client = client;
            _channel = channel;
            _type = EnumConverter.GetString(type);
            _symbol = symbol;
            _handler = handler;
            _interval = EnumConverter.GetString(interval);
            _depth = EnumConverter.GetString(depth);

            var id = $"{channel}.{EnumConverter.GetString(type)}";
            var filter = _symbol != null || _interval != null ? $"{_symbol}{_interval}" : null;
            MessageRouter = MessageRouter.CreateWithOptionalTopicFilter<KucoinUnifiedSocketUpdate<T>>(id, filter, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinUnifiedQuery(
                _client,
                new KucoinUnifiedRequest(ExchangeHelpers.NextId().ToString(), Authenticated ? "SUBSCRIBE" : "subscribe", _channel, _type, _symbol, _interval, _depth),
                Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinUnifiedQuery(
                _client,
                new KucoinUnifiedRequest(ExchangeHelpers.NextId().ToString(), Authenticated ? "UNSUBSCRIBE" : "unsubscribe", _channel, _type, _symbol, _interval, _depth),
                Authenticated);
        }

        private CallResult? DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinUnifiedSocketUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }

    }
}
