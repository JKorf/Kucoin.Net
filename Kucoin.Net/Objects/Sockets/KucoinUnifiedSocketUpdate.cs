

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketUpdate<T>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;
        [JsonIgnore]
        public string? Symbol
        {
            get
            {
                var topicSplit = Topic.Split(':');
                if (topicSplit.Length == 2)
                    return topicSplit[1];

                return null;
            }
        }
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
