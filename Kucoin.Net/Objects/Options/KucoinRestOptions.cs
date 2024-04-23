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
        public static KucoinRestOptions Default { get; set; } = new KucoinRestOptions()
        {
            Environment = KucoinEnvironment.Live
        };

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinRestApiOptions SpotOptions { get; private set; } = new KucoinRestApiOptions();

        /// <summary>
        /// Futures API options
        /// </summary>
        public KucoinRestApiOptions FuturesOptions { get; private set; } = new KucoinRestApiOptions();

        internal KucoinRestOptions Copy()
        {
            var options = Copy<KucoinRestOptions>();
            options.SpotOptions = SpotOptions.Copy();
            options.FuturesOptions = FuturesOptions.Copy();
            return options;
        }
    }
}
