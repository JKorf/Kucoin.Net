using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Interfaces.Clients.Rest.Spot
{
    public interface IKucoinClientSpot : IRestClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        void SetApiCredentials(string apiKey, string apiSecret, string apiPass);

        IKucoinClientSpotAccount Account { get; }
        IKucoinClientSpotExchangeData ExchangeData { get; }
        IKucoinClientSpotTrading Trading { get; }
    }
}
