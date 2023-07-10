---
title: IKucoinRestClientSpotApiTrading
has_children: false
parent: IKucoinRestClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > SpotApi > Trading`  
*Kucoin Spot trading endpoints, placing and mananging orders.*
  

***

## CancelAllOrdersAsync  

[https://docs.kucoin.com/#cancel-all-orders](https://docs.kucoin.com/#cancel-all-orders)  
<p>

*Cancel all open orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelAllOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = default, TradeType? tradeType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Only cancel orders for this symbol|
|_[Optional]_ tradeType|Only cancel orders for this type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelLendOrderAsync  

[https://docs.kucoin.com/#cancel-lend-order](https://docs.kucoin.com/#cancel-lend-order)  
<p>

*Cancel an active lend order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelLendOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> CancelLendOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderAsync  

[https://docs.kucoin.com/#cancel-an-order](https://docs.kucoin.com/#cancel-an-order)  
<p>

*Cancel an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderByClientOrderIdAsync  

[https://docs.kucoin.com/#cancel-single-order-by-clientoid](https://docs.kucoin.com/#cancel-single-order-by-clientoid)  
<p>

*Cancel an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client order id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelStopOrderAsync  

[https://docs.kucoin.com/#cancel-an-order-2](https://docs.kucoin.com/#cancel-an-order-2)  
<p>

*Cancel a stop order by order id*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelStopOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelStopOrderByClientOrderIdAsync  

[https://docs.kucoin.com/#cancel-single-order-by-clientoid-2](https://docs.kucoin.com/#cancel-single-order-by-clientoid-2)  
<p>

*Cancel a stop order by client order id*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelStopOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrder>> CancelStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelStopOrdersAsync  

[https://docs.kucoin.com/#cancel-orders](https://docs.kucoin.com/#cancel-orders)  
<p>

*Cancel all stop orders fitting the provided parameters*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.CancelStopOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrdersAsync(string? symbol = default, IEnumerable<string>? orderIds = default, TradeType? tradeType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol to cancel orders on|
|_[Optional]_ orderIds|Order ids of the orders to cancel|
|_[Optional]_ tradeType|Trade type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBorrowOrderAsync  

[https://docs.kucoin.com/#get-borrow-order](https://docs.kucoin.com/#get-borrow-order)  
<p>

*Get info on a specific borrow order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetBorrowOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinBorrowOrder>> GetBorrowOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The order id of the borrow order|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedBorrowRecordsAsync  

[https://docs.kucoin.com/#get-repayment-record](https://docs.kucoin.com/#get-repayment-record)  
<p>

*Get repaid borrow records*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetClosedBorrowRecordsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinBorrowRecord>>> GetClosedBorrowRecordsAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedLendOrdersAsync  

[https://docs.kucoin.com/#get-lent-history](https://docs.kucoin.com/#get-lent-history)  
<p>

*Get closed lend orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetClosedLendOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetClosedLendOrdersAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedLendsAsync  

[https://docs.kucoin.com/#get-settled-lend-order-history](https://docs.kucoin.com/#get-settled-lend-order-history)  
<p>

*Get active lends*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetClosedLendsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinClosedLend>>> GetClosedLendsAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalOrdersAsync  

[https://docs.kucoin.com/#get-v1-historical-orders-list](https://docs.kucoin.com/#get-v1-historical-orders-list)  
<p>

*Gets a list of historical orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetHistoricalOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>> GetHistoricalOrdersAsync(string? symbol = default, OrderSide? side = default, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter list by symbol|
|_[Optional]_ side|Filter list by order side|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedClosedBorrowRecordsAsync  

[https://docs.kucoin.com/#query-repayment-records](https://docs.kucoin.com/#query-repayment-records)  
<p>

*Get repaid isolated borrow records*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetIsolatedClosedBorrowRecordsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinIsolatedClosedBorrowRecord>>> GetIsolatedClosedBorrowRecordsAsync(string? symbol = default, string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedOpenBorrowRecordsAsync  

[https://docs.kucoin.com/#get-repay-record](https://docs.kucoin.com/#get-repay-record)  
<p>

*Get outstanding isolated borrow records*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetIsolatedOpenBorrowRecordsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinIsolatedOpenBorrowRecord>>> GetIsolatedOpenBorrowRecordsAsync(string? symbol = default, string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingStatusAsync  

[https://docs.kucoin.com/#get-account-lend-record](https://docs.kucoin.com/#get-account-lend-record)  
<p>

*Get lending status per asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetLendingStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinLendHistory>>> GetLendingStatusAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenBorrowRecordsAsync  

[https://docs.kucoin.com/#get-repay-record](https://docs.kucoin.com/#get-repay-record)  
<p>

*Get outstanding borrow records*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOpenBorrowRecordsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinRepayRecord>>> GetOpenBorrowRecordsAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenLendOrdersAsync  

[https://docs.kucoin.com/#get-active-order](https://docs.kucoin.com/#get-active-order)  
<p>

*Get open lend orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOpenLendOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetOpenLendOrdersAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenLendsAsync  

[https://docs.kucoin.com/#get-active-lend-order-list](https://docs.kucoin.com/#get-active-lend-order-list)  
<p>

*Get active lends*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOpenLendsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinOpenLend>>> GetOpenLendsAsync(string? asset = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|The page to retrieve|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://docs.kucoin.com/#get-an-order](https://docs.kucoin.com/#get-an-order)  
<p>

*Get info on a specific order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderByClientOrderIdAsync  

[https://docs.kucoin.com/#get-single-active-order-by-clientoid](https://docs.kucoin.com/#get-single-active-order-by-clientoid)  
<p>

*Get info on a specific order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client order id of the order|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrdersAsync  

[https://docs.kucoin.com/#list-orders](https://docs.kucoin.com/#list-orders)  
<p>

*Gets a list of orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string? symbol = default, OrderSide? side = default, OrderType? type = default, DateTime? startTime = default, DateTime? endTime = default, OrderStatus? status = default, TradeType? tradeType = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter list by symbol|
|_[Optional]_ side|Filter list by order side|
|_[Optional]_ type|Filter list by order type|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ status|Filter list by order status. Defaults to done|
|_[Optional]_ tradeType|The type of orders to retrieve|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentOrdersAsync  

[https://docs.kucoin.com/#get-v1-historical-orders-list](https://docs.kucoin.com/#get-v1-historical-orders-list)  
<p>

*Gets a list of max 1000 orders in the last 24 hours*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetRecentOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinOrder>>> GetRecentOrdersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentUserTradesAsync  

[https://docs.kucoin.com/#recent-fills](https://docs.kucoin.com/#recent-fills)  
<p>

*Gets a list of max 1000 fills in the last 24 hours*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetRecentUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStopOrderAsync  

[https://docs.kucoin.com/#get-single-order-info](https://docs.kucoin.com/#get-single-order-info)  
<p>

*Get a stop order by id*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetStopOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinStopOrder>> GetStopOrderAsync(string orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStopOrderByClientOrderIdAsync  

[https://docs.kucoin.com/#get-single-order-by-clientoid](https://docs.kucoin.com/#get-single-order-by-clientoid)  
<p>

*Get a stop order by client order id*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetStopOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinStopOrder>>> GetStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStopOrdersAsync  

[https://docs.kucoin.com/#list-stop-orders](https://docs.kucoin.com/#list-stop-orders)  
<p>

*Get a list of stop orders fitting the provided parameters*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetStopOrdersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinStopOrder>>> GetStopOrdersAsync(bool? activeOrders = default, string? symbol = default, OrderSide? side = default, OrderType? type = default, TradeType? tradeType = default, DateTime? startTime = default, DateTime? endTime = default, IEnumerable<string>? orderIds = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ activeOrders|True to return active orders, false for completed orders|
|_[Optional]_ symbol|Symbol of the orders|
|_[Optional]_ side|Side of the orders|
|_[Optional]_ type|Type of the orders|
|_[Optional]_ tradeType|Trade type|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ orderIds|Filter list by order ids|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://docs.kucoin.com/#list-fills](https://docs.kucoin.com/#list-fills)  
<p>

*Gets a list of fills*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.GetUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinUserTrade>>> GetUserTradesAsync(string? symbol = default, OrderSide? side = default, OrderType? type = default, DateTime? startTime = default, DateTime? endTime = default, string? orderId = default, TradeType? tradeType = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter list by symbol|
|_[Optional]_ side|Filter list by order side|
|_[Optional]_ type|Filter list by order type|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ orderId|Filter list by order id|
|_[Optional]_ tradeType|The type of orders to retrieve|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceBorrowOrderAsync  

[https://docs.kucoin.com/#post-borrow-order](https://docs.kucoin.com/#post-borrow-order)  
<p>

*Places a Borrow order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceBorrowOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewBorrowOrder>> PlaceBorrowOrderAsync(string asset, BorrowOrderType type, decimal quantity, decimal? maxRate = default, string? term = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Currency to Borrow e.g USDT etc|
|type|The type of the order (FOK, IOC)|
|quantity|Total size|
|_[Optional]_ maxRate|The max interest rate. All interest rates are acceptable if this field is left empty|
|_[Optional]_ term|term (Unit: Day). All terms are acceptable if this field is left empty. Please note to separate the terms via comma. For example, 7,14,28|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceBulkOrderAsync  

[https://docs.kucoin.com/#place-bulk-orders](https://docs.kucoin.com/#place-bulk-orders)  
<p>

*Places bulk orders*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceBulkOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinBulkOrderResponse>> PlaceBulkOrderAsync(string symbol, IEnumerable<KucoinBulkOrderRequestEntry> orders, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|orders|Up to 5 orders to be placed at the same time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceIsolatedBorrowOrderAsync  

[https://docs.kucoin.com/#isolated-margin-borrowing](https://docs.kucoin.com/#isolated-margin-borrowing)  
<p>

*Places an isolated Borrow order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceIsolatedBorrowOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewIsolatedBorrowOrder>> PlaceIsolatedBorrowOrderAsync(string symbol, string asset, decimal quantity, BorrowOrderType type, decimal? maxRate = default, string? term = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|asset|Currency to Borrow e.g USDT etc|
|quantity|Total size|
|type|The type of the order (FOK, IOC)|
|_[Optional]_ maxRate|The max interest rate. All interest rates are acceptable if this field is left empty|
|_[Optional]_ term|term (Unit: Day). All terms are acceptable if this field is left empty. Please note to separate the terms via comma. For example, 7,14,28|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceLendOrderAsync  

[https://docs.kucoin.com/#post-lend-order](https://docs.kucoin.com/#post-lend-order)  
<p>

*Place a new lend order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceLendOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewOrder>> PlaceLendOrderAsync(string asset, decimal quantity, decimal dailyInterestRate, int term, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to lend|
|quantity|Quantity to lend|
|dailyInterestRate|Daily interest rate. 0.01 = 1%|
|term|The term in days|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceMarginOrderAsync  

[https://docs.kucoin.com/#place-a-margin-order](https://docs.kucoin.com/#place-a-margin-order)  
<p>

*Places a margin order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewMarginOrder>> PlaceMarginOrderAsync(string symbol, OrderSide side, NewOrderType type, decimal? price = default, decimal? quantity = default, decimal? quoteQuantity = default, TimeInForce? timeInForce = default, TimeSpan? cancelAfter = default, bool? postOnly = default, bool? hidden = default, bool? iceBerg = default, decimal? visibleIceBergSize = default, string? remark = default, MarginMode? marginMode = default, bool? autoBorrow = default, SelfTradePrevention? selfTradePrevention = default, string? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The side((buy or sell) of the order|
|type|The type of the order|
|_[Optional]_ price|The price of the order. Only valid for limit orders.|
|_[Optional]_ quantity|Quantity of base asset to buy or sell of the order|
|_[Optional]_ quoteQuantity|The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty|
|_[Optional]_ timeInForce|The time the order is in force|
|_[Optional]_ cancelAfter|Cancel after a time|
|_[Optional]_ postOnly|Order is post only|
|_[Optional]_ hidden|Order is hidden|
|_[Optional]_ iceBerg|Order is an iceberg order|
|_[Optional]_ visibleIceBergSize|The maximum visible size of an iceberg order|
|_[Optional]_ remark|Remark on the order|
|_[Optional]_ marginMode|The type of trading, including 'cross' and 'isolated'|
|_[Optional]_ autoBorrow|Auto-borrow to place order.|
|_[Optional]_ selfTradePrevention|Self trade prevention setting|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://docs.kucoin.com/#place-a-new-order](https://docs.kucoin.com/#place-a-new-order)  
<p>

*Places an order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceOrderAsync(/* parameters */);  
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

***

## PlaceStopOrderAsync  

[https://docs.kucoin.com/#place-a-new-order-2](https://docs.kucoin.com/#place-a-new-order-2)  
<p>

*Place a new stop order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.PlaceStopOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewOrder>> PlaceStopOrderAsync(string symbol, OrderSide orderSide, NewOrderType orderType, StopCondition stopCondition, decimal stopPrice, string? remark = default, SelfTradePrevention? selfTradePrevention = default, TradeType? tradeType = default, decimal? price = default, decimal? quantity = default, TimeInForce? timeInForce = default, TimeSpan? cancelAfter = default, bool? postOnly = default, bool? hidden = default, bool? iceberg = default, decimal? visibleSize = default, string? clientOrderId = default, decimal? quoteQuantity = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|orderSide|The side of the order|
|orderType|The type of the order|
|stopCondition|Stop price condition|
|stopPrice|Price to trigger the order placement|
|_[Optional]_ remark|Remark on the order|
|_[Optional]_ selfTradePrevention|Self trade prevention setting|
|_[Optional]_ tradeType|Trade type|
|_[Optional]_ price|The price of the order. Only valid for limit orders.|
|_[Optional]_ quantity|The quantity of the order|
|_[Optional]_ timeInForce|The time the order is in force|
|_[Optional]_ cancelAfter|Cancel after a time|
|_[Optional]_ postOnly|Order is post only|
|_[Optional]_ hidden|Order is hidden|
|_[Optional]_ iceberg|Order is an iceberg order|
|_[Optional]_ visibleSize|The maximum visible size of an iceberg order|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ quoteQuantity|The funds to use for the order. Only valid for market orders. If used, quantity needs to be empty|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayAllAsync  

[https://docs.kucoin.com/#one-click-repayment](https://docs.kucoin.com/#one-click-repayment)  
<p>

*Repay all borrowed for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.RepayAllAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> RepayAllAsync(string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to repay for|
|strategy|Repayment strategy|
|quantity|Quantity to repay|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayAllIsolatedAsync  

[https://docs.kucoin.com/#quick-repayment](https://docs.kucoin.com/#quick-repayment)  
<p>

*Repay all isolated borrowed for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.RepayAllIsolatedAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> RepayAllIsolatedAsync(string symbol, string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to repay for|
|asset|Asset to repay for|
|strategy|Repayment strategy|
|quantity|Quantity to repay|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepaySingleBorrowOrderAsync  

[https://docs.kucoin.com/#repay-a-single-order](https://docs.kucoin.com/#repay-a-single-order)  
<p>

*Repay a Single Order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.RepaySingleBorrowOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> RepaySingleBorrowOrderAsync(string asset, string tradeId, decimal quantity, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to Pay e.g USDT etc|
|tradeId|Trade ID of borrow order|
|quantity|Repayment size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepaySingleIsolatedBorrowOrderAsync  

[https://docs.kucoin.com/#single-repayment](https://docs.kucoin.com/#single-repayment)  
<p>

*Repay a Single Order*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.RepaySingleIsolatedBorrowOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> RepaySingleIsolatedBorrowOrderAsync(string symbol, string asset, decimal quantity, string loanId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to Pay e.g BTC-USDT etc|
|asset|Asset to Pay e.g USDT etc|
|quantity|Repayment size|
|loanId|Loan ID of borrow order|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SetAutoLendAsync  

[https://docs.kucoin.com/#set-auto-lend](https://docs.kucoin.com/#set-auto-lend)  
<p>

*Set up automatic lending for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Trading.SetAutoLendAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult> SetAutoLendAsync(string asset, bool isEnabled, decimal? retainQuantity = default, decimal? dailyInterestRate = default, int? term = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to lend|
|isEnabled|Is enabled or not|
|_[Optional]_ retainQuantity|Reserved quantity in main account|
|_[Optional]_ dailyInterestRate|Daily interest rate. 0.01 = 1%|
|_[Optional]_ term|The term in days|
|_[Optional]_ ct|Cancellation token|

</p>
