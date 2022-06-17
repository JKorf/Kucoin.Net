using System;
using System.IO;
using System.Diagnostics;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account activity info
    /// </summary>
    public class KucoinAccountActivity
    {
        private bool contextCreated = false;
        private KucoinAccountActivityContext? context = null;

        /// <summary>
        /// Creation timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; private set; }
        /// <summary>
        /// The quantity of the activity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; private set; }
        /// <summary>
        /// The remaining balance after the activity
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; private set; }
        /// <summary>
        /// The fee of the activity
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; private set; }
        /// <summary>
        /// The type of activity
        /// </summary>
        [JsonConverter(typeof(BizTypeConverter))]
        [JsonProperty("bizType")]
        public BizType BizType { get; private set; } = default!;
        /// <summary>
        /// The type of activity
        /// </summary>
        [JsonConverter(typeof(AccountTypeConverter))]
        [JsonProperty("accountType")]
        public AccountType AccountType { get; private set; } = default!;
        /// <summary>
        /// The unique key for this activity 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; } = string.Empty;
        /// <summary>
        /// Additional info for this activity
        /// </summary>
        public KucoinAccountActivityContext? Context
        {
            get
            {
                if (!contextCreated)
                    context = KucoinAccountActivityContext.FromJsonString(BizType, ContextString);
                contextCreated = true;
                return context;
            }
        }
        /// <summary>
        /// Additional info for this activity
        /// </summary>
        [JsonProperty("context")]
        public string ContextString { get; private set; } = string.Empty;
        /// <summary>
        /// The asset of the activity
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; private set; } = string.Empty;
        /// <summary>
        /// The direction of the activity
        /// </summary>
        [JsonConverter(typeof(AccountDirectionConverter))]
        [JsonProperty("direction")]
        public AccountDirection Direction { get; private set; }
    }
    /// <summary>
    /// Account activity details
    /// </summary>
    public abstract class KucoinAccountActivityContext
    {
        /// <summary>
        /// Account activity factory method
        /// </summary>
        public static KucoinAccountActivityContext? FromJsonString(BizType bizType, string jsonString)
        {
            switch (bizType)
            {
                case BizType.Deposit:
                case BizType.Withdrawal:
                    return FromJsonString<KucoinAccountActivityTransactionContext>(jsonString);
                case BizType.KCSPayFees:
                case BizType.RefundedFees:
                    //Order
                    //Trade
                    return FromJsonString<KucoinAccountActivityTradeContext>(jsonString);
                case BizType.Exchange:
                case BizType.CrossMargin:
                    return FromJsonString<KucoinAccountActivityTradeContext>(jsonString);
                case BizType.IsolatedMargin:
                    //Symbol
                    return FromJsonString<KucoinAccountActivityTradeContext>(jsonString);
                case BizType.Borrowings:
                    return FromJsonString<KucoinAccountActivityLoanContext>(jsonString);
                case BizType.DebtRepayment:
                    return FromJsonString<KucoinAccountActivityLoanContext>(jsonString);
                case BizType.Transfer:
                case BizType.OtherRewards:
                case BizType.ConvertToKCS:
                case BizType.LiquidationTakeover:
                case BizType.ReturnOfLiquidationTakeover:
                case BizType.EarnSubscription:
                case BizType.EarnRedemption:
                case BizType.EarnProfits:
                case BizType.SubAccountTransfer:
                case BizType.Rewards:
                case BizType.KuCoinBonus:
                case BizType.MiningIncome:
                case BizType.MarginBonus:
                case BizType.BankCardDeal:
                    return null;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Account activity factory method
        /// </summary>
        public static KucoinAccountActivityContextType FromJsonString<KucoinAccountActivityContextType>(string jsonString)
            where KucoinAccountActivityContextType : KucoinAccountActivityContext
        {
            try
            {
                var obj = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(jsonString)), typeof(KucoinAccountActivityContextType));
                if (obj != null)
                    return (KucoinAccountActivityContextType)obj;
            }
            catch (Exception) { }

            Debugger.Break();
            throw new Exception();
        }
    }
    /// <summary>
    /// Account activity details
    /// </summary>
    public class KucoinAccountActivityTradeContext : KucoinAccountActivityContext
    {
        /// <summary>
        /// The id for the order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The id for the trade (for trades)
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order (for trades)
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
    /// <summary>
    /// Account activity details
    /// </summary>
    public class KucoinAccountActivityTransactionContext : KucoinAccountActivityContext
    {
        /// <summary>
        /// The id for the order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
    }
    /// <summary>
    /// Account activity details
    /// </summary>
    public class KucoinAccountActivityLoanContext : KucoinAccountActivityContext
    {
        /// <summary>
        /// The userId of the loan
        /// </summary>
        [JsonProperty("borrowerUserId")]
        public string LoanUserId { get; set; } = string.Empty;
        /// <summary>
        /// The borrow/repay order number
        /// </summary>
        public string LoanOrderNo { get; private set; } = string.Empty;
        [JsonProperty("loanRepayOrderNo")]
        internal string loanRepayOrderNo { set => LoanOrderNo = value; }
        [JsonProperty("loanBorrowOrderNo")]
        internal string loanBorrowOrderNo { set => LoanOrderNo = value; }
        /// <summary>
        /// Is repayment complete?
        /// Loan paid in full???
        /// </summary>
        [JsonProperty("finished")]
        public bool? RepayFinished
        {
            set
            {
                if (!value.HasValue)
                {
                    Debugger.Break();
                    throw new InvalidOperationException();
                }

                if (value.Value)
                    BorrowOrderStatus = LoanStatus.Done;
                else
                    BorrowOrderStatus = LoanStatus.Processing;
            }
        }
        /// <summary>
        /// Is borrow order complete
        /// </summary>
        [JsonProperty("afterOrderStatus")]
        [JsonConverter(typeof(LoanStatusConverter))]
        public LoanStatus BorrowOrderStatus { get; set; } = LoanStatus.Unknown;
    }
}

///// <summary>
///// The Description (for pool-x staking rewards)
///// </summary>
//[JsonProperty("description")]
//public string Description { get; set; } = string.Empty;