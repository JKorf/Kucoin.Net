namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New withdrawal id
    /// </summary>
    [SerializationModel]
    public record KucoinNewWithdrawal
    {
        /// <summary>
        /// ["<c>withdrawalId</c>"] The id of the new withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalId")]
        public string WithdrawalId { get; set; } = string.Empty;
    }
}
