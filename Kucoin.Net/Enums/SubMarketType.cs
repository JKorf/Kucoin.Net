using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Sub market type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubMarketType>))]
    public enum SubMarketType
    {
        /// <summary>
        /// ["<c>US.STOCK</c>"] US Stock
        /// </summary>
        [Map("US.STOCK")]
        UsStock,
        /// <summary>
        /// ["<c>KR.STOCK</c>"] KR stock
        /// </summary>
        [Map("KR.STOCK")]
        KrStock,
        /// <summary>
        /// ["<c>HK.STOCK</c>"] HK stock
        /// </summary>
        [Map("HK.STOCK")]
        HkStock,
        /// <summary>
        /// ["<c>JP.STOCK</c>"] JP stock
        /// </summary>
        [Map("JP.STOCK")]
        JpStock
    }
}
