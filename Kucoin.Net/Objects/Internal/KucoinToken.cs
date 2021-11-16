using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinToken
    {
        public string Token { get; set; } = string.Empty;
        [JsonProperty("instanceServers")]
        public IEnumerable<KucoinInstanceServer> Servers { get; set; } = Array.Empty<KucoinInstanceServer>();
    }

    internal class KucoinInstanceServer
    {
        public int PingInterval { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public bool Encrypt { get; set; }
        public int PingTimeout { get; set; }
    }
}
