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
        /// ["<c>0</c>"] Not enabled
        /// </summary>
        [Map("0")]
        NotEnabled,
        /// <summary>
        /// ["<c>1</c>"] Currently trading
        /// </summary>
        [Map("1")]
        Trading,
        /// <summary>
        /// ["<c>2</c>"] [Futures] Settling
        /// </summary>
        [Map("2")]
        Settling,
        /// <summary>
        /// ["<c>3</c>"] [Futures] Settled
        /// </summary>
        [Map("3")]
        Settled,
        /// <summary>
        /// ["<c>4</c>"] [Futures] Paused
        /// </summary>
        [Map("4")]
        Paused
    }
}
