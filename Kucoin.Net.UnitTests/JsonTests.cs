using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        private JsonToObjectComparer<IKucoinClient> _comparerSpot = new JsonToObjectComparer<IKucoinClient>((json) => TestHelpers.CreateResponseClient(json, new KucoinClientOptions()
        { ApiCredentials = new KucoinApiCredentials("1234", "1234", "12"),  OutputOriginalData = true }));

        private JsonToObjectComparer<IKucoinClient> _comparerFutures = new JsonToObjectComparer<IKucoinClient>((json) => TestHelpers.CreateResponseClientFutures(json, new KucoinClientOptions()
        { FuturesApiCredentials = new KucoinApiCredentials("1234", "1234", "12"), OutputOriginalData = true }));

        [Test]
        public async Task ValidateSpotCalls()
        {   
            await _comparerSpot.ProcessSubject("Spot", c => c.Spot,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" } ,
               parametersToSetNull: new [] { "pageSize", "quoteQuantity" }
                );
        }

        [Test]
        public async Task ValidateFuturesCalls()
        {
            await _comparerFutures.ProcessSubject("Futures", c => c.Futures,
               useNestedJsonPropertyForAllCompare: new List<string> { "data" },
               parametersToSetNull: new[] { "pageSize", "quoteQuantity" },
               ignoreProperties: new Dictionary<string, List<string>> { 
                   { "GetWithdrawHistoryAsync", new List<string> { "withdrawalId" } } ,
                   { "GetUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetRecentUserTradesAsync", new List<string> { "stop" } } ,
                   { "GetAggregatedFullOrderBookAsync", new List<string> { "ts" } } ,
                   { "GetAggregatedPartialOrderBookAsync", new List<string> { "ts" } } ,
               }
                );
        }

    }
}
