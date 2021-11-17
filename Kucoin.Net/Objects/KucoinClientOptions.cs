using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Interfaces.Clients.Socket;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Options for the KucoinClient
    /// </summary>
    public class KucoinClientSpotOptions: RestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinClientSpotOptions Default { get; set; } = new KucoinClientSpotOptions()
        {
            BaseAddress = "https://api.kucoin.com/api/",
            RateLimiters = new List<IRateLimiter>
            {
                new RateLimiter()
                    .AddPartialEndpointLimit("/api/v1/orders", 180, TimeSpan.FromSeconds(3), null, true, true)
                    .AddApiKeyLimit(200, TimeSpan.FromSeconds(10), true, true)
                    .AddTotalRateLimit(100, TimeSpan.FromSeconds(10))
            }
        };

        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }
        
        /// <summary>
        /// ctor
        /// </summary>
        public KucoinClientSpotOptions()
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
        public new void Copy<T>(T input, T def) where T : KucoinClientSpotOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();            
        }
    }

    /// <summary>
    /// Options for the KucoinClient
    /// </summary>
    public class KucoinClientFuturesOptions : RestClientOptions
    {
        /// <summary>
        /// Default options for the futures client
        /// </summary>
        public static KucoinClientFuturesOptions Default { get; set; } = new KucoinClientFuturesOptions()
        {
            BaseAddress = "https://api-futures.kucoin.com/api/"
        };

        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinClientFuturesOptions()
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
        public new void Copy<T>(T input, T def) where T : KucoinClientFuturesOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class KucoinSocketClientSpotOptions: SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinSocketClientSpotOptions Default { get; set; } = new KucoinSocketClientSpotOptions() {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// The spot api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientSpotOptions()
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
        public new void Copy<T>(T input, T def) where T : KucoinSocketClientSpotOptions
        {
            base.Copy(input, def);

            input.ApiCredentials = (KucoinApiCredentials?)def.ApiCredentials?.Copy();
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
    public class KucoinOrderBookSpotOptions : OrderBookOptions
    {
        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IKucoinSocketClientSpot? SocketClient { get; set; }

        /// <summary>
        /// The client to use for the initial order book request
        /// </summary>
        public IKucoinClientSpot? RestClient { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The client to use for the initial order book request.</param>
        /// <param name="limit">Max amount of rows for the book</param>
        public KucoinOrderBookSpotOptions(IKucoinSocketClientSpot? socketClient = null, IKucoinClientSpot? restClient = null, int? limit = null)
        {
            Limit = limit;
            SocketClient = socketClient;
            RestClient = restClient;
        }
    }

    /// <summary>
    /// Options for the KucoinSymbolOrderBook
    /// </summary>
    public class KucoinOrderBookFuturesOptions : OrderBookOptions
    {
        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IKucoinSocketClientFutures? SocketClient { get; set; }

        /// <summary>
        /// The client to use for the initial order book request
        /// </summary>
        public IKucoinClientFutures? RestClient { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The client to use for the initial order book request.</param>
        /// <param name="limit">Max amount of rows for the book</param>
        public KucoinOrderBookFuturesOptions(IKucoinSocketClientFutures? socketClient = null, IKucoinClientFutures? restClient = null, int? limit = null)
        {
            Limit = limit;
            SocketClient = socketClient;
            RestClient = restClient;
        }
    }
}
