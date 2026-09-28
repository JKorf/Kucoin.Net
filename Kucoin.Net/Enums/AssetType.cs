using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Asset class
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetClass>))]
    public enum AssetClass
    {
        /// <summary>
        /// ["<c>CRYPTO</c>"] Crypto currency
        /// </summary>
        [Map("CRYPTO")]
        Crypto,
        /// <summary>
        /// ["<c>METAL</c>"] Metal
        /// </summary>
        [Map("METAL")]
        Metal,
        /// <summary>
        /// ["<c>COMMODITY</c>"] Commodity
        /// </summary>
        [Map("COMMODITY")]
        Commodity,
        /// <summary>
        /// ["<c>STOCK</c>"] Stock
        /// </summary>
        [Map("STOCK")]
        Stock
    }
}
