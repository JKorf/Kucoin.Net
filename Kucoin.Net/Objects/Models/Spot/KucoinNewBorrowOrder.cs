using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    public class KucoinNewBorrowOrder : ICommonOrderId
    {
        /// <summary>
        /// The id of the new borrow order
        /// </summary>
        [JsonProperty("orderId")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Currency of current Order
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => Id;
    }
}
