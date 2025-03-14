using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Order value info
    /// </summary>
    [SerializationModel]
    public record KucoinOrderValuation
    {
        /// <summary>
        /// Total number of the unexecuted buy orders
        /// </summary>
        [JsonPropertyName("openOrderBuySize")]
        public int OpenOrderBuySize { get; set; }
        /// <summary>
        /// Total number of the unexecuted sell orders
        /// </summary>
        [JsonPropertyName("openOrderSellSize")]
        public int OpenOrderSellSize { get; set; }
        /// <summary>
        /// Value of all the unexecuted buy orders
        /// </summary>
        [JsonPropertyName("openOrderBuyCost")]
        public decimal OpenOrderBuyCost { get; set; }
        /// <summary>
        /// Value of all the unexecuted sell orders
        /// </summary>
        [JsonPropertyName("openOrderSellCost")]
        public decimal OpenOrderSellCost { get; set; }
        /// <summary>
        /// settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
