---
title: IKucoinRestClientSpotApiProAccount
has_children: false
parent: IKucoinRestClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > SpotApi > ProAccount`  
*Kucoin Spot trading high frequency endpoints, placing and mananging orders.*
  

***

## CancelOrderAsync  

[https://docs.kucoin.com/spot-hf/#cancellation-of-orders-by-orderid](https://docs.kucoin.com/spot-hf/#cancellation-of-orders-by-orderid)  
<p>

*Cancel an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ProAccount.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order to cancel|
|symbol|Trading pair, such as ETH-BTC|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://docs.kucoin.com/spot-hf/#details-of-a-single-hf-order](https://docs.kucoin.com/spot-hf/#details-of-a-single-hf-order)  
<p>

*Get info on a specific order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ProAccount.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrderHighFrequency>> GetOrderAsync(string orderId, string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order|
|symbol|Trading pair, such as ETH-BTC|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://docs.kucoin.com/spot-hf/#order-placement](https://docs.kucoin.com/spot-hf/#order-placement)  
<p>

*Places an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.ProAccount.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(string symbol, OrderSide side, NewOrderType type, decimal? quantity = default, decimal? price = default, decimal? quoteQuantity = default, TimeInForce? timeInForce = default, TimeSpan? cancelAfter = default, bool? postOnly = default, bool? hidden = default, bool? iceBerg = default, decimal? visibleIceBergSize = default, string? remark = default, string? clientOrderId = default, SelfTradePrevention? selfTradePrevention = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The side of the order|
|type|The type of the order|
|_[Optional]_ quantity|The quantity of the order|
|_[Optional]_ price|The price of the order. Only valid for limit orders.|
|_[Optional]_ quoteQuantity|The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty|
|_[Optional]_ timeInForce|The time the order is in force|
|_[Optional]_ cancelAfter|Cancel after a time|
|_[Optional]_ postOnly|Order is post only|
|_[Optional]_ hidden|Order is hidden|
|_[Optional]_ iceBerg|Order is an iceberg order|
|_[Optional]_ visibleIceBergSize|The maximum visible size of an iceberg order|
|_[Optional]_ remark|Remark on the order|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ selfTradePrevention|Self trade prevention setting|
|_[Optional]_ ct|Cancellation token|

</p>
