using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kucoin.Net.Converters
{
    [JsonSerializable(typeof(KucoinResult<KucoinSubUserKey[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinSubUserKeyDetails>))]    
    [JsonSerializable(typeof(KucoinResult<KucoinSubUserKeyEdited>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinHfOrderDetails>>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinPositionFundingSettlementUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinPositionRiskAdjustResultUpdate>))]    

    // End manual defined attributes
    [JsonSerializable(typeof(KucoinResult<KucoinPaginatedSlider<KucoinAccountTransaction>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinPositionHistoryItem>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginatedSlider<KucoinFundingItem>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginatedSlider<KucoinFuturesInterest>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginatedSlider<KucoinIndex>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginatedSlider<KucoinPremiumIndex>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinFuturesOrder>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinFuturesUserTrade>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinAccountActivity>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinDeposit>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinWithdrawal>>))]
    [JsonSerializable(typeof(KucoinResult<Dictionary<string, decimal>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinAnnouncement>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfPaginated<KucoinHfOrderDetails>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfPaginated<KucoinUserTrade>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinBorrowOrderV3>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinMarginInterest>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinRedemption>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinLendSubscription>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinSubUser>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinSubUserBalances>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinOrder>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinOcoOrder>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinUserTrade>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPaginated<KucoinStopOrder>>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<Dictionary<string, KucoinLeverageUpdate>>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<Dictionary<string, FuturesMarginMode>>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPosition[]>))]
    [JsonSerializable(typeof(KucoinResult<Objects.Models.Futures.KucoinFuturesRiskLimit[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinContract[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesTick[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesTrade[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesKline[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFundingRateHistory[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesOrderResult[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesOrder[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesUserTrade[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinAccount[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTradeFee[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinDepositAddress[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinSymbol[]>))]
    [JsonSerializable(typeof(KucoinResult<string[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTrade[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinKline[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinAssetDetails[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinLeveragedToken[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinBulkMinimalResponseEntry[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfBulkOrderResponse[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfOrderDetails[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinIndexBase[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTradingPairConfiguration[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCrossRiskLimitConfig[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinIsolatedRiskLimitConfig[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinLendingAsset[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinLendingInterest[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOrder[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinUserTrade[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinStopOrder[]>))]
    [JsonSerializable(typeof(KucoinResult<KucoinResult>))]
    [JsonSerializable(typeof(KucoinResult<KucoinAccountOverview>))]
    [JsonSerializable(typeof(KucoinResult<KucoinPosition>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOrderValuation>))]
    [JsonSerializable(typeof(KucoinResult<bool>))]
    [JsonSerializable(typeof(KucoinResult<decimal>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTradeFee>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMarginMode>))]
    [JsonSerializable(typeof(KucoinResult<KucoinLeverage>))]
    [JsonSerializable(typeof(KucoinResult<KucoinToken>))]
    [JsonSerializable(typeof(KucoinResult<KucoinContract>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesTick>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOrderBook>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMarkPrice>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFundingRate>))]
    [JsonSerializable(typeof(KucoinResult<long>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesServiceStatus>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTransactionVolume>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOrderId>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCanceledOrders>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCanceledOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinFuturesOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMaxOpenSize>))]
    [JsonSerializable(typeof(KucoinResult<KucoinUserInfo>))]
    [JsonSerializable(typeof(KucoinResult<KucoinAccountSingle>))]
    [JsonSerializable(typeof(KucoinResult<KucoinUserFee>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTransferableAccount>))]
    [JsonSerializable(typeof(KucoinResult<KucoinUniversalTransfer>))]
    [JsonSerializable(typeof(KucoinResult<KucoinDepositAddress>))]
    [JsonSerializable(typeof(KucoinResult<KucoinWithdrawalQuota>))]
    [JsonSerializable(typeof(KucoinResult<KucoinNewWithdrawal>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMarginAccount>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCrossMarginAccount>))]
    [JsonSerializable(typeof(KucoinResult<KucoinIsolatedMarginAccountsInfo>))]
    [JsonSerializable(typeof(KucoinResult<KucoinIsolatedMarginAccount>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMigrateStatus>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMigrateResult>))]
    [JsonSerializable(typeof(KucoinResult<KucoinApiKey>))]
    [JsonSerializable(typeof(KucoinResult<DateTime>))]
    [JsonSerializable(typeof(KucoinResult<KucoinSymbol>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTick>))]
    [JsonSerializable(typeof(KucoinResult<KucoinTicks>))]
    [JsonSerializable(typeof(KucoinResult<Kucoin24HourStat>))]
    [JsonSerializable(typeof(KucoinResult<KucoinAssetDetails>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCallAuctionInfo>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinModifiedOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinClientOrderId>))]
    [JsonSerializable(typeof(KucoinResult<KucoinHfOrderDetails>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCanceledSymbols>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOpenOrderSymbols>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCancelAfter>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCancelAfterStatus?>))]
    [JsonSerializable(typeof(KucoinResult<KucoinNewMarginOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMarginOpenOrderSymbols>))]
    [JsonSerializable(typeof(KucoinResult<KucoinIndexBase>))]
    [JsonSerializable(typeof(KucoinResult<KucoinMarginConfig>))]
    [JsonSerializable(typeof(KucoinResult<KucoinNewBorrowOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinLendingResult>))]
    [JsonSerializable(typeof(KucoinResult<KucoinCrossMarginSymbols>))]
    [JsonSerializable(typeof(KucoinResult<KucoinSubUser>))]
    [JsonSerializable(typeof(KucoinResult<KucoinSubUserBalances>))]
    [JsonSerializable(typeof(KucoinResult<KucoinBulkOrderResponse>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOcoOrder>))]
    [JsonSerializable(typeof(KucoinResult<KucoinOcoOrderDetails>))]
    [JsonSerializable(typeof(KucoinResult<KucoinStopOrder>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesWalletUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderMarginUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesBalanceUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesWithdrawableUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinContractAnnouncement>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesMarkIndexPrice>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesFundingRate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinIsolatedMarginPositionUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinMarginOrderUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinMarginOrderDoneUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinMarginDebtRatioUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinMarginPositionStatusUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderMatchUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderNewUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinPositionMarkPriceUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinPositionUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesMatch>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesKlineUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesTick>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinFuturesOrderBookChange>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderBookChanged>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamTransactionStatisticsUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesOrderUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamFuturesStopOrderUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamTick>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamSnapshotWrapper>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamBestOffers>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamOrderBook>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamMatch>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamCandle>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamIndicatorPrice>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinCallAuctionInfo>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinBalanceUpdate>))]
    [JsonSerializable(typeof(KucoinSocketUpdate<KucoinStreamStopOrderUpdateBase>))]
    [JsonSerializable(typeof(IDictionary<string, object>))]
    [JsonSerializable(typeof(KucoinOrderBase[]))]
    [JsonSerializable(typeof(KucoinStreamMatchBase[]))]
    [JsonSerializable(typeof(KucoinStreamStopOrderUpdateBase[]))]
    [JsonSerializable(typeof(KucoinTradeBase[]))]
    [JsonSerializable(typeof(KucoinAccountOverview[]))]
    [JsonSerializable(typeof(KucoinAccountTransaction[]))]
    [JsonSerializable(typeof(KucoinCancelRequest[]))]
    [JsonSerializable(typeof(KucoinFundingItem[]))]
    [JsonSerializable(typeof(KucoinFundingRate[]))]
    [JsonSerializable(typeof(KucoinFuturesInterest[]))]
    [JsonSerializable(typeof(KucoinFuturesOrderRequestEntry[]))]
    [JsonSerializable(typeof(KucoinFuturesServiceStatus[]))]
    [JsonSerializable(typeof(KucoinFuturesWithdrawalQuota[]))]
    [JsonSerializable(typeof(KucoinIndex[]))]
    [JsonSerializable(typeof(KucoinDecomposionItem[]))]
    [JsonSerializable(typeof(KucoinLeverage[]))]
    [JsonSerializable(typeof(KucoinLeverageUpdate[]))]
    [JsonSerializable(typeof(KucoinMarginMode[]))]
    [JsonSerializable(typeof(KucoinMarkPrice[]))]
    [JsonSerializable(typeof(KucoinMaxOpenSize[]))]
    [JsonSerializable(typeof(KucoinOrderValuation[]))]
    [JsonSerializable(typeof(KucoinPositionBase[]))]
    [JsonSerializable(typeof(KucoinPositionUpdate[]))]
    [JsonSerializable(typeof(KucoinPositionHistoryItem[]))]
    [JsonSerializable(typeof(KucoinPremiumIndex[]))]
    [JsonSerializable(typeof(KucoinRiskLimit[]))]
    [JsonSerializable(typeof(KucoinFuturesRiskLimit[]))]
    [JsonSerializable(typeof(KucoinTransactionVolume[]))]
    [JsonSerializable(typeof(KucoinTransfer[]))]
    [JsonSerializable(typeof(KucoinTransferResult[]))]
    [JsonSerializable(typeof(Kucoin24HourStat[]))]
    [JsonSerializable(typeof(KucoinAccountActivity[]))]
    [JsonSerializable(typeof(KucoinAccountActivityContext[]))]
    [JsonSerializable(typeof(KucoinAccountSingle[]))]
    [JsonSerializable(typeof(KucoinAllTick[]))]
    [JsonSerializable(typeof(KucoinAnnouncement[]))]
    [JsonSerializable(typeof(KucoinApiKey[]))]
    [JsonSerializable(typeof(KucoinAssetBase[]))]
    [JsonSerializable(typeof(KucoinAsset[]))]
    [JsonSerializable(typeof(KucoinAssetNetwork[]))]
    [JsonSerializable(typeof(KucoinBorrowOrderV3[]))]
    [JsonSerializable(typeof(KucoinBulkOrderRequestEntry[]))]
    [JsonSerializable(typeof(KucoinBulkOrderResponse[]))]
    [JsonSerializable(typeof(KucoinBulkOrderResponseEntry[]))]
    [JsonSerializable(typeof(KucoinCallAuctionInfo[]))]
    [JsonSerializable(typeof(KucoinCancelAfter[]))]
    [JsonSerializable(typeof(KucoinCancelAfterStatus[]))]
    [JsonSerializable(typeof(KucoinCanceledOrder[]))]
    [JsonSerializable(typeof(KucoinCanceledOrders[]))]
    [JsonSerializable(typeof(KucoinCanceledSymbols[]))]
    [JsonSerializable(typeof(KucoinCancelError[]))]
    [JsonSerializable(typeof(KucoinClientOrderId[]))]
    [JsonSerializable(typeof(KucoinCrossMarginAccount[]))]
    [JsonSerializable(typeof(KucoinCrossMarginAccountAsset[]))]
    [JsonSerializable(typeof(KucoinCrossMarginSymbols[]))]
    [JsonSerializable(typeof(KucoinCrossMarginSymbol[]))]
    [JsonSerializable(typeof(KucoinDeposit[]))]
    [JsonSerializable(typeof(KucoinHfBulkOrderRequestEntry[]))]
    [JsonSerializable(typeof(KucoinHfOrder[]))]
    [JsonSerializable(typeof(KucoinHistoricalDeposit[]))]
    [JsonSerializable(typeof(KucoinHistoricalOrder[]))]
    [JsonSerializable(typeof(KucoinHistoricalWithdrawal[]))]
    [JsonSerializable(typeof(KucoinHold[]))]
    [JsonSerializable(typeof(KucoinInnerTransfer[]))]
    [JsonSerializable(typeof(KucoinIsolatedMarginAccountsInfo[]))]
    [JsonSerializable(typeof(KucoinIsolatedMarginAccount[]))]
    [JsonSerializable(typeof(KucoinIsolatedMarginAccountAsset[]))]
    [JsonSerializable(typeof(KucoinLendingResult[]))]
    [JsonSerializable(typeof(KucoinLendSubscription[]))]
    [JsonSerializable(typeof(KucoinMarginAccount[]))]
    [JsonSerializable(typeof(KucoinMarginAccountDetails[]))]
    [JsonSerializable(typeof(KucoinMarginConfig[]))]
    [JsonSerializable(typeof(KucoinMarginInterest[]))]
    [JsonSerializable(typeof(KucoinMarginOpenOrderSymbols[]))]
    [JsonSerializable(typeof(KucoinMigrateResult[]))]
    [JsonSerializable(typeof(KucoinMigrateStatus[]))]
    [JsonSerializable(typeof(KucoinModifiedOrder[]))]
    [JsonSerializable(typeof(KucoinNewAccount[]))]
    [JsonSerializable(typeof(KucoinNewBorrowOrder[]))]
    [JsonSerializable(typeof(KucoinNewMarginOrder[]))]
    [JsonSerializable(typeof(KucoinNewWithdrawal[]))]
    [JsonSerializable(typeof(KucoinOcoOrder[]))]
    [JsonSerializable(typeof(KucoinOcoOrderDetails[]))]
    [JsonSerializable(typeof(KucoinOcoOrderInfo[]))]
    [JsonSerializable(typeof(KucoinOpenOrderSymbols[]))]
    [JsonSerializable(typeof(KucoinFullOrderBook[]))]
    [JsonSerializable(typeof(KucoinOrderBook[]))]
    [JsonSerializable(typeof(KucoinFullOrderBookEntry[]))]
    [JsonSerializable(typeof(KucoinOrderBookEntry[]))]
    [JsonSerializable(typeof(KucoinOrderId[]))]
    [JsonSerializable(typeof(KucoinRedemption[]))]
    [JsonSerializable(typeof(KucoinRepayOrderV3[]))]
    [JsonSerializable(typeof(KucoinRiskLimitCrossMargin[]))]
    [JsonSerializable(typeof(KucoinRiskLimitIsolatedMargin[]))]
    [JsonSerializable(typeof(KucoinSubUser[]))]
    [JsonSerializable(typeof(KucoinSubUserBalances[]))]
    [JsonSerializable(typeof(KucoinSubUserBalance[]))]
    [JsonSerializable(typeof(KucoinSubUserKey[]))]
    [JsonSerializable(typeof(KucoinSubUserKeyDetails[]))]
    [JsonSerializable(typeof(KucoinSubUserKeyEdited[]))]
    [JsonSerializable(typeof(KucoinTick[]))]
    [JsonSerializable(typeof(KucoinTicks[]))]
    [JsonSerializable(typeof(KucoinTransferableAccount[]))]
    [JsonSerializable(typeof(KucoinUniversalTransfer[]))]
    [JsonSerializable(typeof(KucoinUserFee[]))]
    [JsonSerializable(typeof(KucoinUserInfo[]))]
    [JsonSerializable(typeof(KucoinWithdrawal[]))]
    [JsonSerializable(typeof(KucoinWithdrawalQuota[]))]
    [JsonSerializable(typeof(KucoinContractAnnouncement[]))]
    [JsonSerializable(typeof(KucoinFuturesOrderBookChange[]))]
    [JsonSerializable(typeof(KucoinPositionFundingSettlementUpdate[]))]
    [JsonSerializable(typeof(KucoinPositionMarkPriceUpdate[]))]
    [JsonSerializable(typeof(KucoinPositionRiskAdjustResultUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesBalanceUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesFundingRate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesKlineUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesKline[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesMarkIndexPrice[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesMatch[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesOrderUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesStopOrderUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesTick[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesWalletUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamFuturesWithdrawableUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamIndicatorPrice[]))]
    [JsonSerializable(typeof(KucoinStreamTransactionStatisticsUpdate[]))]
    [JsonSerializable(typeof(KucoinBalanceUpdate[]))]
    [JsonSerializable(typeof(KucoinIsolatedMarginPositionUpdate[]))]
    [JsonSerializable(typeof(KucoinIsolatedMarginAsset[]))]
    [JsonSerializable(typeof(KucoinMarginDebtRatioUpdate[]))]
    [JsonSerializable(typeof(KucoinMarginOrderDoneUpdate[]))]
    [JsonSerializable(typeof(KucoinMarginOrderUpdate[]))]
    [JsonSerializable(typeof(KucoinMarginPositionStatusUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamBestOffers[]))]
    [JsonSerializable(typeof(KucoinStreamCandle[]))]
    [JsonSerializable(typeof(KucoinStreamFundingBookUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamMatch[]))]
    [JsonSerializable(typeof(KucoinStreamMatchEngineUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamMatchEngineOpenUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamMatchEngineDoneUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamMatchEngineChangeUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamMatchEngineMatchUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamOrderBook[]))]
    [JsonSerializable(typeof(KucoinStreamOrderBookChanged[]))]
    [JsonSerializable(typeof(KucoinStreamOrderBookEntry[]))]
    [JsonSerializable(typeof(KucoinStreamOrderMarginUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamOrderBaseUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamOrderNewUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamOrderUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamOrderMatchUpdate[]))]
    [JsonSerializable(typeof(KucoinStreamSnapshotWrapper[]))]
    [JsonSerializable(typeof(KucoinStreamSnapshot[]))]
    [JsonSerializable(typeof(KucoinMarketChange[]))]
    [JsonSerializable(typeof(KucoinStreamTick[]))]
    [JsonSerializable(typeof(KucoinPong))]
    [JsonSerializable(typeof(KucoinPing))]
    [JsonSerializable(typeof(KucoinSocketResponse))]
    [JsonSerializable(typeof(KucoinRequest))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(decimal?))]
    [JsonSerializable(typeof(DateTime?))]
    internal partial class KucoinSourceGenerationContext : JsonSerializerContext
    {
    }
}
