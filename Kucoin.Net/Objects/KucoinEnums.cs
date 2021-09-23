namespace Kucoin.Net.Objects
{
    /// <summary>
    /// The time for each candlestick
    /// </summary>
    public enum KucoinKlineInterval
    {
        /// <summary>
        /// 1m
        /// </summary>
        OneMinute,
        /// <summary>
        /// 3m
        /// </summary>
        ThreeMinutes,
        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes,
        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes,
        /// <summary>
        /// 30m
        /// </summary>
        ThirtyMinutes,
        /// <summary>
        /// 1h
        /// </summary>
        OneHour,
        /// <summary>
        /// 2h
        /// </summary>
        TwoHours,
        /// <summary>
        /// 4h
        /// </summary>
        FourHours,
        /// <summary>
        /// 6h
        /// </summary>
        SixHours,
        /// <summary>
        /// 8h
        /// </summary>
        EightHours,
        /// <summary>
        /// 12h
        /// </summary>
        TwelfHours,
        /// <summary>
        /// 1d
        /// </summary>
        OneDay,
        /// <summary>
        /// 1w
        /// </summary>
        OneWeek
    }

    /// <summary>
    /// Account type
    /// </summary>
    public enum KucoinAccountType
    {
        /// <summary>
        /// Main account
        /// </summary>
        Main,
        /// <summary>
        /// Trade account
        /// </summary>
        Trade,
        /// <summary>
        /// Margin account
        /// </summary>
        Margin,
        /// <summary>
        /// Pool account
        /// </summary>
        Pool
    }

    /// <summary>
    /// Status of a deposit
    /// </summary>
    public enum KucoinDepositStatus
    {
        /// <summary>
        /// In progress
        /// </summary>
        Processing,
        /// <summary>
        /// Successful
        /// </summary>
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        Failure
    }

    /// <summary>
    /// Status of a withdrawal
    /// </summary>
    public enum KucoinWithdrawalStatus
    {
        /// <summary>
        /// In progress
        /// </summary>
        Processing,
        /// <summary>
        /// In progress
        /// </summary>
        WalletProcessing,
        /// <summary>
        /// Successful
        /// </summary>
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        Failure
    }

    /// <summary>
    /// Order side
    /// </summary>
    public enum KucoinOrderSide
    {
        /// <summary>
        /// Buy order
        /// </summary>
        Buy,
        /// <summary>
        /// Sell order
        /// </summary>
        Sell
    }

    /// <summary>
    /// New order type
    /// </summary>
    public enum KucoinNewOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        Market
    }

    /// <summary>
    /// Order type
    /// </summary>
    public enum KucoinOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        Market,
        /// <summary>
        /// Limit stop order
        /// </summary>
        LimitStop,
        /// <summary>
        /// Market stop order
        /// </summary>
        MarketStop,
        /// <summary>
        /// Stop order
        /// </summary>
        Stop
    }

    /// <summary>
    /// Borrow order type
    /// </summary>
    public enum KucoinBorrowOrderType
    {
        /// <summary>
        /// FOK
        /// </summary>
        FOK,
        /// <summary>
        /// IOC
        /// </summary>
        IOC
    }

    /// <summary>
    /// Time the order is valid for
    /// </summary>
    public enum KucoinTimeInForce
    {
        /// <summary>
        /// Good until cancelled by user
        /// </summary>
        GoodTillCancelled,
        /// <summary>
        /// Good until a certain time
        /// </summary>
        GoodTillTime,
        /// <summary>
        /// Immediately has to be (partially) filled or it will be cancelled
        /// </summary>
        ImmediateOrCancel,
        /// <summary>
        /// Immediately has to be full filled or it will be cancelled
        /// </summary>
        FillOrKill
    }

    /// <summary>
    /// Self trade prevention types
    /// </summary>
    public enum KucoinSelfTradePrevention
    {
        /// <summary>
        /// No self trade prevention
        /// </summary>
        None,
        /// <summary>
        /// Decrease the amount of the existing order by the amount of the new order
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

    /// <summary>
    /// Stop condition
    /// </summary>
    public enum KucoinStopCondition
    {
        /// <summary>
        /// No stop condition
        /// </summary>
        None,
        /// <summary>
        /// Loss condition
        /// </summary>
        Loss,
        /// <summary>
        /// Entry condition
        /// </summary>
        Entry
    }

    /// <summary>
    /// Order status
    /// </summary>
    public enum KucoinOrderStatus
    {
        /// <summary>
        /// Order is active
        /// </summary>
        Active,
        /// <summary>
        /// Order is done
        /// </summary>
        Done
    }

    /// <summary>
    /// Direction
    /// </summary>
    public enum KucoinAccountDirection
    {
        /// <summary>
        /// In
        /// </summary>
        In,
        /// <summary>
        /// Out
        /// </summary>
        Out
    }

    /// <summary>
    /// Liquidity type of a trade
    /// </summary>
    public enum KucoinLiquidityType
    {
        /// <summary>
        /// Maker, order was on the order book and got filled
        /// </summary>
        Maker,
        /// <summary>
        /// Taker, trade filled an existing order on the order book
        /// </summary>
        Taker
    }

    /// <summary>
    /// Order operation type
    /// </summary>
    public enum KucoinOrderOperationType
    {
        /// <summary>
        /// Matched
        /// </summary>
        Deal,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancel
    }

    /// <summary>
    /// Reason for an update
    /// </summary>
    public enum KucoinMatchUpdateReason
    {
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled,
        /// <summary>
        /// Filled
        /// </summary>
        Filled
    }

    /// <summary>
    /// Match update type
    /// </summary>
    public enum KucoinMatchUpdateType
    {
        /// <summary>
        /// Open
        /// </summary>
        Open,
        /// <summary>
        /// Match
        /// </summary>
        Match,
        /// <summary>
        /// Filled
        /// </summary>
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Update
        /// </summary>
        Update
    }

    /// <summary>
    /// BizType Description
    /// </summary>
    public enum KucoinBizType
    {
        /// <summary>
        /// Assets Transferred in After V1 to V2 Upgrading
        /// </summary>
        AssetsTransferred,
        /// <summary>
        /// Deposit
        /// </summary>
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        Withdrawal,
        /// <summary>
        /// Transfer
        /// </summary>
        Transfer,
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// Exchange
        /// </summary>
        Exchange,
        /// <summary>
        /// Vote for Coin
        /// </summary>
        VoteForCoin,
        /// <summary>
        /// KuCoin Bonus
        /// </summary>
        KuCoinBonus,
        /// <summary>
        /// Referal Bonus
        /// </summary>
        ReferralBonus,
        /// <summary>
        /// Activities Rewards
        /// </summary>
        Rewards,
        /// <summary>
        /// Distribution (such as get GAS by holding NEO)
        /// </summary>
        Distribution,
        /// <summary>
        /// Airdrop/Fork
        /// </summary>
        AirdropFork,
        /// <summary>
        /// Other rewards, except Vote, Airdrop, Fork
        /// </summary>
        OtherRewards,
        /// <summary>
        /// Fee Rebate
        /// </summary>
        FeeRebate,
        /// <summary>
        /// Use credit card to buy crypto
        /// </summary>
        BuyCrypto,
        /// <summary>
        /// Use credit card to sell crypto
        /// </summary>
        SellCrypto,
        /// <summary>
        /// Public Offering Purchase for Spotlight
        /// </summary>
        PublicOfferingPurchase,
        /// <summary>
        /// Send red envelope
        /// </summary>
        SendRedEnvelope,
        /// <summary>
        /// Open red envelope
        /// </summary>
        OpenRedEnvelope,
        /// <summary>
        /// Staking
        /// </summary>
        Staking,
        /// <summary>
        /// LockDrop Vesting
        /// </summary>
        LockDropVesting,
        /// <summary>
        /// Staking Profits
        /// </summary>
        StakingProfits,
        /// <summary>
        /// Redemption
        /// </summary>
        Redemption,
        /// <summary>
        /// Refunded Fees
        /// </summary>
        RefundedFees,
        /// <summary>
        /// KCS Pay Fees
        /// </summary>
        KCSPayFees,
        /// <summary>
        /// Margin Trade
        /// </summary>
        MarginTrade,
        /// <summary>
        /// Loans
        /// </summary>
        Loans,
        /// <summary>
        /// Borrowings
        /// </summary>
        Borrowings,
        /// <summary>
        /// Debt Repayment
        /// </summary>
        DebtRepayment,
        /// <summary>
        /// Loans Repaid
        /// </summary>
        LoansRepaid,
        /// <summary>
        /// Lendings
        /// </summary>
        Lendings,
        /// <summary>
        /// Pool-X transactions
        /// </summary>
        PoolTransactions,
        /// <summary>
        /// Instant Exchange
        /// </summary>
        InstantExchange,
        /// <summary>
        /// Sub-account transfer
        /// </summary>
        SubAccountTransfer,
        /// <summary>
        /// Liquidation Fees
        /// </summary>
        LiquidationFees,
        /// <summary>
        /// Soft Staking Profits
        /// </summary>
        SoftStakingProfits,
        /// <summary>
        /// Voting Earnings on Pool-X
        /// </summary>
        VotingEarnings,
        /// <summary>
        /// Redemption of Voting on Pool-X
        /// </summary>
        RedemptionOfVoting,
        /// <summary>
        /// Voting on Pool-X
        /// </summary>
        Voting,
        /// <summary>
        /// Convert to KCS
        /// </summary>
        ConvertToKCS
    }

    /// <summary>
    /// Type of trade
    /// </summary>
    public enum KucoinTradeType
    {
        /// <summary>
        /// Stop trade
        /// </summary>
        SpotTrade,
        /// <summary>
        /// Margin trade
        /// </summary>
        MarginTrade
    }

    /// <summary>
    /// Mode of Margin
    /// </summary>
    public enum KucoinMarginMode
    {
        /// <summary>
        /// Cross Mode
        /// </summary>
        CrossMode,
        /// <summary>
        /// Isolated Mode, This mode is not supported by platform yet.
        /// </summary>
        IsolatedMode,
    }

}
