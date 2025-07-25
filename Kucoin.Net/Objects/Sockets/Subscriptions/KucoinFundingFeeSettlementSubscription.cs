﻿using CryptoExchange.Net.Interfaces;
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
        private string _topic;
        private Action<DataEvent<KucoinContractAnnouncement>> _dataHandler;

        public KucoinFundingFeeSettlementSubscription(ILogger logger, Action<DataEvent<KucoinContractAnnouncement>> dataHandler) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<KucoinSocketUpdate<KucoinContractAnnouncement>>("/contract/announcement", DoHandleMessage);

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

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinContractAnnouncement>> message)
        {
            message.Data.Data.Event = message.Data.Subject;
            _dataHandler.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Data.Symbol, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

    }
}
