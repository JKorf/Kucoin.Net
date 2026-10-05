using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinUnifiedPingQuery : Query<KucoinPong>
    {
        public KucoinUnifiedPingQuery(string id) : base(new KucoinUnifiedPing { Id = id, Op = "ping" }, false)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateVoid<KucoinPong>(id);
        }
    }
}
