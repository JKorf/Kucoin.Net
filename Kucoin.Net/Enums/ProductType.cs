using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public enum ProductType
    {
        /// <summary>
        /// ["<c>SPOT</c>"] Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// ["<c>CROSS</c>"] Cross margin
        /// </summary>
        [Map("CROSS")]
        CrossMargin,
        /// <summary>
        /// ["<c>ISOLATED</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        IsolatedMargin
    }
}
