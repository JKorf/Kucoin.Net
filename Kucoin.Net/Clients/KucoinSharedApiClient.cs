using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc />
    public class KucoinSharedApiClient : IKucoinSharedApiClient
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
            IKucoinSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
