using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinFundingFeeSettlementSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<KucoinContractAnnouncement>> _dataHandler;

        public KucoinFundingFeeSettlementSubscription(ILogger logger, SocketApiClient client, Action<DataEvent<KucoinContractAnnouncement>> dataHandler) : base(logger, false)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<KucoinContractAnnouncement>>("/contract/announcement", DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<KucoinSocketUpdate<KucoinContractAnnouncement>>("/contract/announcement", DoHandleMessage);

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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinContractAnnouncement> message)
        {
            message.Data.Event = message.Subject;
            _dataHandler.Invoke(
                new DataEvent<KucoinContractAnnouncement>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                );
            return CallResult.SuccessResult;
        }

    }
}
