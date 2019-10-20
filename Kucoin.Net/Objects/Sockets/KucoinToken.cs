using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinToken
    {
        public string Token { get; set; } = "";
        [JsonProperty("instanceServers")]
        public IEnumerable<KucoinInstanceServer> Servers { get; set; } = new List<KucoinInstanceServer>();
    }

    internal class KucoinInstanceServer
    {
        public int PingInterval { get; set; }
        public string Endpoint { get; set; } = "";
        public string Protocol { get; set; } = "";
        public bool Encrypt { get; set; }
        public int PingTimeout { get; set; }
    }
}
