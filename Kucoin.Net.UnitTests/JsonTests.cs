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
        private JsonToObjectComparer<IKucoinRestClient> _comparerSpot = new JsonToObjectComparer<IKucoinRestClient>((json) => TestHelpers.CreateResponseClient(json, x =>
        {
            x.ApiCredentials = new KucoinApiCredentials("1234", "1234", "12");
            x.SpotOptions.OutputOriginalData = false;
            x.RateLimiterEnabled = false;
            x.FuturesOptions.OutputOriginalData = false;
        }));

        [Test]
        public async Task ValidateSpotMarginCalls()
        {
            await _comparerSpot.ProcessSubject("Spot/Margin", c => c.SpotApi.Margin,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize" }
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
