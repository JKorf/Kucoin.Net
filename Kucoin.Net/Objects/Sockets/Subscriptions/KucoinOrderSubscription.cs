using CryptoExchange.Net.Clients;
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
    internal class KucoinOrderSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<KucoinStreamOrderNewUpdate>>? _onNewOrder;
        private readonly Action<DataEvent<KucoinStreamOrderUpdate>>? _onOrderData;
        private readonly Action<DataEvent<KucoinStreamOrderMatchUpdate>>? _onTradeData;
        private readonly string _topic = "/spotMarket/tradeOrdersV2";


        public KucoinOrderSubscription(
            ILogger logger,
            SocketApiClient client,
            Action<DataEvent<KucoinStreamOrderNewUpdate>>? onNewOrder,
            Action<DataEvent<KucoinStreamOrderUpdate>>? onOrderData,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>>? onTradeData
            ) : base(logger, true)
        {
            _client = client;
            _onOrderData = onOrderData;
            _onTradeData = onTradeData;
            _onNewOrder = onNewOrder;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderMatchUpdate>>(_topic + "match", DoHandleMatchMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderNewUpdate>>(_topic + "received", DoHandleNewMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "open", DoHandleUpdateMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "update", DoHandleUpdateMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "filled", DoHandleUpdateMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "canceled", DoHandleUpdateMessage),
                ]);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMatchMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamOrderMatchUpdate>> message)
        {
            _onTradeData?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleUpdateMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamOrderUpdate>> message)
        {
            _onOrderData?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleNewMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamOrderNewUpdate>> message)
        {
            _onNewOrder?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
