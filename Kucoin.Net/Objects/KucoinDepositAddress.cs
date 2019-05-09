namespace Kucoin.Net.Objects
{
    public class KucoinDepositAddress
    {
        /// <summary>
        /// The address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// A memo for the address
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// The chain of the address
        /// </summary>
        public string Chain { get; set; }
    }
}
