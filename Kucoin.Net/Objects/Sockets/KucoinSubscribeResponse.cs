using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSubscribeResponse
    {
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
