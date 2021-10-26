using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class KucoinFuturesTrade
    {
        /// <summary>
        /// The sequence number of the trade
        /// </summary>
        public long Sequence { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The side of the trade
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonProperty("tradeId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Taker order id
        /// </summary>
        public string TakerOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Maker order id
        /// </summary>
        public string MakerOrderId { get; set; } = string.Empty;
    }
}
