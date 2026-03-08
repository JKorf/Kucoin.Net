using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of cancellation
    /// </summary>
    [SerializationModel]
    public record KucoinCanceledSymbols
    {
        /// <summary>
        /// ["<c>succeedSymbols</c>"] The succeeded symbols
        /// </summary>
        [JsonPropertyName("succeedSymbols")]
        public string[] SucceededSymbols { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>failedSymbols</c>"] The failed symbols
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>error</c>"] The error
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; } = string.Empty;
    }
}
