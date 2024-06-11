using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinBalanceSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly Action<DataEvent<KucoinStreamOrderMarginUpdate>>? _onOrderMarginUpdate;
        private readonly Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? _onBalanceUpdate;
        private readonly Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? _onWithdrawableUpdate;
        private readonly string _topic = "/contractAccount/wallet";
        private static readonly MessagePath _subjectPath = MessagePath.Get().Property("subject");

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinBalanceSubscription(
            ILogger logger,
            Action<DataEvent<KucoinStreamOrderMarginUpdate>>? onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>>? onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>>? onWithdrawableUpdate
            ) : base(logger, true)
        {
            _onOrderMarginUpdate = onOrderMarginUpdate;
            _onBalanceUpdate = onBalanceUpdate;
            _onWithdrawableUpdate = onWithdrawableUpdate;

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
            if (message.Data is KucoinSocketUpdate<KucoinStreamOrderMarginUpdate> marginUpdate)
                _onOrderMarginUpdate?.Invoke(message.As(marginUpdate.Data, marginUpdate.Topic, null, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate> balanceUpdate)
                _onBalanceUpdate?.Invoke(message.As(balanceUpdate.Data, balanceUpdate.Topic, null, SocketUpdateType.Update));
            if (message.Data is KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate> withdrawableUpdate)
                _onWithdrawableUpdate?.Invoke(message.As(withdrawableUpdate.Data, withdrawableUpdate.Topic, null, SocketUpdateType.Update));

            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var subject = message.GetValue<string>(_subjectPath);
            if (string.Equals(subject, "orderMargin.change", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinStreamOrderMarginUpdate>);
            if (string.Equals(subject, "availableBalance.change", StringComparison.Ordinal))
                return typeof(KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate>);
            return typeof(KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate>);
        }
    }
}
