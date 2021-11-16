using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Service status
    /// </summary>
    public class KucoinFuturesServiceStatus
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
