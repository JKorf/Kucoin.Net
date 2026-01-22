using CryptoExchange.Net.Sockets;
using System;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinPingQuery : Query<KucoinPong>
    {
        public KucoinPingQuery(string id) : base(new KucoinPing { Id = id, Type = "ping" }, false)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateWithoutHandler<KucoinPong>(id);
        }
    }
}
