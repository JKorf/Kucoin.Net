using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc />
    public class KucoinUserClientProvider : UserClientProvider<
        IKucoinRestClient,
        IKucoinSocketClient,
        KucoinRestOptions,
        KucoinSocketOptions,
        KucoinCredentials,
        KucoinEnvironment
        >, IKucoinUserClientProvider
    {
        /// <inheritdoc/>
        public override string ExchangeName => KucoinExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public KucoinUserClientProvider(Action<KucoinOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<KucoinRestOptions> restOptions,
            IOptions<KucoinSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IKucoinRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<KucoinRestOptions> options)
            => new KucoinRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IKucoinSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<KucoinSocketOptions> options)
            => new KucoinSocketClient(options, loggerFactory);
    }
}
