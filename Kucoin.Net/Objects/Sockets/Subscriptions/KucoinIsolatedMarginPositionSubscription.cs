using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinIsolatedMarginPositionSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinIsolatedMarginPositionUpdate>>? _onPositionChange;
        private string _topic;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinIsolatedMarginPositionSubscription(
            ILogger logger,
            string symbol,
            Action<DataEvent<KucoinIsolatedMarginPositionUpdate>>? onPositionChange
            ) : base(logger, true)
        {
            _onPositionChange = onPositionChange;
            _topic = "/margin/isolatedPosition:" + symbol;

            ListenerIdentifiers = new HashSet<string> { _topic };
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
            var data = (KucoinSocketUpdate<KucoinIsolatedMarginPositionUpdate>)message.Data;
            _onPositionChange?.Invoke(message.As(data.Data, data.Topic, data.Data.Tag, SocketUpdateType.Update));

            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(KucoinSocketUpdate<KucoinIsolatedMarginPositionUpdate>);
        }
    }
}
