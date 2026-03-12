using CryptoExchange.Net.Attributes;


namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopOrderEvent>))]
    public enum StopOrderEvent
    {
        /// <summary>
        /// ["<c>open</c>"] Stop order opened
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>triggered</c>"] Stop order triggered by price
        /// </summary>
        [Map("triggered")]
        Triggered,
        /// <summary>
        /// ["<c>cancel</c>"] Stop order canceled
        /// </summary>
        [Map("cancel")]
        Canceled
    }
}
