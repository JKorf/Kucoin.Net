using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    internal class KucoinSocketUpdate<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
