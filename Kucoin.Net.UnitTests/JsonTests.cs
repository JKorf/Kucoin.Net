using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IKucoinClient> _comparerSpot = new JsonToObjectComparer<IKucoinClient>((json) => TestHelpers.CreateResponseClient(json, new KucoinClientOptions()
        {
            ApiCredentials = new KucoinApiCredentials("1234", "1234", "12"),
            SpotApiOptions = new KucoinRestApiClientOptions { RateLimiters = new List<IRateLimiter>(), OutputOriginalData  = true },
            FuturesApiOptions = new KucoinRestApiClientOptions { RateLimiters = new List<IRateLimiter>(), OutputOriginalData = true }
        }));

        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparerSpot.ProcessSubject("Spot/Account", c => c.SpotApi.Account,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" } ,
               parametersToSetNull: new [] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            await _comparerSpot.ProcessSubject("Spot/ExchangeData", c => c.SpotApi.ExchangeData,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            await _comparerSpot.ProcessSubject("Spot/Trading", c => c.SpotApi.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>>
               {
                   { "GetUntriggeredStopOrdersAsync", new List<string> { "stop" } } ,
                   { "GetOrdersAsync", new List<string> { "stop" } } ,
                   { "GetOrderAsync", new List<string> { "stop" } } ,
                   { "GetUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetRecentUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetOrderByClientOrderIdAsync", new List<string> { "stop" } } ,
                   { "GetRecentOrdersAsync", new List<string> { "stop" } } ,
               }
                );
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            await _comparerSpot.ProcessSubject("Futures/Account", c => c.FuturesApi.Account,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> { 
                   { "GetWithdrawHistoryAsync", new List<string> { "withdrawalId" } } ,                   
               }
                );
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            await _comparerSpot.ProcessSubject("Futures/ExchangeData", c => c.FuturesApi.ExchangeData,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> {
                   { "GetAggregatedFullOrderBookAsync", new List<string> { "ts" } } ,
                   { "GetAggregatedPartialOrderBookAsync", new List<string> { "ts" } } ,
               }
                );
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            await _comparerSpot.ProcessSubject("Futures/Trading", c => c.FuturesApi.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> {
                   { "GetUntriggeredStopOrdersAsync", new List<string> { "stop" } } ,
                   { "GetOrdersAsync", new List<string> { "stop" } } ,
                   { "GetOrderAsync", new List<string> { "stop" } } ,
                   { "GetOrderByClientOrderIdAsync", new List<string> { "stop" } } ,
                   { "GetClosedOrdersAsync", new List<string> { "stop" } } ,
                   { "GetUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetRecentUserTradesAsync", new List<string> { "stop" } } ,
               }
                );
        }

    }
}
