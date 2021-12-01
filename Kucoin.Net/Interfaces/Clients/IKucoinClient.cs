using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;

namespace Kucoin.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Client for accessing the Kucoin Spot API. 
    /// </summary>
    public interface IKucoinClient : IRestClient
    {
        IKucoinClientSpotMarket SpotApi { get; }
        IKucoinClientFuturesMarket FuturesApi { get; }
    }
}
