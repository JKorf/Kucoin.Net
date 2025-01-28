namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New withdrawal id
    /// </summary>
    public record KucoinNewWithdrawal
    {
        /// <summary>
        /// The id of the new withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalId")]
        public string WithdrawalId { get; set; } = string.Empty;
    }
}
