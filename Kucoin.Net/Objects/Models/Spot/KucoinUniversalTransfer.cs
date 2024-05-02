using Newtonsoft.Json;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Universal transfer
    /// </summary>
    public record KucoinUniversalTransfer
    {
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
        public TransferType Type { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// From account
        /// </summary>
        [JsonProperty("fromAccountType"), JsonConverter(typeof(EnumConverter))]
        public AccountType FromAccountType { get; set; }
        /// <summary>
        /// To account
        /// </summary>
        [JsonProperty("toAccountType"), JsonConverter(typeof(EnumConverter))]
        public AccountType ToAccountType { get; set; }
    }
}
