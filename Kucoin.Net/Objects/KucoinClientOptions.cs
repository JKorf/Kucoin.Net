using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Interfaces.Clients.Socket;

namespace Kucoin.Net.Objects
{
    public class KucoinRestSubClientOptions: RestSubClientOptions
    {
        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        public new void Copy<T>(T input, T def) where T : KucoinRestSubClientOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Options for the KucoinClient
    /// </summary>
    public class KucoinClientOptions: RestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinClientOptions Default { get; set; } = new KucoinClientOptions()
        {
            OptionsSpot = new KucoinRestSubClientOptions
            {
                BaseAddress = "https://api.kucoin.com/api/",
                RateLimiters = new List<IRateLimiter>
                {
                    new RateLimiter()
                        .AddPartialEndpointLimit("/api/v1/orders", 180, TimeSpan.FromSeconds(3), null, true, true)
                        .AddApiKeyLimit(200, TimeSpan.FromSeconds(10), true, true)
                        .AddTotalRateLimit(100, TimeSpan.FromSeconds(10))
                }
            },
            OptionsFutures = new KucoinRestSubClientOptions
            {
                BaseAddress = "https://api-futures.kucoin.com/api/"
            }
        };

        public KucoinRestSubClientOptions OptionsSpot { get; set; }
        public KucoinRestSubClientOptions OptionsFutures { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : KucoinClientOptions
        {
            base.Copy(input, def);

            input.OptionsSpot = new KucoinRestSubClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);

            input.OptionsFutures = new KucoinRestSubClientOptions();
            def.OptionsFutures.Copy(input.OptionsFutures, def.OptionsFutures);
        }
    }

    public class KucoinSocketSubClientOptions : SocketSubClientOptions
    {
        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        public new void Copy<T>(T input, T def) where T : KucoinSocketSubClientOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class KucoinSocketClientOptions: SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinSocketClientOptions Default { get; set; } = new KucoinSocketClientOptions() {
            // Base address is requested from REST API
            OptionsSpot = new KucoinSocketSubClientOptions { },
            OptionsFutures = new KucoinSocketSubClientOptions { },
            SocketSubscriptionsCombineTarget = 10
        };

        public KucoinSocketSubClientOptions OptionsSpot { get; set; }
        public KucoinSocketSubClientOptions OptionsFutures { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : KucoinSocketClientOptions
        {
            base.Copy(input, def);

            input.OptionsSpot = new KucoinSocketSubClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);

            input.OptionsFutures = new KucoinSocketSubClientOptions();
            def.OptionsFutures.Copy(input.OptionsFutures, def.OptionsFutures);
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class KucoinSocketClientFuturesOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the futures client
        /// </summary>
        public static KucoinSocketClientFuturesOptions Default { get; set; } = new KucoinSocketClientFuturesOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// The spot api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientFuturesOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : KucoinSocketClientFuturesOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Options for the KucoinSymbolOrderBook
    /// </summary>
    public class KucoinOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IKucoinSocketClient? SocketClient { get; set; }

        /// <summary>
        /// The client to use for the initial order book request
        /// </summary>
        public IKucoinClient? RestClient { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The client to use for the initial order book request.</param>
        /// <param name="limit">Max amount of rows for the book</param>
        public KucoinOrderBookOptions(IKucoinSocketClient? socketClient = null, IKucoinClient? restClient = null, int? limit = null)
        {
            Limit = limit;
            SocketClient = socketClient;
            RestClient = restClient;
        }
    }
}
