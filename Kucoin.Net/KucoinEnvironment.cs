using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin environments
    /// </summary>
    public class KucoinEnvironment: TradeEnvironment
    {
        /// <summary>
        /// Spot API address
        /// </summary>
        public string SpotAddress { get; }

        /// <summary>
        /// Futures API address
        /// </summary>
        public string FuturesAddress { get; }

        internal KucoinEnvironment(string name, string spotAddress, string futuresAddress) : 
            base(name)
        {
            SpotAddress = spotAddress;
            FuturesAddress = futuresAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static KucoinEnvironment Live { get; } = new KucoinEnvironment(TradeEnvironmentNames.Live, KucoinApiAddresses.Default.SpotAddress, KucoinApiAddresses.Default.FuturesAddress);

        /// <summary>
        /// Testnet/sandbox environment
        /// </summary>
        public static KucoinEnvironment Testnet { get; } = new KucoinEnvironment(TradeEnvironmentNames.Testnet, KucoinApiAddresses.TestNet.SpotAddress, KucoinApiAddresses.TestNet.FuturesAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotAddress"></param>
        /// <param name="futuresAddress"></param>
        /// <returns></returns>
        public static KucoinEnvironment CreateCustom(string name, string spotAddress, string futuresAddress)
            => new KucoinEnvironment(name, spotAddress, futuresAddress);
    }
}
