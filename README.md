# Kucoin.Net
![Build status](https://travis-ci.com/JKorf/Kucoin.Net.svg?branch=master) ![Nuget version](https://img.shields.io/nuget/v/kucoin.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/kucoin.Net.svg)

Kucoin.Net is a wrapper around the Kucoin API as described on [Kucoin](https://docs.kucoin.com/), including all features the API provides using clear and readable objects, both for the REST  as the websocket API's.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Kucoin.Net/issues)**

[Documentation](https://jkorf.github.io/Kucoin.Net/)

## Donations
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated. If you prefer to donate any other currency please contact me.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Release notes
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

