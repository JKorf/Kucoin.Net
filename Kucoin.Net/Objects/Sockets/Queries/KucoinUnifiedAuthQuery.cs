using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Internal;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinUnifiedAuthQuery : Query<KucoinUnifiedAuthResult>
    {
        private readonly SocketApiClient _client;

        public KucoinUnifiedAuthQuery(SocketApiClient client, KucoinUnifiedAuthRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.Create(
                MessageRoute.CreateForQuery<KucoinUnifiedAuthResult>(request.Id, HandleMessage), // For wss://wsapi-push.kucoin.com
                MessageRoute.CreateForQuery<KucoinUnifiedWelcome, KucoinUnifiedAuthResult>("welcome", HandleMessage) // For wss://wsapi.kucoin.com/v2/private
                );
        }

        public CallResult<KucoinUnifiedAuthResult> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinUnifiedWelcome message)
        {
            if (connection.ConnectionUriString.EndsWith("v2/private"))
            {
                // Welcome message means success only for the trade API
                return CallResult.Ok<KucoinUnifiedAuthResult>(new KucoinUnifiedAuthResult(), originalData);
            }

            return null;
        }

        public CallResult<KucoinUnifiedAuthResult> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinUnifiedAuthResult message)
        {
            if (message.Result)
                return CallResult.Ok(message, originalData);

            return CallResult.Fail<KucoinUnifiedAuthResult>(new ServerError(_client.GetErrorInfo(message.Message!, message.Message)));
        }
    }
}
