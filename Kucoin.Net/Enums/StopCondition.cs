namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop condition
    /// </summary>
    public enum StopCondition
    {
        /// <summary>
        /// No stop condition
        /// </summary>
        None,
        /// <summary>
        /// Loss condition, triggers when the last trade price changes to a value at or below the stopPrice.
        /// </summary>
        Loss,
        /// <summary>
        /// Entry condition, triggers when the last trade price changes to a value at or above the stopPrice.
        /// </summary>
        Entry
    }
}
