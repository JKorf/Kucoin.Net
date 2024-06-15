using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot;

/// <summary>
/// New order id
/// </summary>
public class KucoinModifiedOrder
{
    /// <summary>
    /// The id of the new order
    /// </summary>
    [JsonProperty("newOrderId")]
    public string Id { get; set; } = string.Empty;
}
