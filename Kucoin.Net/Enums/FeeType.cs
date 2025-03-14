using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Maker fee rate
        /// </summary>
        [Map("makerFee")]
        MakerFee,
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [Map("takerFee")]
        TakerFee
    }
}
