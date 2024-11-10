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
    internal class KucoinMarginPositionSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinMarginDebtRatioUpdate>>? _onDebtRatioChange;
        private readonly Action<DataEvent<KucoinMarginPositionStatusUpdate>>? _onPositionStatusChange;
        private readonly string _topic = "/margin/position";
        private static readonly MessagePath _subjectPath = MessagePath.Get().Property("subject");

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinMarginPositionSubscription(
            ILogger logger,
            Action<DataEvent<KucoinMarginDebtRatioUpdate>>? onDebtRatioChange,
            Action<DataEvent<KucoinMarginPositionStatusUpdate>>? onPositionStatusChange
            ) : base(logger, true)
        {
            _onDebtRatioChange = onDebtRatioChange;
            _onPositionStatusChange = onPositionStatusChange;

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
            if (message.Data is KucoinSocketUpdate<KucoinMarginDebtRatioUpdate> debtUpdate)
                _onDebtRatioChange?.Invoke(message.As(debtUpdate.Data, debtUpdate.Topic, null, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinMarginPositionStatusUpdate> statusUpdate)
                _onPositionStatusChange?.Invoke(message.As(statusUpdate.Data, statusUpdate.Topic, null, SocketUpdateType.Update));

            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_subjectPath);
            if (string.Equals(type, "debt.ratio", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>);
            if (string.Equals(type, "position.status", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>);

            return null;
        }
    }
}
