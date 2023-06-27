---
title: IKucoinRestClientFuturesApiExchangeData
has_children: false
parent: IKucoinRestClientFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > FuturesApi > ExchangeData`  
*Kucoin Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAggregatedFullOrderBookAsync  

[https://docs.kucoin.com/futures/#get-full-order-book-level-2](https://docs.kucoin.com/futures/#get-full-order-book-level-2)  
<p>

*Get the full order book, aggregated by price*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAggregatedPartialOrderBookAsync  

[https://docs.kucoin.com/futures/#get-part-order-book-level-2](https://docs.kucoin.com/futures/#get-part-order-book-level-2)  
<p>

*Get the partial order book, aggregated by price*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetAggregatedPartialOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|depth|Amount of rows in the book, either 20 or 100|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetContractAsync  

[https://docs.kucoin.com/futures/#get-order-info-of-the-contract](https://docs.kucoin.com/futures/#get-order-info-of-the-contract)  
<p>

*Get a contract*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetContractAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCurrentFundingRateAsync  

[https://docs.kucoin.com/futures/#get-current-funding-rate](https://docs.kucoin.com/futures/#get-current-funding-rate)  
<p>

*Get the current funding rate*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetCurrentFundingRateAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCurrentMarkPriceAsync  

[https://docs.kucoin.com/futures/#get-current-mark-price](https://docs.kucoin.com/futures/#get-current-mark-price)  
<p>

*Get the current mark price*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetCurrentMarkPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIndexListAsync  

[https://docs.kucoin.com/futures/#get-index-list](https://docs.kucoin.com/futures/#get-index-list)  
<p>

*Get index list*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetIndexListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? offset = default, int? pageSize = default, bool? forward = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ offset|Result offset|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ forward|Forward or backwards direction|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetInterestRatesAsync  

[https://docs.kucoin.com/futures/#get-interest-rate-list](https://docs.kucoin.com/futures/#get-interest-rate-list)  
<p>

*Get interest rate list*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetInterestRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? offset = default, int? pageSize = default, bool? forward = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ offset|Result offset|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ forward|Forward or backwards direction|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://docs.kucoin.com/futures/#get-k-line-data-of-contract](https://docs.kucoin.com/futures/#get-k-line-data-of-contract)  
<p>

*Get kline data*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|interval|Interval of the klines|
|_[Optional]_ startTime|Start time to retrieve klines from|
|_[Optional]_ endTime|End time to retrieve klines for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenContractsAsync  

[https://docs.kucoin.com/futures/#get-open-contract-list](https://docs.kucoin.com/futures/#get-open-contract-list)  
<p>

*Get open contract list*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetOpenContractsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPremiumIndexAsync  

[https://docs.kucoin.com/futures/#get-premium-index](https://docs.kucoin.com/futures/#get-premium-index)  
<p>

*Get premium index*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetPremiumIndexAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? offset = default, int? pageSize = default, bool? forward = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ offset|Result offset|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ forward|Forward or backwards direction|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://docs.kucoin.com/futures/#server-time](https://docs.kucoin.com/futures/#server-time)  
<p>

*Get the server time*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServiceStatusAsync  

[https://docs.kucoin.com/futures/#get-the-service-status](https://docs.kucoin.com/futures/#get-the-service-status)  
<p>

*Get the service status*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetServiceStatusAsync();  
```  

```csharp  
Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickerAsync  

[https://docs.kucoin.com/futures/#get-real-time-ticker](https://docs.kucoin.com/futures/#get-real-time-ticker)  
<p>

*Get the ticker for a contract*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://docs.kucoin.com/futures/#transaction-history](https://docs.kucoin.com/futures/#transaction-history)  
<p>

*Get the most recent trades*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol of the contract|
|_[Optional]_ ct|Cancellation token|

</p>
