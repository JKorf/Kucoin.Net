using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Self trade prevention types
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePrevention>))]
    public enum SelfTradePrevention
    {
        /// <summary>
        /// ["<c></c>"] No self trade prevention
        /// </summary>
        [Map("")]
        None,
        /// <summary>
        /// ["<c>DC</c>"] Decrease the quantity of the existing order by the amount of the new order
        /// </summary>
        [Map("DC")]
        DecreaseAndCancel,
        /// <summary>
        /// ["<c>CO</c>"] Cancel the oldest order
        /// </summary>
        [Map("CO")]
        CancelOldest,
        /// <summary>
        /// ["<c>CN</c>"] Cancel the newest order
        /// </summary>
        [Map("CN")]
        CancelNewest,
        /// <summary>
        /// ["<c>CB</c>"] Cancel both orders
        /// </summary>
        [Map("CB")]
        CancelBoth
    }
}
