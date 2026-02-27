

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinUnifiedWelcome
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("pingInterval")]
        public long PingInterval { get; set; }
    }
}
