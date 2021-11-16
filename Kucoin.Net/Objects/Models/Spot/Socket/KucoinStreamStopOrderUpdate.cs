using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot.Socket
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
        public override OrderSide OrderSide { get; set; }
    }
}
