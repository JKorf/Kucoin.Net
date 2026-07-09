using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Collateral status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CollateralStatus>))]
    public enum CollateralStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Normal
        /// </summary>
        [Map("1")]
        Normal,
        /// <summary>
        /// ["<c>2</c>"] Approaching platform cap
        /// </summary>
        [Map("2")]
        ApproachingPlatformCap,
        /// <summary>
        /// ["<c>3</c>"] Platform cap exceeded
        /// </summary>
        [Map("3")]
        PlatformCapExceeded,
    }
}
