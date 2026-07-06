using Kucoin.Net.Clients;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Kucoin.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;
using System.Collections.Generic;

namespace Kucoin.Net.UnitTests
{
    [NonParallelizable]
    internal class KucoinRestIntegrationTests : RestIntegrationTest<KucoinRestClient>
    {
        public override bool Run { get; set; } = false;

        public KucoinRestIntegrationTests()
        {
        }

        public override KucoinRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new KucoinRestClient(null, loggerFactory, Options.Create(new KucoinRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new KucoinCredentials(key, sec, pass) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetKlinesAsync("TSTTST", Enums.KlineInterval.OneDay, default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("400100"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetUserInfoAsync(default), true, "data");
            //await RunAndCheckResult(client => client.SpotApi.Account.GetSubUserInfoAsync(default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountsAsync(default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetBasicUserFeeAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetSymbolTradingFeesAsync("ETH-USDT", default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetAccountLedgersAsync(default, default, default, default, default, default, default, default), true, "data", ignoreProperties: [
                "context" // custom converter
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetHfAccountLedgersAsync(default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTransferableAsync("ETH", Enums.AccountType.Trade, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDepositsAsync(default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalsAsync(default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalQuotasAsync("ETH", default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetMarginAccountAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetCrossMarginAccountsAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetIsolatedMarginAccountAsync("ETH-USDT", default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolsAsync(default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickerAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickersAsync(default), false, "data", ignoreProperties: [
                "priceChange", // Same value as changePrice
                "priceChangePercent" // Same value as changeRate
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.Get24HourStatsAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetMarketsAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAggregatedPartialOrderBookAsync("ETH-USDT", 20, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync("ETH-USDT", default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH-USDT", Enums.KlineInterval.OneDay, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false, "data", ignoreProperties: [
                "withdrawMinSize", // Same value as withdrawalMinSize
                "withdrawMinFee" // Same value as withdrawalMinFee
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetAsync("ETH", default), false, "data", ignoreProperties: [
                "withdrawMinSize", // Same value as withdrawalMinSize
                "withdrawMinFee" // Same value as withdrawalMinFee
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetFiatPricesAsync(default, default, default), false, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOcoOrdersAsync(default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetRecentOrdersAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetRecentUserTradesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetStopOrdersAsync(default, default, default, default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetStopOrdersAsync(default, default, default, default, default, default, default, default, default, default, default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetAccountOverviewAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetPositionAsync("XBTUSDM", default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetPositionsAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetPositionHistoryAsync(default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetFundingHistoryAsync("XBTUSDM", default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetOpenOrderValueAsync("XBTUSDM", default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetRiskLimitLevelAsync("XBTUSDM", default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetMaxWithdrawMarginAsync("XBTUSDM", default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Account.GetTradingFeeAsync("XBTUSDM", default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetSymbolsAsync(default), false, "data", ignoreProperties: [
                "nextFundingRateTime", // Same as nextFundingRateDateTime
                "period", // Deprecated
                "k", // Unknown
                "m", // Unknown
                "f", // Unknown
                ]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetContractAsync("XBTUSDM", default), false, "data", ignoreProperties: [
                "nextFundingRateTime", // Same as nextFundingRateDateTime
                "period", // Deprecated
                "k", // Unknown
                "m", // Unknown
                "f", // Unknown
                ]);
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTickerAsync("XBTUSDM", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTickersAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync("XBTUSDM", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetAggregatedPartialOrderBookAsync("XBTUSDM", 20, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetTradeHistoryAsync("XBTUSDM", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetInterestRatesAsync("XBTUSDM", default, default, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetIndexListAsync("XBTUSDM", default, default, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetCurrentMarkPriceAsync("XBTUSDM", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetPremiumIndexAsync("XBTUSDM", default, default, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetCurrentFundingRateAsync("XBTUSDM", default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetServerTimeAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetServiceStatusAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetKlinesAsync("XBTUSDM", Enums.FuturesKlineInterval.OneDay, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.Get24HourTransactionVolumeAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("XBTUSDM", DateTime.UtcNow.AddDays(-3), DateTime.UtcNow, default), false, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestFuturesTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetUntriggeredStopOrdersAsync(default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetClosedOrdersAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetRecentUserTradesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.FuturesApi.Trading.GetMaxOpenPositionSizeAsync("XBTUSDTM", 50000, 1, default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            if (!Authenticated)
                return;

            await TestOrderBook(new KucoinSpotSymbolOrderBook("ETH-USDT", null, null, GetClient(null), new KucoinSocketClient()));
            await TestOrderBook(new KucoinFuturesSymbolOrderBook("ETHUSDTM", null, null, GetClient(null), new KucoinSocketClient()));
        }
    }
}
