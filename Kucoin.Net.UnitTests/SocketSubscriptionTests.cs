using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Testing;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects;
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
            });
            var tester = new SocketSubscriptionValidator<KucoinSocketClient>(client, "Subscriptions/Spot", "wss://ws-api-spot.kucoin.com", "data", stjCompare: false);
            await tester.ValidateAsync<KucoinStreamTick>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("BTC-USDT", handler), "Ticker");
            await tester.ValidateAsync<KucoinStreamTick>((client, handler) => client.SpotApi.SubscribeToAllTickerUpdatesAsync(handler), "Tickers");
            await tester.ValidateAsync<KucoinStreamSnapshot>((client, handler) => client.SpotApi.SubscribeToSnapshotUpdatesAsync("BTC", handler), "Snapshot", "data.data");
            await tester.ValidateAsync<KucoinStreamBestOffers>((client, handler) => client.SpotApi.SubscribeToBestOfferUpdatesAsync("ETH-USDT", handler), "BestOffers", "data");
            await tester.ValidateAsync<KucoinStreamOrderBook>((client, handler) => client.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync("BTC-USDT", handler), "AggBook", "data");
            await tester.ValidateAsync<KucoinStreamMatch>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("BTC-USDT", handler), "Trades", "data");
            await tester.ValidateAsync<KucoinStreamCandle>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("BTC-USDT", Enums.KlineInterval.EightHours, handler), "Klines", "data");
            await tester.ValidateAsync<KucoinStreamOrderBookChanged>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("BTC-USDT", 5, handler), "Book", "data");
            await tester.ValidateAsync<KucoinStreamIndicatorPrice>((client, handler) => client.SpotApi.SubscribeToIndexPriceUpdatesAsync("USDT-BTC", handler), "IndexPrice", "data");
            await tester.ValidateAsync<KucoinStreamIndicatorPrice>((client, handler) => client.SpotApi.SubscribeToMarkPriceUpdatesAsync("USDT-BTC", handler), "MarkPrice", "data");
            await tester.ValidateAsync<KucoinStreamFundingBookUpdate>((client, handler) => client.SpotApi.SubscribeToFundingBookUpdatesAsync("BTC", handler), "FundingBook", "data");
            
            // Auth subscriptions can't be tested atm as they create a new rest client
            // to retrieve the connection url
            //await tester.ValidateAsync<KucoinStreamOrderNewUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler, null, null), "NewOrder", "data");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var client = new KucoinSocketClient(opts =>
            {
                opts.ApiCredentials = new KucoinApiCredentials("123", "456", "789");
            });
            var tester = new SocketSubscriptionValidator<KucoinSocketClient>(client, "Subscriptions/Futures", "wss://ws-api-spot.kucoin.com", "data", stjCompare: false);
            //await tester.ValidateAsync<KucoinStreamFuturesMatch>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("XBTUSDTM", handler), "Trades");
            //await tester.ValidateAsync<KucoinStreamFuturesTick>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("XBTUSDM", handler), "Tickers");
            //await tester.ValidateAsync<KucoinFuturesOrderBookChange>((client, handler) => client.FuturesApi.SubscribeToOrderBookUpdatesAsync("XBTUSDM", handler), "Book");
            //await tester.ValidateAsync<KucoinStreamOrderBookChanged>((client, handler) => client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync("XBTUSDM", 5, handler), "PartialBook", ignoreProperties: new List<string> { "ts" });
            await tester.ValidateAsync<KucoinStreamFuturesMarkIndexPrice>((client, handler) => client.FuturesApi.SubscribeToMarketUpdatesAsync("XBTUSDM", handler, null), "MarkPrice");
            await tester.ValidateAsync<KucoinStreamFuturesFundingRate>((client, handler) => client.FuturesApi.SubscribeToMarketUpdatesAsync("XBTUSDM", null, handler), "FundingRate");
            await tester.ValidateAsync<KucoinContractAnnouncement>((client, handler) => client.FuturesApi.SubscribeToSystemAnnouncementsAsync(handler), "Announcement");
            await tester.ValidateAsync<KucoinStreamTransactionStatisticsUpdate>((client, handler) => client.FuturesApi.SubscribeTo24HourSnapshotUpdatesAsync("XBTUSDM", handler), "Snapshot");
        }
    }
}
