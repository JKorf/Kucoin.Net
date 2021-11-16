using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures.Socket
{
    /// <summary>
    /// Futures stop order update
    /// </summary>
    public class KucoinStreamFuturesStopOrderUpdate: KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter)), JsonProperty("side")]
        public override OrderSide OrderSide { get; set; }
    }
}
