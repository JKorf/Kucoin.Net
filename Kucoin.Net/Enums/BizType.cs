using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// BizType Description
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BizType>))]
    public enum BizType
    {
        /// <summary>
        /// Assets Transferred in After V1 to V2 Upgrading
        /// </summary>
        [Map("Assets Transferred in After Upgrading")]
        AssetsTransferred,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("Deposit")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("Withdrawal", "Withdraw")]
        Withdrawal,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("Transfer")]
        Transfer,
        /// <summary>
        /// Trade
        /// </summary>
        [Map("Trade_Exchange")]
        Trade,
        /// <summary>
        /// Vote for Coin
        /// </summary>
        [Map("Vote for Coin")]
        VoteForCoin,
        /// <summary>
        /// KuCoin Bonus
        /// </summary>
        [Map("KuCoin Bonus")]
        KuCoinBonus,
        /// <summary>
        /// Referral Bonus
        /// </summary>
        [Map("Referral Bonus")]
        ReferralBonus,
        /// <summary>
        /// Activities Rewards
        /// </summary>
        [Map("Rewards")]
        Rewards,
        /// <summary>
        /// Distribution (such as get GAS by holding NEO)
        /// </summary>
        [Map("Distribution")]
        Distribution,
        /// <summary>
        /// Airdrop/Fork
        /// </summary>
        [Map("Airdrop/Fork")]
        AirdropFork,
        /// <summary>
        /// Other rewards, except Vote, Airdrop, Fork
        /// </summary>
        [Map("Other rewards")]
        OtherRewards,
        /// <summary>
        /// Fee Rebate
        /// </summary>
        [Map("Fee Rebate")]
        FeeRebate,
        /// <summary>
        /// Use credit card to buy crypto
        /// </summary>
        [Map("Buy Crypto")]
        BuyCrypto,
        /// <summary>
        /// Use credit card to sell crypto
        /// </summary>
        [Map("Sell Crypto")]
        SellCrypto,
        /// <summary>
        /// Public Offering Purchase for Spotlight
        /// </summary>
        [Map("Public Offering Purchase")]
        PublicOfferingPurchase,
        /// <summary>
        /// Send red envelope
        /// </summary>
        [Map("Send red envelope")]
        SendRedEnvelope,
        /// <summary>
        /// Open red envelope
        /// </summary>
        [Map("Open red envelope")]
        OpenRedEnvelope,
        /// <summary>
        /// Staking
        /// </summary>
        [Map("Staking")]
        Staking,
        /// <summary>
        /// LockDrop Vesting
        /// </summary>
        [Map("LockDrop Vesting")]
        LockDropVesting,
        /// <summary>
        /// Staking Profits
        /// </summary>
        [Map("Staking Profits")]
        StakingProfits,
        /// <summary>
        /// Redemption
        /// </summary>
        [Map("Redemption")]
        Redemption,
        /// <summary>
        /// Refunded Fees
        /// </summary>
        [Map("Refunded Fees")]
        RefundedFees,
        /// <summary>
        /// KCS Pay Fees
        /// </summary>
        [Map("KCS Pay Fees")]
        KCSPayFees,
        /// <summary>
        /// Margin Trade
        /// </summary>
        [Map("Margin Trade")]
        MarginTrade,
        /// <summary>
        /// Loans
        /// </summary>
        [Map("Loans")]
        Loans,
        /// <summary>
        /// Borrowings
        /// </summary>
        [Map("Borrowings")]
        Borrowings,
        /// <summary>
        /// Debt Repayment
        /// </summary>
        [Map("Debt Repayment")]
        DebtRepayment,
        /// <summary>
        /// Loans Repaid
        /// </summary>
        [Map("Loans Repaid")]
        LoansRepaid,
        /// <summary>
        /// Lendings
        /// </summary>
        [Map("Lendings")]
        Lendings,
        /// <summary>
        /// Pool-X transactions
        /// </summary>
        [Map("Pool transactions")]
        PoolTransactions,
        /// <summary>
        /// Instant Exchange
        /// </summary>
        [Map("Instant Exchange")]
        InstantExchange,
        /// <summary>
        /// Sub-account transfer
        /// </summary>
        [Map("Sub Account Transfer")]
        SubAccountTransfer,
        /// <summary>
        /// Liquidation Fees
        /// </summary>
        [Map("Liquidation Fees")]
        LiquidationFees,
        /// <summary>
        /// Soft Staking Profits
        /// </summary>
        [Map("Soft Staking Profits")]
        SoftStakingProfits,
        /// <summary>
        /// Voting Earnings on Pool-X
        /// </summary>
        [Map("Voting Earnings")]
        VotingEarnings,
        /// <summary>
        /// Redemption of Voting on Pool-X
        /// </summary>
        [Map("Redemption of Voting")]
        RedemptionOfVoting,
        /// <summary>
        /// Voting on Pool-X
        /// </summary>
        [Map("Voting")]
        Voting,
        /// <summary>
        /// Convert to KCS
        /// </summary>
        [Map("Convert to KCS")]
        ConvertToKCS,
        /// <summary>
        /// Broker transfer
        /// </summary>
        [Map("BROKER_TRANSFER")]
        BrokerTransfer,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("Cross Margin")]
        CrossMargin,
        /// <summary>
        /// Mining income
        /// </summary>
        [Map("Mining Income")]
        MiningIncome,
        /// <summary>
        /// Bank card deal
        /// </summary>
        [Map("Bank Card Deal")]
        BankCardDeal,
        /// <summary>
        /// Margin bonus
        /// </summary>
        [Map("Bonus received")]
        MarginBonus,
        /// <summary>
        /// Liquidation takeover
        /// </summary>
        [Map("Liquidation Takeover")]
        LiquidationTakeover,
        /// <summary>
        /// Return of liquidation takeover
        /// </summary>
        [Map("Return of Liquidation Takeover")]
        ReturnOfLiquidationTakeover,
        /// <summary>
        /// KuCoin Event
        /// </summary>
        [Map("KuCoin Event")]
        KuCoinEvent,
        /// <summary>
        /// Fiat Deposit
        /// </summary>
        [Map("Fiat Deposit")]
        FiatDeposit,
        /// <summary>
        /// Fiat Withdrawal
        /// </summary>
        [Map("Fiat Withdrawal")]
        FiatWithdrawal,
        /// <summary>
        /// KuCoin Earn Profits
        /// </summary>
        [Map("KuCoin Earn Profits")]
        KuCoinEarnProfits
    }
}
