using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Order value info
    /// </summary>
    public class KucoinOrderValuation
    {
        /// <summary>
        /// Total number of the unexecuted buy orders
        /// </summary>
        public int OpenOrderBuySize { get; set; }
        /// <summary>
        /// Total number of the unexecuted sell orders
        /// </summary>
        public int OpenOrderSellSize { get; set; }
        /// <summary>
        /// Value of all the unexecuted buy orders
        /// </summary>
        public decimal OpenOrderBuyCost { get; set; }
        /// <summary>
        /// Value of all the unexecuted sell orders
        /// </summary>
        public decimal OpenOrderSellCost { get; set; }
        /// <summary>
        /// settlement currency
        /// </summary>
        public string SettleCurrency { get; set; } = string.Empty;
    }
}
