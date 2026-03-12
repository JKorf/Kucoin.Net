using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Biz type filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BizTypeFilter>))]
    public enum BizTypeFilter
    {
        /// <summary>
        /// ["<c>DEPOSIT</c>"] Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// ["<c>WITHDRAW</c>"] Withdrawal
        /// </summary>
        [Map("WITHDRAW")]
        Withdrawal,
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
        /// ["<c>TRADE_EXCHANGE</c>"] Trade
        /// </summary>
        [Map("TRADE_EXCHANGE")]
        TradeExchange,
        /// <summary>
        /// ["<c>MARGIN_EXCHANGE</c>"] Margin trade
        /// </summary>
        [Map("MARGIN_EXCHANGE")]
        MarginExchange,
        /// <summary>
        /// ["<c>KUCOIN_BONUS</c>"] Bonus
        /// </summary>
        [Map("KUCOIN_BONUS")]
        KucoinBonus,
        /// <summary>
        /// ["<c>BROKER_TRANSFER</c>"] Broker transfer
        /// </summary>
        [Map("BROKER_TRANSFER")]
        BrokerTransfer
    }
}
