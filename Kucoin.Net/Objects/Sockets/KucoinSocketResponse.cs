﻿using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
        [JsonProperty("code")]
        public int? Code { get; set; }
        [JsonProperty("data")]
        public string? Data { get; set; }
    }
}
