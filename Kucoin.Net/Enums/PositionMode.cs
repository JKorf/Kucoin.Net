using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// ["<c>0</c>"] One way position mode
        /// </summary>
        [Map("0")]
        OneWay,
        /// <summary>
        /// ["<c>1</c>"] Hedge mode
        /// </summary>
        [Map("1")]
        HedgeMode
    }
}
