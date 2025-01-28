

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinPing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
