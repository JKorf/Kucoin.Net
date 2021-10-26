using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Futures.Socket
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
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
