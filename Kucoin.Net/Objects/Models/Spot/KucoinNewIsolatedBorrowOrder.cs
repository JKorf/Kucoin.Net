using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// New Borrow order
    /// </summary>
    public class KucoinNewIsolatedBorrowOrder: KucoinNewBorrowOrder
    {
        /// <summary>
        /// Actuall borrowed size
        /// </summary>
        public decimal ActualSize { get; set; }
    }
}
