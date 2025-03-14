using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Objects.Models.Spot;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order result
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesOrderResult: KucoinOrderId
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Result code, 200000 is success
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Result message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
