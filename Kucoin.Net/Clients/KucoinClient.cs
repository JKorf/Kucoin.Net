using CryptoExchange.Net;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinClient" />
    public class KucoinClient : BaseRestClient, IKucoinClient
    {
        /// <inheritdoc />
        public IKucoinClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IKucoinClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Create a new instance of KucoinClient using the default options
        /// </summary>
        public KucoinClient() : this(KucoinClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public KucoinClient(KucoinClientOptions options) : base("Kucoin", options)
        { 
            SpotApi = AddApiClient(new KucoinClientSpotApi(log, this, options));
            FuturesApi = AddApiClient(new KucoinClientFuturesApi(log, this, options));
        }

        /// <inheritdoc />
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KucoinClientOptions options)
        {
            KucoinClientOptions.Default = options;
        }
    }
}
