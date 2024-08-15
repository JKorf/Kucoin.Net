using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    public interface IKucoinRestClientSpotApiShared :
        ITickerRestClient,
        ISpotSymbolRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        IBalanceRestClient,
        ISpotOrderRestClient
    {
    }
}
