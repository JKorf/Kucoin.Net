using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinFundingFeeSettlementSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private string _topic;
        private Action<DataEvent<KucoinContractAnnouncement>> _dataHandler;

        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "/contract/announcement" };

        public KucoinFundingFeeSettlementSubscription(ILogger logger, Action<DataEvent<KucoinContractAnnouncement>> dataHandler) : base(logger, false)
        {
            _topic = "/contract/announcement";
            _dataHandler = dataHandler;
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
            var data = (KucoinSocketUpdate<KucoinContractAnnouncement>)message.Data;
            data.Data.Event = data.Subject;
            _dataHandler.Invoke(message.As(data.Data, data.Topic, data.Data.Symbol, SocketUpdateType.Update));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(KucoinSocketUpdate<KucoinContractAnnouncement>);
    }
}
