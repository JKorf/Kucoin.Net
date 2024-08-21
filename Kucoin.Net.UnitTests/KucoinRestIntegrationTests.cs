using Kucoin.Net.Clients;
using Kucoin.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.UnitTests
{
    [NonParallelizable]
    internal class KucoinRestIntegrationTests : RestIntergrationTest<KucoinRestClient>
    {
        public override bool Run { get; set; }

        public KucoinRestIntegrationTests()
        {
        }

        public override KucoinRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new KucoinRestClient(null, loggerFactory, opts =>
            {
                opts.OutputOriginalData = true;
                opts.ApiCredentials = Authenticated ? new KucoinApiCredentials(key, sec, pass) : null;
            });
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(400100));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetUserInfoAsync(default), true);
            //await RunAndCheckResult(client => client.SpotApi.Account.GetSubUserInfoAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountsAsync(default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetBasicUserFeeAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetSymbolTradingFeesAsync("ETH-USDT", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetAccountLedgersAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTransferableAsync("ETH", Enums.AccountType.SpotHf, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetHistoricalDepositsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetHistoricalWithdrawalsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalQuotasAsync("ETH", default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetMarginAccountAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetCrossMarginAccountsAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetIsolatedMarginAccountAsync("ETH-USDT", default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.Get24HourStatsAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetMarketsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAggregatedPartialOrderBookAsync("ETH-USDT", 20, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync("ETH-USDT", default), true);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetAsync("ETH", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetFiatPricesAsync(default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetLeveragedTokensAsync(default), true);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOcoOrdersAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetRecentOrdersAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetRecentUserTradesAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetStopOrdersAsync(default, default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetStopOrdersAsync(default, default, default, default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestFuturesAccount()
        {
            await RunAndCheckResult(client => client.FuturesApi.Account.GetAccountOverviewAsync(default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetPositionAsync("XBTUSDM", default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetPositionHistoryAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetFundingHistoryAsync("XBTUSDM", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetOpenOrderValueAsync("XBTUSDM", default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetRiskLimitLevelAsync("XBTUSDM", default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetMaxWithdrawMarginAsync("XBTUSDM", default), true);
            await RunAndCheckResult(client => client.FuturesApi.Account.GetTradingFeeAsync("XBTUSDM", default), true);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetOpenContractsAsync(default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetContractAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetTickerAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetAggregatedPartialOrderBookAsync("XBTUSDM", 20, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetTradeHistoryAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetInterestRatesAsync("XBTUSDM", default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetIndexListAsync("XBTUSDM", default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetCurrentMarkPriceAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetPremiumIndexAsync("XBTUSDM", default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetCurrentFundingRateAsync("XBTUSDM", default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetServiceStatusAsync(default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetKlinesAsync("XBTUSDM", Enums.FuturesKlineInterval.OneDay, default, default, default), false);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.Get24HourTransactionVolumeAsync(default), true);
            await RunAndCheckResult(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("XBTUSDM", DateTime.UtcNow.AddDays(-3), DateTime.UtcNow, default), false);
        }

        [Test]
        public async Task TestFuturesTrading()
        {
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetUntriggeredStopOrdersAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetClosedOrdersAsync(default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetRecentUserTradesAsync(default), true);
            await RunAndCheckResult(client => client.FuturesApi.Trading.GetMaxOpenPositionSizeAsync("XBTUSDTM", 50000, 1, default), true);
        }
    }
}
