using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Service status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ServiceStatus>))]
    public enum ServiceStatus
    {
        /// <summary>
        /// ["<c>open</c>"] Open
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>close</c>"] Closed
        /// </summary>
        [Map("close")]
        Close,
        /// <summary>
        /// ["<c>cancelOnly</c>"] Only cancelation available
        /// </summary>
        [Map("cancelOnly")]
        CancelOnly
    }
}
