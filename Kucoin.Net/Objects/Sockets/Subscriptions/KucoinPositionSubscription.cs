using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinPositionSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DataEvent<KucoinPositionUpdate>>? _onPositionUpdate;
        private readonly Action<DataEvent<KucoinPositionMarkPriceUpdate>>? _onMarkPriceUpdate;
        private readonly Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? _onFundingSettlementUpdate;
        private readonly Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? _onRiskAdjustUpdate;
        private readonly string _topic;

        public KucoinPositionSubscription(
            ILogger logger,
            SocketApiClient client,
            string? symbol,
            Action<DataEvent<KucoinPositionUpdate>>? onPositionUpdate,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>>? onMarkPriceUpdate,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? onFundingSettlementUpdate,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? onRiskAdjustUpdate
            ) : base(logger, true)
        {
            _client = client;
            _topic = symbol == null ? "/contract/positionAll" : "/contract/position:" + symbol;
            _onPositionUpdate = onPositionUpdate;
            _onMarkPriceUpdate = onMarkPriceUpdate;
            _onFundingSettlementUpdate = onFundingSettlementUpdate;
            _onRiskAdjustUpdate = onRiskAdjustUpdate;

            MessageMatcher = MessageMatcher.Create([
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinPositionUpdate>>(_topic + "position.change", DoHandleMessage),
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>>(_topic + "position.changemarkPriceChange", DoHandleMessage),
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>>(_topic + "position.settlement", DoHandleMessage),
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>>(_topic + "position.adjustRiskLimit", DoHandleMessage),
                ]);

            var routerTopic = symbol == null ? "/contract/positionAll" : "/contract/position";
            MessageRouter = MessageRouter.Create([
                    MessageRoute<KucoinSocketUpdate<KucoinPositionUpdate>>.CreateWithOptionalTopicFilter(routerTopic + "position.change", symbol, DoHandleMessage),
                    MessageRoute<KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>>.CreateWithOptionalTopicFilter(routerTopic + "position.changemarkPriceChange", symbol, DoHandleMessage),
                    MessageRoute<KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>>.CreateWithOptionalTopicFilter(routerTopic + "position.settlement", symbol, DoHandleMessage),
                    MessageRoute<KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>>.CreateWithOptionalTopicFilter(routerTopic + "position.adjustRiskLimit",symbol, DoHandleMessage),
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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinPositionMarkPriceUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onMarkPriceUpdate?.Invoke(
                    new DataEvent<KucoinPositionMarkPriceUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinPositionUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.CurrentTime);

            _onPositionUpdate?.Invoke(
                    new DataEvent<KucoinPositionUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(message.Data.Symbol)
                        .WithDataTimestamp(message.Data.CurrentTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onFundingSettlementUpdate?.Invoke(
                    new DataEvent<KucoinPositionFundingSettlementUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate> message)
        {
            _onRiskAdjustUpdate?.Invoke(
                    new DataEvent<KucoinPositionRiskAdjustResultUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                        .WithStreamId(message.Topic)
                        .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }
    }
}
