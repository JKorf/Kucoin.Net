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
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinPositionSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinPositionUpdate>>? _onPositionUpdate;
        private readonly Action<DataEvent<KucoinPositionMarkPriceUpdate>>? _onMarkPriceUpdate;
        private readonly Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? _onFundingSettlementUpdate;
        private readonly Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? _onRiskAdjustUpdate;
        private readonly string _topic;
        private static readonly MessagePath _subjectPath = MessagePath.Get().Property("subject");
        private static readonly MessagePath _changeReasonPath = MessagePath.Get().Property("data").Property("changeReason");

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinPositionSubscription(
            ILogger logger,
            string symbol,
            Action<DataEvent<KucoinPositionUpdate>>? onPositionUpdate,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>>? onMarkPriceUpdate,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? onFundingSettlementUpdate,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? onRiskAdjustUpdate
            ) : base(logger, true)
        {
            _topic = "/contract/position:" + symbol;
            _onPositionUpdate = onPositionUpdate;
            _onMarkPriceUpdate = onMarkPriceUpdate;
            _onFundingSettlementUpdate = onFundingSettlementUpdate;
            _onRiskAdjustUpdate = onRiskAdjustUpdate;

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

        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<object> message)
        {
            if (message.Data is KucoinSocketUpdate<KucoinPositionMarkPriceUpdate> markUpdate)
                _onMarkPriceUpdate?.Invoke(message.As(markUpdate.Data, markUpdate.Topic, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinPositionUpdate> positionUpdate)
                _onPositionUpdate?.Invoke(message.As(positionUpdate.Data, positionUpdate.Topic, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate> fundUpdate)
                _onFundingSettlementUpdate?.Invoke(message.As(fundUpdate.Data, fundUpdate.Topic, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate> riskAdjust)
                _onRiskAdjustUpdate?.Invoke(message.As(riskAdjust.Data, riskAdjust.Topic, SocketUpdateType.Update));


            return Task.FromResult(new CallResult(null));
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var subject = message.GetValue<string>(_subjectPath);
            if (subject == "position.change")
            {
                var change = message.GetValue<string>(_changeReasonPath);
                if (change == null || change == "markPriceChange")
                    return typeof(KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>);
                else
                    return typeof(KucoinSocketUpdate<KucoinPositionUpdate>);
            }
            if (subject == "position.settlement")
                return typeof(KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>);
            if (subject == "position.adjustRiskLimit")
                return typeof(KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>);

            return null;
        }
    }
}
