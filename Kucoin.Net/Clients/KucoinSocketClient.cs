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
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.Authentication;

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
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public KucoinSocketClient(Action<KucoinSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        [ActivatorUtilitiesConstructor]
        public KucoinSocketClient(IOptions<KucoinSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Kucoin")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new KucoinSocketClientSpotApi(_logger, this, options.Value));
            FuturesApi = AddApiClient(new KucoinSocketClientFuturesApi(_logger, this, options.Value));
        }

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            SpotApi.SetOptions(options);
            FuturesApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<KucoinSocketOptions> optionsDelegate)
        {
            KucoinSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }
    }
}
