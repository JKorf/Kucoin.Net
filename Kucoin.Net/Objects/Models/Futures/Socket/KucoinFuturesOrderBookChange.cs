using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Order book change
    /// </summary>
    public class KucoinFuturesOrderBookChange
    {
        /// <summary>
        /// Sequence number
        /// </summary>
        public long Sequence { get; set; }
        [JsonProperty("change")]
        internal string Change { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price => decimal.Parse(Change.Split(',')[0]);
        /// <summary>
        /// Side
        /// </summary>
        public OrderSide Side => Change.Split(',')[1] == "sell" ? OrderSide.Sell : OrderSide.Buy;
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity => decimal.Parse(Change.Split(',')[2]);
    }
}
