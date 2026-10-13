using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Objects.Options
{
    /// <inheritdoc />
    public class KucoinSharedApiOptions : SharedApiOptions
    {
        /// <summary>
        /// The API version to use for Shared API resolution
        /// </summary>
        public KucoinApiVersion ApiVersion { get; set; } = KucoinApiVersion.Classic;
    }

    /// <summary>
    /// API version for the Kucoin API
    /// </summary>
    public enum KucoinApiVersion
    {
        /// <summary>
        /// Classic Spot and Futures API
        /// </summary>
        Classic,
        /// <summary>
        /// Unified API
        /// </summary>
        Unified
    }
}
