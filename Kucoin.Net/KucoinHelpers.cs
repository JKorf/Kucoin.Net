using CryptoExchange.Net.Clients;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.SymbolOrderBooks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Kucoin.Net
{
    /// <summary>
    /// Helpers for Kucoin
    /// </summary>
    public static class KucoinHelpers
    {
        /// <summary>
        /// Add the IKucoinClient and IKucoinSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IKucoinSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddKucoin(
            this IServiceCollection services, 
            Action<KucoinRestOptions>? defaultRestOptionsDelegate = null,
            Action<KucoinSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = KucoinRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                KucoinRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                KucoinSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IKucoinRestClient, KucoinRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddTransient<ICryptoSocketClient, CryptoSocketClient>();
            services.AddSingleton<IKucoinOrderBookFactory, KucoinOrderBookFactory>();
            services.AddTransient(x => x.GetRequiredService<IKucoinRestClient>().SpotApi.CommonSpotClient);
            services.AddTransient(x => x.GetRequiredService<IKucoinRestClient>().FuturesApi.CommonFuturesClient);
            if (socketClientLifeTime == null)
                services.AddSingleton<IKucoinSocketClient, KucoinSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IKucoinSocketClient), typeof(KucoinSocketClient), socketClientLifeTime.Value));
            return services;
        }

        /// <summary>
        /// Validate the string is a valid Kucoin symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static void ValidateKucoinSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if (!Regex.IsMatch(symbolString, "^((([A-Z]|[0-9]){1,})[-](([A-Z]|[0-9]){1,}))$"))
                throw new ArgumentException($"{symbolString} is not a valid Kucoin symbol. Should be [BaseAsset]-[QuoteAsset] e.g. ETH-BTC");
        }
    }
}
