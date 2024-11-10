using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Clients;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinSocketClient" />
    public class KucoinSocketClient : BaseSocketClient, IKucoinSocketClient
    {
        #region Api clients

        /// <inheritdoc />
        public IKucoinSocketClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IKucoinSocketClientFuturesApi FuturesApi { get; }

        #endregion
        /// <summary>
        /// Create a new instance of the OKXSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger</param>
        public KucoinSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public KucoinSocketClient(Action<KucoinSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        [ActivatorUtilitiesConstructor]
        public KucoinSocketClient(Action<KucoinSocketOptions>? optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Kucoin")
        {
            var options = KucoinSocketOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new KucoinSocketClientSpotApi(_logger, this, options));
            FuturesApi = AddApiClient(new KucoinSocketClientFuturesApi(_logger, this, options));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<KucoinSocketOptions> optionsDelegate)
        {
            var options = KucoinSocketOptions.Default.Copy();
            optionsDelegate(options);
            KucoinSocketOptions.Default = options;
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }
    }
}
