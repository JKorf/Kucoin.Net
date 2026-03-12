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
        /// ["<c>NORMAL</c>"] Normal trade
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// ["<c>LIQUID</c>"] Liquidation
        /// </summary>
        [Map("LIQUID")]
        Liquidation,
        /// <summary>
        /// ["<c>ADL</c>"] Auto deleverage
        /// </summary>
        [Map("ADL")]
        Adl,
        /// <summary>
        /// ["<c>SETTLEMENT</c>"] Settlement
        /// </summary>
        [Map("SETTLEMENT")]
        Settlement,
        /// <summary>
        /// ["<c>RECONCILIATION</c>"] Reconciliation trade
        /// </summary>
        [Map("RECONCILIATION")]
        Reconciliation
    }
}
