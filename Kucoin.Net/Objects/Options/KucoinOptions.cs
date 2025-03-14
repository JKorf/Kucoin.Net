using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin options
    /// </summary>
    public class KucoinOptions : LibraryOptions<KucoinRestOptions, KucoinSocketOptions, ApiCredentials, KucoinEnvironment>
    {
    }
}
