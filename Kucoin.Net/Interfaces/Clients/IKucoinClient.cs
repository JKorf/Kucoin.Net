using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Kucoin Spot API. 
    /// </summary>
    public interface IKucoinClient : IRestClient
    {
        IKucoinClientSpotApi SpotApi { get; }
        IKucoinClientFuturesApi FuturesApi { get; }
    }
}
