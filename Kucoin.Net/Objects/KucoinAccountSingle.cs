namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Account info
    /// </summary>
    public class KucoinAccountSingle
    {
        /// <summary>
        /// The currency of the account
        /// </summary>
        public string Currency { get; set; } = "";
        /// <summary>
        /// The total balance of the account
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// the available amount in the account
        /// </summary>
        public decimal Available { get; set; }
        /// <summary>
        /// The amount in hold of the account
        /// </summary>
        public decimal Holds { get; set; }
    }
}
