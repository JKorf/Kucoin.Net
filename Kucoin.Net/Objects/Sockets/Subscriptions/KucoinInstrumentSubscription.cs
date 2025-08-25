using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinInstrumentSubscription : Subscription<KucoinSocketResponse, KucoinSocketResponse>
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

            if (symbols?.Count > 0)
            {
                var checkers = new List<MessageHandlerLink>();
                foreach(var symbol in symbols)
                {
                    checkers.Add(new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>>(topic + ":" + symbol + "mark.index.price", DoHandleMessage));
                    checkers.Add(new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>>(topic + ":" + symbol + "funding.rate", DoHandleMessage));
                }

                MessageMatcher = MessageMatcher.Create(checkers.ToArray());
            }
            else
            {
                MessageMatcher = MessageMatcher.Create(
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>>(topic + "mark.index.price", DoHandleMessage),
                    new MessageHandlerLink<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>>(topic + "funding.rate", DoHandleMessage));
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

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>> message)
        {
            _markIndexPriceHandler?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Topic.Split(new char[] { ':' }).Last(), SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<KucoinSocketUpdate<KucoinStreamFuturesFundingRate>> message)
        {
            _fundingRateHandler?.Invoke(message.As(message.Data.Data, message.Data.Topic, message.Data.Topic.Split(new char[] { ':' }).Last(), SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
