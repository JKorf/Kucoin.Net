using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record KucoinPositionMode
    {
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("positionMode")]
        public PositionMode PositionMode { get; set; }
    }
}
