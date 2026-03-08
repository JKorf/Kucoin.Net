namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Permission info
    /// </summary>
    public record KucoinUaTransferPermission
    {
        /// <summary>
        /// ["<c>subUid</c>"] Sub uid
        /// </summary>
        [JsonPropertyName("subUid")]
        public decimal SubUid { get; set; }
        /// <summary>
        /// ["<c>subToSub</c>"] Sub to sub
        /// </summary>
        [JsonPropertyName("subToSub")]
        public bool SubToSub { get; set; }
    }


}
