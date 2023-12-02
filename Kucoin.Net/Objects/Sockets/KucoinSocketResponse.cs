using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("code")]
        public int? Code { get; set; }
        [JsonProperty("data")]
        public string? Data { get; set; }
    }
}
