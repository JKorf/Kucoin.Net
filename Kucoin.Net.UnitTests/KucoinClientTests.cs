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

namespace Kucoin.Net.UnitTests
{
    [TestFixture]
    public class KucoinClientTests
    {
        [TestCase()]
        public void TestConversions()
        {
            var ignoreMethods = new []{"GetServerTime", "GetFiatPrices"};
            var defaultParameterValues = new Dictionary<string, object>
            {
                { "symbol", "ETH-BTC" },
                { "symbols", new [] { "ETH-BTC" }},
                { "pageSize", 10 },
                { "funds", null },
                { "limit", 20 }
            };

            var methods = typeof(KucoinClient).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var callResultMethods = methods.Where(m => m.ReturnType.IsGenericType && m.ReturnType.GetGenericTypeDefinition() == typeof(WebCallResult<>));
            foreach (var method in callResultMethods)
            {
                if (ignoreMethods.Contains(method.Name))
                    continue;

                var expectedType = method.ReturnType.GetGenericArguments()[0];
                var expected = typeof(TestHelpers).GetMethod("CreateObjectWithTestParameters").MakeGenericMethod(expectedType).Invoke(null, null);
                var parameters = TestHelpers.CreateParametersForMethod(method, defaultParameterValues);
                var client = TestHelpers.CreateResponseClient(SerializeExpected(expected), new KucoinClientOptions(){ ApiCredentials = new KucoinApiCredentials("Test", "Test", "Test") });

                // act
                var result = method.Invoke(client, parameters);
                var callResult = result.GetType().GetProperty("Success").GetValue(result);
                var data = result.GetType().GetProperty("Data").GetValue(result);

                // assert
                Assert.AreEqual(true, callResult);
                Assert.IsTrue(TestHelpers.AreEqual(expected, data), method.Name);
            }
        }

        public string SerializeExpected<T>(T data)
        {
            var result = new KucoinResult<T>()
            {
                Code = 200000,
                Data = data,
                Message = null
            };

            return JsonConvert.SerializeObject(result);
        }

        [TestCase()]
        public void ReceivingError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((RestClient)client, JsonConvert.SerializeObject(resultObj));

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 400001);
            Assert.IsTrue(result.Error.Message == "Error occured");
        }

        [TestCase()]
        public void ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((RestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
        }

        [TestCase()]
        public void ReceivingHttpErrorWithJsonError_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            var resultObj = new KucoinResult<object>()
            {
                Code = 400001,
                Data = default,
                Message = "Error occured"
            };

            TestHelpers.SetResponse((RestClient)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 400001);
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
