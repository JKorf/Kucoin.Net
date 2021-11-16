using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinResult<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }

        public T Data { get; set; } = default!;
    }
}
