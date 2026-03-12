using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Unit a quantity is denoted in
    /// </summary>
    [JsonConverter(typeof(EnumConverter<QuantityUnit>))]
    public enum QuantityUnit
    {
        /// <summary>
        /// ["<c>BASECCY</c>"] Base asset
        /// </summary>
        [Map("BASECCY")]
        BaseAsset,
        /// <summary>
        /// ["<c>QUOTECCY</c>"] Quote asset
        /// </summary>
        [Map("QUOTECCY")]
        QuoteAsset
    }
}
