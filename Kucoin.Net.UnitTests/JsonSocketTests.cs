using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Newtonsoft.Json;
using Kucoin.Net.Objects.Models.Futures.Socket;
using System.IO;
using System;
using System.Text;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Models.Futures;
using System.Diagnostics;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class JsonSocketTests
    {
        [Test]
        public async Task ValidateSpotOrderUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamOrderBaseUpdate>(@"JsonResponses\Spot\Socket\OrderUpdate1.txt");
            await TestFileToObject<KucoinStreamOrderMatchUpdate>(@"JsonResponses\Spot\Socket\OrderUpdate2.txt");
            await TestFileToObject<KucoinStreamOrderBaseUpdate>(@"JsonResponses\Spot\Socket\OrderUpdate3.txt");
            await TestFileToObject<KucoinStreamOrderBaseUpdate>(@"JsonResponses\Spot\Socket\OrderUpdate4.txt");
            await TestFileToObject<KucoinStreamOrderBaseUpdate>(@"JsonResponses\Spot\Socket\OrderUpdate5.txt");
        }

        [Test]
        public async Task ValidateSpotBalanceUpdateStreamJson()
        {
            await TestFileToObject<KucoinBalanceUpdate>(@"JsonResponses\Spot\Socket\BalanceUpdate.txt", new List<string> { "relationContext" } );
        }

        [Test]
        public async Task ValidateSpotStopOrderUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamStopOrderUpdate>(@"JsonResponses\Spot\Socket\StopOrderUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotFundingBookUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFundingBookUpdate>(@"JsonResponses\Spot\Socket\FundingBookUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotMarkPriceUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamIndicatorPrice>(@"JsonResponses\Spot\Socket\MarkPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotIndexPriceUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamIndicatorPrice>(@"JsonResponses\Spot\Socket\IndexPriceUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotOrderBookUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamOrderBookChanged>(@"JsonResponses\Spot\Socket\OrderBookUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotKlineUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamCandle>(@"JsonResponses\Spot\Socket\KlineUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotTradeUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamMatch>(@"JsonResponses\Spot\Socket\TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotMarketSnapshotUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamSnapshotWrapper>(@"JsonResponses\Spot\Socket\MarketSnapshotUpdate.txt");
        }

        [Test]
        public async Task ValidateSpotTickerUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamTick>(@"JsonResponses\Spot\Socket\TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateFuturesOrderUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFuturesOrderUpdate>(@"JsonResponses\Futures\Socket\OrderUpdate1.txt");
            await TestFileToObject<KucoinStreamFuturesOrderUpdate>(@"JsonResponses\Futures\Socket\OrderUpdate2.txt");
        }

        [Test]
        public async Task ValidateFuturesBalanceUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamOrderMarginUpdate>(@"JsonResponses\Futures\Socket\BalanceUpdate1.txt");
            await TestFileToObject<KucoinStreamFuturesBalanceUpdate>(@"JsonResponses\Futures\Socket\BalanceUpdate2.txt");
            await TestFileToObject<KucoinStreamFuturesWithdrawableUpdate>(@"JsonResponses\Futures\Socket\BalanceUpdate3.txt");
        }

        [Test]
        public async Task ValidateFuturesStopOrderUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFuturesStopOrderUpdate>(@"JsonResponses\Futures\Socket\StopOrderUpdate.txt");
        }

        [Test]
        public async Task ValidateFuturesPositionUpdateStreamJson()
        {
            await TestFileToObject<KucoinPositionUpdate>(@"JsonResponses\Futures\Socket\PositionUpdate1.txt");
            await TestFileToObject<KucoinPositionUpdate>(@"JsonResponses\Futures\Socket\PositionUpdate5.txt");
            await TestFileToObject<KucoinPositionMarkPriceUpdate>(@"JsonResponses\Futures\Socket\PositionUpdate2.txt");
            await TestFileToObject<KucoinPositionFundingSettlementUpdate>(@"JsonResponses\Futures\Socket\PositionUpdate3.txt");
            await TestFileToObject<KucoinPositionRiskAdjustResultUpdate>(@"JsonResponses\Futures\Socket\PositionUpdate4.txt");
        }

        [Test]
        public async Task ValidateFuturesTickerUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFuturesTick>(@"JsonResponses\Futures\Socket\TickerUpdate.txt");
        }

        [Test]
        public async Task ValidateFuturesTradeUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFuturesMatch>(@"JsonResponses\Futures\Socket\TradeUpdate.txt");
        }

        [Test]
        public async Task ValidateFuturesMarketUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamFuturesMarkIndexPrice>(@"JsonResponses\Futures\Socket\MarketUpdate1.txt");
            await TestFileToObject<KucoinStreamFuturesFundingRate>(@"JsonResponses\Futures\Socket\MarketUpdate2.txt");
        }

        [Test]
        public async Task ValidateFuturesSnapshotUpdateStreamJson()
        {
            await TestFileToObject<KucoinStreamTransactionStatisticsUpdate>(@"JsonResponses\Futures\Socket\SnapshotUpdate.txt");
        }

        private static async Task TestFileToObject<T>(string filePath, List<string> ignoreProperties = null)
        {
            var listener = new EnumValueTraceListener();
            Trace.Listeners.Add(listener);
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string json;
            try
            {
                var file = File.OpenRead(Path.Combine(path, filePath));
                using var reader = new StreamReader(file);
                json = await reader.ReadToEndAsync();
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            var result = JsonConvert.DeserializeObject<T>(json);
            JsonToObjectComparer<IKucoinSocketClient>.ProcessData("", result, json, ignoreProperties: new Dictionary<string, List<string>>
            {
                { "", ignoreProperties ?? new List<string>() }
            });
            Trace.Listeners.Remove(listener);
        }
    }

    internal class EnumValueTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            if(message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }

        public override void WriteLine(string message)
        {
            if(message.Contains("Cannot map"))
                throw new Exception("Enum value error: " + message);
        }
    }
}
