using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin Rest client options
    /// </summary>
    public class KucoinRestOptions : RestExchangeOptions<KucoinEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static KucoinRestOptions Default { get; set; } = new KucoinRestOptions()
        {
            Environment = KucoinEnvironment.Live
        };

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinRestApiOptions SpotOptions { get; private set; } = new KucoinRestApiOptions();

        /// <summary>
        /// Futures API options
        /// </summary>
        public KucoinRestApiOptions FuturesOptions { get; private set; } = new KucoinRestApiOptions();

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinRestApiOptions UnifiedOptions { get; private set; } = new KucoinRestApiOptions();

        internal KucoinRestOptions Set(KucoinRestOptions targetOptions)
        {
            targetOptions = base.Set<KucoinRestOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.UnifiedOptions = FuturesOptions.Set(targetOptions.UnifiedOptions);
            return targetOptions;
        }
    }
}
