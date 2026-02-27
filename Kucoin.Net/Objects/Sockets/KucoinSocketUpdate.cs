

using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinUnifiedSocketUpdate<T>
    {
        [JsonPropertyName("T")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("P")]
        public DateTime PushTime { get; set; }
        [JsonPropertyName("dp")]
        public string? Depth { get; set; }
        [JsonPropertyName("t")]
        public string? PushType { get; set; }
        [JsonPropertyName("d")]
        public T Data { get; set; } = default!;
    }
}
