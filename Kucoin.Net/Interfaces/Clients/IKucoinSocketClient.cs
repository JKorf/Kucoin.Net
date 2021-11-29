using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Socket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Interfaces.Clients
{
    public interface IKucoinSocketClient : ISocketClient
    {
        IKucoinSocketClientSpotMarket SpotMarket { get; }
        IKucoinSocketClientFuturesMarket FuturesMarket { get; }
    }
}
