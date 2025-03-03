using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Migration status
    /// </summary>
    public record KucoinMigrateStatus
    {
        /// <summary>
        /// Status of migration
        /// </summary>
        [JsonPropertyName("status")]
        public MigrateStatus Status { get; set; }
    }
}
