using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinIsolatedMarginPositionSubscription : Subscription
    {
        private readonly SocketApiClient _client;

        private readonly Action<DataEvent<KucoinIsolatedMarginPositionUpdate>>? _onPositionChange;
        private string _topic;

        public KucoinIsolatedMarginPositionSubscription(
            ILogger logger,
            SocketApiClient client,
            string symbol,
            Action<DataEvent<KucoinIsolatedMarginPositionUpdate>>? onPositionChange
            ) : base(logger, true)
        {
            _client = client;
            _onPositionChange = onPositionChange;
            _topic = "/margin/isolatedPosition:" + symbol;

            MessageRouter = MessageRouter.CreateWithTopicFilter<KucoinSocketUpdate<KucoinIsolatedMarginPositionUpdate>>("/margin/isolatedPosition", symbol, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinIsolatedMarginPositionUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _onPositionChange?.Invoke(
                new DataEvent<KucoinIsolatedMarginPositionUpdate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithSymbol(message.Data.Tag)
                    .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );

            return CallResult.SuccessResult;
        }
    }
}
