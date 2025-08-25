using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinPositionSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
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
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>> message)
        {
            _onMarkPriceUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinPositionUpdate>> message)
        {
            _onPositionUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>> message)
        {
            _onFundingSettlementUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>> message)
        {
            _onRiskAdjustUpdate?.Invoke(message.As(message.Data.Data, message.Data.Topic, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
