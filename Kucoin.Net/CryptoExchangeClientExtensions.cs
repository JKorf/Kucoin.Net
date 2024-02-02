using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Net.Clients
{
    public static class CryptoExchangeClientExtensions
    {
        public static IKucoinRestClient Kucoin(this ICryptoExchangeClient baseClient) => baseClient.TryGet<IKucoinRestClient>() ?? new KucoinRestClient();
    }
}
