using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using CryptoExchange.Net;
using System.Threading.Tasks;
using Kucoin.Net.Clients.Rest.Spot;
using System.Diagnostics;
using Kucoin.Net.Clients.Socket;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects.Internal;

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class KucoinClientTests
    {
        [TestCase()]
        public async Task ReceivingError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient(new KucoinClientSpotOptions());
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default!,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((KucoinClientSpot)client, JsonConvert.SerializeObject(resultObj));

            // act
            var result = await client.ExchangeData.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error!.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient(new KucoinClientSpotOptions());
            TestHelpers.SetResponse((KucoinClientSpot)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.ExchangeData.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithJsonError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient(new KucoinClientSpotOptions());
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default!,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((KucoinClientSpot)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.ExchangeData.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error!.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }

        [TestCase("BTC-USDT", true)]
        [TestCase("NANO-USDT", true)]
        [TestCase("NANO-BTC", true)]
        [TestCase("ETH-BTC", true)]
        [TestCase("BE-ETC", true)]
        [TestCase("NANO-USDTDASADS", true)]
        [TestCase("A-USDTDASADS", true)]
        [TestCase("-USDTDASADSD", false)]
        [TestCase("BTCUSDT", false)]
        [TestCase("BTCUSD", false)]
        public void CheckValidKucoinSymbol(string symbol, bool isValid)
        {
            if (isValid)
                Assert.DoesNotThrow(symbol.ValidateKucoinSymbol);
            else
                Assert.Throws(typeof(ArgumentException), symbol.ValidateKucoinSymbol);
        }

        [Test]
        public void CheckRestInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(KucoinClientSpot));
            var ignore = new string[] { "IKucoinClientSpot" };
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IKucoinClientSpot") && !ignore.Contains(t.Name));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod, $"{method.Name} not found in interface {clientInterface.Name}");
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }

        [Test]
        public void CheckSocketInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(KucoinSocketClientSpot));
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IKucoinSocketClientSpot"));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task<CallResult<UpdateSubscription>>))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod);
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }
    }
}
