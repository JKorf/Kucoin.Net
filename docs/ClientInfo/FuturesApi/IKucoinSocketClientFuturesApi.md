---
title: IKucoinSocketClientFuturesApi
has_children: true
parent: Socket API documentation
---
*[generated documentation]*  
`KucoinSocketClient > FuturesApi`  
*Futures socket api*
  

***

## SubscribeTo24HourSnapshotUpdatesAsync  

[https://docs.kucoin.com/futures/#transaction-statistics-timer-event](https://docs.kucoin.com/futures/#transaction-statistics-timer-event)  
<p>

*Subscribe to snapshot updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeTo24HourSnapshotUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBalanceUpdatesAsync  

[https://docs.kucoin.com/futures/#account-balance-events](https://docs.kucoin.com/futures/#account-balance-events)  
<p>

*Subscribe to wallet updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToBalanceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate, Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate, Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onOrderMarginUpdate|Data handler for order margin updates|
|onBalanceUpdate|Data handler for balance updates|
|onWithdrawableUpdate|Data handler for withdrawable funds updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarketUpdatesAsync  

[https://docs.kucoin.com/futures/#contract-market-data](https://docs.kucoin.com/futures/#contract-market-data)  
<p>

*Subscribe to market data updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToMarketUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate, Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|onMarkIndexPriceUpdate|Mark/Index price update handler|
|onFundingRateUpdate|Funding price update handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://docs.kucoin.com/futures/#level-2-market-data](https://docs.kucoin.com/futures/#level-2-market-data)  
<p>

*Subscribe to full order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderUpdatesAsync  

[https://docs.kucoin.com/futures/#trade-orders](https://docs.kucoin.com/futures/#trade-orders)  
<p>

*Subscribe to order updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol, Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|[Optional] Symbol|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdatesAsync  

[https://docs.kucoin.com/futures/#message-channel-for-the-5-best-ask-bid-full-data-of-level-2](https://docs.kucoin.com/futures/#message-channel-for-the-5-best-ask-bid-full-data-of-level-2)  
[https://docs.kucoin.com/futures/#message-channel-for-the-50-best-ask-bid-full-data-of-level-2](https://docs.kucoin.com/futures/#message-channel-for-the-50-best-ask-bid-full-data-of-level-2)  
<p>

*Subscribe to partial order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|limit|The amount of levels to receive, either 5 or 50|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPositionUpdatesAsync  

[https://docs.kucoin.com/futures/#position-change-events](https://docs.kucoin.com/futures/#position-change-events)  
<p>

*Subscribe to position updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToPositionUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinPositionUpdate>> onPositionUpdate, Action<DataEvent<KucoinPositionMarkPriceUpdate>> onMarkPriceUpdate, Action<DataEvent<KucoinPositionFundingSettlementUpdate>> onFundingSettlementUpdate, Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>> onRiskAdjustUpdate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|onPositionUpdate|Handler for position changes|
|onMarkPriceUpdate|Handler for update when position change due to mark price changes|
|onFundingSettlementUpdate|Handler for funding settlement updates|
|onRiskAdjustUpdate|Handler for risk adjust updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToStopOrderUpdatesAsync  

[https://docs.kucoin.com/futures/#stop-order-lifecycle-event](https://docs.kucoin.com/futures/#stop-order-lifecycle-event)  
<p>

*Subscribe to stop order updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToStopOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToSystemAnnouncementsAsync  

[https://docs.kucoin.com/futures/#system-annoucements](https://docs.kucoin.com/futures/#system-annoucements)  
<p>

*Subscribe system announcement*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToSystemAnnouncementsAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://docs.kucoin.com/futures/#get-real-time-symbol-ticker](https://docs.kucoin.com/futures/#get-real-time-symbol-ticker)  
<p>

*Subscribe to ticker updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://docs.kucoin.com/futures/#execution-data](https://docs.kucoin.com/futures/#execution-data)  
<p>

*Subscribe to trade updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.FuturesApi.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
