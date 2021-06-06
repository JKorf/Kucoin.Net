using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// User fee
    /// </summary>
    public class KucoinUserFee
    {
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
