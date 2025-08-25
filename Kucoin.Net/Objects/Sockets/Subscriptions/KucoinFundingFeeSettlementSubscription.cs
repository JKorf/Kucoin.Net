using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinFundingFeeSettlementSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<KucoinContractAnnouncement>> _dataHandler;

        public KucoinFundingFeeSettlementSubscription(ILogger logger, SocketApiClient client, Action<DataEvent<KucoinContractAnnouncement>> dataHandler) : base(logger, false)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<KucoinContractAnnouncement>>("/contract/announcement", DoHandleMessage);

            _topic = "/contract/announcement";
            _dataHandler = dataHandler;
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinContractAnnouncement>> message)
        {
            message.Data.Data.Event = message.Data.Subject;
            _dataHandler.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

    }
}
