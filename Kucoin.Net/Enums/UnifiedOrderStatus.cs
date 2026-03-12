using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnifiedOrderStatus>))]
    public enum UnifiedOrderStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Not triggered
        /// </summary>
        [Map("0")]
        NotTriggered,
        /// <summary>
        /// ["<c>1</c>"] Triggered
        /// </summary>
        [Map("1")]
        Triggered,
        /// <summary>
        /// ["<c>2</c>"] Live
        /// </summary>
        [Map("2")]
        Live,
        /// <summary>
        /// ["<c>3</c>"] Filled
        /// </summary>
        [Map("3")]
        Filled,
        /// <summary>
        /// ["<c>4</c>"] Partially filled
        /// </summary>
        [Map("4")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>5</c>"] Canceled
        /// </summary>
        [Map("5")]
        Canceled,
        /// <summary>
        /// ["<c>6</c>"] Partially canceled
        /// </summary>
        [Map("6")]
        PartiallyCanceled
    }
}
