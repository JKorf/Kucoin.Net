using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinWelcomeSubscription : SystemSubscription
    {
        public KucoinWelcomeSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<KucoinWelcome>("welcome");
        }
    }
}
