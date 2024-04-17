using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }

    internal class KucoinResult<T> : KucoinResult
    {
        public T Data { get; set; } = default!;
    }
}
