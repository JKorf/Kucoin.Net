using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Cancelled order
    /// </summary>
    public class KucoinCancelledOrder
    {
        /// <summary>
        /// Order id of the cancelled order
        /// </summary>
        public string CancelledOrderId { get; set; }
        /// <summary>
        /// Client order id of the cancelled order
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; }
    }
}
