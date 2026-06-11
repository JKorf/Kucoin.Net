using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinOrderSubscription : Subscription
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

            MessageRouter = MessageRouter.Create([
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderMatchUpdate>>(_topic + "match", DoHandleMatchMessage),
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderNewUpdate>>(_topic + "received", DoHandleNewMessage),
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "open", DoHandleUpdateMessage),
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "update", DoHandleUpdateMessage),
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "filled", DoHandleUpdateMessage),
                MessageRoute.CreateForEvent<KucoinSocketUpdate<KucoinStreamOrderUpdate>>(_topic + "canceled", DoHandleUpdateMessage),
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

        public CallResult DoHandleMatchMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamOrderMatchUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onTradeData?.Invoke(
                    new DataEvent<KucoinStreamOrderMatchUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithSymbol(message.Data.Symbol)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            
            return CallResult.Ok();
        }

        public CallResult DoHandleUpdateMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onOrderData?.Invoke(
                    new DataEvent<KucoinStreamOrderUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithSymbol(message.Data.Symbol)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.Ok();
        }

        public CallResult DoHandleNewMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamOrderNewUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onNewOrder?.Invoke(
                    new DataEvent<KucoinStreamOrderNewUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithSymbol(message.Data.Symbol)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.Ok();
        }
    }
}
