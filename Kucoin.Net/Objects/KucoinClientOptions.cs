using System.Net.Http;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Options for the KucoinClient
    /// </summary>
    public class KucoinClientOptions: RestClientOptions
    {
        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }
        
        /// <summary>
        /// The futures api credentials
        /// </summary>
        public KucoinApiCredentials? FuturesApiCredentials { get; set; }

        /// <summary>
        /// Base address for the futures API
        /// </summary>
        public string FuturesBaseAddress { get; set; }

        /// <summary>
        /// Create new client options
        /// </summary>
        public KucoinClientOptions() : this(null, "https://api.kucoin.com/api/", "https://api-futures.kucoin.com/api/")
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public KucoinClientOptions(HttpClient client) : this(client, "https://api.kucoin.com/api/", "https://api-futures.kucoin.com/api/")
        {
        }

        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="addresses">The base addresses to use</param>
        public KucoinClientOptions(KucoinApiAddresses addresses) : this(null, addresses.SpotAddress, addresses.FuturesAddress)
        {
        }


        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="addresses">The base addresses to use</param>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public KucoinClientOptions(KucoinApiAddresses addresses, HttpClient client) : this(client, addresses.SpotAddress, addresses.FuturesAddress)
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="spotApiAddress">Custom API address to use for the spot API</param>
        /// <param name="futuresApiAddress">Custom API address to use for the futures API</param>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public KucoinClientOptions(HttpClient? client, string spotApiAddress, string futuresApiAddress) : base(spotApiAddress)
        {
            HttpClient = client;
            FuturesBaseAddress = futuresApiAddress;
        }

        /// <summary>
        /// Make a copy of the options
        /// </summary>
        /// <returns></returns>
        public KucoinClientOptions Copy()
        {
            var copy = Copy<KucoinClientOptions>();
            if (ApiCredentials != null)
                copy.ApiCredentials = new KucoinApiCredentials(ApiCredentials.Key!.GetString(), ApiCredentials.Secret!.GetString(), ApiCredentials.PassPhrase.GetString());
            return copy;
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class KucoinSocketClientOptions: SocketClientOptions
    {
        /// <summary>
        /// The spot api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The futures api credentials
        /// </summary>
        public KucoinApiCredentials? FuturesApiCredentials { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientOptions(): base("https://api.kucoin.com/api/") // Real url is retrieved from rest API
        {
            SocketSubscriptionsCombineTarget = 10;
        }

        /// <summary>
        /// Create a copy of the options
        /// </summary>
        /// <returns></returns>
        public KucoinSocketClientOptions Copy()
        {
            var copy = Copy<KucoinSocketClientOptions>();
            copy.SocketSubscriptionsCombineTarget = SocketSubscriptionsCombineTarget;
            if (ApiCredentials != null)
                copy.ApiCredentials = new KucoinApiCredentials(ApiCredentials.Key!.GetString(), ApiCredentials.Secret!.GetString(), ApiCredentials.PassPhrase.GetString());
            return copy;
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
        public KucoinOrderBookOptions(IKucoinSocketClient? socketClient = null, IKucoinClient? restClient = null, int? limit = null) : base("Kucoin", limit == null, false)
        {
            Limit = limit;
            SocketClient = socketClient;
            RestClient = restClient;
        }
    }
}
