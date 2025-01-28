

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public int? Code { get; set; }
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }
}
