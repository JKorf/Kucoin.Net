using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Ids of cancelled orders
    /// </summary>
    [SerializationModel]
    public record KucoinCanceledOrders
    {
        /// <summary>
        /// List of canceled order ids
        /// </summary>
        [JsonPropertyName("cancelledOrderIds")]
        public string[] CancelledOrderIds { get; set; } = Array.Empty<string>();
    }
}
