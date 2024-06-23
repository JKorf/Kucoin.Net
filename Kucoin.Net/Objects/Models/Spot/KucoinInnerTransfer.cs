namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Sub transfer info
    /// </summary>
    public record KucoinInnerTransfer
    {
        /// <summary>
        /// The id of the new sub transfer
        /// </summary>
        public string OrderId { get; set; } = string.Empty;
    }
}
