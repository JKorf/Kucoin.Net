using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Interfaces.Clients.Rest.Futures
{
    /// <summary>
    /// Client for accessing the Kucoin Futures API. 
    /// </summary>
    public interface IKucoinClientFutures : IRestClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        void SetApiCredentials(string apiKey, string apiSecret, string apiPass);

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        IKucoinClientFuturesAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        IKucoinClientFuturesExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        IKucoinClientFuturesTrading Trading { get; }
    }
}
