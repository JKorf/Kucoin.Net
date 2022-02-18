namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public class KucoinFundingRate: KucoinIndexBase
    {
        /// <summary>
        /// Predicted value
        /// </summary>
        public decimal PredictedValue { get; set; }
    }
}
