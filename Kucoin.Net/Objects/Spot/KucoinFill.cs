using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Fill info
    /// </summary>
    public class KucoinFill: KucoinTradeBase, ICommonTrade
    {        
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType Type { get; set; }        
        /// <summary>
        /// The funds of the fill
        /// </summary>
        public decimal Funds { get; set; }        
        /// <summary>
        /// The stop condition of the fill
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public KucoinStopCondition Stop { get; set; }
        /// <summary>
        /// The id of the counter order
        /// </summary>
        public string CounterOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Was forced to become taker
        /// </summary>
        public bool ForceTaker { get; set; }

        string ICommonTrade.CommonId => TradeId;
        decimal ICommonTrade.CommonPrice => Price;
        decimal ICommonTrade.CommonQuantity => Quantity;
        decimal ICommonTrade.CommonFee => Fee;
        string? ICommonTrade.CommonFeeAsset => FeeCurrency;
        DateTime ICommonTrade.CommonTradeTime => CreatedAt;
    }
}
