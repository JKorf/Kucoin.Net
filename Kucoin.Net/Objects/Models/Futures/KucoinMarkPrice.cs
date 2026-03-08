namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark price
    /// </summary>
    [SerializationModel]
    public record KucoinMarkPrice: KucoinIndexBase
    {        
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
    }
}
