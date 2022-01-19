There are a decent amount of breaking changes when moving from version 3.x.x to version 4.x.x. Although the interface has changed, the available endpoints/information have not, so there should be no need to completely rewrite your program.
Most endpoints are now available under a slightly different name or path, and most data models have remained the same, barring a few renames.
In this document most changes will be described. If you have any other questions or issues when updating, feel free to open an issue.

Changes related to `IExchangeClient`, options and client structure are also (partially) covered in the [CryptoExchange.Net Migration Guide](https://jkorf.github.io/CryptoExchange.Net/Migration%20Guide.html)

### Namespaces
There are a few namespace changes:  
|Type|Old|New|
|----|---|---|
|Enums|`Kucoin.Net.Objects`|`Kucoin.Net.Enums`  |
|Clients|`Kucoin.Net`|`Kucoin.Net.Clients`  |
|Client interfaces|`Kucoin.Net.Interfaces`|`Kucoin.Net.Interfaces.Clients`  |
|Objects|`Kucoin.Net.Objects`|`Kucoin.Net.Objects.Models`  |
|SymbolOrderBook|`Kucoin.Net`|`Kucoin.Net.SymbolOrderBooks`|

### Client options
*V3*
```csharp
var kucoinClient = new KucoinClient(new KucoinClientOptions
{
	LogLevel = LogLevel.Trace,
	HttpClient = httpClient,
	ApiCredentials = new KucoinApiCredentials("SPOT-API-KEY", "SPOT-API-SECRET", "SPOT-API-PASSPHRASE"),
	FuturesApiCredentials = new KucoinApiCredentials("FUTURES-API-KEY", "FUTURES-API-SECRET", "FUTURES-API-PASSPHRASE")
});
```

*V4*
```csharp
var kucoinClient = new KucoinClient(new KucoinClientOptions()
{
	LogLevel = LogLevel.Trace,
	HttpClient = httpClient,
	SpotApiOptions = new KucoinRestApiClientOptions
	{
		ApiCredentials = new KucoinApiCredentials("SPOT-API-KEY", "SPOT-API-SECRET", "SPOT-API-PASSPHRASE")
	},
	FuturesApiOptions = new KucoinRestApiClientOptions
	{
		ApiCredentials = new KucoinApiCredentials("FUTURES-API-KEY", "FUTURES-API-SECRET", "FUTURES-API-PASSPHRASE")
	},
});
```

### Client structure
Version 4 splits both spot and futures rest API into 3 topics, `Account`, `ExchangeData` and `Trading`, and adds the `Api` post fix to the sub client. More info on this can be found on the [CryptoExchange.Net wiki](https://github.com/JKorf/CryptoExchange.Net/wiki/Clients). 
This means that all calls will have changed from for example `client.Spot.GetTickersAsync()` to `client.SpotApi.ExchangeData.GetTickersAsync()`.  
For the socket client only the sub client name has changed to have a `Streams` postfix: `socketClient.SpotStreams.Subscribe`.

*V3*
```csharp
 var accounts = await kucoinClient.Spot.GetAccountsAsync();
var withdrawals = await kucoinClient.Spot.GetWithdrawalsAsync();

var tickers = await kucoinClient.Spot.GetTickersAsync();
var symbols = await kucoinClient.Spot.GetSymbolsAsync();

var order = await kucoinClient.Spot.PlaceOrderAsync();
var trades = await kucoinClient.Spot.GetUserTradesAsync();
```

*V4*  
```csharp
var accounts = await kucoinClient.SpotApi.Account.GetAccountsAsync();
var withdrawals = await kucoinClient.SpotApi.Account.GetWithdrawalsAsync();

var tickers = await kucoinClient.SpotApi.ExchangeData.GetTickersAsync();
var symbols = await kucoinClient.SpotApi.ExchangeData.GetSymbolsAsync();

var order = await kucoinClient.SpotApi.Trading.PlaceOrderAsync();
var trades = await kucoinClient.SpotApi.Trading.GetUserTradesAsync();
```

### Enum names
`Kucoin` prefixes have been removed  
*V3*  
`KucoinOrderSide, KucoinKlineInterval`, etc  

*V4*  
`OrderSide, KlineInterval`

### Definitions
Some names have been changed to a common definition. This includes where the name is part of a bigger name  
|Old|New||
|----|---|---|
|`Currency`|`Asset`|`GetCurrenciesAsync` -> `GetAssetsAsync`|
|`Size/Amount`|`Quantity`|`WithdrawMinSize` -> `WithdrawMinQuantity` |
|`Open/High/Low/Close`|`OpenPrice/HighPrice/LowPrice/ClosePrice`||
|`kline.StartTime`|`kline.OpenTime`||
|`BestAsk/BestBid`|`BestAskPrice/BestBidPrice`||
|`VolumeValue`|`QuoteVolume`||
|`DealQuantity`|`QuantityFilled`||
|`Chain`|`Network`||

Some names have slightly changed to be consistent across different libraries   
`RemainingQuantity` -> `QuantityRemaining`  
`OrderId` -> `Id` (on a `KucoinOrder` model)  

### Changed methods
The spot PlaceOrderAsync call has had the price and quantity parameter swapped(!) and has been changed to make the clientOrderId optional, and ordering the parameters to be the same as for the PlaceOrderAsync calls in other libraries:  
*V3*  
```csharp
Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
            string symbol,
            string clientOrderId,
            KucoinOrderSide side,
            KucoinNewOrderType type,
            decimal? price = null,    // <-- 
            decimal? quantity = null, // <--
            decimal? funds = null,
            KucoinTimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            KucoinSelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);
```
*V4*  
```csharp
async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal? quantity = null,   // <-- 
            decimal? price = null,      // <-- 
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);
```