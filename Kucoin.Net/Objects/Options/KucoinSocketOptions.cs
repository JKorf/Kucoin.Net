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
        public static KucoinSocketOptions Default { get; set; } = new KucoinSocketOptions()
        {
            Environment = KucoinEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions<KucoinApiCredentials> SpotOptions { get; private set; } = new SocketApiOptions<KucoinApiCredentials>()
        {
            MaxSocketConnections = 50
        };

        /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions<KucoinApiCredentials> FuturesOptions { get; private set; } = new SocketApiOptions<KucoinApiCredentials>()
        {
            MaxSocketConnections = 50
        };

        internal KucoinSocketOptions Copy()
        {
            var options = Copy<KucoinSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions<KucoinApiCredentials>>();
            options.FuturesOptions = FuturesOptions.Copy<SocketApiOptions<KucoinApiCredentials>>();
            return options;
        }
    }
}
