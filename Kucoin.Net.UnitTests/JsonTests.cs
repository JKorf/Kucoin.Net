using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IKucoinClientSpot> _comparerSpot = new JsonToObjectComparer<IKucoinClientSpot>((json) => TestHelpers.CreateResponseClient(json, new KucoinClientSpotOptions()
        { ApiCredentials = new KucoinApiCredentials("1234", "1234", "12"),  OutputOriginalData = true, RateLimiters = new List<IRateLimiter>() }));

        private JsonToObjectComparer<IKucoinClientFutures> _comparerFutures = new JsonToObjectComparer<IKucoinClientFutures>((json) => TestHelpers.CreateResponseClientFutures(json, new KucoinClientFuturesOptions()
        { ApiCredentials = new KucoinApiCredentials("1234", "1234", "12"), OutputOriginalData = true, RateLimiters = new List<IRateLimiter>() }));

        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparerSpot.ProcessSubject("Spot/Account", c => c.Account,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" } ,
               parametersToSetNull: new [] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            await _comparerSpot.ProcessSubject("Spot/ExchangeData", c => c.ExchangeData,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            await _comparerSpot.ProcessSubject("Spot/Trading", c => c.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            await _comparerFutures.ProcessSubject("Futures/Account", c => c.Account,
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
            await _comparerFutures.ProcessSubject("Futures/ExchangeData", c => c.ExchangeData,
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
            await _comparerFutures.ProcessSubject("Futures/Trading", c => c.Trading,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> {
                   { "GetUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetRecentUserTradesAsync", new List<string> { "stop" } } ,
               }
                );
        }

    }
}
