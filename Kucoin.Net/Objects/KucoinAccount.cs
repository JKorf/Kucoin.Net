using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Converts;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Account info
    /// </summary>
    public class KucoinAccount : ICommonBalance
    {
        /// <summary>
        /// The id of the account
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The currency of the account
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The type of the account
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
        public KucoinAccountType Type { get; set; }
        /// <summary>
        /// The total balance of the account
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// The available balance of the account
        /// </summary>
        public decimal Available { get; set; }
        /// <summary>
        /// The amount of balance that's in hold
        /// </summary>
        public decimal Holds { get; set; }

        string ICommonBalance.CommonAsset => Currency;
        decimal ICommonBalance.CommonAvailable => Available;
        decimal ICommonBalance.CommonTotal => Balance;
    }
}
