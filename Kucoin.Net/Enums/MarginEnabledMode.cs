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
        /// ["<c>1</c>"] Is enabled
        /// </summary>
        [Map("1")]
        Enabled,
        /// <summary>
        /// ["<c>2</c>"] Is not enabled
        /// </summary>
        [Map("2")]
        Disabled
    }
}
