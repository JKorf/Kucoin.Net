namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Permission info
    /// </summary>
    public record KucoinUaTransferPermission
    {
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonPropertyName("subUid")]
        public decimal SubUid { get; set; }
        /// <summary>
        /// Sub to sub
        /// </summary>
        [JsonPropertyName("subToSub")]
        public bool SubToSub { get; set; }
    }


}
