using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin Rest client options
    /// </summary>
    public class KucoinRestOptions : RestExchangeOptions<KucoinEnvironment, KucoinApiCredentials>
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

        internal KucoinRestOptions Set(KucoinRestOptions targetOptions)
        {
            targetOptions = base.Set<KucoinRestOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            return targetOptions;
        }
    }
}
