namespace Kucoin.Net.Objects.Models.Spot;

/// <summary>
/// New order id
/// </summary>
    [SerializationModel]
public record KucoinModifiedOrder
{
    /// <summary>
    /// ["<c>newOrderId</c>"] The id of the new order
    /// </summary>
    [JsonPropertyName("newOrderId")]
    public string Id { get; set; } = string.Empty;
}
