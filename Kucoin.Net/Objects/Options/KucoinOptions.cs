using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin options
    /// </summary>
    public class KucoinOptions : LibraryOptions<KucoinRestOptions, KucoinSocketOptions, KucoinCredentials, KucoinEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
