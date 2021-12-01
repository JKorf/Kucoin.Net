using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Interfaces.Clients
{
    public interface IKucoinSocketClient : ISocketClient
    {
        IKucoinSocketClientSpotStreams  SpotStreams { get; }
        IKucoinSocketClientFuturesStreams  FuturesStreams { get; }
    }
}
