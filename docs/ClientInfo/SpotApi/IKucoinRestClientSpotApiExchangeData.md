---
title: IKucoinRestClientSpotApiExchangeData
has_children: false
parent: IKucoinRestClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > SpotApi > ExchangeData`  
*Kucoin Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## Get24HourStatsAsync  

[https://docs.kucoin.com/#get-24hr-stats](https://docs.kucoin.com/#get-24hr-stats)  
<p>

*Gets the 24 hour stats of a symbol*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.Get24HourStatsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get stats for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAggregatedFullOrderBookAsync  

[https://docs.kucoin.com/#get-full-order-book-aggregated](https://docs.kucoin.com/#get-full-order-book-aggregated)  
<p>

*Get a full aggregated order book for a symbol. Orders for the same price are combined.*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get order book for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAggregatedPartialOrderBookAsync  

[https://docs.kucoin.com/#get-part-order-book-aggregated](https://docs.kucoin.com/#get-part-order-book-aggregated)  
<p>

*Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetAggregatedPartialOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get order book for|
|limit|The limit of results (20 / 100)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetAsync  

[https://docs.kucoin.com/#get-currency-detail-recommend](https://docs.kucoin.com/#get-currency-detail-recommend)  
<p>

*Get info on a specific asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetAssetAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinAssetDetails>> GetAssetAsync(string asset, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetsAsync  

[https://docs.kucoin.com/#get-currencies](https://docs.kucoin.com/#get-currencies)  
<p>

*Gets a list of supported currencies*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinAsset>>> GetAssetsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFiatPricesAsync  

[https://docs.kucoin.com/#get-fiat-price](https://docs.kucoin.com/#get-fiat-price)  
<p>

*Gets a list of prices for all*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetFiatPricesAsync();  
```  

```csharp  
Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = default, IEnumerable<string>? assets = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fiatBase|The three letter code of the fiat to convert to. Defaults to USD|
|_[Optional]_ assets|The assets to get price for. Defaults to all|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://docs.kucoin.com/#get-klines](https://docs.kucoin.com/#get-klines)  
<p>

*Get kline data for a symbol*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get klines for|
|interval|The interval of a kline|
|_[Optional]_ startTime|The start time of the data|
|_[Optional]_ endTime|The end time of the data|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendMarketDataAsync  

[https://docs.kucoin.com/#lending-market-data](https://docs.kucoin.com/#lending-market-data)  
<p>

*Get lending market data*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetLendMarketDataAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinLendingMarketEntry>>> GetLendMarketDataAsync(string asset, int? term = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|_[Optional]_ term|Filter by term|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginConfigurationAsync  

[https://docs.kucoin.com/#get-margin-configuration-info](https://docs.kucoin.com/#get-margin-configuration-info)  
<p>

*Get margin configuration*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetMarginConfigurationAsync();  
```  

```csharp  
Task<WebCallResult<KucoinMarginConfig>> GetMarginConfigurationAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginMarkPriceAsync  

[https://docs.kucoin.com/#get-mark-price](https://docs.kucoin.com/#get-mark-price)  
<p>

*Get the mark price of a symbol*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetMarginMarkPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinIndexBase>> GetMarginMarkPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to retrieve|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginTradeHistoryAsync  

[https://docs.kucoin.com/#margin-trade-data](https://docs.kucoin.com/#margin-trade-data)  
<p>

*Get the last 300 fills for borrow/lending orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetMarginTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinBorrowOrderDetails>>> GetMarginTradeHistoryAsync(string asset, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginTradingPairConfigurationAsync  

[https://docs.kucoin.com/#query-isolated-margin-trading-pair-configuration](https://docs.kucoin.com/#query-isolated-margin-trading-pair-configuration)  
<p>

*Get Margin Trading Pair ConfigurationAsync*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetMarginTradingPairConfigurationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinTradingPairConfiguration>>> GetMarginTradingPairConfigurationAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarketsAsync  

[https://docs.kucoin.com/#get-market-list](https://docs.kucoin.com/#get-market-list)  
<p>

*Gets a list of supported markets*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetMarketsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<string>>> GetMarketsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://docs.kucoin.com/#server-time](https://docs.kucoin.com/#server-time)  
<p>

*Gets the server time*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolsAsync  

[https://docs.kucoin.com/#get-symbols-list](https://docs.kucoin.com/#get-symbols-list)  
<p>

*Gets a list of symbols supported by the server*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetSymbolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinSymbol>>> GetSymbolsAsync(string? market = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ market|Only get symbols for a specific market, for example 'ALTS'|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickerAsync  

[https://docs.kucoin.com/#get-ticker](https://docs.kucoin.com/#get-ticker)  
<p>

*Gets ticker info of a symbol*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get info for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://docs.kucoin.com/#get-all-tickers](https://docs.kucoin.com/#get-all-tickers)  
<p>

*Gets the ticker for all trading pairs*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://docs.kucoin.com/#get-trade-histories](https://docs.kucoin.com/#get-trade-histories)  
<p>

*Gets the recent trade history for a symbol*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get trade history for|
|_[Optional]_ ct|Cancellation token|

</p>
