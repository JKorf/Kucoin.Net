---
title: IKucoinSocketClientSpotApi
has_children: true
parent: Socket API documentation
---
*[generated documentation]*  
`KucoinSocketClient > SpotApi`  
*Spot socket api*
  

***

## SubscribeToAggregatedOrderBookUpdatesAsync  

[https://docs.kucoin.com/#level-2-market-data](https://docs.kucoin.com/#level-2-market-data)  
<p>

*Subscribe to aggregated order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAggregatedOrderBookUpdatesAsync  

[https://docs.kucoin.com/#level-2-market-data](https://docs.kucoin.com/#level-2-market-data)  
<p>

*Subscribe to aggregated order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAllTickerUpdatesAsync  

[https://docs.kucoin.com/#all-symbols-ticker](https://docs.kucoin.com/#all-symbols-ticker)  
<p>

*Subscribe to updates for all symbol tickers*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToAllTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBalanceUpdatesAsync  

[https://docs.kucoin.com/#account-balance-notice](https://docs.kucoin.com/#account-balance-notice)  
<p>

*Subscribe to balance updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToBalanceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onBalanceChange|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToFundingBookUpdatesAsync  

[https://docs.kucoin.com/#order-book-change](https://docs.kucoin.com/#order-book-change)  
<p>

*Subscribe to funding book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToFundingBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(string currency, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|currency|Currencies to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToFundingBookUpdatesAsync  

[https://docs.kucoin.com/#order-book-change](https://docs.kucoin.com/#order-book-change)  
<p>

*Subscribe to funding book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToFundingBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToFundingBookUpdatesAsync(IEnumerable<string> currencies, Action<DataEvent<KucoinStreamFundingBookUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|currencies|Currencies to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexPriceUpdatesAsync  

[https://docs.kucoin.com/#index-price](https://docs.kucoin.com/#index-price)  
<p>

*Subscribe to index price updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToIndexPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexPriceUpdatesAsync  

[https://docs.kucoin.com/#index-price](https://docs.kucoin.com/#index-price)  
<p>

*Subscribe to index price updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToIndexPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://docs.kucoin.com/#klines](https://docs.kucoin.com/#klines)  
<p>

*Subscribe to kline updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to subscribe|
|interval|Interval of the klines|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceUpdatesAsync  

[https://docs.kucoin.com/#mark-price](https://docs.kucoin.com/#mark-price)  
<p>

*Subscribe to mark price updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToMarkPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceUpdatesAsync  

[https://docs.kucoin.com/#mark-price](https://docs.kucoin.com/#mark-price)  
<p>

*Subscribe to mark price updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToMarkPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMatchEngineUpdatesAsync  

<p>

*<para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>*  
*<para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>*  
*<para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>*  
*<para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>*  
*<para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>*  
*<para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in quantity</para>*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToMatchEngineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMatchEngineUpdatesAsync  

<p>

*<para>Subscribe to match engine updates. There are different update types with classes derived from <see cref="KucoinStreamOrderBaseUpdate" /></para>*  
*<para><see cref="KucoinStreamMatchEngineUpdate" />: A valid order is received by the matching engine</para>*  
*<para><see cref="KucoinStreamMatchEngineOpenUpdate" />: A limit order is opened on the order book</para>*  
*<para><see cref="KucoinStreamMatchEngineDoneUpdate" />: An order is no longer on the order book</para>*  
*<para><see cref="KucoinStreamMatchEngineMatchUpdate" />: An order is matched with another order</para>*  
*<para><see cref="KucoinStreamMatchEngineChangeUpdate" />: An order is changed (decreased) in quantity</para>*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToMatchEngineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMatchEngineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamMatchEngineUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://docs.kucoin.com/#level2-5-best-ask-bid-orders](https://docs.kucoin.com/#level2-5-best-ask-bid-orders)  
[https://docs.kucoin.com/#level2-50-best-ask-bid-orders](https://docs.kucoin.com/#level2-50-best-ask-bid-orders)  
<p>

*Subscribe to full order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|limit|The amount of levels to receive, either 5 or 50|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://docs.kucoin.com/#level2-5-best-ask-bid-orders](https://docs.kucoin.com/#level2-5-best-ask-bid-orders)  
[https://docs.kucoin.com/#level2-50-best-ask-bid-orders](https://docs.kucoin.com/#level2-50-best-ask-bid-orders)  
<p>

*Subscribe to full order book updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe|
|limit|The amount of levels to receive, either 5 or 50|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderUpdatesAsync  

[https://docs.kucoin.com/#private-order-change-events](https://docs.kucoin.com/#private-order-change-events)  
<p>

*Subscribe to order updates for your own orders*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToOrderUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<KucoinStreamOrderBaseUpdate>> onOrderData, Action<DataEvent<KucoinStreamOrderMatchUpdate>> onTradeData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onOrderData|Data handler for order updates|
|onTradeData|Data handler for trade updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToSnapshotUpdatesAsync  

[https://docs.kucoin.com/#symbol-snapshot](https://docs.kucoin.com/#symbol-snapshot)  
<p>

*Subscribe to updates for symbol or market snapshots*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToSnapshotUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbolOrMarket|The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToSnapshotUpdatesAsync  

[https://docs.kucoin.com/#symbol-snapshot](https://docs.kucoin.com/#symbol-snapshot)  
<p>

*Subscribe to updates for symbol or market snapshots*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToSnapshotUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbolOrMarkets|The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToStopOrderUpdatesAsync  

[https://docs.kucoin.com/#margin-order-done-event](https://docs.kucoin.com/#margin-order-done-event)  
<p>

*Subscribe to updates for stop orders*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToStopOrderUpdatesAsync(/* parameters */);  
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

## SubscribeToTickerUpdatesAsync  

[https://docs.kucoin.com/#symbol-ticker](https://docs.kucoin.com/#symbol-ticker)  
<p>

*Subscribe to updates for a symbol ticker*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://docs.kucoin.com/#symbol-ticker](https://docs.kucoin.com/#symbol-ticker)  
<p>

*Subscribe to updates for a symbol ticker*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe to|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://docs.kucoin.com/#match-execution-data](https://docs.kucoin.com/#match-execution-data)  
<p>

*Subscribe to trade updates*  

```csharp  
var client = new KucoinSocketClient();  
var result = await client.SpotApi.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|onData|The data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
