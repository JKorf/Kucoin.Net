

using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinUnifiedAccountRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        public string? Depth { get; set; }
        [JsonPropertyName("accountType")]
        public string TradeType { get; set; }
        
        public KucoinUnifiedAccountRequest(string id, string action, string channel, string type)
        {
            Id = id;
            Action = action;
            Channel = channel;
            TradeType = type;
        }
    }
}
