using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Internal;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal interface IKucoinUnifiedOpQuery
    {
        bool IsFutures { get; }
    }

    internal class KucoinUnifiedOpQuery<TResponse> : Query<TResponse>, IKucoinUnifiedOpQuery
    {
        private readonly SocketApiClient _client;
        public bool IsFutures { get; }

        public KucoinUnifiedOpQuery(SocketApiClient client, KucoinUnifiedOpRequest request, bool futures) : base(request, true, 1)
        {
            _client = client;
            IsFutures = futures;
            MessageRouter = MessageRouter.CreateForQuery<KucoinSocketOpResponse<TResponse>, TResponse>(request.Id, HandleMessage);
        }

        public CallResult<TResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, KucoinSocketOpResponse<TResponse> message)
        {
            if (message.Code != "200000")
            {
                var error = new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message));
                return CallResult.Fail<TResponse>(error, originalData);
            }

            return CallResult.Ok(message.Data, originalData);
        }
    }
}
