using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.Options;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc />
    public class KucoinSharedApiClient : SharedApiClientBase, IKucoinSharedApiClient
    {
        /// <inheritdoc />
        public IKucoinRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IKucoinRestClientFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IKucoinSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IKucoinSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSharedApiClient(
            IKucoinRestClient restClient,
            IKucoinSocketClient socketClient,
            IOptions<KucoinOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                  restClient.SpotApi.SharedApi,
                  restClient.FuturesApi.SharedApi,
                  socketClient.SpotApi.SharedApi,
                  socketClient.FuturesApi.SharedApi
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
