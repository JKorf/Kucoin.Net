

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinUnifiedPing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("op")]
        public string Op { get; set; } = string.Empty;
    }
}
