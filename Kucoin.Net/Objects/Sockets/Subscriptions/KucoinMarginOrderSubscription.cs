using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinMarginOrderSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<KucoinMarginOrderUpdate>>? _onNewOrder;
        private readonly Action<DataEvent<KucoinMarginOrderUpdate>>? _onOrderData;
        private readonly Action<DataEvent<KucoinMarginOrderDoneUpdate>>? _onOrderDone;
        private readonly string _topic;

        public KucoinMarginOrderSubscription(
            ILogger logger,
            SocketApiClient client,
            string asset,
            Action<DataEvent<KucoinMarginOrderUpdate>>? onNewOrder,
            Action<DataEvent<KucoinMarginOrderUpdate>>? onOrderData,
            Action<DataEvent<KucoinMarginOrderDoneUpdate>>? onOrderDone
            ) : base(logger, true)
        {
            _client = client;
            _onOrderData = onOrderData;
            _onOrderDone = onOrderDone;
            _onNewOrder = onNewOrder;

            _topic = "/margin/loan:" + asset;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginOrderUpdate>>(_topic + "order.open", DoHandleOpenMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginOrderUpdate>>(_topic + "order.update", DoHandleUpdateMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginOrderDoneUpdate>>(_topic + "order.done", DoHandleDoneMessage)
                ]);

            MessageRouter = MessageRouter.Create([
                MessageRoute<KucoinSocketUpdate<KucoinMarginOrderUpdate>>.CreateWithTopicFilter("/margin/loan" + "order.open", asset, DoHandleOpenMessage),
                MessageRoute<KucoinSocketUpdate<KucoinMarginOrderUpdate>>.CreateWithTopicFilter("/margin/loan" + "order.update",  asset, DoHandleUpdateMessage),
                MessageRoute<KucoinSocketUpdate<KucoinMarginOrderDoneUpdate>>.CreateWithTopicFilter("/margin/loan" + "order.done",  asset, DoHandleDoneMessage)
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

        public CallResult DoHandleDoneMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinMarginOrderDoneUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onOrderDone?.Invoke(
                    new DataEvent<KucoinMarginOrderDoneUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleOpenMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinMarginOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onNewOrder?.Invoke(
                    new DataEvent<KucoinMarginOrderUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleUpdateMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinMarginOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onOrderData?.Invoke(
                    new DataEvent<KucoinMarginOrderUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }
    }
}
