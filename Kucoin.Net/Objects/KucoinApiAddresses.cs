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
        public string UnifiedAddress { get; set; } = string.Empty;

        /// <summary>
        /// The default addresses to connect to the kucoin.com API
        /// </summary>
        public static KucoinApiAddresses Default = new KucoinApiAddresses
        {
            SpotAddress = "https://api.kucoin.com/",
            FuturesAddress = "https://api-futures.kucoin.com/",
            UnifiedAddress = "https://api.kucoin.com/",
        };
    }
}
