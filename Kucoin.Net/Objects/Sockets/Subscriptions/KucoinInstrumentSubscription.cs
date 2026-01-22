using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinInstrumentSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> _markIndexPriceHandler;
        private Action<DataEvent<KucoinStreamFuturesFundingRate>> _fundingRateHandler;

        public KucoinInstrumentSubscription(ILogger logger, SocketApiClient client, List<string>? symbols, Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> markIndexPriceHandler, Action<DataEvent<KucoinStreamFuturesFundingRate>> fundingRateHandler) : base(logger, false)
        {
            _client = client;
            var topic = "/contract/instrument";
            _topic = symbols?.Any() == true ? topic + ":" + string.Join(",", symbols) : topic;
            _markIndexPriceHandler = markIndexPriceHandler;
            _fundingRateHandler = fundingRateHandler;

            IndividualSubscriptionCount = symbols?.Count ?? 1;

            if (symbols?.Count > 0)
            {
                var routes = new List<MessageRoute>();
                foreach (var symbol in symbols)
                {
                    routes.Add(MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>>.CreateWithTopicFilter(topic + "mark.index.price",  symbol, DoHandleMessage));
                    routes.Add(MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>>.CreateWithTopicFilter(topic + "funding.rate", symbol, DoHandleMessage));
                }

                MessageRouter = MessageRouter.Create(routes.ToArray());
            }
            else
            {
                MessageRouter = MessageRouter.Create(
                    MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>>.CreateWithoutTopicFilter(topic + "mark.index.price", DoHandleMessage),
                    MessageRoute<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>>.CreateWithoutTopicFilter(topic + "funding.rate", DoHandleMessage));
            }
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "subscribe", _topic, Authenticated);
        }

        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new KucoinQuery(_client, "unsubscribe", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _markIndexPriceHandler?.Invoke(
                new DataEvent<KucoinStreamFuturesMarkIndexPrice>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithSymbol(message.Topic.Split(new char[] { ':' }).Last())
                    .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketUpdate<KucoinStreamFuturesFundingRate> message)
        {
            _client.UpdateTimeOffset(message.Data.Timestamp);

            _fundingRateHandler?.Invoke(
                new DataEvent<KucoinStreamFuturesFundingRate>(KucoinExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Topic)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithSymbol(message.Topic.Split(new char[] { ':' }).Last())
                    .WithDataTimestamp(message.Data.Timestamp, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }
    }
}
