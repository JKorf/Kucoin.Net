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
        public string UnifiedRestAddress { get; }

        /// <summary>
        /// Unified websocket API private address
        /// </summary>
        public string UnifiedSocketPrivateAddress { get; }

        /// <summary>
        /// Unified websocket API spot address
        /// </summary>
        public string UnifiedSocketSpotAddress { get; }

        /// <summary>
        /// Unified websocket API futures address
        /// </summary>
        public string UnifiedSocketFuturesAddress { get; }

        internal KucoinEnvironment(
            string name,
            string spotAddress,
            string futuresAddress,
            string unifiedRestAddress,
            string unifiedSocketPrivateAddress,
            string unifiedSocketSpotAddress,
            string unifiedSocketFuturesAddress) : 
            base(name)
        {
            SpotAddress = spotAddress;
            FuturesAddress = futuresAddress;
            UnifiedRestAddress = unifiedRestAddress;
            UnifiedSocketPrivateAddress = unifiedSocketPrivateAddress;
            UnifiedSocketSpotAddress = unifiedSocketSpotAddress;
            UnifiedSocketFuturesAddress = unifiedSocketFuturesAddress;
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
             "Europe" => Europe,
             "Australia" => Australia,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name, Australia.Name, Europe.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static KucoinEnvironment Live { get; } = new KucoinEnvironment(
            TradeEnvironmentNames.Live,
            KucoinApiAddresses.Default.SpotAddress, 
            KucoinApiAddresses.Default.FuturesAddress,
            KucoinApiAddresses.Default.UnifiedRestAddress,
            KucoinApiAddresses.Default.UnifiedSocketPrivateAddress,
            KucoinApiAddresses.Default.UnifiedSocketSpotAddress,
            KucoinApiAddresses.Default.UnifiedSocketFuturesAddress);

        /// <summary>
        /// Australian live environment
        /// </summary>
        public static KucoinEnvironment Australia { get; } = new KucoinEnvironment(
            "Australia",
            KucoinApiAddresses.Australia.SpotAddress,
            KucoinApiAddresses.Australia.FuturesAddress,
            KucoinApiAddresses.Australia.UnifiedRestAddress,
            KucoinApiAddresses.Australia.UnifiedSocketPrivateAddress,
            KucoinApiAddresses.Australia.UnifiedSocketSpotAddress,
            KucoinApiAddresses.Australia.UnifiedSocketFuturesAddress);

        /// <summary>
        /// European live environment
        /// </summary>
        public static KucoinEnvironment Europe { get; } = new KucoinEnvironment(
            "Europe",
            KucoinApiAddresses.Europe.SpotAddress,
            KucoinApiAddresses.Europe.FuturesAddress,
            KucoinApiAddresses.Europe.UnifiedRestAddress,
            KucoinApiAddresses.Europe.UnifiedSocketPrivateAddress,
            KucoinApiAddresses.Europe.UnifiedSocketSpotAddress,
            KucoinApiAddresses.Europe.UnifiedSocketFuturesAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static KucoinEnvironment CreateCustom(
            string name,
            string spotAddress,
            string futuresAddress, 
            string unifiedRestAddress, 
            string unifiedSocketPrivateAddress,
            string unifiedSocketSpotAddress,
            string unifiedSocketFuturesAddress)
            => new KucoinEnvironment(name, spotAddress, futuresAddress, unifiedRestAddress, unifiedSocketPrivateAddress, unifiedSocketSpotAddress, unifiedSocketFuturesAddress);
    }
}
