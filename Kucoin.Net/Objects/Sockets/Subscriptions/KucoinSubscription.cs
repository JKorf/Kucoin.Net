using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DateTime, string?, KucoinSocketUpdate<T>> _handler;

        public KucoinSubscription(ILogger logger, SocketApiClient client, string topic, List<string>? symbols, Action<DateTime, string?, KucoinSocketUpdate<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _handler = handler;

            IndividualSubscriptionCount = symbols?.Count ?? 1;

            if (symbols?.Count > 0)
            {
                MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<T>>(symbols.Select(s => topic + ":" + s), DoHandleMessage);
                MessageRouter = MessageRouter.CreateWithTopicFilters<KucoinSocketUpdate<T>>(topic, symbols, DoHandleMessage);
            }
            else
            {
                MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<T>>(topic, DoHandleMessage);
                if (topic.EndsWith(":all"))
                    topic = topic.Replace(":all", "");
                MessageRouter = MessageRouter.CreateWithoutTopicFilter<KucoinSocketUpdate<T>>(topic, DoHandleMessage);
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
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
