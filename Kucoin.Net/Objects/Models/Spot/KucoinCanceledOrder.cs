namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Canceled order
    /// </summary>
    [SerializationModel]
    public record KucoinCanceledOrder
    {
        /// <summary>
        /// ["<c>cancelledOrderId</c>"] Order id of the canceled order
        /// </summary>
        
        [JsonPropertyName("cancelledOrderId")]
        public string CanceledOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOid</c>"] Client order id of the canceled order
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
