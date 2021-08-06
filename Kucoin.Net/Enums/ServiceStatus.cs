using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Service status
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        Open,
        /// <summary>
        /// Closed
        /// </summary>
        Close,
        /// <summary>
        /// Only cancellation available
        /// </summary>
        CancelOnly
    }
}
