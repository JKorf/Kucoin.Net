using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Time the order is valid for
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// Good until canceled by user
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// Good until a certain time
        /// </summary>
        [Map("GTT")]
        GoodTillTime,
        /// <summary>
        /// Immediately has to be (partially) filled or it will be canceled
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// Immediately has to be full filled or it will be canceled
        /// </summary>
        [Map("FOK")]
        FillOrKill
    }
}
