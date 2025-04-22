using Kucoin.Net.Clients;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Models.Futures.Socket;

namespace Kucoin.Net.UnitTests
{
    [NonParallelizable]
    internal class KucoinSocketIntegrationTests : SocketIntegrationTest<KucoinSocketClient>
    {
        public override bool Run { get; set; } = false;

        public KucoinSocketIntegrationTests()
        {
        }

        public override KucoinSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new KucoinSocketClient(Options.Create(new KucoinSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec, pass) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<KucoinStreamTick>((client, updateHandler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(default, default), false, true);
            await RunAndCheckUpdate<KucoinStreamTick>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETH-USDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<KucoinStreamTransactionStatisticsUpdate>((client, updateHandler) => client.FuturesApi.SubscribeTo24HTickerUpdatesAsync("ETHUSDTM", updateHandler, default), true, false);
        } 
    }
}
