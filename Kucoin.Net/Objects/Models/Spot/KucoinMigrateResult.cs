using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Migration result
    /// </summary>
    public record KucoinMigrateResult
    {
        /// <summary>
        /// Ids of user accounts successfully migrated
        /// </summary>
        [JsonPropertyName("successUsers")]
        public IEnumerable<string> SuccessUsers { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Ids of user accounts failed to migrate
        /// </summary>
        [JsonPropertyName("failUsers")]
        public Dictionary<string, string> FailedUsers { get; set; } = new Dictionary<string, string>();
    }
}
