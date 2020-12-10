using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Order info
    /// </summary>
    public class KucoinOrder: ICommonOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// the symbol of the order
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The operation type
        /// </summary>
        [JsonProperty("opType"), JsonConverter(typeof(OperationTypeConverter))]
        public KucoinOrderOperationType OperationType { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType Type { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public KucoinOrderSide Side { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
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
        public string FeeCurrency { get; set; } = "";
        /// <summary>
        /// The self trade prevention type
        /// </summary>
        [JsonProperty("stp"), JsonConverter(typeof(SelfTradePreventionConverter))]
        public KucoinSelfTradePrevention? SelfTradePrevention { get; set; }
        /// <summary>
        /// The stop condition
        /// </summary>
        [JsonConverter(typeof(StopConditionConverter))]
        public KucoinStopCondition Stop { get; set; }
        /// <summary>
        /// Whether the stop condition is triggered
        /// </summary>
        public bool StopTriggered { get; set; }
        /// <summary>
        /// The stop price
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The time in force of the order
        /// </summary>
        [JsonConverter(typeof(TimeInForceConverter))]
        public KucoinTimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Whether the order is post only
        /// </summary>
        public bool PostOnly { get; set; }
        /// <summary>
        /// Whether the order is hidden
        /// </summary>
        public bool Hidden { get; set; }
        /// <summary>
        /// Whether it is an iceberg order
        /// </summary>
        public bool Iceberg { get; set; }
        /// <summary>
        /// The max visible size of the iceberg
        /// </summary>
        [JsonProperty("visibleSize")]
        public decimal? VisibleIcebergSize { get; set; }
        /// <summary>
        /// Time after which the order is cancelled
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CancelAfter { get; set; }
        /// <summary>
        /// The source of the order
        /// </summary>
        public string Channel { get; set; } = "";
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = "";
        /// <summary>
        /// Remark for the order
        /// </summary>
        [JsonOptionalProperty]
        public string Remark { get; set; } = "";
        /// <summary>
        /// Tags for the order
        /// </summary>
        [JsonOptionalProperty]
        public string Tags { get; set; } = "";
        /// <summary>
        /// Whether the order is active
        /// </summary>
        [JsonOptionalProperty]
        public bool? IsActive { get; set; }
        /// <summary>
        /// If there is a cancel request for this order
        /// </summary>
        public bool CancelExist { get; set; }
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }

        string ICommonOrderId.CommonId => Id;
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price ?? 0;
        decimal ICommonOrder.CommonQuantity => Quantity;
        string ICommonOrder.CommonStatus => IsActive == true ? "Open" : "-"; // TODO
        bool ICommonOrder.IsActive => IsActive ?? false;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Side == KucoinOrderSide.Sell
            ? IExchangeClient.OrderSide.Sell
            : IExchangeClient.OrderSide.Buy;

        IExchangeClient.OrderType ICommonOrder.CommonType =>
            Type == KucoinOrderType.Limit || Type == KucoinOrderType.LimitStop
                ? IExchangeClient.OrderType.Limit
                : IExchangeClient.OrderType.Market;
    }
}
