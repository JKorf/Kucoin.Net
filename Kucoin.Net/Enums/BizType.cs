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
        /// ["<c>Assets Transferred in After Upgrading</c>"] Assets Transferred in After V1 to V2 Upgrading
        /// </summary>
        [Map("Assets Transferred in After Upgrading")]
        AssetsTransferred,
        /// <summary>
        /// ["<c>Deposit</c>"] Deposit
        /// </summary>
        [Map("Deposit")]
        Deposit,
        /// <summary>
        /// ["<c>Withdrawal</c>"] Withdrawal
        /// </summary>
        [Map("Withdrawal", "Withdraw")]
        Withdrawal,
        /// <summary>
        /// ["<c>Transfer</c>"] Transfer
        /// </summary>
        [Map("Transfer")]
        Transfer,
        /// <summary>
        /// ["<c>Trade_Exchange</c>"] Trade
        /// </summary>
        [Map("Trade_Exchange")]
        Trade,
        /// <summary>
        /// ["<c>Vote for Coin</c>"] Vote for Coin
        /// </summary>
        [Map("Vote for Coin")]
        VoteForCoin,
        /// <summary>
        /// ["<c>KuCoin Bonus</c>"] KuCoin Bonus
        /// </summary>
        [Map("KuCoin Bonus")]
        KuCoinBonus,
        /// <summary>
        /// ["<c>Referral Bonus</c>"] Referral Bonus
        /// </summary>
        [Map("Referral Bonus")]
        ReferralBonus,
        /// <summary>
        /// ["<c>Rewards</c>"] Activities Rewards
        /// </summary>
        [Map("Rewards")]
        Rewards,
        /// <summary>
        /// ["<c>Distribution</c>"] Distribution (such as get GAS by holding NEO)
        /// </summary>
        [Map("Distribution")]
        Distribution,
        /// <summary>
        /// ["<c>Airdrop/Fork</c>"] Airdrop/Fork
        /// </summary>
        [Map("Airdrop/Fork")]
        AirdropFork,
        /// <summary>
        /// ["<c>Other rewards</c>"] Other rewards, except Vote, Airdrop, Fork
        /// </summary>
        [Map("Other rewards")]
        OtherRewards,
        /// <summary>
        /// ["<c>Fee Rebate</c>"] Fee Rebate
        /// </summary>
        [Map("Fee Rebate")]
        FeeRebate,
        /// <summary>
        /// ["<c>Buy Crypto</c>"] Use credit card to buy crypto
        /// </summary>
        [Map("Buy Crypto")]
        BuyCrypto,
        /// <summary>
        /// ["<c>Sell Crypto</c>"] Use credit card to sell crypto
        /// </summary>
        [Map("Sell Crypto")]
        SellCrypto,
        /// <summary>
        /// ["<c>Public Offering Purchase</c>"] Public Offering Purchase for Spotlight
        /// </summary>
        [Map("Public Offering Purchase")]
        PublicOfferingPurchase,
        /// <summary>
        /// ["<c>Send red envelope</c>"] Send red envelope
        /// </summary>
        [Map("Send red envelope")]
        SendRedEnvelope,
        /// <summary>
        /// ["<c>Open red envelope</c>"] Open red envelope
        /// </summary>
        [Map("Open red envelope")]
        OpenRedEnvelope,
        /// <summary>
        /// ["<c>Staking</c>"] Staking
        /// </summary>
        [Map("Staking")]
        Staking,
        /// <summary>
        /// ["<c>LockDrop Vesting</c>"] LockDrop Vesting
        /// </summary>
        [Map("LockDrop Vesting")]
        LockDropVesting,
        /// <summary>
        /// ["<c>Staking Profits</c>"] Staking Profits
        /// </summary>
        [Map("Staking Profits")]
        StakingProfits,
        /// <summary>
        /// ["<c>Redemption</c>"] Redemption
        /// </summary>
        [Map("Redemption")]
        Redemption,
        /// <summary>
        /// ["<c>Refunded Fees</c>"] Refunded Fees
        /// </summary>
        [Map("Refunded Fees")]
        RefundedFees,
        /// <summary>
        /// ["<c>KCS Pay Fees</c>"] KCS Pay Fees
        /// </summary>
        [Map("KCS Pay Fees")]
        KCSPayFees,
        /// <summary>
        /// ["<c>Margin Trade</c>"] Margin Trade
        /// </summary>
        [Map("Margin Trade")]
        MarginTrade,
        /// <summary>
        /// ["<c>Loans</c>"] Loans
        /// </summary>
        [Map("Loans")]
        Loans,
        /// <summary>
        /// ["<c>Borrowings</c>"] Borrowings
        /// </summary>
        [Map("Borrowings")]
        Borrowings,
        /// <summary>
        /// ["<c>Debt Repayment</c>"] Debt Repayment
        /// </summary>
        [Map("Debt Repayment", "DebtRepayment")]
        DebtRepayment,
        /// <summary>
        /// ["<c>Loans Repaid</c>"] Loans Repaid
        /// </summary>
        [Map("Loans Repaid")]
        LoansRepaid,
        /// <summary>
        /// ["<c>Lendings</c>"] Lendings
        /// </summary>
        [Map("Lendings")]
        Lendings,
        /// <summary>
        /// ["<c>Pool transactions</c>"] Pool-X transactions
        /// </summary>
        [Map("Pool transactions")]
        PoolTransactions,
        /// <summary>
        /// ["<c>Instant Exchange</c>"] Instant Exchange
        /// </summary>
        [Map("Instant Exchange")]
        InstantExchange,
        /// <summary>
        /// ["<c>Sub Account Transfer</c>"] Sub-account transfer
        /// </summary>
        [Map("Sub Account Transfer")]
        SubAccountTransfer,
        /// <summary>
        /// ["<c>Liquidation Fees</c>"] Liquidation Fees
        /// </summary>
        [Map("Liquidation Fees")]
        LiquidationFees,
        /// <summary>
        /// ["<c>Soft Staking Profits</c>"] Soft Staking Profits
        /// </summary>
        [Map("Soft Staking Profits")]
        SoftStakingProfits,
        /// <summary>
        /// ["<c>Voting Earnings</c>"] Voting Earnings on Pool-X
        /// </summary>
        [Map("Voting Earnings")]
        VotingEarnings,
        /// <summary>
        /// ["<c>Redemption of Voting</c>"] Redemption of Voting on Pool-X
        /// </summary>
        [Map("Redemption of Voting")]
        RedemptionOfVoting,
        /// <summary>
        /// ["<c>Voting</c>"] Voting on Pool-X
        /// </summary>
        [Map("Voting")]
        Voting,
        /// <summary>
        /// ["<c>Convert to KCS</c>"] Convert to KCS
        /// </summary>
        [Map("Convert to KCS", "CONVERT_TO_KCS")]
        ConvertToKCS,
        /// <summary>
        /// ["<c>BROKER_TRANSFER</c>"] Broker transfer
        /// </summary>
        [Map("BROKER_TRANSFER")]
        BrokerTransfer,
        /// <summary>
        /// ["<c>Cross Margin</c>"] Cross margin
        /// </summary>
        [Map("Cross Margin")]
        CrossMargin,
        /// <summary>
        /// ["<c>Mining Income</c>"] Mining income
        /// </summary>
        [Map("Mining Income")]
        MiningIncome,
        /// <summary>
        /// ["<c>Bank Card Deal</c>"] Bank card deal
        /// </summary>
        [Map("Bank Card Deal")]
        BankCardDeal,
        /// <summary>
        /// ["<c>Bonus received</c>"] Margin bonus
        /// </summary>
        [Map("Bonus received")]
        MarginBonus,
        /// <summary>
        /// ["<c>Liquidation Takeover</c>"] Liquidation takeover
        /// </summary>
        [Map("Liquidation Takeover")]
        LiquidationTakeover,
        /// <summary>
        /// ["<c>Return of Liquidation Takeover</c>"] Return of liquidation takeover
        /// </summary>
        [Map("Return of Liquidation Takeover")]
        ReturnOfLiquidationTakeover,
        /// <summary>
        /// ["<c>KuCoin Event</c>"] KuCoin Event
        /// </summary>
        [Map("KuCoin Event")]
        KuCoinEvent,
        /// <summary>
        /// ["<c>Fiat Deposit</c>"] Fiat Deposit
        /// </summary>
        [Map("Fiat Deposit")]
        FiatDeposit,
        /// <summary>
        /// ["<c>Fiat Withdrawal</c>"] Fiat Withdrawal
        /// </summary>
        [Map("Fiat Withdrawal")]
        FiatWithdrawal,
        /// <summary>
        /// ["<c>KuCoin Earn Profits</c>"] KuCoin Earn Profits
        /// </summary>
        [Map("KuCoin Earn Profits")]
        KuCoinEarnProfits,
        /// <summary>
        /// ["<c>Hold to Earn Earnings</c>"] Hold to Earn Earnings
        /// </summary>
        [Map("Hold to Earn Earnings")]
        HoldToEarnEarnings,
        /// <summary>
        /// ["<c>Fee Refunds using KCS</c>"] Fee Refunds using KCS
        /// </summary>
        [Map("Fee Refunds using KCS")]
        FeeRefundsKcs,
        /// <summary>
        /// ["<c>KCS Fee Deduction</c>"] KCS Fee Deduction
        /// </summary>
        [Map("KCS Fee Deduction")]
        KcsFeeDeduction,
        /// <summary>
        /// ["<c>Spot</c>"] Spot
        /// </summary>
        [Map("Spot")]
        Spot,
        /// <summary>
        /// ["<c>Rebate</c>"] Rebate
        /// </summary>
        [Map("Rebate")]
        Rebate,
        /// <summary>
        /// ["<c>Isolated Margin Trading</c>"] Isolated margin trade
        /// </summary>
        [Map("Isolated Margin Trading")]
        IsolatedMarginTrade,
        /// <summary>
        /// ["<c>Cross Margin Trading</c>"] Cross margin trade
        /// </summary>
        [Map("Cross Margin Trading")]
        CrossMarginTrade,
        /// <summary>
        /// ["<c>COUPON_RETURNED_FEES</c>"] Coupon returned fees
        /// </summary>
        [Map("COUPON_RETURNED_FEES")]
        CouponReturnedFees
    }
}
