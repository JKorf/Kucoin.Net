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
    internal class KucoinMarginPositionSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<KucoinMarginDebtRatioUpdate>>? _onDebtRatioChange;
        private readonly Action<DataEvent<KucoinMarginPositionStatusUpdate>>? _onPositionStatusChange;
        private readonly string _topic = "/margin/position";
        

        public KucoinMarginPositionSubscription(
            ILogger logger,
            SocketApiClient client,
            Action<DataEvent<KucoinMarginDebtRatioUpdate>>? onDebtRatioChange,
            Action<DataEvent<KucoinMarginPositionStatusUpdate>>? onPositionStatusChange
            ) : base(logger, true)
        {
            _client = client;
            _onDebtRatioChange = onDebtRatioChange;
            _onPositionStatusChange = onPositionStatusChange;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>>(_topic + "debt.ratio", DoHandleMessage),
                new MessageHandlerLink<KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>>(_topic + "position.status", DoHandleMessage)
                ]);

            MessageRouter = MessageRouter.Create([
                MessageRoute<KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>>.CreateWithoutTopicFilter(_topic + "debt.ratio", DoHandleMessage),
                MessageRoute<KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>>.CreateWithoutTopicFilter(_topic + "position.status", DoHandleMessage)
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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinMarginDebtRatioUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onDebtRatioChange?.Invoke(
                    new DataEvent<KucoinMarginDebtRatioUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinMarginPositionStatusUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onPositionStatusChange?.Invoke(
                    new DataEvent<KucoinMarginPositionStatusUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }
    }
}
