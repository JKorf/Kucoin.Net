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

        /// <summary>
        /// Unified API address
        /// </summary>
        public string UnifiedAddress { get; }

        internal KucoinEnvironment(string name, string spotAddress, string futuresAddress, string unifiedAddress) : 
            base(name)
        {
            SpotAddress = spotAddress;
            FuturesAddress = futuresAddress;
            UnifiedAddress = unifiedAddress;
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
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static KucoinEnvironment Live { get; } = new KucoinEnvironment(
            TradeEnvironmentNames.Live,
            KucoinApiAddresses.Default.SpotAddress, 
            KucoinApiAddresses.Default.FuturesAddress,
            KucoinApiAddresses.Default.UnifiedAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static KucoinEnvironment CreateCustom(string name, string spotAddress, string futuresAddress, string unifiedAddress)
            => new KucoinEnvironment(name, spotAddress, futuresAddress, unifiedAddress);
    }
}
