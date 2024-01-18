using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.SocketsV2;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinSubscription<T> : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override List<string> StreamIdentifiers { get; set;  }

        public KucoinSubscription(ILogger logger, string topic, List<string>? symbols, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _handler = handler;

            StreamIdentifiers = symbols?.Any() == true ? symbols.Select(s => topic + ":" + s.ToLowerInvariant()).ToList() : new List<string> { topic };
        }

        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery("subscribe", _topic, Authenticated);
        }

        public override BaseQuery? GetUnsubQuery()
        {
            return new KucoinQuery("unsubscribe", _topic, Authenticated);
        }

        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<object> message)
        {
            var data = (KucoinSocketUpdate<T>)message.Data;
            string? topic = data.Topic.Contains(":") ? data.Topic.Split(':').Last() : null;
            if (topic == "all")
                topic = data.Subject;

            _handler.Invoke(message.As(data.Data, topic, SocketUpdateType.Update));
            return Task.FromResult(new CallResult(null));
        }

        public override Type? GetMessageType(SocketMessage message) => typeof(KucoinSocketUpdate<T>);
    }
}
