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
        /// ["<c>TradingDisabled</c>"] [Spot] Trading disabled
        /// </summary>
        [Map("TradingDisabled")]
        TradingDisabled,
        /// <summary>
        /// ["<c>TradingEnabled</c>"] [Spot] Trading enabled
        /// </summary>
        [Map("TradingEnabled")]
        TradingEnabled,


        /// <summary>
        /// ["<c>Init</c>"] [Futures] Init
        /// </summary>
        [Map("Init")]
        Init,
        /// <summary>
        /// ["<c>Settled</c>"] [Futures] Settled
        /// </summary>
        [Map("Settled")]
        Settled,
        /// <summary>
        /// ["<c>Paused</c>"] [Futures] Paused
        /// </summary>
        [Map("Paused")]
        Paused,
        /// <summary>
        /// ["<c>Open</c>"] [Futures] Open
        /// </summary>
        [Map("Open")]
        Open,
        /// <summary>
        /// ["<c>PrepareSettled</c>"] [Futures] Preparing settlement
        /// </summary>
        [Map("PrepareSettled")]
        PrepareSettled,
        /// <summary>
        /// ["<c>BeingSettled</c>"] [Futures] Being settled
        /// </summary>
        [Map("BeingSettled")]
        BeingSettled,
        /// <summary>
        /// ["<c>Closed</c>"] [Futures] Closed
        /// </summary>
        [Map("Closed")]
        Closed,
        /// <summary>
        /// ["<c>CancelOnly</c>"] [Futures] CancelOnly
        /// </summary>
        [Map("CancelOnly")]
        CancelOnly,
    }
}
