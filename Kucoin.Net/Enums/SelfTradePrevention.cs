using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// No self trade prevention
        /// </summary>
        [Map("")]
        None,
        /// <summary>
        /// Decrease the quantity of the existing order by the amount of the new order
        /// </summary>
        [Map("DC")]
        DecreaseAndCancel,
        /// <summary>
        /// Cancel the oldest order
        /// </summary>
        [Map("CO")]
        CancelOldest,
        /// <summary>
        /// Cancel the newest order
        /// </summary>
        [Map("CN")]
        CancelNewest,
        /// <summary>
        /// Cancel both orders
        /// </summary>
        [Map("CB")]
        CancelBoth
    }
}
