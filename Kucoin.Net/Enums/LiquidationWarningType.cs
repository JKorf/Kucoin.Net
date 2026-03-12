using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Liquidation warning type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LiquidationWarningType>))]
    public enum LiquidationWarningType
    {
        /// <summary>
        /// ["<c>MARGIN_CALL</c>"] Margin call, risk rate 80% - 85%
        /// </summary>
        [Map("MARGIN_CALL")]
        MarginCall,
        /// <summary>
        /// ["<c>REDUCE_ONLY</c>"] Reduce only, risk rate 85% - 90%
        /// </summary>
        [Map("REDUCE_ONLY")]
        ReduceOnly,
        /// <summary>
        /// ["<c>LIQUIDATION_WARNING</c>"] Liquidation warning, risk rate 90% - 100%
        /// </summary>
        [Map("LIQUIDATION_WARNING")]
        LiquidationWarning,
        /// <summary>
        /// ["<c>FORCE_LIQUIDATION</c>"] Forced liquidation, risk rate 100%
        /// </summary>
        [Map("FORCE_LIQUIDATION")]
        ForceLiquidation
    }
}
