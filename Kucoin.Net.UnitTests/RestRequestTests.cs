using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/Account", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserInfoAsync(), "GetUserInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSubUserInfoAsync(), "GetSubUserInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountsAsync(), "GetAccounts");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountAsync("123"), "GetAccount");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBasicUserFeeAsync(), "GetBasicUserFee");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSymbolTradingFeesAsync("ETHUSDT"), "GetSymbolTradingFees");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountLedgersAsync("ETHUSDT"), "GetAccountLedgers", ignoreProperties: new List<string> { "context" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransferableAsync("ETHUSDT", Enums.AccountType.Isolated), "GetTransferable");
            await tester.ValidateAsync(client => client.SpotApi.Account.UniversalTransferAsync(1, Enums.TransferAccountType.IsolatedV2, Enums.TransferAccountType.IsolatedV2, Enums.TransferType.Internal), "UniversalTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.InnerTransferAsync("ETH", Enums.AccountType.Isolated, Enums.AccountType.Isolated, 1), "InnerTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositsAsync("ETH"), "GetDeposits");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetHistoricalDepositsAsync("ETH"), "GetHistoricalDeposits");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("ETH"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressesAsync("ETH"), "GetDepositAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.CreateDepositAddressAsync("ETH"), "CreateDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalsAsync("ETH"), "GetWithdrawals");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetHistoricalWithdrawalsAsync("ETH"), "GetHistoricalWithdrawals");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalQuotasAsync("ETH"), "GetWithdrawalQuotas");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("ETH", "123", 1), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.CancelWithdrawalAsync("123"), "CancelWithdrawal");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMarginAccountAsync(), "GetMarginAccount");
            // await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginAccountsAsync(), "GetCrossMarginAccounts", ignoreProperties: new List<string> { "timestamp" }); CHECK
            // await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(), "GetIsolatedMarginAccounts"); CHECK
            // await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginAccountAsync(), "GetIsolatedMarginAccount");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolsAsync(), "GetSymbols");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.Get24HourStatsAsync("ETHUSDT"), "Get24HourStats");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetMarketsAsync(), "GetMarkets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedPartialOrderBookAsync("ETHUSDT", 20), "GetAggregatedPartialOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync("ETHUSDT"), "GetAggregatedFullOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetFiatPricesAsync(), "GetFiatPrices");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetLeveragedTokensAsync(), "GetLeveragedTokens");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(h => h.Key == "KC-API-SIGN");
        }
    }
}
