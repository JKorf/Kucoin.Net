using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Collections.Generic;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc />
    public class KucoinUserClientProvider : IKucoinUserClientProvider
    {
        private static ConcurrentDictionary<string, IKucoinRestClient> _restClients = new ConcurrentDictionary<string, IKucoinRestClient>();
        private static ConcurrentDictionary<string, IKucoinSocketClient> _socketClients = new ConcurrentDictionary<string, IKucoinSocketClient>();

        private readonly IOptions<KucoinRestOptions> _restOptions;
        private readonly IOptions<KucoinSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, KucoinEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IKucoinRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, KucoinEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IKucoinSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, KucoinEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IKucoinRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, KucoinEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new KucoinRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IKucoinSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, KucoinEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new KucoinSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<KucoinRestOptions> SetRestEnvironment(KucoinEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new KucoinRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<KucoinSocketOptions> SetSocketEnvironment(KucoinEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new KucoinSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
