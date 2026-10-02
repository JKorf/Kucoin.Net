using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Internal;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinUnifiedAuthQuery : Query<KucoinUnifiedWelcome>
    {
        private readonly SocketApiClient _client;

        public KucoinUnifiedAuthQuery(SocketApiClient client, KucoinUnifiedAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateForQuery<KucoinUnifiedWelcome>("welcome", HandleMessage);
        }

        public CallResult<KucoinUnifiedWelcome> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinUnifiedWelcome message)
        {
#warning TODO error handling..

            return CallResult.Ok(message, originalData);
        }
    }
}
