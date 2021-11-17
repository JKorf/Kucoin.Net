using Kucoin.Net.Objects;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;

namespace Kucoin.Net.Clients.Rest.Futures
{
    public class KucoinClientFutures: KucoinBaseClient, IKucoinClientFutures
    {
        public IKucoinClientFuturesAccount Account { get; }

        public IKucoinClientFuturesExchangeData ExchangeData { get; }

        public IKucoinClientFuturesTrading Trading { get; }

        public KucoinClientFutures() : this(KucoinClientFuturesOptions.Default)
        {

        }

        public KucoinClientFutures(KucoinClientFuturesOptions options) : base("Kucoin[Futures]", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
            Account = new KucoinClientFuturesAccount(this);
            ExchangeData = new KucoinClientFuturesExchangeData(this);
            Trading = new KucoinClientFuturesTrading(this);
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string apiPass)
        {
            SetAuthenticationProvider(new KucoinAuthenticationProvider(new KucoinApiCredentials(apiKey, apiSecret, apiPass)));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(KucoinClientFuturesOptions options)
        {
            KucoinClientFuturesOptions.Default = options;
        }
    }
}
