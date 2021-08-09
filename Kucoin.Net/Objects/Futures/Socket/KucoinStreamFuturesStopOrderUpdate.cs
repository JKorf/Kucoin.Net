using Kucoin.Net.Converters;
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
        public override KucoinOrderSide OrderSide { get; set; }
    }
}
