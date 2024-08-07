using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var client = new KucoinSocketClient(opts =>
            {
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.Environment = KucoinEnvironment.CreateCustom("UnitTesting", KucoinApiAddresses.Default.SpotAddress, KucoinApiAddresses.Default.FuturesAddress);
            });
            var tester = new SocketSubscriptionValidator<KucoinSocketClient>(client, "Subscriptions/Spot", "wss://ws-api-spot.kucoin.com", "data", stjCompare: false);
            await tester.ValidateAsync<KucoinStreamTick>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("BTC-USDT", handler), "Ticker");
            await tester.ValidateAsync<KucoinStreamTick>((client, handler) => client.SpotApi.SubscribeToAllTickerUpdatesAsync(handler), "Tickers");
            await tester.ValidateAsync<KucoinStreamSnapshot>((client, handler) => client.SpotApi.SubscribeToSnapshotUpdatesAsync("BTC", handler), "Snapshot", "data.data");
            await tester.ValidateAsync<KucoinStreamBestOffers>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETH-USDT", handler), "BestOffers", "data");
            await tester.ValidateAsync<KucoinStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync("BTC-USDT", handler), "AggBook", "data");
            await tester.ValidateAsync<KucoinStreamMatch>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("BTC-USDT", handler), "Trades", "data");
            await tester.ValidateAsync<KucoinStreamCandle>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("BTC-USDT", Enums.KlineInterval.EightHours, handler), "Klines", "data");
            await tester.ValidateAsync<KucoinStreamOrderBookChanged>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("BTC-USDT", 5, handler), "Book", "data");
            await tester.ValidateAsync<KucoinStreamIndicatorPrice>((client, handler) => client.SpotApi.SubscribeToIndexPriceUpdatesAsync("USDT-BTC", handler), "IndexPrice", "data");
            await tester.ValidateAsync<KucoinStreamIndicatorPrice>((client, handler) => client.SpotApi.SubscribeToMarkPriceUpdatesAsync("USDT-BTC", handler), "MarkPrice", "data");
            await tester.ValidateAsync<KucoinStreamFundingBookUpdate>((client, handler) => client.SpotApi.SubscribeToFundingBookUpdatesAsync("BTC", handler), "FundingBook", "data");
            await tester.ValidateAsync<KucoinStreamOrderNewUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler, null, null), "NewOrder", "data");
            await tester.ValidateAsync<KucoinStreamOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(null, handler, null), "OrderUpdate", "data");
            await tester.ValidateAsync<KucoinStreamOrderMatchUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(null, null, handler), "MatchOrder", "data");
            await tester.ValidateAsync<KucoinBalanceUpdate>((client, handler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(handler), "Balance", "data", ignoreProperties: new List<string> { "relationContext" });
            await tester.ValidateAsync<KucoinStreamStopOrderUpdateBase>((client, handler) => client.SpotApi.SubscribeToStopOrderUpdatesAsync(handler), "StopOrder", "data");
            await tester.ValidateAsync<KucoinIsolatedMarginPositionUpdate>((client, handler) => client.SpotApi.SubscribeToIsolatedMarginPositionUpdatesAsync("ETH-USDT", handler), "IsolatedMarginPosition", "data", ignoreProperties: new List<string> { "changeAssets" });
            await tester.ValidateAsync<KucoinMarginDebtRatioUpdate>((client, handler) => client.SpotApi.SubscribeToMarginPositionUpdatesAsync(handler, x => { }), "DebtRatio", "data");
            await tester.ValidateAsync<KucoinMarginPositionStatusUpdate>((client, handler) => client.SpotApi.SubscribeToMarginPositionUpdatesAsync(x => { }, handler), "PositionStatus", "data");
            await tester.ValidateAsync<KucoinMarginOrderUpdate>((client, handler) => client.SpotApi.SubscribeToMarginOrderUpdatesAsync("BTC", handler, null, null), "MarginNewOrder", "data");
            await tester.ValidateAsync<KucoinMarginOrderUpdate>((client, handler) => client.SpotApi.SubscribeToMarginOrderUpdatesAsync("BTC", null, handler, null), "MarginOrderUpdate", "data");
            await tester.ValidateAsync<KucoinMarginOrderDoneUpdate>((client, handler) => client.SpotApi.SubscribeToMarginOrderUpdatesAsync("BTC", null, null, handler), "MarginOrderDone", "data");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var client = new KucoinSocketClient(opts =>
            {
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
                opts.Environment = KucoinEnvironment.CreateCustom("UnitTesting", KucoinApiAddresses.Default.SpotAddress, KucoinApiAddresses.Default.FuturesAddress);
            });
            var tester = new SocketSubscriptionValidator<KucoinSocketClient>(client, "Subscriptions/Futures", "wss://ws-api-spot.kucoin.com", "data", stjCompare: false);
            await tester.ValidateAsync<KucoinStreamFuturesMatch>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("XBTUSDTM", handler), "Trades");
            await tester.ValidateAsync<KucoinStreamFuturesTick>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("XBTUSDM", handler), "Tickers");
            await tester.ValidateAsync<KucoinFuturesOrderBookChange>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("XBTUSDM", handler), "Book", ignoreProperties: new List<string> { "change", "timestamp" });
            await tester.ValidateAsync<KucoinStreamOrderBookChanged>((client, handler) => client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync("XBTUSDM", 5, handler), "PartialBook", ignoreProperties: new List<string> { "ts" });
            await tester.ValidateAsync<KucoinStreamFuturesMarkIndexPrice>((client, handler) => client.FuturesApi.SubscribeToMarketUpdatesAsync("XBTUSDM", handler, null), "MarkPrice");
            await tester.ValidateAsync<KucoinStreamFuturesFundingRate>((client, handler) => client.FuturesApi.SubscribeToMarketUpdatesAsync("XBTUSDM", null, handler), "FundingRate");
            await tester.ValidateAsync<KucoinContractAnnouncement>((client, handler) => client.FuturesApi.SubscribeToSystemAnnouncementsAsync(handler), "Announcement");
            await tester.ValidateAsync<KucoinStreamTransactionStatisticsUpdate>((client, handler) => client.FuturesApi.SubscribeTo24HourSnapshotUpdatesAsync("XBTUSDM", handler), "Snapshot");
            await tester.ValidateAsync<KucoinStreamFuturesOrderUpdate>((client, handler) => client.FuturesApi.SubscribeToOrderUpdatesAsync("XBTUSDM", handler), "Order");
            await tester.ValidateAsync<KucoinStreamFuturesStopOrderUpdate>((client, handler) => client.FuturesApi.SubscribeToStopOrderUpdatesAsync(handler), "StopOrder");
            await tester.ValidateAsync<KucoinStreamOrderMarginUpdate>((client, handler) => client.FuturesApi.SubscribeToBalanceUpdatesAsync(handler, null, null), "OrderMargin");
            await tester.ValidateAsync<KucoinStreamFuturesBalanceUpdate>((client, handler) => client.FuturesApi.SubscribeToBalanceUpdatesAsync(null, handler, null), "Balance");
            await tester.ValidateAsync<KucoinStreamFuturesWithdrawableUpdate>((client, handler) => client.FuturesApi.SubscribeToBalanceUpdatesAsync(null, null, handler), "Withdrawable");
            await tester.ValidateAsync<KucoinPositionUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync(handler, null, null, null), "Position");
            await tester.ValidateAsync<KucoinPositionMarkPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync(null, handler, null, null), "PositionMarkPrice");
            await tester.ValidateAsync<KucoinPositionFundingSettlementUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync(null, null, handler, null), "FundingSettlement");
            await tester.ValidateAsync<KucoinPositionRiskAdjustResultUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync(null, null, null, handler), "RiskAdjust");
        }
    }
}
