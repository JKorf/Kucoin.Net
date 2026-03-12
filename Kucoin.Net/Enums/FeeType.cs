using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Type of fee paid
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeType>))]
    public enum FeeType
    {
        /// <summary>
        /// ["<c>makerFee</c>"] Maker fee rate
        /// </summary>
        [Map("makerFee")]
        MakerFee,
        /// <summary>
        /// ["<c>takerFee</c>"] Taker fee rate
        /// </summary>
        [Map("takerFee")]
        TakerFee
    }
}
