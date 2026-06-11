using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Internal;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinUnifiedAccountQuery : Query<KucoinSocketResponse>
    {
        private readonly SocketApiClient _client;

        public KucoinUnifiedAccountQuery(SocketApiClient client, KucoinUnifiedAccountRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateForQuery<KucoinSocketResponse>(request.Id, HandleMessage);
        }

        public CallResult<KucoinSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketResponse message)
        {
            if (string.Equals(message.Type, "error", StringComparison.Ordinal))
                return CallResult.Fail<KucoinSocketResponse>(new ServerError(message.Code!.Value, _client.GetErrorInfo(message.Code.Value, message.Data!)), originalData);

            return CallResult.Ok(message, originalData);
        }
    }
}
