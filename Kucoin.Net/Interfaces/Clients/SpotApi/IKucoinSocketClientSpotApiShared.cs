using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    public interface IKucoinSocketClientSpotApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient
    {
    }
}
