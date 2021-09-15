using CryptoExchange.Net;
using Kucoin.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot.Socket;
using Newtonsoft.Json.Linq;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class KucoinSocketClientTests
    {
        [Test]
        public async Task Subscribe_Should_SucceedIfAckResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new Objects.KucoinSocketClientOptions
            {
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                ApiCredentials = null
            });

            // act
            var subTask = client.Spot.SubscribeToTickerUpdatesAsync("ETH-BTC", test => { });

            await Task.Delay(10);

            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"type\": \"ack\", \"id\":\"{id}\"}}");
            var subResult = await subTask;

            // assert
            Assert.IsTrue(subResult.Success);
        }

        [Test]
        public async Task Subscribe_Should_FailIfNotAckResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new Objects.KucoinSocketClientOptions
            {
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                ApiCredentials = null
            });

            // act
            var subTask = client.Spot.SubscribeToTickerUpdatesAsync("ETH-BTC", test => { });
            await Task.Delay(10);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"type\": \"error\", \"id\":\"{id}\", \"data\": \"TestError\", \"code\": \"1234\"}}");
            var subResult = await subTask;

            // assert
            Assert.IsFalse(subResult.Success);
            Assert.IsTrue(subResult.Error.Code == 1234);
            Assert.IsTrue(subResult.Error.Message == "TestError");
        }
        
        [Test]
        public async Task UpdateTick_Should_TriggerAction()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new Objects.KucoinSocketClientOptions
            {
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                ApiCredentials = null
            });
            KucoinStreamTick result = null;

            // act
            var subTask = client.Spot.SubscribeToTickerUpdatesAsync("ETH-BTC", test => result = test.Data);
            await Task.Delay(10);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"type\": \"ack\", \"id\":\"{id}\"}}");
            var subResult = await subTask;

            var expected = TestHelpers.CreateObjectWithTestParameters<KucoinStreamTick>();
            var update = new KucoinUpdateMessage<KucoinStreamTick>()
            {
                Type = "message",
                Subject = "trade.ticker",
                Topic = "/market/ticker:ETH-BTC",
                Data = expected
            };
            socket.InvokeMessage(JsonConvert.SerializeObject(update));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(result, expected, "Symbol"));
        }

        [Test]
        public async Task UpdateSnapshot_Should_TriggerAction()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new Objects.KucoinSocketClientOptions
            {
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                ApiCredentials = null
            });
            KucoinStreamSnapshot result = null;

            // act
            var subTask = client.Spot.SubscribeToSnapshotUpdatesAsync("ETH-BTC", test => result = test.Data);
            await Task.Delay(10);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"type\": \"ack\", \"id\":\"{id}\"}}");
            var subResult = await subTask;

            var expected = TestHelpers.CreateObjectWithTestParameters<KucoinStreamSnapshot>();
            var update = new KucoinUpdateMessage<KucoinStreamSnapshotWrapper>()
            {
                Type = "message",
                Subject = "trade.ticker",
                Topic = "/market/snapshot:ETH-BTC",
                Data = new KucoinStreamSnapshotWrapper()
                {
                    Data = expected,
                    Sequence = 1
                }
            };
            socket.InvokeMessage(JsonConvert.SerializeObject(update));

            // assert
            Assert.IsTrue(subResult.Success);
            Assert.IsTrue(TestHelpers.AreEqual(result, expected));
        }
    }
}
