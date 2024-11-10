using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ids of cancelled orders
    /// </summary>
    public record KucoinCanceledOrders
    {
        /// <summary>
        /// List of canceled order ids
        /// </summary>
        public IEnumerable<string> CancelledOrderIds { get; set; } = Array.Empty<string>();
    }
}
