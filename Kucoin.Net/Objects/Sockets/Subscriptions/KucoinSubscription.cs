using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinSubscription<T> : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set;  }

        public KucoinSubscription(ILogger logger, string topic, List<string>? symbols, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _handler = handler;

            ListenerIdentifiers = symbols?.Any() == true ? new HashSet<string>(symbols.Select(s => topic + ":" + s)) : new HashSet<string> { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery("subscribe", _topic, Authenticated);
        }

        public override Query? GetUnsubQuery()
        {
            return new KucoinQuery("unsubscribe", _topic, Authenticated);
        }

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (KucoinSocketUpdate<T>)message.Data;
            string? topic = data.Topic.Contains(":") ? data.Topic.Split(':').Last() : null;
            if (string.Equals(topic, "all", StringComparison.Ordinal))
                topic = data.Subject;

            _handler.Invoke(message.As(data.Data, data.Topic, topic, SocketUpdateType.Update));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(KucoinSocketUpdate<T>);
    }
}
