using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;
using NUnit.Framework;
using System;
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
            await tester.ValidateAsync(client => client.SpotApi.Account.GetCrossMarginAccountsAsync(), "GetCrossMarginAccounts", ignoreProperties: new List<string> { "timestamp" });
            await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(), "GetIsolatedMarginAccounts");
        }

        [Test]
        public async Task ValidateSpotSubAccountCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/SubAccount", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountsAsync(), "GetSubAccounts");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.CreateSubAccountAsync("123", "123", "123"), "CreateSubAccount");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountBalancesAsync("123"), "GetSubAccountBalances");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountsBalancesAsync(), "GetSubAccountsBalances");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountApiKeyAsync("123"), "GetSubAccountApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.CreateSubAccountApiKeyAsync("123","123", "123"), "CreateSubAccountApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.EditSubAccountApiKeyAsync("123", "123", "123"), "EditSubAccountApiKey");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.DeleteSubAccountApiKeyAsync("123", "123", "123"), "DeleteSubAccountApiKey");
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

        [Test]
        public async Task ValidateSpotMarginCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/Margin", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.Margin.BorrowAsync("ETH", Enums.TimeInForce.GoodTillCanceled, 1), "Borrow");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RepayAsync("ETH", 1), "Repay");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetBorrowHistoryAsync("ETH"), "GetBorrowHistory");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetRepayHistoryAsync("ETH"), "GetRepayHistory");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetInterestHistoryAsync("ETH"), "GetInterestHistory");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetLendingAssetsAsync("ETH"), "GetLendingAssets");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetInterestRatesAsync("ETH"), "GetInterestRates");
            await tester.ValidateAsync(client => client.SpotApi.Margin.SubscribeAsync("ETH", 1, 1), "Subscribe");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RedeemAsync("ETH", 1, "123"), "Redeem");
            await tester.ValidateAsync(client => client.SpotApi.Margin.EditSubscriptionOrderAsync("ETH", 1, "123"), "EditSubscriptionOrder");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetRedemptionOrdersAsync("ETH", "123"), "GetRedemptionOrders", ignoreProperties: new List<string> { "redeemAmount", "receiptAmount" });
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetSubscriptionOrdersAsync("ETH", "123"), "GetSubscriptionOrders", ignoreProperties: new List<string> { "purchaseAmount", "lendAmount", "redeemAmount", "incomeAmount" });
            await tester.ValidateAsync(client => client.SpotApi.Margin.SetLeverageMultiplierAsync(1), "SetLeverageMultiplier");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetCrossMarginSymbolsAsync(), "GetCrossMarginSymbols", "data.items");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/Trading", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTestOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1), "PlaceTestOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMarginOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1, 1), "PlaceMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTestMarginOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1, 1), "PlaceTestMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOcoOrderAsync("ETHUSDT", Enums.OrderSide.Buy, 1, 1, 1, 1), "PlaceOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceBulkOrderAsync("ETHUSDT", new[] { new KucoinBulkOrderRequestEntry() }), "PlaceBulkOrder", ignoreProperties: new List<string> { "timeInForce", "iceberge" });
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("123"), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOcoOrderAsync("123"), "CancelOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOcoOrdersAsync(new[] { "123" }), "CancelOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderByClientOrderIdAsync("123"), "CancelOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOcoOrderByClientOrderIdAsync("123"), "CancelOcoOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync(), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync(), "GetOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrdersAsync(), "GetOcoOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetRecentOrdersAsync(), "GetRecentOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderByClientOrderIdAsync("123"), "GetOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrderAsync("123"), "GetOcoOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrderByClientOrderIdAsync("123"), "GetOcoOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOcoOrderDetailsAsync("123"), "GetOcoOrderDetails");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("123"), "GetOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync(), "GetUserTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetRecentUserTradesAsync(), "GetRecentUserTrades");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceStopOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, Enums.StopCondition.Entry, 1), "PlaceStopOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelStopOrderAsync("123"), "CancelStopOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelStopOrderByClientOrderIdAsync("123"), "CancelStopOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelStopOrdersAsync(), "CancelStopOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetStopOrdersAsync(), "GetStopOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetStopOrderAsync("123"), "GetStopOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetStopOrderByClientOrderIdAsync("123"), "GetStopOrderByClientOrderId");
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Futures/Account", "https://api-futures.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetAccountOverviewAsync(), "GetAccountOverview");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTransactionHistoryAsync(), "GetTransactionHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Account.TransferToMainAccountAsync("ETH", 1, Enums.AccountType.Main), "TransferToMainAccount");
            await tester.ValidateAsync(client => client.FuturesApi.Account.TransferToFuturesAccountAsync("ETH", 1, Enums.AccountType.Main), "TransferToFuturesAccount");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTransferToMainAccountHistoryAsync("ETH"), "GetTransferToMainAccountHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionAsync("ETHUSDT"), "GetPosition");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionsAsync(), "GetPositions");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionHistoryAsync(), "GetPositionHistory", ignoreProperties: new List<string> { "roe" });
            await tester.ValidateAsync(client => client.FuturesApi.Account.ToggleAutoDepositMarginAsync("ETHUSDT", true), "ToggleAutoDepositMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.AddMarginAsync("ETHUSDT", 1), "AddMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.RemoveMarginAsync("ETHUSDT", 1), "RemoveMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetFundingHistoryAsync("ETHUSDT"), "GetFundingHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetOpenOrderValueAsync("ETHUSDT"), "GetOpenOrderValue");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetRiskLimitLevelAsync("ETHUSDT"), "GetRiskLimitLevel");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetRiskLimitLevelAsync("ETHUSDT", 1), "SetRiskLimitLevel");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetMaxWithdrawMarginAsync("ETHUSDT"), "GetMaxWithdrawMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTradingFeeAsync("ETHUSDT"), "GetTradingFee");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Futures/ExchangeData", "https://api-futures.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetOpenContractsAsync(), "GetOpenContracts", ignoreProperties: new List<string> { "nextFundingRateTime" });
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetContractAsync("ETHUSDT"), "GetContract", ignoreProperties: new List<string> { "nextFundingRateTime" });
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync("ETHUSDT"), "GetAggregatedFullOrderBook", ignoreProperties: new List<string> { "ts" });
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetAggregatedPartialOrderBookAsync("ETHUSDT", 20), "GetAggregatedPartialOrderBook", ignoreProperties: new List<string> { "ts" });
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetInterestRatesAsync("ETHUSDT"), "GetInterestRates");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetIndexListAsync("ETHUSDT"), "GetIndexList");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetCurrentMarkPriceAsync("ETHUSDT"), "GetCurrentMarkPrice");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetPremiumIndexAsync("ETHUSDT"), "GetPremiumIndex");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetCurrentFundingRateAsync("ETHUSDT"), "GetCurrentFundingRate");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetServiceStatusAsync(), "GetServiceStatus");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.Get24HourTransactionVolumeAsync(), "Get24HourTransactionVolume");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETHUSDT", DateTime.UtcNow, DateTime.UtcNow), "GetFundingRateHistory");
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Futures/Trading", "https://api-futures.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceTestOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1, 1), "PlaceTestOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceTpSlOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1, 1), "PlaceTpSlOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceMultipleOrdersAsync(new[] { new KucoinFuturesOrderRequestEntry() }), "PlaceMultipleOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelOrderAsync("123"), "CancelOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelOrderByClientOrderIdAsync("ETHUSDT", "123"), "CancelOrderByClientOrderId");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllOrdersAsync(), "CancelAllOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllStopOrdersAsync(), "CancelAllStopOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrdersAsync(), "GetOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetUntriggeredStopOrdersAsync(), "GetUntriggeredStopOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetClosedOrdersAsync(), "GetClosedOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderAsync("123"), "GetOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderByClientOrderIdAsync("123"), "GetOrderByClientOrderId");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetUserTradesAsync(), "GetUserTrades", ignoreProperties: new List<string> { "stop" });
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetRecentUserTradesAsync(), "GetRecentUserTrades", ignoreProperties: new List<string> { "stop" });
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetMaxOpenPositionSizeAsync("XBTUSDTM", 1, 1), "GetMaxOpenPositionSize");
        }

        [Test]
        public async Task ValidateSpotHfTradingCalls()
        {
            var client = new KucoinRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<KucoinRestClient>(client, "Endpoints/Spot/HfTrading", "https://api.kucoin.com", IsAuthenticated, "data", stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceOrderWaitAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.NewOrderType.Market, 1), "PlaceOrderWait");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceMultipleOrdersAsync(new[] { new KucoinHfBulkOrderRequestEntry() }), "PlaceMultipleOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceMultipleOrdersWaitAsync(new[] { new KucoinHfBulkOrderRequestEntry() }), "PlaceMultipleOrdersWait");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.EditOrderAsync("ETHUSDT", "123", newPrice: 1), "EditOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelOrderAsync("ETHUSDT", "123"), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelOrderWaitAsync("ETHUSDT", "123"), "CancelOrderWait");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelOrderByClientOrderIdAsync("ETHUSDT", "123"), "CancelOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelOrderByClientOrderIdWaitAsync("ETHUSDT", "123"), "CancelOrderByClientOrderIdWait");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetOrderAsync("ETHUSDT", "123"), "GetOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetOrderByClientOrderIdAsync("ETHUSDT", "123"), "GetOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelAllOrdersBySymbolAsync("ETHUSDT"), "CancelAllOrdersBySymbol");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelAllOrdersAsync(), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetSymbolsWithOpenOrdersAsync(), "GetSymbolsWithOpenOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetClosedOrdersAsync("ETHUSDT"), "GetClosedOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades", ignoreProperties: new List<string> { "id" });
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelAfterAsync(TimeSpan.Zero), "CancelAfter");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetCancelAfterStatusAsync(), "GetCancelAfterStatus");

            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceMarginOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.NewOrderType.Limit, 1, 1), "PlaceMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.PlaceTestMarginOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.NewOrderType.Limit, 1, 1), "PlaceTestMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelMarginOrderAsync("ETHUSDT", "123"), "CancelMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelMarginOrderByClientOrderIdAsync("ETHUSDT", "123"), "CancelMarginOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.CancelAllMarginOrdersBySymbolAsync("ETHUSDT"), "CancelAllMarginOrdersBySymbol");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetOpenMarginOrdersAsync("ETHUSDT", Enums.TradeType.IsolatedMarginTrade), "GetOpenMarginOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetClosedMarginOrdersAsync("ETHUSDT"), "GetClosedMarginOrders");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetMarginOrderAsync("ETHUSDT", "123"), "GetMarginOrder");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetMarginOrderByClientOrderIdAsync("ETHUSDT", "123"), "GetMarginOrderByClientOrderId");
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetMarginUserTradesAsync("ETHUSDT", Enums.TradeType.IsolatedMarginTrade), "GetMarginUserTrades", ignoreProperties: new List<string> { "id" });
            await tester.ValidateAsync(client => client.SpotApi.HfTrading.GetMarginSymbolsWithOpenOrdersAsync(true), "GetMarginSymbolsWithOpenOrders");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(h => h.Key == "KC-API-SIGN");
        }
    }
}
