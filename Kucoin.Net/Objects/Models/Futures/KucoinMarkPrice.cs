namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark price
    /// </summary>
    public record KucoinMarkPrice: KucoinIndexBase
    {        
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
    }
}
