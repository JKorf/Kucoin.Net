---
title: IKucoinRestClientFuturesApiTrading
has_children: false
parent: IKucoinRestClientFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > FuturesApi > Trading`  
*Kucoin Futures trading endpoints, placing and mananging orders.*
  

***

## CancelAllOrdersAsync  

[https://docs.kucoin.com/futures/#limit-order-mass-cancelation](https://docs.kucoin.com/futures/#limit-order-mass-cancelation)  
<p>

*Cancel all open orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.CancelAllOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Cancel only orders for this symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllStopOrdersAsync  

[https://docs.kucoin.com/futures/#stop-order-mass-cancelation](https://docs.kucoin.com/futures/#stop-order-mass-cancelation)  
<p>

*Cancel all open stop orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.CancelAllStopOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelAllStopOrdersAsync(string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Cancel only orders for this symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderAsync  

[https://docs.kucoin.com/futures/#cancel-an-order](https://docs.kucoin.com/futures/#cancel-an-order)  
<p>

*Cancel an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedOrdersAsync  

[https://docs.kucoin.com/futures/#get-list-of-orders-completed-in-24h](https://docs.kucoin.com/futures/#get-list-of-orders-completed-in-24h)  
<p>

*Get list of 1000 most recent orders in the last 24 hours*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetClosedOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetClosedOrdersAsync(string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://docs.kucoin.com/futures/#get-details-of-a-single-order](https://docs.kucoin.com/futures/#get-details-of-a-single-order)  
<p>

*Get details on an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of order to retrieve|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderByClientOrderIdAsync  

[https://docs.kucoin.com/futures/#get-details-of-a-single-order](https://docs.kucoin.com/futures/#get-details-of-a-single-order)  
<p>

*Get details on an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|Client order id of order to retrieve|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrdersAsync  

[https://docs.kucoin.com/futures/#get-order-list](https://docs.kucoin.com/futures/#get-order-list)  
<p>

*Get list of orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = default, OrderStatus? status = default, OrderSide? side = default, OrderType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ status|Filter by status|
|_[Optional]_ side|Filter by side|
|_[Optional]_ type|Filter by type|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ currentPage|Current page|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentUserTradesAsync  

[https://docs.kucoin.com/futures/#recent-fills](https://docs.kucoin.com/futures/#recent-fills)  
<p>

*Get list of 1000 most recent user trades in the last 24 hours*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetRecentUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUntriggeredStopOrdersAsync  

[https://docs.kucoin.com/futures/#get-untriggered-stop-order-list](https://docs.kucoin.com/futures/#get-untriggered-stop-order-list)  
<p>

*Get list of untriggered stop orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetUntriggeredStopOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = default, OrderSide? side = default, OrderType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ side|Filter by side|
|_[Optional]_ type|Filter by type|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ currentPage|Current page|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://docs.kucoin.com/futures/#get-fills](https://docs.kucoin.com/futures/#get-fills)  
<p>

*Get list of user trades*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.GetUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = default, string? symbol = default, OrderSide? side = default, OrderType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ side|Filter by side|
|_[Optional]_ type|Filter by type|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ currentPage|Current page|
|_[Optional]_ pageSize|Size of a page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://docs.kucoin.com/futures/#place-an-order](https://docs.kucoin.com/futures/#place-an-order)  
<p>

*Place a new order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.FuturesApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(string symbol, OrderSide side, NewOrderType type, decimal leverage, int quantity, decimal? price = default, TimeInForce? timeInForce = default, bool? postOnly = default, bool? hidden = default, bool? iceberg = default, decimal? visibleSize = default, string? remark = default, StopType? stopType = default, StopPriceType? stopPriceType = default, decimal? stopPrice = default, bool? reduceOnly = default, bool? closeOrder = default, bool? forceHold = default, string? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The contract for the order|
|side|Side of the order|
|type|Type of order|
|leverage|Leverage of the order|
|quantity|Quantity of contract to buy or sell|
|_[Optional]_ price|Limit price, only for limit orders|
|_[Optional]_ timeInForce|Time in force, only for limit orders|
|_[Optional]_ postOnly|Post only flag, invalid when timeInForce is IOC|
|_[Optional]_ hidden|Orders not displaying in order book. When hidden chose|
|_[Optional]_ iceberg|Only visible portion of the order is displayed in the order book|
|_[Optional]_ visibleSize|The maximum visible size of an iceberg order|
|_[Optional]_ remark|Remark for the order|
|_[Optional]_ stopType||
|_[Optional]_ stopPriceType||
|_[Optional]_ stopPrice|Stop price|
|_[Optional]_ reduceOnly|A mark to reduce the position size only. Set to false by default|
|_[Optional]_ closeOrder|A mark to close the position. Set to false by default. All the positions will be closed if true|
|_[Optional]_ forceHold|A mark to forcely hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>
