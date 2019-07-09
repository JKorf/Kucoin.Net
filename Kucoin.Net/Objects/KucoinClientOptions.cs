using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Kucoin.Net.Objects
{
    public class KucoinClientOptions: RestClientOptions
    {
        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials ApiCredentials { get; set; }

        public KucoinClientOptions()
        {
            BaseAddress = "https://api.kucoin.com/api/";
        }

        public KucoinClientOptions Copy()
        {
            var copy = Copy<KucoinClientOptions>();
            if (ApiCredentials != null)
                copy.ApiCredentials = new KucoinApiCredentials(ApiCredentials.Key.GetString(), ApiCredentials.Secret.GetString(), ApiCredentials.PassPhrase.GetString());
            return copy;
        }
    }

    public class KucoinSocketClientOptions: SocketClientOptions
    {
        /// <summary>
        /// The api credentials
        /// </summary>
        public new KucoinApiCredentials ApiCredentials { get; set; }

        /// <summary>
        /// The amount of subscriptions that should be made on a single socket connection.
        /// Setting this to a higher number increases subscription speed, but having more subscriptions on a single connection will also increase the amount of traffic on that single connection.
        /// Make sure the socket doesn't overflow, and consider combining multiple markets on a subscription instead of increasing this number.
        /// </summary>
        public new int SocketSubscriptionsCombineTarget { get; set; }


        public KucoinSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 1;
        }

        public KucoinSocketClientOptions Copy()
        {
            var copy = Copy<KucoinSocketClientOptions>();
            copy.SocketSubscriptionsCombineTarget = SocketSubscriptionsCombineTarget;
            if (ApiCredentials != null)
                copy.ApiCredentials = new KucoinApiCredentials(ApiCredentials.Key.GetString(), ApiCredentials.Secret.GetString(), ApiCredentials.PassPhrase.GetString());
            return copy;
        }
    }

    public class KucoinOrderBookOptions : OrderBookOptions
    {
        public KucoinOrderBookOptions() : base("Kucoin", true)
        {
        }
    }
}
