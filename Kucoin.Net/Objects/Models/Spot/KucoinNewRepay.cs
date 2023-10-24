using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    public class KucoinNewRepay
    {
        /// <summary>
        /// Repayment order number
        /// </summary>
        [JsonProperty("orderNo")]
        public string Id { get; set; } = string.Empty;


        /// <summary>
        /// Actual borrowed amount
        /// </summary>
        [JsonProperty("actualSize")]
        public decimal Amount { get; set; }
    }
}
