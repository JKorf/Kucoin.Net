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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kucoin.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;

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

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.kucoin.com/")]
        [TestCase(TradeEnvironmentNames.Testnet, "https://openapi-sandbox.kucoin.com/")]
        [TestCase("", "https://api.kucoin.com/")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Kucoin:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddKucoin(configuration.GetSection("Kucoin"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IKucoinRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Kucoin", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddKucoin(configuration.GetSection("Kucoin"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IKucoinRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.kucoin.com/"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Kucoin:Environment:Name", "test" },
                    { "Kucoin:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddKucoin(configuration.GetSection("Kucoin"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IKucoinRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.kucoin.com/"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:PassPhrase", "222" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:PassPhrase", "111" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddKucoin(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IKucoinRestClient>();
            var socketClient = provider.GetRequiredService<IKucoinSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
