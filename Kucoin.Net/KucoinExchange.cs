using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net
{
    public static class KucoinExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Kucoin";

        /// <summary>
        /// Rate limiter for the Kucoin API
        /// </summary>
        public static IKucoinAccessLimiter RateLimiter => RateLimiters;

        /// <summary>   
        /// Format 2 assets to form a symbol accepted by the API
        /// </summary>
        /// <param name="baseAsset">The base asset</param>
        /// <param name="quoteAsset">The quote asset</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

        internal static KucoinRateLimiters RateLimiters { get; } = new KucoinRateLimiters();
    }

    public interface IKucoinAccessLimiter : IAccessLimiter
    {
        void Configure(VipLevel level);
    }

    internal class KucoinRateLimiters : IKucoinAccessLimiter
    {
        private VipLevel _vipLevel = VipLevel.Vip0;
        private static readonly Dictionary<VipLevel, int> _spotLimits = new Dictionary<VipLevel, int>
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
        private static readonly Dictionary<VipLevel, int> _futuresLimits = new Dictionary<VipLevel, int>
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
        private static readonly Dictionary<VipLevel, int> _managementLimits = new Dictionary<VipLevel, int>
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

        public event Action<RateLimitEvent> RateLimitTriggered
        {
            add
            {
                SpotRest.RateLimitTriggered += value;
                FuturesRest.RateLimitTriggered += value;
                ManagementRest.RateLimitTriggered += value;
                PublicRest.RateLimitTriggered += value;
                Socket.RateLimitTriggered += value;
            }
            remove
            {
                SpotRest.RateLimitTriggered -= value;
                FuturesRest.RateLimitTriggered -= value;
                ManagementRest.RateLimitTriggered -= value;
                PublicRest.RateLimitTriggered -= value;
                Socket.RateLimitTriggered -= value;
            }
        }

        public void Configure(VipLevel level)
        {
            _vipLevel = level;
            Initialize();
        }

        public KucoinRateLimiters()
        {
            Initialize();
        }

        public void Initialize()
        {
            SpotRest = new RateLimitGate("Spot Rest").AddGuard(new RateLimitGuard((def, host, key) => host, Array.Empty<IGuardFilter>(), _spotLimits[_vipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.Fixed)); // Might be fixed but from the first request timestamp instead of the the whole interval
            FuturesRest = new RateLimitGate("Futures Rest").AddGuard(new RateLimitGuard((def, host, key) => host, Array.Empty<IGuardFilter>(), _futuresLimits[_vipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.Fixed)); // Might be fixed but from the first request timestamp instead of the the whole interval
            ManagementRest = new RateLimitGate("Management Rest").AddGuard(new RateLimitGuard((def, host, key) => host, Array.Empty<IGuardFilter>(), _managementLimits[_vipLevel], TimeSpan.FromSeconds(30), RateLimitWindowType.Fixed)); // Might be fixed but from the first request timestamp instead of the the whole interval
            PublicRest = new RateLimitGate("Public Rest").AddGuard(new RateLimitGuard((def, host, key) => host, Array.Empty<IGuardFilter>(), 2000, TimeSpan.FromSeconds(30), RateLimitWindowType.Fixed)); // Might be fixed but from the first request timestamp instead of the the whole interval
            Socket = new RateLimitGate("Socket")
                    .AddGuard(new RateLimitGuard((def, host, key) => host, new LimitItemTypeFilter(RateLimitItemType.Connection), 30, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed))
                    .AddGuard(new RateLimitGuard((def, host, key) => def.Path, new LimitItemTypeFilter(RateLimitItemType.Request), 100, TimeSpan.FromMinutes(10), RateLimitWindowType.Fixed));
        }

        public IRateLimitGate SpotRest { get; private set; } 
        public IRateLimitGate FuturesRest { get; private set; } 
        public IRateLimitGate ManagementRest { get; private set; } 
        public IRateLimitGate PublicRest { get; private set; } 
        public IRateLimitGate Socket { get; private set; } 
    }
}
