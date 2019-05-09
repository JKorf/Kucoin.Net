using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects
{
    public enum KucoinKlineInterval
    {
        OneMinute,
        ThreeMinutes,
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinutes,
        OneHour,
        TwoHours,
        FourHours,
        SixHours,
        EightHours,
        TwelfHours,
        OneDay,
        OneWeek
    }

    public enum KucoinAccountType
    {
        Main,
        Trade
    }

    public enum KucoinDepositStatus
    {
        Processing,
        Success,
        Failure
    }

    public enum KucoinWithdrawalStatus
    {
        Processing,
        WalletProcessing,
        Success,
        Failure
    }

    public enum KucoinOrderSide
    {
        Buy,
        Sell
    }

    public enum KucoinNewOrderType
    {
        Limit,
        Market
    }

    public enum KucoinOrderType
    {
        Limit,
        Market,
        LimitStop,
        MarketStop
    }

    public enum KucoinTimeInForce
    {
        GoodTillCancelled,
        GoodTillTime,
        ImmediateOrCancel,
        FillOrKill
    }

    public enum KucoinSelfTradePrevention
    {
        DecreaseAndCancel,
        CancelOldest,
        CancelNewest,
        CancelBoth
    }

    public enum KucoinStopCondition
    {
        None,
        Loss,
        Entry
    }

    public enum KucoinOrderStatus
    {
        Active,
        Done
    }

    public enum KucoinAccountDirection
    {
        In,
        Out
    }

    public enum KucoinLiquidityType
    {
        Maker,
        Taker
    }

    public enum KucoinOrderOperationType
    {
        Deal,
        Cancel
    }

    public enum KucoinMatchUpdateReason
    {
        Cancelled,
        Filled
    }

    public enum KucoinMatchUpdateType
    {
        Received,
        Open,
        Done,
        Match,
        Change,
        Stop,
        Activate
    }
}
