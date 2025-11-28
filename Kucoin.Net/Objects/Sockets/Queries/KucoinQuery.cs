using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Internal;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinQuery : Query<KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;

        public KucoinQuery(SocketApiClient client, string type, string topic, bool auth) : base(new KucoinRequest(ExchangeHelpers.NextId().ToString(), type, topic, auth), auth)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<KucoinSocketResponse>(((KucoinRequest)Request).Id, HandleMessage);
            MessageRouter = MessageRouter.Create<KucoinSocketResponse>(((KucoinRequest)Request).Id, HandleMessage);
        }

        public CallResult<KucoinSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketResponse message)
        {
            if (string.Equals(message.Type, "error", StringComparison.Ordinal))
                return new CallResult<KucoinSocketResponse>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Data!)));

            return new CallResult<KucoinSocketResponse>(message, originalData, null);
        }
    }
}
