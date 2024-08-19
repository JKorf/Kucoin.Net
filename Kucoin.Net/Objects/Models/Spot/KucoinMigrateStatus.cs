using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        [JsonProperty("status")]
        public MigrateStatus Status { get; set; }
    }
}
