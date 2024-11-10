using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stop order update
    /// </summary>
    public record KucoinStreamFuturesStopOrderUpdate: KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// Stop price type
        /// </summary>
        [JsonConverter(typeof(StopPriceTypeConverter))]
        public StopPriceType StopPriceType { get; set; }

        /// <summary>
        /// Error info if there was an error with the order
        /// </summary>
        public string? Error { get; set; }
    }
}
