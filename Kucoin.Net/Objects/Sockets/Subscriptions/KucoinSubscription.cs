using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinSubscription<T> : Subscription<KucoinSocketResponse, KucoinSocketUpdate<T>>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override List<string> Identifiers { get; }

        public KucoinSubscription(ILogger logger, string topic, List<string>? symbols, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _handler = handler;

            Identifiers = symbols?.Any() == true ? symbols.Select(s => topic + ":" + s.ToLowerInvariant()).ToList() : new List<string> { topic };
        }

        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery("subscribe", _topic, Authenticated);
        }

        public override BaseQuery? GetUnsubQuery()
        {
            return new KucoinQuery("unsubscribe", _topic, Authenticated);
        }

        public override Task<CallResult> HandleEventAsync(SocketConnection connection, DataEvent<ParsedMessage<KucoinSocketUpdate<T>>> message)
        {
            string? topic = message.Data.TypedData.Topic.Contains(":") ? message.Data.TypedData.Topic.Split(':').Last() : null;
            if (topic == "all")
                topic = message.Data.TypedData.Subject;

            _handler.Invoke(message.As(message.Data.TypedData.Data, topic, SocketUpdateType.Update));
            return Task.FromResult(new CallResult(null));
        }
    }
}
