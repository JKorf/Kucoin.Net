using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinWelcomeSubscription : SystemSubscription
    {
        public KucoinWelcomeSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutHandler<KucoinWelcome>("welcome");
        }
    }
}
