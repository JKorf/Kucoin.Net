using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of cancellation
    /// </summary>
    public record KucoinCanceledSymbols
    {
        /// <summary>
        /// The succeeded symbols
        /// </summary>
        [JsonProperty("succeedSymbols")]
        public IEnumerable<string> SucceededSymbols { get; set; } = Array.Empty<string>();
        /// <summary>
        /// The failed symbols
        /// </summary>
        [JsonProperty("failedSymbols")]
        public IEnumerable<KucoinCancelError> FailedSymbols { get; set; } = Array.Empty<KucoinCancelError>();
    }

    /// <summary>
    /// Symbol cancel error info
    /// </summary>
    public record KucoinCancelError
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The error
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;
    }
}
