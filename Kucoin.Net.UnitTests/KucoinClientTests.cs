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

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class KucoinClientTests
    {
        [TestCase()]
        public async Task ReceivingError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default!,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((KucoinClient)client, JsonConvert.SerializeObject(resultObj));

            // act
            var result = await client.Spot.GetAssetsAsync();

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
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((KucoinClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.Spot.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithJsonError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default!,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((KucoinClient)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.Spot.GetAssetsAsync();

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
    }
}
