using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models.Unified;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinUnifiedBalanceSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly string _type;
        private readonly Action<DataEvent<KucoinUaBalanceUpdate>> _handler;

        public KucoinUnifiedBalanceSubscription(
            ILogger logger, 
            SocketApiClient client,    
            Action<DataEvent<KucoinUaBalanceUpdate>> handler)
            : base(logger, true)
        {
            _client = client;
            _type = "UNIFIED";
            _handler = handler;

            var id = $"balance.UNIFIED";
            MessageRouter = MessageRouter.CreateForEvent<KucoinUnifiedSocketUpdate<KucoinUaBalanceUpdate>>(id, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinUnifiedAccountQuery(
                _client,
                new KucoinUnifiedAccountRequest(ExchangeHelpers.NextId().ToString(), "SUBSCRIBE", "balance", _type),
                Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinUnifiedAccountQuery(
                _client,
                new KucoinUnifiedAccountRequest(ExchangeHelpers.NextId().ToString(), "UNSUBSCRIBE", "balance", _type),
                Authenticated);
        }

        private CallResult? DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinUnifiedSocketUpdate<KucoinUaBalanceUpdate> message)
        {
            _client.UpdateTimeOffset(message.PushTime);

            _handler.Invoke(
                new DataEvent<KucoinUaBalanceUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Type)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.PushTime, _client.GetTimeOffset()));
            return CallResult.Ok();
        }

    }
}
