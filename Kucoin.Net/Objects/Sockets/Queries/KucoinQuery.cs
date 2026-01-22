using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Kucoin.Net.Objects.Internal;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinQuery : Query<KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;

        public KucoinQuery(SocketApiClient client, string type, string topic, bool auth) : base(new KucoinRequest(ExchangeHelpers.NextId().ToString(), type, topic, auth), auth)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<KucoinSocketResponse>(((KucoinRequest)Request).Id, HandleMessage);
        }

        public CallResult<KucoinSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketResponse message)
        {
            if (string.Equals(message.Type, "error", StringComparison.Ordinal))
                return new CallResult<KucoinSocketResponse>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Data!)));

            return new CallResult<KucoinSocketResponse>(message, originalData, null);
        }
    }
}
