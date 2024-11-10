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
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinOrderSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinStreamOrderNewUpdate>>? _onNewOrder;
        private readonly Action<DataEvent<KucoinStreamOrderUpdate>>? _onOrderData;
        private readonly Action<DataEvent<KucoinStreamOrderMatchUpdate>>? _onTradeData;
        private readonly string _topic = "/spotMarket/tradeOrdersV2";
        private static readonly MessagePath _typePath = MessagePath.Get().Property("data").Property("type");

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinOrderSubscription(
            ILogger logger,
            Action<DataEvent<KucoinStreamOrderNewUpdate>>? onNewOrder,
            Action<DataEvent<KucoinStreamOrderUpdate>>? onOrderData,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>>? onTradeData
            ) : base(logger, true)
        {
            _onOrderData = onOrderData;
            _onTradeData = onTradeData;
            _onNewOrder = onNewOrder;

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
            if (message.Data is KucoinSocketUpdate<KucoinStreamOrderMatchUpdate> matchUpdate)
                _onTradeData?.Invoke(message.As(matchUpdate.Data, matchUpdate.Topic, matchUpdate.Data.Symbol, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinStreamOrderUpdate> orderUpdate)
                _onOrderData?.Invoke(message.As(orderUpdate.Data, orderUpdate.Topic, orderUpdate.Data.Symbol, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinStreamOrderNewUpdate> newOrderUpdate)
                _onNewOrder?.Invoke(message.As(newOrderUpdate.Data, newOrderUpdate.Topic, newOrderUpdate.Data.Symbol, SocketUpdateType.Update));

            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            if (string.Equals(type, "match", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinStreamOrderMatchUpdate>);
            if (string.Equals(type, "received", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinStreamOrderNewUpdate>);
            return typeof(KucoinSocketUpdate<KucoinStreamOrderUpdate>);
        }
    }
}
