using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("WITHDRAW")]
        Withdrawal,
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
        /// Trade
        /// </summary>
        [Map("TRADE_EXCHANGE")]
        TradeExchange,
        /// <summary>
        /// Margin trade
        /// </summary>
        [Map("MARGIN_EXCHANGE")]
        MarginExchange,
        /// <summary>
        /// Bonus
        /// </summary>
        [Map("KUCOIN_BONUS")]
        KucoinBonus,
        /// <summary>
        /// Broker transfer
        /// </summary>
        [Map("BROKER_TRANSFER")]
        BrokerTransfer
    }
}
