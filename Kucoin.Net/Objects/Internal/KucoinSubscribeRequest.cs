using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("privateChannel")]
        public bool PrivateChannel { get; set; }
        [JsonProperty("response")]
        public bool Response { get; set; }
        
        public KucoinRequest(string id, string type, string topic, bool userEvents)
        {
            Id = id;
            Topic = topic;
            Type = type;
            Response = true;
            PrivateChannel = userEvents;
        }
    }
}
