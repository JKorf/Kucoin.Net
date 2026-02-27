using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedBusinessType>))]
    public enum UnifiedBusinessType
    {
        /// <summary>
        /// Trade
        /// </summary>
        [Map("TRADE_EXCHANGE")]
        TradeExchange,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// Subaccount transfer
        /// </summary>
        [Map("SUB_TRANSFER")]
        SubTransfer,
        /// <summary>
        /// Returned fees
        /// </summary>
        [Map("RETURNED_FEES")]
        ReturnedFees,
        /// <summary>
        /// Deduction fees
        /// </summary>
        [Map("DEDUCTION_FEES")]
        DeductionFees,
        /// <summary>
        /// Other
        /// </summary>
        [Map("OTHER")]
        Other,
        /// <summary>
        /// Subaccount transfer
        /// </summary>
        [Map("SUB_TO_SUB_TRANSFER")]
        SubToSubTransfer,
        /// <summary>
        /// Spot exchange
        /// </summary>
        [Map("SPOT_EXCHANGE")]
        SpotExchange,
        /// <summary>
        /// Spot exchange rebate
        /// </summary>
        [Map("SPOT_EXCHANGE_REBATE")]
        SpotExchangeRebate,
        /// <summary>
        /// Futures exchange open
        /// </summary>
        [Map("FUTURES_EXCHANGE_OPEN")]
        FuturesExchangeOpen,
        /// <summary>
        /// Futures exchange close
        /// </summary>
        [Map("FUTURES_EXCHANGE_CLOSE")]
        FuturesExchangeClose,
        /// <summary>
        /// Futures exchange rebate
        /// </summary>
        [Map("FUTURES_EXCHANGE_REBATE")]
        FuturesExchangeRebate,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// Liability interest
        /// </summary>
        [Map("LIABILITY_INTEREST")]
        LiabilityInterest,
        /// <summary>
        /// KCS deduction fees
        /// </summary>
        [Map("KCS_DEDUCTION_FEES")]
        KcsDeductionFees,
        /// <summary>
        /// KCS returned fees
        /// </summary>
        [Map("KCS_RETURNED_FEES")]
        KcsReturnedFees,
        /// <summary>
        /// Auto exchange user
        /// </summary>
        [Map("AUTO_EXCHANGE_USER")]
        AutoExchangeUser
    }
}
