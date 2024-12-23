using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Sockets.Queries
{
    internal class KucoinPingQuery : Query<KucoinPong>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public KucoinPingQuery(string id) : base(new KucoinPing { Id = id, Type = "ping" }, false)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string> { id };
        }
    }
}
