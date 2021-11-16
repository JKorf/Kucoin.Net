using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Canceled order
    /// </summary>
    public class KucoinCanceledOrder: ICommonOrderId
    {
        /// <summary>
        /// Order id of the canceled order
        /// </summary>
        
        [JsonProperty("cancelledOrderId")]
        public string CanceledOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id of the canceled order
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => CanceledOrderId;
    }
}
