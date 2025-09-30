using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.SymbolOrderBooks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IKucoinRestClient and IKucoinSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddKucoin(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new KucoinOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? KucoinEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? KucoinEnvironment.Live.Name;
            options.Rest.Environment = KucoinEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = KucoinEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddKucoinCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IKucoinRestClient and IKucoinSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Kucoin services</param>
        /// <returns></returns>
        public static IServiceCollection AddKucoin(
            this IServiceCollection services,
            Action<KucoinOptions>? optionsDelegate = null)
        {
            var options = new KucoinOptions();
            // Reset environment so we know if they're overridden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? KucoinEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? KucoinEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddKucoinCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddKucoinCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IKucoinRestClient, KucoinRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<KucoinRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new KucoinRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<KucoinRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<KucoinRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options.Proxy, options.HttpKeepAliveInterval);
            });
            services.Add(new ServiceDescriptor(typeof(IKucoinSocketClient), x => { return new KucoinSocketClient(x.GetRequiredService<IOptions<KucoinSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<IKucoinOrderBookFactory, KucoinOrderBookFactory>();
            services.AddTransient<IKucoinTrackerFactory, KucoinTrackerFactory>();
            services.AddTransient<ITrackerFactory, KucoinTrackerFactory>();
            services.AddSingleton<IKucoinUserClientProvider, KucoinUserClientProvider>(x =>
            new KucoinUserClientProvider(
                x.GetRequiredService<HttpClient>(),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<KucoinRestOptions>>(),
                x.GetRequiredService<IOptions<KucoinSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IKucoinRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IKucoinSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IKucoinRestClient>().FuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IKucoinSocketClient>().FuturesApi.SharedClient);

            return services;
        }
    }
}
