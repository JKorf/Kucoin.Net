using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{

    /// <summary>
    /// Trade type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedTradeType>))]
    public enum UnifiedTradeType
    {
        /// <summary>
        /// Normal trade
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUID")]
        Liquidation,
        /// <summary>
        /// Auto deleverage
        /// </summary>
        [Map("ADL")]
        Adl,
        /// <summary>
        /// Settlement
        /// </summary>
        [Map("SETTLEMENT")]
        Settlement,
        /// <summary>
        /// Reconciliation trade
        /// </summary>
        [Map("RECONCILIATION")]
        Reconciliation
    }
}
