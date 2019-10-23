using System.Collections.Generic;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Ids of cancelled orders
    /// </summary>
    public class KucoinCancelledOrders
    {
        /// <summary>
        /// List of canceled order ids
        /// </summary>
        public IEnumerable<string> CancelledOrderIds { get; set; } = new List<string>();
    }
}
