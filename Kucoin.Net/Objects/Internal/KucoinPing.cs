using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinPing
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
