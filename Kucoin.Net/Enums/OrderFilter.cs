using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderFilter>))]
    public enum OrderFilter
    {
        /// <summary>
        /// ["<c>NORMAL</c>"] Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// ["<c>ADVANCED</c>"] Advanced
        /// </summary>
        [Map("ADVANCED")]
        Advanced
    }
}
