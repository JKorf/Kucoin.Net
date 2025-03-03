

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("topic")]
        public string Topic { get; set; }
        [JsonPropertyName("privateChannel")]
        public bool PrivateChannel { get; set; }
        [JsonPropertyName("response")]
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
