using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Cancelled order
    /// </summary>
    public class KucoinCancelledOrder: ICommonOrderId
    {
        /// <summary>
        /// Order id of the cancelled order
        /// </summary>
        public string CancelledOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id of the cancelled order
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => CancelledOrderId;
    }
}
