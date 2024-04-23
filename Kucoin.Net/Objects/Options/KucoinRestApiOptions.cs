using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <inheritdoc />
    public class KucoinRestApiOptions : RestApiOptions<KucoinApiCredentials>
    {
        /// <summary>
        /// The broker reference name to use
        /// </summary>
        public string? BrokerName { get; set; }

        /// <summary>
        /// The private key of the broker
        /// </summary>
        public string? BrokerKey { get; set; }

        internal KucoinRestApiOptions Copy()
        {
            var result = base.Copy<KucoinRestApiOptions>();
            result.BrokerKey = BrokerKey;
            result.BrokerName = BrokerName;
            return result;
        }
    }
}
