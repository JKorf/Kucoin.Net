using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of cancellation
    /// </summary>
    [SerializationModel]
    public record KucoinCanceledSymbols
    {
        /// <summary>
        /// The succeeded symbols
        /// </summary>
        [JsonPropertyName("succeedSymbols")]
        public string[] SucceededSymbols { get; set; } = Array.Empty<string>();
        /// <summary>
        /// The failed symbols
        /// </summary>
        [JsonPropertyName("failedSymbols")]
        public KucoinCancelError[] FailedSymbols { get; set; } = Array.Empty<KucoinCancelError>();
    }

    /// <summary>
    /// Symbol cancel error info
    /// </summary>
    [SerializationModel]
    public record KucoinCancelError
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The error
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; } = string.Empty;
    }
}
