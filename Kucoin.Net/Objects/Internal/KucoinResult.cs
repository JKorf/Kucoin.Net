

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }

    internal class KucoinResult<T> : KucoinResult
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
