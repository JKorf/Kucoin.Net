using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Migration result
    /// </summary>
    [SerializationModel]
    public record KucoinMigrateResult
    {
        /// <summary>
        /// Ids of user accounts successfully migrated
        /// </summary>
        [JsonPropertyName("successUsers")]
        public string[] SuccessUsers { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Ids of user accounts failed to migrate
        /// </summary>
        [JsonPropertyName("failUsers")]
        public Dictionary<string, string> FailedUsers { get; set; } = new Dictionary<string, string>();
    }
}
