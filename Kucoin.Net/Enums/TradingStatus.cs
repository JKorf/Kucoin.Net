using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Trading status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradingStatus>))]
    public enum TradingStatus
    {
        /// <summary>
        /// Not enabled
        /// </summary>
        [Map("0")]
        NotEnabled,
        /// <summary>
        /// Currently trading
        /// </summary>
        [Map("1")]
        Trading,
        /// <summary>
        /// [Futures] Settling
        /// </summary>
        [Map("2")]
        Settling,
        /// <summary>
        /// [Futures] Settled
        /// </summary>
        [Map("3")]
        Settled,
        /// <summary>
        /// [Futures] Paused
        /// </summary>
        [Map("4")]
        Paused
    }
}
