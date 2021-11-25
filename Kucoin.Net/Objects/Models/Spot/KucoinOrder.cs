using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
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
        public OrderOperationType? OperationType { get; set; }        
        /// <summary>
        /// The funds of the order
        /// </summary>
        [JsonProperty("funds")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// The funds of the deal
        /// </summary>
        [JsonProperty("dealFunds")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// The quantity of the deal
        /// </summary>
        [JsonProperty("dealSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The fee of the order
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset of the fee
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The stop condition
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public StopCondition Stop { get; set; }        
        /// <summary>
        /// Time after which the order is canceled
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CancelAfter { get; set; }
        /// <summary>
        /// The source of the order
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// Tags for the order
        /// </summary>
        public string Tags { get; set; } = string.Empty;        
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(TradeTypeConverter))]
        public TradeType TradeType { get; set; }


        string ICommonOrderId.CommonId => Id;
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price ?? 0;
        decimal ICommonOrder.CommonQuantity => Quantity ?? 0;
        IExchangeClient.OrderStatus ICommonOrder.CommonStatus {
            get
            {
                if (IsActive == null)
                    return IExchangeClient.OrderStatus.Active;

                return !IsActive.Value && QuantityFilled != Quantity ? IExchangeClient.OrderStatus.Canceled : !IsActive.Value ? IExchangeClient.OrderStatus.Filled :
                IExchangeClient.OrderStatus.Active;
            }
        }
        
        bool ICommonOrder.IsActive => IsActive ?? false;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Side == OrderSide.Sell
            ? IExchangeClient.OrderSide.Sell
            : IExchangeClient.OrderSide.Buy;

        IExchangeClient.OrderType ICommonOrder.CommonType =>
            Type == OrderType.Limit || Type == OrderType.LimitStop
                ? IExchangeClient.OrderType.Limit
                : IExchangeClient.OrderType.Market;

        DateTime ICommonOrder.CommonOrderTime => CreateTime;
    }

    /// <summary>
    /// Stop order info
    /// </summary>
    public class KucoinStopOrder: KucoinOrder
    {
        /// <summary>
        /// User id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(StopOrderStatusConverter))]
        public StopOrderStatus Status { get; set; }
        /// <summary>
        /// Time after which the order is canceled
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Domain id
        /// </summary>
        public string DomainId { get; set; } = string.Empty;
        /// <summary>
        /// Trade source
        /// </summary>
        public string TradeSource { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee rate
        /// </summary>
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Time stop order was triggered
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? StopTriggerTime { get; set; }
    }
}
