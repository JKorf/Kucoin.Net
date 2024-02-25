# ![.Kucoin.Net](https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true) Kucoin.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Kucoin.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Kucoin.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Kucoin.Net?style=for-the-badge)

Kucoin.Net is a client library for accessing the [Kucoin REST and Websocket API](https://docs.kucoin.com/). All data is mapped to readable models and enum values. Additional features include an implementation for maintaining a client side order book, easy integration with other exchange client libraries and more.

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Get the library
[![Nuget version](https://img.shields.io/nuget/v/kucoin.net.svg?style=for-the-badge)](https://www.nuget.org/packages/Kucoin.Net) [![Nuget downloads](https://img.shields.io/nuget/dt/kucoin.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/Kucoin.Net)
 
	dotnet add package Kucoin.Net

## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USDT ticker via rest request
	var restClient = new KucoinRestClient();
	var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETH-USDT");
	var lastPrice = tickerResult.Data.LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USDT ticker updates via the websocket API
	var socketClient = new KucoinSocketClient();
	var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH-USDT", (update) =>
	{
		var lastPrice = update.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples  [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Kraken.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_common).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Huobi|[JKorf/Huobi.Net](https://github.com/JKorf/Huobi.Net)|[![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg?style=flat-square)](https://www.nuget.org/packages/Huobi.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Rest Api
|API|Supported|Location|
|--|--:|--|
|Account Basic Info|✓|`restClient.SpotApi.Account` / `restClient.FuturesApi.Account`|
|Account Sub-Account|X||
|Funding Overview|✓|`restClient.SpotApi.Account`|
|Funding Deposit|✓|`restClient.SpotApi.Account`|
|Funding Withdrawal|✓|`restClient.SpotApi.Account`|
|Funding Transfer|✓|`restClient.SpotApi.Account`|
|Funding Trade Fee|✓|`restClient.SpotApi.Account`|
|Spot Trading Market Data|✓|`restClient.SpotApi.ExchangeData`|
|Spot Trading HF Trade|✓|`restClient.SpotApi.ProAccount`|
|Spot Trading Orders|✓|`restClient.SpotApi.Trading`|
|Spot Trading Fills|✓|`restClient.SpotApi.Trading`|
|Spot Trading Stop Order|✓|`restClient.SpotApi.Trading`|
|Margin Trading Margin HT Trade|X||
|Margin Trading Margin Orders|✓|`restClient.SpotApi.Trading`|
|Margin Trading Margin Info|✓|`restClient.SpotApi.ExchangeData`|
|Margin Trading Isolated Margin|✓|`restClient.SpotApi.ExchangeData` / `restClient.SpotApi.Account`|
|Margin Trading Margin Trading(V3)|X||
|Margin Trading Lending Market(V3)|X||
|Futures Trading Market Data|✓|`restClient.FuturesApi.ExchangeData`|
|Futures Trading Orders|✓|`restClient.FuturesApi.Trading`|
|Futures Trading Fills|✓|`restClient.FuturesApi.Trading`|
|Futures Trading Positions|✓|`restClient.FuturesApi.Acount` / `restClient.FuturesApi.Trading`|
|Futures Trading Risk Limit|✓|`restClient.FuturesApi.Acount`|
|Futures Trading Funding Fees|✓|`restClient.FuturesApi.ExchangeData`|

### Websocket Api
|API|Supported|Location|
|--|--:|--|
|Spot Public|✓|`socketClient.SpotApi`|
|Spot Private|✓|`socketClient.SpotApi`|
|Margin Public|✓|`socketClient.SpotApi`|
|Margin Private|✓|`socketClient.SpotApi`|
|Futures Public|✓|`socketClient.FuturesApi`|
|Futures Private|✓|`socketClient.FuturesApi`|

## Support the project
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1qz0jv0my7fc60rxeupr23e75x95qmlq6489n8gh  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 5.2.0 - 25 Feb 2024
    * Updated CryptoExchange.Net and implemented reworked websocket message handling. For release notes for the CryptoExchange.Net base library see: https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes
    * Fixed issue in DI registration causing http client to not be correctly injected
    * Added multi symbol support for socket client subscriptions
    * Added BestOffers stream subscription to socket client spot api
    * Updated socket client spot api order subscription
    * Removed deprecated match engine subscriptions from socket client spot api
    * Updated some namespaces

* Version 5.1.0 - 23 Dec 2023
    * Added SpotApi.Account.GetUserInfoAsync
    * Added SpotApi.Account.UniversalTransferAsync
    * Added SpotApi.Account.GetCrossMarginAccountsAsync
    * Added SpotApi.Trading.PlaceTestOrderAsync
    * Added SpotApi.Trading.PlaceTestMarginOrderAsync
    * Updated SpotApi.ExchangeData.GetAssetsAsync to V3 API
    * Updated SpotApi.ExchangeData.GetAssetAsync to V3 API
    * Updated API doc links on endpoints
    * Renamed SpotApi.Account.GetUserInfoAsync to GetSubUserInfoAsync
    * Removed obsolete SpotApi.Account.GetAccountLedgerAsync

* Version 5.0.8 - 03 Dec 2023
    * Updated CryptoExchange.Net

* Version 5.0.7 - 07 Nov 2023
    * Fixed broker signing

* Version 5.0.6 - 29 Oct 2023
    * Added broker reference options

* Version 5.0.5 - 24 Oct 2023
    * Updated CryptoExchange.Net

* Version 5.0.4 - 09 Oct 2023
    * Updated CryptoExchange.Net version
    * Added ISpotClient and IFuturesClient to DI injection

* Version 5.0.3 - 25 Aug 2023
    * Updated CryptoExchange.Net

* Version 5.0.2 - 23 Jul 2023
    * Fixed credentials provided in orderbook options not getting used

* Version 5.0.1 - 11 Jul 2023
    * Fixed KucoinFuturesSymbolOrderBook constructor being internal instead of public

* Version 5.0.0 - 25 Jun 2023
    * Updated CryptoExchange.Net to version 6.0.0
    * Renamed KucoinClient to KucoinRestClient
    * Renamed **Streams to **Api on the KucoinSocketClient
    * Updated endpoints to consistently use a base url without any path as basis to make switching environments/base urls clearer
    * Added IKucoinOrderBookFactory and implementation for creating order books
    * Updated dependency injection register method (AddKucoin)

* Version 4.3.3 - 15 Apr 2023
    * Fixed futures ping, preventing regular disconnects
    * Added basic high frequency endpoints
    * Fixed testnet SpotApi.GetSymbolsAsync endpoint
    * Added futures RiskLimit endpoints
    * Added futures TransferToFutures endpoint
    * Added various new optional parameters
    * Removed deprecated endpoints

* Version 4.3.2 - 18 Mar 2023
    * Added HighFrequency account type
    * Added Network property to asset details model
    * Fixed Futures api size parameter type

* Version 4.3.1 - 14 Feb 2023
    * Updated CryptoExchange.Net

* Version 4.3.0 - 05 Feb 2023
    * Updated GetSymbolsAsync to V2 endpoint
    * Added MinFunds property to Symbols model
    * Added missing Network properties on Withdraw/Deposit models
    * Added feeDeductType parameter on WithdrawAsync endpoint

* Version 4.2.1 - 21 Nov 2022
    * Fixed reconnect url

* Version 4.2.0 - 17 Nov 2022
    * Updated CryptoExchange.Net
    * Removed Api Credentials need for order book endpoints

* Version 4.1.0 - 11 Oct 2022
    * Fixed leverage field being an int instead of decimal
    * Fixed CancelAfter property on order model
    * Added fromTag and toTag to InnerTransferAsync endpoint
    * Fixed PlaceIsolatedBorrowOrderAsync type parameter

* Version 4.0.17 - 15 Aug 2022
    * Fixed SpotApi.Trading.PlaceStopOrderAsync cancelAfter parameter
    * Fixed KucoinBalanceUpdate Timestamp property not getting deserialized correctly
    * Fixed KucoinBorrowRecord RepayTime deserialization

* Version 4.0.16 - 31 Jul 2022
    * Added PlaceBulkOrderAsync endpoint
    * Fixed QuantityStep mapping on ISpotClient GetSymbolsAsync

* Version 4.0.15 - 18 Jul 2022
    * Fix for websocket not reconnecting
    * Updated CryptoExchange.Net

* Version 4.0.14 - 16 Jul 2022
    * Added isolated margin endpoints
    * Updated xml api docs references
    * Updated CryptoExchange.Net

* Version 4.0.13 - 10 Jul 2022
    * Updated CryptoExchange.Net

* Version 4.0.12 - 12 Jun 2022
    * Added margin endpoints
    * Updated CryptoExchange.Net

* Version 4.0.11 - 24 May 2022
    * Updated CryptoExchange.Net

* Version 4.0.10 - 22 May 2022
    * Fixed MaxSocketConnections incorrectly being set to 10 instead of 50
    * Added TradeType filter to CancelAllOrdersAsync
    * Updated CryptoExchange.Net

* Version 4.0.9 - 08 May 2022
    * Removed deprecated spot GetOrderBookAsync endpoint and fixed the CommonSpotClient order book endpoint
    * Updated CryptoExchange.Net

* Version 4.0.8 - 01 May 2022
    * Updated CryptoExchange.Net which fixed an timing related issue in the websocket reconnection logic
    * Added seconds representation to KlineInterval enum

* Version 4.0.7 - 21 Apr 2022
    * Fixed timeInForce parameter being sent as null if not specified
    * Fixed typo

* Version 4.0.6 - 14 Apr 2022
    * Fixed NullReference exception when PlaceOrder on common futures client fails
    * Fixed deserialization error on PlaceMarginOrderAsync
    * Fixed Common clients 15 minute klines returning 5 minute klines
    * Updated CryptoExchange.Net

* Version 4.0.5 - 10 Mar 2022
    * Updated CryptoExchange.Net, fixing order deserialization in .net framework

* Version 4.0.4 - 08 Mar 2022
    * Added Spot GetMarginAccountAsync endpoint
    * Added Spot GetRiskLimitAsync endpoint
    * Added Spot GetMarginConfigurationAsync endpoint
    * Updated GetAssetAsync endpoint to V2, now includes networks
    * Updated CryptoExchange.Net

* Version 4.0.3 - 01 Mar 2022
    * Updated KucoinContract model with missing properties
    * Updated CryptoExchange.Net improving the websocket reconnection robustness

* Version 4.0.2 - 27 Feb 2022
    * Fixed quantity/price parameter issue in ISpotClient PlaceOrderAsync
    * Updated CryptoExchange.Net to fix timestamping issue when request is ratelimiter

* Version 4.0.1 - 24 Feb 2022
    * Updated CryptoExchange.Net

* Version 4.0.0 - 18 Feb 2022
	* Added Github.io page for documentation: https://jkorf.github.io/Kucoin.Net/
	* Added unit tests for parsing the returned JSON for each endpoint and subscription
	* Added AddKucoin extension method on IServiceCollection for easy dependency injection
	* Added URL reference to API endpoint documentation for each endpoint
	* Added default rate limiter

	* Refactored client structure to be consistent across exchange implementations
	* Renamed various properties to be consistent across exchange implementations

	* Cleaned up project structure
	* Fixed various models

	* Updated CryptoExchange.Net, see https://github.com/JKorf/CryptoExchange.Net#release-notes
	* See https://jkorf.github.io/Kucoin.Net/MigrationGuide.html for additional notes for updating from V3 to V4

* Version 3.1.6 - 04 Nov 2021
    * Fixed futures order deserialization

* Version 3.1.5 - 03 Nov 2021
    * Added PostBorrowOrder, GetBorrowOrderAsync and RepaySingleBorrowOrderAsync endpoints
    * Fixed authentication when there are special characters in the parameters
    * Fixed StopPriceType parsing in Futures order model
    * Added missing funding book subscription in socket client interface
    * Fixed futures user trade timestamp deserialization
    * Fixed futures position AverageEntryPrice deserialization
    * Added some missing model properties

* Version 3.1.4 - 08 Oct 2021
    * Fixed price serialization new future order
    * Updated CryptoExchange.Net to fix some socket issues

* Version 3.1.3 - 06 Oct 2021
    * Updated CryptoExchange.Net, fixing socket issue when calling from .Net Framework

* Version 3.1.2 - 05 Oct 2021
    * Added optional chain parameter to GetWithdrawalQuotasAsync endpoint
    * Fix for Future order StopOrderType deserialization
    * Fixed some properties on KucoinPosition model

* Version 3.1.1 - 29 Sep 2021
    * Changed GetFiatPricesAsync parameter from `params` to `IEnumerable<string>`
    * Updated CryptoExchange.Net

* Version 3.1.0 - 20 Sep 2021
    * Added missing SetApiCredentials endpoints
    * Updated CryptoExchange.Net

* Version 3.0.10 - 20 Sep 2021
    * upl kucoin

* Version 3.0.9 - 15 Sep 2021
    * Updated CryptoExchange.Net

* Version 3.0.8 - 14 Sep 2021
    * Fix for futures credentials not working when using SetDefaultOptions

* Version 3.0.7 - 14 Sep 2021
    * Fixed clientOrderId parameter in futures PlaceOrderAsync
    * Fixed clientOrderId in futures stream order update
    * Added PlaceMarginOrderAsync endpoint
    * Fixed PlaceOrderAsync not returning order id

* Version 3.0.6 - 03 Sep 2021
    * Added timestamp to trade update
    * Fixed PlaceOrderAsync optional stopType and stopPriceType parameters not being optional on futures

* Version 3.0.5 - 02 Sep 2021
    * Fix for disposing order book closing socket even if there are other connections

* Version 3.0.4 - 26 Aug 2021
    * Updated CryptoExchange.Net

* Version 3.0.3 - 24 Aug 2021
    * Updated CryptoExchange.Net, improving websocket and SymbolOrderBook performance

* Version 3.0.2 - 13 Aug 2021
    * Fix for OperationCancelledException being thrown when closing a socket from a .net framework project

* Version 3.0.1 - 13 Aug 2021
    * Added GetDepositAddressesAsync to retrieve all deposit addresses for a currency
    * Added chain parameter to GetDepositAddressAsync and CreateDepositAddressAsync

* Version 3.0.0 - 12 Aug 2021
	* Release version with new CryptoExchange.Net version 4.0.0
		* Multiple changes regarding logging and socket connection, see [CryptoExchange.Net release notes](https://github.com/JKorf/CryptoExchange.Net#release-notes)
		
* Version 3.0.0-beta3 - 09 Aug 2021
    * Added Futures support
    * Fixed KucoinSymbolOrderBook
    * Renamed GetSymbolTradesAsync to GetTradeHistoryAsync
    * Renamed GetFillsAsync to GetUserTradesAsync
    * Renamed GetRecentFillsAsync to GetRecentUserTradesAsync

* Version 3.0.0-beta2 - 26 Jul 2021
    * Updated CryptoExchange.Net

* Version 3.0.0-beta1 - 09 Jul 2021
    * Added stop order endpoints
    * Added Async postfix for async methods
    * Updated CryptoExchange.Net

* Version 2.3.9 - 05 mei 2021
    * Fixed order deserialization when quantity is null

* Version 2.3.8 - 04 mei 2021
    * Added some margin socket subscriptions

* Version 2.3.7 - 28 apr 2021
    * Added new GetAccountLedgers
    * Changed GetAccountLedger to [Obsolete]
    * Fixed AccountActivityContext parsing
    * Updated CryptoExchange.Net

* Version 2.3.6 - 19 apr 2021
    * Updated CryptoExchange.Net

* Version 2.3.5 - 30 mrt 2021
    * Updated CryptoExchange.Net

* Version 2.3.4 - 16 mrt 2021
    * Fixed full order book timestamp deserialization

* Version 2.3.3 - 16 mrt 2021
    * Fixed orderbook endpoint not found

* Version 2.3.2 - 16 mrt 2021
    * Added fee endpoints
    * Added CancelOrderByClientOrderId endpoint
    * Added GetOrderByClientOrderId endpoint
    * Updated IKucoinClient interface

* Version 2.3.1 - 05 mrt 2021
    * Fixed Filled order update parsing

* Version 2.3.0 - 04 mrt 2021
    * Added socket kline subscription
    * Added socket order book subscription
    * Added multiple market support for snapshot subscription
    * Updated match subscriptions

* Version 2.2.1 - 01 mrt 2021
    * Added Nuget SymbolPackage

* Version 2.2.0 - 01 mrt 2021
    * Added config for deterministic build
    * Updated CryptoExchange.Net

* Version 2.1.2 - 22 jan 2021
    * Updated for ICommonKline

* Version 2.1.1 - 14 jan 2021
    * Updated CryptoExchange.Net

* Version 2.1.0 - 21 dec 2020
    * Update CryptoExchange.Net
    * Updated to latest IExchangeClient

* Version 2.0.17 - 11 dec 2020
    * Fix for GetKlines sending null timestamp

* Version 2.0.16 - 11 dec 2020
    * Updated CryptoExchange.Net
    * Implemented IExchangeClient

* Version 2.0.15 - 19 nov 2020
    * Fixed order model to allow null values
    * Updated CryptoExchange.Net

* Version 2.0.14 - 08 Oct 2020
    * Fixed incorrect paramter on GetSymbols
    * Updated CryptoExchange.Net

* Version 2.0.13 - 28 Aug 2020
    * Updated CryptoExchange.Net

* Version 2.0.12 - 12 Aug 2020
    * Fixed cancelAfter parameter in PlaceOrder
    * Updated CryptoExchange.Net

* Version 2.0.11 - 05 Aug 2020
    * Fixed withdraw endpoint
    * Added InnerTransfer support

* Version 2.0.10 - 03 Aug 2020
    * Fixed timestamp parameters

* Version 2.0.9 - 22 Jul 2020
    * Added missing nullable

* Version 2.0.8 - 22 Jul 2020
    * More nullable fields for new markets

* Version 2.0.7 - 20 Jul 2020
    * Made decimals in Tick model nullable to support new markets

* Version 2.0.6 - 07 Jul 2020
    * Fixed parsing error in MatchEngine updates

* Version 2.0.5 - 21 Jun 2020
    * Updated CryptoExchange

* Version 2.0.4 - 16 Jun 2020
    * Updated CryptoExchange.Net

* Version 2.0.3 - 07 Jun 2020
	* Updated CryptoExchange.Net to fix order book desync

* Version 2.0.2 - 03 Mar 2020
    * Updated CryptoExchange

* Version 2.0.1 - 23 Oct 2019
	* Fixed validation length symbols

* Version 2.0.0 - 23 Oct 2019
	* See CryptoExchange.Net 3.0 release notes
	* Added input validation
	* Added CancellationToken support to all requests
	* Now using IEnumerable<> for collections	
	* Renamed Market -> Symbol	

* Version 1.0.4 - 30 Sep 2019
    * Fixed Bid/Ask reversed in tick
    * Fixed error on empty self trade prevention field

* Version 1.0.3 - 23 Sep 2019
    * Fixed parameters not passed to certain requests

* Version 1.0.2 - 07 Aug 2019
    * Updated CryptoExchange.Net

* Version 1.0.1 - 05 Aug 2019
    * added code docs xml

* Version 1.0.0 - 09 jul 2019
	* Updated KucoinSymbolOrderBook

* Version 0.0.2 - 14 may 2019
	* Added an order book implementation for easily keeping an updated order book
	* Added additional constructor to ApiCredentials to be able to read from file
	* Added ConfigureAwait calls to prevent deadlocks

* Version 0.0.1 - 09 may 2019
	* Initial release

