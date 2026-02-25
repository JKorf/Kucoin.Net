using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin enabled mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginEnabledMode>))]
    public enum MarginEnabledMode
    {
        /// <summary>
        /// Is enabled
        /// </summary>
        [Map("1")]
        Enabled,
        /// <summary>
        /// Is not enabled
        /// </summary>
        [Map("2")]
        Disabled
    }
}
