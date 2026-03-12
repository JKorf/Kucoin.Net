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
        /// ["<c>GTC</c>"] Good until canceled by user
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// ["<c>GTT</c>"] Good until a certain time
        /// </summary>
        [Map("GTT")]
        GoodTillTime,
        /// <summary>
        /// ["<c>IOC</c>"] Immediately has to be (partially) filled or it will be canceled
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>FOK</c>"] Immediately has to be full filled or it will be canceled
        /// </summary>
        [Map("FOK")]
        FillOrKill
    }
}
