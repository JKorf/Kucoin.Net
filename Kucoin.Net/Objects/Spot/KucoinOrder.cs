using System;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Order info
    /// </summary>
    public class KucoinOrder: KucoinOrderBase, ICommonOrder
    {        
        /// <summary>
        /// The operation type
        /// </summary>
        [JsonProperty("opType"), JsonConverter(typeof(OperationTypeConverter))]
        public KucoinOrderOperationType? OperationType { get; set; }        
        /// <summary>
        /// The funds of the order
        /// </summary>
        public decimal? Funds { get; set; }
        /// <summary>
        /// The funds of the deal
        /// </summary>
        public decimal? DealFunds { get; set; }
        /// <summary>
        /// The quantity of the deal
        /// </summary>
        [JsonProperty("dealSize")]
        public decimal DealQuantity { get; set; }
        /// <summary>
        /// The fee of the order
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The currency of the fee
        /// </summary>
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The stop condition
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public KucoinStopCondition Stop { get; set; }        
        /// <summary>
        /// Time after which the order is cancelled
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CancelAfter { get; set; }
        /// <summary>
        /// The source of the order
        /// </summary>
        public string Channel { get; set; } = string.Empty;
                /// <summary>
        /// Tags for the order
        /// </summary>
        [JsonOptionalProperty]
        public string Tags { get; set; } = string.Empty;        
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(TradeTypeConverter))]
        public KucoinTradeType TradeType { get; set; }


        string ICommonOrderId.CommonId => Id;
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price ?? 0;
        decimal ICommonOrder.CommonQuantity => Quantity ?? 0;
        IExchangeClient.OrderStatus ICommonOrder.CommonStatus {
            get
            {
                if (IsActive == null)
                    return IExchangeClient.OrderStatus.Active;

                return !IsActive.Value && DealQuantity != Quantity ? IExchangeClient.OrderStatus.Canceled : !IsActive.Value ? IExchangeClient.OrderStatus.Filled :
                IExchangeClient.OrderStatus.Active;
            }
        }
        
        bool ICommonOrder.IsActive => IsActive ?? false;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Side == KucoinOrderSide.Sell
            ? IExchangeClient.OrderSide.Sell
            : IExchangeClient.OrderSide.Buy;

        IExchangeClient.OrderType ICommonOrder.CommonType =>
            Type == KucoinOrderType.Limit || Type == KucoinOrderType.LimitStop
                ? IExchangeClient.OrderType.Limit
                : IExchangeClient.OrderType.Market;

        DateTime ICommonOrder.CommonOrderTime => CreatedAt;
    }
}
