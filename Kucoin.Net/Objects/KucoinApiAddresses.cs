namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Kucoin API addresses
    /// </summary>
    public class KucoinApiAddresses
    {
        /// <summary>
        /// The address used by the KucoinClient for the SPOT API
        /// </summary>
        public string SpotAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the KucoinClient for the futures API
        /// </summary>
        public string FuturesAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the KucoinClient for the Unified trading API
        /// </summary>
        public string UnifiedRestAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the KucoinSocketClient for the spot subscription for the Unified trading API
        /// </summary>
        public string UnifiedSocketPrivateAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the KucoinSocketClient for the spot subscription for the Unified trading API
        /// </summary>
        public string UnifiedSocketSpotAddress { get; set; } = string.Empty;

        /// <summary>
        /// The address used by the KucoinSocketClient for the futures subscription for the Unified trading API
        /// </summary>
        public string UnifiedSocketFuturesAddress { get; set; } = string.Empty;

        /// <summary>
        /// The default addresses to connect to the kucoin.com API
        /// </summary>
        public static KucoinApiAddresses Default = new KucoinApiAddresses
        {
            SpotAddress = "https://api.kucoin.com/",
            FuturesAddress = "https://api-futures.kucoin.com/",
            UnifiedRestAddress = "https://api.kucoin.com/",
            UnifiedSocketSpotAddress = "wss://x-push-spot.kucoin.com",
            UnifiedSocketFuturesAddress = "wss://x-push-futures.kucoin.com",
            UnifiedSocketPrivateAddress = "wss://wsapi-push.kucoin.com"
        };

        /// <summary>
        /// Live environment for Australian users
        /// </summary>
        public static KucoinApiAddresses Australia = new KucoinApiAddresses
        {
            SpotAddress = "https://api.kucoin.com/",
            FuturesAddress = "https://api-futures.kucoin.com/",
            UnifiedRestAddress = "https://api.kucoin.com/",
            UnifiedSocketSpotAddress = "wss://x-push-spot.kucoin.com",
            UnifiedSocketFuturesAddress = "wss://x-push-futures.kucoin.com",
            UnifiedSocketPrivateAddress = "wss://wsapi-push.kucoin.com"
        };

        /// <summary>
        /// Live environment for European users
        /// </summary>
        public static KucoinApiAddresses Europe = new KucoinApiAddresses
        {
            SpotAddress = "https://api.kucoin.eu/",
            FuturesAddress = "https://api-futures.kucoin.eu/",
            UnifiedRestAddress = "https://api.kucoin.eu/",
            UnifiedSocketSpotAddress = "wss://x-push-spot.kucoin.eu",
            UnifiedSocketFuturesAddress = "wss://x-push-futures.kucoin.eu",
            UnifiedSocketPrivateAddress = "wss://wsapi-push.kucoin.eu"
        };
    }
}
