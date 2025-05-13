using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// Crypto currency
        /// </summary>
        [Map("0")]
        CryptoCurrency,
        /// <summary>
        /// Fiat
        /// </summary>
        [Map("1")]
        Fiat
    }
}
