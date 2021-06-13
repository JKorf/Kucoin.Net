using System.Net.Http;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

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
        /// Create new client options
        /// </summary>
        public KucoinClientOptions() : this(null, "https://api.kucoin.com/api/")
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public KucoinClientOptions(HttpClient client) : this(client, "https://api.kucoin.com/api/")
        {
        }

        /// <summary>
        /// Create new client options
        /// </summary>
        /// <param name="apiAddress">Custom API address to use</param>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public KucoinClientOptions(HttpClient? client, string apiAddress) : base(apiAddress)
        {
            HttpClient = client;
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
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The amount of subscriptions that should be made on a single socket connection.
        /// Setting this to a higher number increases subscription speed, but having more subscriptions on a single connection will also increase the amount of traffic on that single connection.
        /// Make sure the socket doesn't overflow, and consider combining multiple symbols on a subscription instead of increasing this number.
        /// </summary>
        public new int SocketSubscriptionsCombineTarget { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientOptions(): base("https://api.kucoin.com/api/") // Real url is retrieved from rest API
        {
            SocketSubscriptionsCombineTarget = 1;
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
        /// ctor
        /// </summary>
        public KucoinOrderBookOptions(int? limit = null) : base("Kucoin", limit == null, false)
        {
            Limit = limit;
        }
    }
}
