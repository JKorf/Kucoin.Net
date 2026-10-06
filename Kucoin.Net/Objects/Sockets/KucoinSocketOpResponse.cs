

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketOpResponse<T>
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
