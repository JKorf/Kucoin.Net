﻿using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Margin mode
    /// </summary>
    public enum FuturesMarginMode
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
    }
}
