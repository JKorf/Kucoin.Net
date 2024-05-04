using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Moq;

namespace Kucoin.Net.UnitTests.TestImplementations
{
    public class TestHelpers
    {
        public static IKucoinRestClient CreateClient(Action<KucoinRestOptions> options = null)
        {
            IKucoinRestClient client;
            client = options != null ? new KucoinRestClient(options) : new KucoinRestClient();
            client.SpotApi.RequestFactory = Mock.Of<IRequestFactory>();
            client.FuturesApi.RequestFactory = Mock.Of<IRequestFactory>();
            return client;
        }

        public static void SetResponse(KucoinRestClient client, string responseData, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.StatusCode).Returns(statusCode);
            response.Setup(c => c.IsSuccessStatusCode).Returns(statusCode == HttpStatusCode.OK);
            response.Setup(c => c.GetResponseStreamAsync()).Returns(Task.FromResult((Stream)responseStream));

            var request = new Mock<IRequest>();
            request.Setup(c => c.Uri).Returns(new Uri("http://www.test.com"));
            request.Setup(c => c.GetResponseAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(response.Object));
            request.Setup(c => c.GetHeaders()).Returns(new Dictionary<string, IEnumerable<string>>());

            var factory = Mock.Get(client.SpotApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);

            factory = Mock.Get(client.FuturesApi.RequestFactory);
            factory.Setup(c => c.Create(It.IsAny<HttpMethod>(), It.IsAny<Uri>(), It.IsAny<int>()))
                .Returns(request.Object);
        }
    }
}
