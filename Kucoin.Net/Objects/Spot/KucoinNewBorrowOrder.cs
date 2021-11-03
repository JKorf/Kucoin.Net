using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    public class KucoinNewBorrowOrder : ICommonOrderId
    {
        /// <summary>
        /// The id of the new borrow order
        /// </summary>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Currency of current Order
        /// </summary>
        [JsonProperty("Currency")]
        public string Asset { get; set; } = string.Empty;

        string ICommonOrderId.CommonId => OrderId;
    }
}
