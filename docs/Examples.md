---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

<Details>
<Summary>
<b>Spot API</b>

</Summary>
<BlockQuote>

#### Get market data
````C#
// Getting info on all symbols
var symbolData = await kucoinClient.SpotApi.ExchangeData.GetSymbolsAsync();

// Getting tickers for all symbols
var tickerData = await kucoinClient.SpotApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var orderBookData = await kucoinClient.SpotApi.ExchangeData.GetOrderBookAsync("BTC-USDT");

// Getting recent trades of a symbol
var tradeHistoryData = await kucoinClient.SpotApi.ExchangeData.GetTradeHistoryAsync("BTC-USDT");
````

#### Requesting balances
Balances are stored in `accounts` on Kucoin, so balances can be retrieved using `GetAccountsAsync`
````C#
var accountData = await kucoinClient.SpotApi.Account.GetAccountsAsync();
````
#### Placing order
````C#
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await kucoinClient.SpotApi.Trading.PlaceOrderAsync(
				"BTC-USDT", 
				OrderSide.Buy, 
				NewOrderType.Limit, 
				0.001m, 
				50000, 
				timeInForce: TimeInForce.GoodTillCanceled);
													
// Placing a buy market order, spending 50 USDT
var orderData = await kucoinClient.SpotApi.Trading.PlaceOrderAsync(
				"BTC-USDT",
				OrderSide.Buy,
				NewOrderType.Market,
				quoteQuantity: 50);
													
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData = await kucoinClient.SpotApi.Trading.PlaceStopOrderAsync(
				"BTC-USDT",
				OrderSide.Buy,
				NewOrderType.Limit,
				StopCondition.Loss,
				40000,
				price: 39000,
				quantity: 0.001m,
				timeInForce: TimeInForce.GoodTillCanceled);
````

#### Requesting a specific order
````C#
// Request info on order with id `1234`
var orderData = await kucoinClient.SpotApi.Trading.GetOrderAsync("1234");
````

#### Requesting order history
````C#
// Get all orders conform the parameters. This example gets all BTC-USDT limit orders which are currently active
 var ordersData = await kucoinClient.SpotApi.Trading.GetOrdersAsync("BTC-USDT", type: OrderType.Limit, status: OrderStatus.Active);
````

#### Cancel order
````C#
// Cancel order with id `1234`
var orderData = await kucoinClient.SpotApi.Trading.CancelOrderAsync("1234");
````

#### Get user trades
````C#
var userTradesResult = await kucoinClient.SpotApi.Trading.GetUserTradesAsync();
````

#### Subscribing to market data updates
````C#
var subscribeResult = await kucoinSocketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(data =>
{
	// Handle ticker data
});
````

#### Subscribing to order updates
````C#
var subscribeResult = await kucoinSocketClient.SpotStreams.SubscribeToOrderUpdatesAsync(data =>
{
	// Handle order updates
},
data =>
{
	// Handle match updates
});
````
</BlockQuote>
</Details>

<Details>
<Summary>
<b>Futures API</b>

</Summary>
<BlockQuote>

#### Get market data
````C#
 // Getting info on all contracts
var contractData = await kucoinClient.FuturesApi.ExchangeData.GetOpenContractsAsync();

// Getting the order book of a symbol
var orderBookData = await kucoinClient.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync("XBTUSDM");

// Getting recent trades of a symbol
var tradeHistoryData = await kucoinClient.FuturesApi.ExchangeData.GetTradeHistoryAsync("XBTUSDM");
````

#### Requesting positions
````C#
// Getting your current positions
var positionResultData = await kucoinClient.FuturesApi.Account.GetPositionsAsync();
````

#### Placing order
````C#
// Placing a market buy order for 10 contracts with 10x leverage
var positionResultData = await kucoinClient.FuturesApi.Trading.PlaceOrderAsync(
				"XBTUSDM",
				OrderSide.Buy,
				NewOrderType.Market,
				10,
				10);
````

#### Requesting a specific order
````C#
// Get info on an order id 1234
var orderResult = await kucoinClient.FuturesApi.Trading.GetOrderAsync("1234");

````

#### Requesting order history
````C#
// Get all orders for the account. Can apply filters as parameters
var orderResult = await kucoinClient.FuturesApi.Trading.GetOrdersAsync();
````

#### Cancel order
````C#
// Cancel order with id 1234
var orderResult = await kucoinClient.FuturesApi.Trading.CancelOrderAsync("1234");

````

#### Get user trades
````C#
var userTradesResult = await kucoinClient.FuturesApi.Trading.GetUserTradesAsync();
````

#### Subscribing to position updates
````C#
// Any of the data handler can be passed as `null` to ignore that type of update
kucoinSocketClient.FuturesStreams.SubscribeToPositionUpdatesAsync("XBTUSDM",
                data =>
                {
					// Handle position updates
                },
                data =>
                {
					// Handle position update because of mark price change
                },
                data =>
                {
					// Handle funding settlement update
                },
                data =>
                {
					// Handle risk adjust update
                });
````
</BlockQuote>
</Details>
