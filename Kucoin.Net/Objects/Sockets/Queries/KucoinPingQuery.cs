using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinPingQuery : Query<KucoinPong>
    {
        public KucoinPingQuery(string id) : base(new KucoinPing { Id = id, Type = "ping" }, false)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<KucoinPong>(id);
        }
    }
}
