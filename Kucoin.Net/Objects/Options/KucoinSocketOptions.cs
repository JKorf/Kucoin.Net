using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin socket client options
    /// </summary>
    public class KucoinSocketOptions : SocketExchangeOptions<KucoinEnvironment, KucoinApiCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static KucoinSocketOptions Default { get; set; } = new KucoinSocketOptions()
        {
            Environment = KucoinEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions<KucoinApiCredentials> SpotOptions { get; private set; } = new SocketApiOptions<KucoinApiCredentials>()
        {
            MaxSocketConnections = 150
        };

        /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions<KucoinApiCredentials> FuturesOptions { get; private set; } = new SocketApiOptions<KucoinApiCredentials>()
        {
            MaxSocketConnections = 50
        };

        internal KucoinSocketOptions Set(KucoinSocketOptions targetOptions)
        {
            targetOptions = base.Set<KucoinSocketOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            return targetOptions;
        }
    }
}
