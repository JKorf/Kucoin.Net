using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinPing
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
