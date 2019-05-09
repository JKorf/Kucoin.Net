using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinUpdateMessage<T>
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
