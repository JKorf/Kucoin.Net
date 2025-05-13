using System;
using System.Collections.Generic;


namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("instanceServers")]
        public KucoinInstanceServer[] Servers { get; set; } = Array.Empty<KucoinInstanceServer>();
    }

    internal class KucoinInstanceServer
    {
        [JsonPropertyName("pingInterval")]
        public int PingInterval { get; set; }
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; } = string.Empty;
        [JsonPropertyName("protocol")]
        public string Protocol { get; set; } = string.Empty;
        [JsonPropertyName("encrypt")]
        public bool Encrypt { get; set; }
        [JsonPropertyName("pingTimeout")]
        public int PingTimeout { get; set; }
    }
}
