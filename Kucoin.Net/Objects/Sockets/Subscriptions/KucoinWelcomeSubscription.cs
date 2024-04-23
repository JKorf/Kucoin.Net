using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Sockets.Subscriptions
{
    internal class KucoinWelcomeSubscription : SystemSubscription<KucoinWelcome>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "welcome" };

        public KucoinWelcomeSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<KucoinWelcome> message) => new CallResult(null);
    }
}
