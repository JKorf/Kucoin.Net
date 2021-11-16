using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ids of cancelled orders
    /// </summary>
    public class KucoinCanceledOrders: ICommonOrderId
    {
        /// <summary>
        /// List of canceled order ids
        /// </summary>
        public IEnumerable<string> CancelledOrderIds { get; set; } = Array.Empty<string>();

        string ICommonOrderId.CommonId => CancelledOrderIds.FirstOrDefault();
    }
}
