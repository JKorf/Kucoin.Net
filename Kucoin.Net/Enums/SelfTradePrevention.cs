namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Self trade prevention types
    /// </summary>
    public enum SelfTradePrevention
    {
        /// <summary>
        /// No self trade prevention
        /// </summary>
        None,
        /// <summary>
        /// Decrease the quantity of the existing order by the amount of the new order
        /// </summary>
        DecreaseAndCancel,
        /// <summary>
        /// Cancel the oldest order
        /// </summary>
        CancelOldest,
        /// <summary>
        /// Cancel the newest order
        /// </summary>
        CancelNewest,
        /// <summary>
        /// Cancel both orders
        /// </summary>
        CancelBoth
    }
}
