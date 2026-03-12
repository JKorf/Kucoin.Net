using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin order done reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginOrderDoneReason>))]
    public enum MarginOrderDoneReason
    {
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>canceled</c>"] Cancelled
        /// </summary>
        [Map("canceled")]
        Canceled
    }
}
