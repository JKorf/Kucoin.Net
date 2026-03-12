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
        /// ["<c>TRADE_EXCHANGE</c>"] Trade
        /// </summary>
        [Map("TRADE_EXCHANGE")]
        TradeExchange,
        /// <summary>
        /// ["<c>TRANSFER</c>"] Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// ["<c>SUB_TRANSFER</c>"] Subaccount transfer
        /// </summary>
        [Map("SUB_TRANSFER")]
        SubTransfer,
        /// <summary>
        /// ["<c>RETURNED_FEES</c>"] Returned fees
        /// </summary>
        [Map("RETURNED_FEES")]
        ReturnedFees,
        /// <summary>
        /// ["<c>DEDUCTION_FEES</c>"] Deduction fees
        /// </summary>
        [Map("DEDUCTION_FEES")]
        DeductionFees,
        /// <summary>
        /// ["<c>OTHER</c>"] Other
        /// </summary>
        [Map("OTHER")]
        Other,
        /// <summary>
        /// ["<c>SUB_TO_SUB_TRANSFER</c>"] Subaccount transfer
        /// </summary>
        [Map("SUB_TO_SUB_TRANSFER")]
        SubToSubTransfer,
        /// <summary>
        /// ["<c>SPOT_EXCHANGE</c>"] Spot exchange
        /// </summary>
        [Map("SPOT_EXCHANGE")]
        SpotExchange,
        /// <summary>
        /// ["<c>SPOT_EXCHANGE_REBATE</c>"] Spot exchange rebate
        /// </summary>
        [Map("SPOT_EXCHANGE_REBATE")]
        SpotExchangeRebate,
        /// <summary>
        /// ["<c>FUTURES_EXCHANGE_OPEN</c>"] Futures exchange open
        /// </summary>
        [Map("FUTURES_EXCHANGE_OPEN")]
        FuturesExchangeOpen,
        /// <summary>
        /// ["<c>FUTURES_EXCHANGE_CLOSE</c>"] Futures exchange close
        /// </summary>
        [Map("FUTURES_EXCHANGE_CLOSE")]
        FuturesExchangeClose,
        /// <summary>
        /// ["<c>FUTURES_EXCHANGE_REBATE</c>"] Futures exchange rebate
        /// </summary>
        [Map("FUTURES_EXCHANGE_REBATE")]
        FuturesExchangeRebate,
        /// <summary>
        /// ["<c>FUNDING_FEE</c>"] Funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// ["<c>LIABILITY_INTEREST</c>"] Liability interest
        /// </summary>
        [Map("LIABILITY_INTEREST")]
        LiabilityInterest,
        /// <summary>
        /// ["<c>KCS_DEDUCTION_FEES</c>"] KCS deduction fees
        /// </summary>
        [Map("KCS_DEDUCTION_FEES")]
        KcsDeductionFees,
        /// <summary>
        /// ["<c>KCS_RETURNED_FEES</c>"] KCS returned fees
        /// </summary>
        [Map("KCS_RETURNED_FEES")]
        KcsReturnedFees,
        /// <summary>
        /// ["<c>AUTO_EXCHANGE_USER</c>"] Auto exchange user
        /// </summary>
        [Map("AUTO_EXCHANGE_USER")]
        AutoExchangeUser
    }
}
