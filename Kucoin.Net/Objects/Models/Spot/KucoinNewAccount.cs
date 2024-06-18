namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New account id
    /// </summary>
    public record KucoinNewAccount
    {
        /// <summary>
        /// The id of the new account
        /// </summary>
        public string Id { get; set; } = string.Empty;
    }
}
