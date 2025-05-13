using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transaction info
    /// </summary>
    [SerializationModel]
    public record KucoinTransactionVolume
    {
        /// <summary>
        /// Transaction volume in last 24h
        /// </summary>
        [JsonPropertyName("turnoverOf24h")]
        public decimal Turnover { get; set; }
    }
}
