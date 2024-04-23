using CryptoExchange.Net.Clients;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinRestClient" />
    public class KucoinRestClient : BaseRestClient, IKucoinRestClient
    {
        /// <inheritdoc />
        public IKucoinRestClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IKucoinRestClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Create a new instance of KucoinClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public KucoinRestClient(Action<KucoinRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public KucoinRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<KucoinRestOptions>? optionsDelegate = null) : base(loggerFactory, "Kucoin")
        {
            var options = KucoinRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new KucoinRestClientSpotApi(_logger, httpClient, this, options));
            FuturesApi = AddApiClient(new KucoinRestClientFuturesApi(_logger, httpClient, this, options));
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
        /// <param name="optionsFunc">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<KucoinRestOptions> optionsFunc)
        {
            var options = KucoinRestOptions.Default.Copy();
            optionsFunc(options);
            KucoinRestOptions.Default = options;
        }
    }
}
