using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Kucoin.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinUnifiedWelcomeSubscription : SystemSubscription
    {
        private readonly SocketApiClient _client;

        public KucoinUnifiedWelcomeSubscription(SocketApiClient client, ILogger logger) : base(logger, false)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<KucoinUnifiedWelcome>("welcome", HandleUpdate);
        }

        private CallResult? HandleUpdate(SocketConnection connection, DateTime time, string? arg3, KucoinUnifiedWelcome welcome)
        {
            if (connection.Properties.ContainsKey("periodicPingSet"))
                return CallResult.SuccessResult;

            connection.QueryPeriodic(
                "Ping",
                TimeSpan.FromMilliseconds(welcome.PingInterval),
                 x => new KucoinPingQuery(DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()!),
                 (connection, result) =>
                 {
                     if (result.Error?.ErrorType == ErrorType.Timeout)
                     {
                         // Ping timeout, reconnect
                         _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                         _ = connection.TriggerReconnectAsync();
                     }
                 });

            connection.Properties.Add("periodicPingSet", true);
            return CallResult.SuccessResult;
        }
    }
}
