using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot.Socket
{
    /// <summary>
    /// Stop order update
    /// </summary>
    public class KucoinStreamStopOrderUpdate: KucoinStreamStopOrderUpdateBase
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public override KucoinOrderSide OrderSide { get; set; }
    }
}
