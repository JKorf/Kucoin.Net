using Kucoin.Net.Converts;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Service status
    /// </summary>
    public class KucoinFuturesServerStatus
    {
        /// <summary>
        /// Service status
        /// </summary>
        [JsonConverter(typeof(ServiceStatusConverter))]
        public ServiceStatus Status { get; set; }
        /// <summary>
        /// Info
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
