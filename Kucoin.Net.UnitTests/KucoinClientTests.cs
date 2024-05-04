using Newtonsoft.Json;
using NUnit.Framework;
using Kucoin.Net.Objects;
using Kucoin.Net.UnitTests.TestImplementations;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Clients;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Net.Http;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.JsonNet;

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

            TestHelpers.SetResponse((KucoinRestClient)client, JsonConvert.SerializeObject(resultObj));

            // act
            var result = await client.SpotApi.ExchangeData.GetAssetsAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.Code == 400001);
            Assert.That(result.Error.Message == "Error occured");
        }

        [TestCase()]
        public async Task ReceivingHttpErrorWithNoJson_Should_ReturnErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetResponse((KucoinRestClient)client, "", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetAssetsAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
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

            TestHelpers.SetResponse((KucoinRestClient)client, JsonConvert.SerializeObject(resultObj), System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetAssetsAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error!.Code == 400001);
            Assert.That(result.Error.Message == "Error occured");
        }

        [Test]
        public void CheckSignatureExample()
        {
            var authProvider = new KucoinAuthenticationProvider(
                new KucoinApiCredentials("5c2db93503aa674c74a31734", "f03a5284-5c39-4aaa-9b20-dea10bdcf8e3", "QWIxMjM0NTY3OCkoKiZeJSQjQA==")
                );
            var client = (RestApiClient)new KucoinRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v1/deposit-addresses",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["KC-API-SIGN"].ToString();
                },
                "7QP/oM0ykidMdrfNEUmng8eZjg/ZvPafjIqmxiVfYu4=",
                new Dictionary<string, object>
                {
                    { "currency", "BTC" }
                },
                time: DateTimeConverter.ConvertFromMilliseconds(1547015186532));
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<KucoinRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<KucoinSocketClient>();
        }
    }
}
