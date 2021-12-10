using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class BizTypeConverter : BaseConverter<KucoinBizType>
    {
        private readonly bool _useCaps;

        public BizTypeConverter() : base(true) { }        

        public BizTypeConverter(bool useCaps) : this() { _useCaps = useCaps; }

        protected override List<KeyValuePair<KucoinBizType, string>> Mapping => new List<KeyValuePair<KucoinBizType, string>>
        {
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.ConvertToKCS, _useCaps ? "" : "Convert to KCS"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Deposit, _useCaps ? "DEPOSIT" : "Deposit"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Exchange, _useCaps ? "TRADE_EXCHANGE" : "Exchange"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Trade, _useCaps ? "TRADE_EXCHANGE" : "Trade"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.KCSPayFees, _useCaps ? "" : "KCS Pay Fees"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.OtherRewards, _useCaps ? "" : "Other rewards"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.RefundedFees, _useCaps ? "" : "Refunded Fees"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Rewards, _useCaps ? "" : "Rewards"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.SoftStakingProfits, _useCaps ? "" : "Soft Staking Profits"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Staking, _useCaps ? "" : "Staking"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.StakingProfits, _useCaps ? "" : "Staking Profits"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Transfer, _useCaps ? "TRANSFER" : "Transfer"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Withdrawal, _useCaps ? "WITHDRAW" : "Withdrawal"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.AssetsTransferred, _useCaps ? "" : "Assets Transferred in After Upgrading"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.VoteForCoin, _useCaps ? "" : "Vote for Coin"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.KuCoinBonus, _useCaps ? "KUCOIN_BONUS" : "KuCoin Bonus"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.ReferralBonus, _useCaps ? "" : "Referral Bonus"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Distribution, _useCaps ? "" : "Distribution"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.AirdropFork, _useCaps ? "" : "Airdrop/Fork"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.FeeRebate, _useCaps ? "" : "Fee Rebate"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.BuyCrypto, _useCaps ? "" : "Buy Crypto"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.SellCrypto, _useCaps ? "" : "Sell Crypto"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.PublicOfferingPurchase, _useCaps ? "" : "Public Offering Purchase"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.SendRedEnvelope, _useCaps ? "" : "Send red envelope"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.OpenRedEnvelope, _useCaps ? "" : "Open red envelope"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.LockDropVesting, _useCaps ? "" : "LockDrop Vesting"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Redemption, _useCaps ? "" : "Redemption"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.MarginTrade, _useCaps ? "MARGIN_EXCHANGE" : "Margin Trade"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Loans, _useCaps ? "" : "Loans"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Borrowings, _useCaps ? "" : "Borrowings"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.DebtRepayment, _useCaps ? "" : "Debt Repayment"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.LoansRepaid, _useCaps ? "" : "Loans Repaid"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Lendings, _useCaps ? "" : "Lendings"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.PoolTransactions, _useCaps ? "" : "Pool transactions"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.InstantExchange, _useCaps ? "" : "Instant Exchange"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.SubAccountTransfer, _useCaps ? "SUB_TRANSFER" : "Sub-account transfer"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.LiquidationFees, _useCaps ? "" : "Liquidation Fees"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.VotingEarnings, _useCaps ? "" : "Voting Earnings"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.RedemptionOfVoting, _useCaps ? "" : "Redemption of Voting"),
            new KeyValuePair<KucoinBizType, string>(KucoinBizType.Voting, _useCaps ? "" : "Voting"),           
        };
    }
}
