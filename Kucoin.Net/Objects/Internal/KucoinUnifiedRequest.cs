

using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Internal
{
    internal class KucoinUnifiedRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        [JsonPropertyName("symbol"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Symbol { get; set; }
        [JsonPropertyName("interval"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Interval { get; set; }
        [JsonPropertyName("depth"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Depth { get; set; }
        [JsonPropertyName("tradeType")]
        public string TradeType { get; set; }
        
        public KucoinUnifiedRequest(string id, string action, string channel, string type, string? symbol, string? interval, string? depth)
        {
            Id = id;
            Action = action;
            Channel = channel;
            Symbol = symbol;
            TradeType = type;
            Interval = interval;
            Depth = depth;
        }
    }
}
