using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Status of a deposit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// ["<c>PROCESSING</c>"] In progress
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILURE</c>"] Failed
        /// </summary>
        [Map("FAILURE")]
        Failure
    }
}
