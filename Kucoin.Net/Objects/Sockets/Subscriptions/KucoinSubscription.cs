using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinSubscription<T> : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public KucoinSubscription(ILogger logger, SocketApiClient client, string topic, List<string>? symbols, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _handler = handler;

            if (symbols?.Count > 0)
            {
                MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<T>>(symbols.Select(s => topic + ":" + s), DoHandleMessage);
                MessageRouter = MessageRouter.Create<KucoinSocketUpdate<T>>(symbols.Select(s => topic + ":" + s), DoHandleMessage);
            }
            else
            {
                MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<T>>(topic, DoHandleMessage);
                MessageRouter = MessageRouter.Create<KucoinSocketUpdate<T>>(topic, (string?)null, DoHandleMessage);
            }
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<T> message)
        {
            string? topic = message.Topic.Contains(":") ? message.Topic.Split(':').Last() : null;
            if (string.Equals(topic, "all", StringComparison.Ordinal))
                topic = message.Subject;

            _handler.Invoke(
                new DataEvent<T>(message.Data, receiveTime, originalData)
                    .WithSymbol(topic)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
