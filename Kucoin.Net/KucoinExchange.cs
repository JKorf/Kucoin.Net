using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net
{
    /// <summary>
    /// Kucoin exchange information and configuration
    /// </summary>
    public static class KucoinExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Kucoin";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.kucoin.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://www.kucoin.com/docs/beginners/introduction"
            };

        /// <summary>
        /// Rate limiter configuration for the Kucoin API
        /// </summary>
        public static KucoinRateLimiters RateLimiter { get; } = new KucoinRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Kucoin API
    /// </summary>
    public class KucoinRateLimiters
    {
        private static readonly Dictionary<VipLevel, int> _spotLimits = new()
        {
            { VipLevel.Vip0, 4000 },
            { VipLevel.Vip1, 6000 },
            { VipLevel.Vip2, 8000 },
            { VipLevel.Vip3, 10000 },
            { VipLevel.Vip4, 13000 },
            { VipLevel.Vip5, 16000 },
            { VipLevel.Vip6, 20000 },
            { VipLevel.Vip7, 23000 },
            { VipLevel.Vip8, 26000 },
            { VipLevel.Vip9, 30000 },
            { VipLevel.Vip10, 33000 },
            { VipLevel.Vip11, 36000 },
            { VipLevel.Vip12, 40000 },
        };

        private static readonly Dictionary<VipLevel, int> _futuresLimits = new()
        {
            { VipLevel.Vip0, 2000 },
            { VipLevel.Vip1, 2000 },
            { VipLevel.Vip2, 4000 },
            { VipLevel.Vip3, 5000 },
            { VipLevel.Vip4, 6000 },
            { VipLevel.Vip5, 7000 },
            { VipLevel.Vip6, 8000 },
            { VipLevel.Vip7, 10000 },
            { VipLevel.Vip8, 12000 },
            { VipLevel.Vip9, 14000 },
            { VipLevel.Vip10, 16000 },
            { VipLevel.Vip11, 18000 },
            { VipLevel.Vip12, 20000 },
        };

        private static readonly Dictionary<VipLevel, int> _managementLimits = new()
        {
            { VipLevel.Vip0, 2000 },
            { VipLevel.Vip1, 2000 },
            { VipLevel.Vip2, 4000 },
            { VipLevel.Vip3, 5000 },
            { VipLevel.Vip4, 6000 },
            { VipLevel.Vip5, 7000 },
            { VipLevel.Vip6, 8000 },
            { VipLevel.Vip7, 10000 },
            { VipLevel.Vip8, 12000 },
            { VipLevel.Vip9, 14000 },
            { VipLevel.Vip10, 16000 },
            { VipLevel.Vip11, 18000 },
            { VipLevel.Vip12, 20000 },
        };

        internal IRateLimitGate SpotRest { get; private set; }
        internal IRateLimitGate FuturesRest { get; private set; }
        internal IRateLimitGate ManagementRest { get; private set; }
        internal IRateLimitGate PublicRest { get; private set; }
        internal IRateLimitGate Socket { get; private set; }

        /// <summary>
        /// The VIP level to use when calculating rate limits
        /// </summary>
        public VipLevel VipLevel { get; private set; } = VipLevel.Vip0;

        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal KucoinRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        /// <summary>
        /// Configure the rate limit with a different VIP level
        /// </summary>
        /// <param name="level"></param>
        public void Configure(VipLevel level)
        {
            VipLevel = level;
            Initialize();
        }

        private void Initialize()
        {
            SpotRest = new RateLimitGate("Spot Rest").AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, Array.Empty<IGuardFilter>(), _spotLimits[VipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.FixedAfterFirst)); // Might be fixed but from the first request timestamp instead of the the whole interval
            FuturesRest = new RateLimitGate("Futures Rest").AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, Array.Empty<IGuardFilter>(), _futuresLimits[VipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.FixedAfterFirst)); // Might be fixed but from the first request timestamp instead of the the whole interval
            ManagementRest = new RateLimitGate("Management Rest").AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, Array.Empty<IGuardFilter>(), _managementLimits[VipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.FixedAfterFirst)); // Might be fixed but from the first request timestamp instead of the the whole interval
            PublicRest = new RateLimitGate("Public Rest").AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, Array.Empty<IGuardFilter>(), 2000, TimeSpan.FromSeconds(30), RateLimitWindowType.FixedAfterFirst)); // Might be fixed but from the first request timestamp instead of the the whole interval
            Socket = new RateLimitGate("Socket")
                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new LimitItemTypeFilter(RateLimitItemType.Connection), 30, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed))
                    .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 100, TimeSpan.FromSeconds(10), RateLimitWindowType.Fixed));

            SpotRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            FuturesRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            ManagementRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            PublicRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Socket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }
    }
}
