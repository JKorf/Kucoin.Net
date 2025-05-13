using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <inheritdoc />
    public class KucoinRestApiOptions : RestApiOptions
    {
        /// <summary>
        /// The broker reference name to use
        /// </summary>
        public string? BrokerName { get; set; }

        /// <summary>
        /// The private key of the broker
        /// </summary>
        public string? BrokerKey { get; set; }

        internal KucoinRestApiOptions Set(KucoinRestApiOptions targetOptions)
        {
            targetOptions = base.Set<KucoinRestApiOptions>(targetOptions);
            targetOptions.BrokerName = BrokerName;
            targetOptions.BrokerKey = BrokerKey;
            return targetOptions;
        }
    }
}
