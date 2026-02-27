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
        /// Margin call, risk rate 80% - 85%
        /// </summary>
        [Map("MARGIN_CALL")]
        MarginCall,
        /// <summary>
        /// Reduce only, risk rate 85% - 90%
        /// </summary>
        [Map("REDUCE_ONLY")]
        ReduceOnly,
        /// <summary>
        /// Liquidation warning, risk rate 90% - 100%
        /// </summary>
        [Map("LIQUIDATION_WARNING")]
        LiquidationWarning,
        /// <summary>
        /// Forced liquidation, risk rate 100%
        /// </summary>
        [Map("FORCE_LIQUIDATION")]
        ForceLiquidation
    }
}
