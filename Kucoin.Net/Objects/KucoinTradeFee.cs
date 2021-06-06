using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Trade fee
    /// </summary>
    public class KucoinTradeFee
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Fee rate for trades as taker
        /// </summary>
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Fee rate for trades as maker
        /// </summary>
        public decimal MakerFeeRate { get; set; }
    }
}
