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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public KucoinEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Kucoin environment by name
        /// </summary>
        public static KucoinEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             TradeEnvironmentNames.Testnet => Testnet,
             "" => Live,
             null => Live,
             _ => default
         };

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
