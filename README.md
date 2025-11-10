# ![.Kucoin.Net](https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true) Kucoin.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Kucoin.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Kucoin.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Kucoin.Net?style=for-the-badge)

Kucoin.Net is a strongly typed client library for accessing the [Kucoin REST and Websocket API](https://www.kucoin.com/docs/beginners/introduction).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments (production, testnet)
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

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

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=for-the-badge)](https://www.nuget.org/packages/Kucoin.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/Kucoin.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/Kucoin.Net)

	dotnet add package Kucoin.Net
	
### GitHub packages
Kucoin.Net is available on [GitHub packages](https://github.com/JKorf/Kucoin.Net/pkgs/nuget/Kucoin.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/Kucoin.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/Kucoin.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/Kucoin.Net/releases).

## How to use
*REST Endpoints*  

```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new KucoinRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETH-USDT");
var lastPrice = tickerResult.Data.LastPrice;
```

*Websocket streams*  

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new KucoinSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH-USDT", (update) =>
{
	var lastPrice = update.Data.LastPrice;
});
```

For information on the clients, dependency injection, response processing and more see the [Kucoin.Net documentation](https://cryptoexchange.jkorf.dev?library=Kucoin.Net) or have a look at the examples [here](https://github.com/JKorf/Kucoin.Net/tree/master/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Kraken.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Rest Api
|API|Supported|Location|
|--|--:|--|
|Account Basic Info|✓|`restClient.SpotApi.Account` / `restClient.FuturesApi.Account`|
|Account Sub-Account|✓|`restClient.SpotApi.SubAccount`|
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
|Margin Trading Margin HF Trade|X||
|Margin Trading Margin Orders|✓|`restClient.SpotApi.Trading`|
|Margin Trading Margin Info|✓|`restClient.SpotApi.ExchangeData`|
|Margin Trading Isolated Margin|✓|`restClient.SpotApi.ExchangeData` / `restClient.SpotApi.Account`|
|Margin Trading Margin Trading(V3)|✓|`restClient.SpotApi.Margin`|
|Margin Trading Lending Market(V3)|✓|`restClient.SpotApi.Margin`|
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
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://www.kucoin.com/r/rf/QBS4FPED)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 7.10.0 - 03 Nov 2025
    * Updated CryptoExchange.Net to version 9.12.0
    * Added support for using SharedSymbol.UsdOrStable in Shared APIs
    * Added FiatDeposit, FiatWithdrawal, KuCoinEarnProfits to BizType enum
    * Updated restClient.FuturesApi.Account.GetPositionAsync to V2, updating return type to array
    * Fixed exception when initial trade snapshot has no items in TradeTracker
    * Removed some unhelpful verbose logs

* Version 7.9.1 - 27 Oct 2025
    * Added restClient.SpotApi.Margin.GetMarginCollateralRatioAsync endpoint

* Version 7.9.0 - 16 Oct 2025
    * Updated CryptoExchange.Net version to 9.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added public Unified API endpoints
    * Added ClientOrderId mapping on SharedUserTrade models
    * Added ITransferRestClient.TransferAsync implementation
    * Updated SpotApi IBalanceRestClient.GetBalancesAsync to support funding/margin wallet balance retrieval
    * Removed old testnet environment settings

* Version 7.8.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation
    * Added ContractAddress mapping in Shared IAssetClient implementation
    * Added restClient.FuturesApi.Account.SetPositionModeAsync endpoint
    * Added restClient.FuturesApi.Account.GetPositionModeAsync endpoint
    * Added QuoteVolume to KucoinFuturesKline model

* Version 7.7.1 - 10 Sep 2025
    * Fixed restClient.SpotApi.Account.GetWithdrawalAsync response deserialization

* Version 7.7.0 - 01 Sep 2025
    * Updated CryptoExchange.Net version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default
    * Added restClient.SpotApi.Account.GetHfAccountLedgersAsync endpoint

* Version 7.6.0 - 25 Aug 2025
    * Updated CryptoExchange.Net version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 7.5.1 - 21 Aug 2025
    * Added error handling for non 200 http status code response for Futures api

* Version 7.5.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Added restClient.SpotApi.Margin.GetBorrowInterestRateAsync endpoint
    * Updated rest request sending too prevent duplicate parameter serialization
    * Updated restClient.SpotApi.Trading.PlaceBulkOrderAsync to return CallResult objects with errors for failed orders
    * Fixed error responses not correctly getting logged as error

* Version 7.4.0 - 04 Aug 2025
    * Updated CryptoExchange.Net to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for multi-symbol Shared socket subscriptions
    * Added socketClient.SpotApi.SubscribeToKlineUpdatesAsync overload for multiple symbols

* Version 7.3.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching
    * Added restClient.FuturesApi.Account.GetCrossMarginRequirementAsync endpoint

* Version 7.2.0 - 15 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated leverage and quantity parameters for restClient.FuturesApi.Trading.PlaceTestOrderAsync to be optional
    * Fixed culture issue when parsing decimal values in Futures Api order book update

* Version 7.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)KucoinUserClientProvider allowing for easy client management when handling multiple users
    * Added restClient.FuturesApi.Account.SetMarginModesAsync endpoint
    * Added restClient.FuturesApi.Account.GetCrossMarginRiskLimitAsync endpoint
    * Added Period and NextFundingTime to restClient.FuturesApi.ExchangeData.GetCurrentFundingRateAsync response model
    * Adjusted margin endpoint ratelimits
    * Updated restClient.SpotApi.Account.GetCrossMarginAccountsAsync response model

* Version 7.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to Futures and Spot Shared clients
    * Added IBookTickerRestClient implementation to SpotApi and FuturesApi Shared clients
    * Added IFuturesOrderClientIdClient implementation to FuturesApi Shared client
    * Added IFuturesTpSlRestClient implementation to FuturesApi Shared client
    * Added ISpotOrderClientIdClient implementation to SpotApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to SpotApi Shared client
    * Added IsTriggerOrder, TriggerPrice properties to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerOrder, IsCloseOrder properties to SharedFuturesOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added QuoteVolume property mapping to SharedSpotTicker model
    * Added All property to retrieve all available environment on KucoinEnvironment
    * Added restClient.SpotApi.Account.GetWithdrawalAsync endpoint
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Updated restClient.FuturesApi.Trading.PlaceOrderAsync leverage parameter to optional
    * Updated restClient.FuturesApi.Trading.PlaceTpSlOrderAsync leverage and side parameters to optional
    * Updated PlaceMultipleOrdersAsync endpoints to return a list of CallResult models and an error if all orders fail to place
    * Updated restClient.SpotApi.TradingHf.EditOrderAsync rate limit weight from 3 to 1
    * Replaced KucoinApiCredentials with ApiCredentials
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient and IFuturesClient implementations
    * Removed legacy AddKucoin(restOptions, socketOptions) DI overload
    * Fixed some typos
    * Fixed incorrect DataTradeMode on certain Shared interface responses
    * Fixed Shared OrderPrice and OrderQuantity being returned as 0 instead of null if there is no value

* Version 7.0.0-beta3 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments

* Version 7.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Added Shared spot ticker QuoteVolume mapping
    * Fixed incorrect DataTradeMode on responses

* Version 7.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to Futures and Spot Shared clients
    * Added IBookTickerRestClient implementation to SpotApi and FuturesApi Shared clients
    * Added IFuturesOrderClientIdClient implementation to FuturesApi Shared client
    * Added IFuturesTpSlRestClient implementation to FuturesApi Shared client
    * Added ISpotOrderClientIdClient implementation to SpotApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to SpotApi Shared client
    * Added IsTriggerOrder, TriggerPrice properties to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerOrder, IsCloseOrder properties to SharedFuturesOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Updated restClient.FuturesApi.Trading.PlaceOrderAsync leverage parameter to optional
    * Updated restClient.FuturesApi.Trading.PlaceTpSlOrderAsync leverage and side parameters to optional
    * Updated PlaceMultipleOrdersAsync endpoints to return a list of CallResult models and an error if all orders fail to place
    * Replaced KucoinApiCredentials with ApiCredentials
    * Removed Newtonsoft.Json dependency
    * Removed legacy ISpotClient and IFuturesClient implementations
    * Removed legacy AddKucoin(restOptions, socketOptions) DI overload
    * Fixed some typos

* Version 6.1.0 - 24 Mar 2025
    * Added restClient.SpotApi.HfTrading.GetOpenOrdersV2Async endpoint

* Version 6.0.2 - 19 Mar 2025
    * Added TransactionId property to KucoinUserTrade model
    * Added missing value to BizType enum
    * Fixed some values for restClient.SpotApi.Account.GetAccountLedgersAsync bizType parameter
    * Fixed deserialization issue for restClient.SpotApi.Account.GetAccountLedgersAsync

* Version 6.0.1 - 05 Mar 2025
    * Fix restClient.SpotApi.Account.GetAccountsAsync accountType parameter serialization

* Version 6.0.0 - 03 Mar 2025
    * Updated library from Newtonsoft.Json to System.Text.Json for json (de)serialization
    * Added USDC balance info to Futures API shared GetBalancesAsync
    * Added restClient.SpotApi.Account.GetDepositAddressesV3Async endpoint
    * Added restClient.SpotApi.Account.CreateDepositAddressV3Async endpoint
    * Added restClient.SpotApi.ExchangeData.GetCallAuctionOrderBookAsync endpoint
    * Added restClient.SpotApi.ExchangeData.GetCallAuctionInfoAsync endpoint
    * Added socketClient.SpotApi.SubscribeToCallAuctionOrderBookUpdatesAsync endpoint
    * Added socketClient.SpotApi.SubscribeToCallAuctionInfoUpdatesAsync endpoint
    * Updated takeProfitPrice and stopLossPrice on restClient.FuturesApi.Trading.PlaceTpSlOrderAsync to triggerStopUpPrice and triggerStopDownPrice
    * Updated KucoinSymbol response model with call auction info
    * Updated Enum mappings
    * Updated API documentation references to new API docs
    * Removed no longer needed converters
    * Renamed restClient.FuturesApi.ExchangeData.GetOpenContractsAsync to GetSymbolsAsync
    * Renamed restClient.SpotApi.Margin.GetMarginTradingPairConfigurationAsync to GetSymbolsAsync
    * Renamed socketClient.FuturesApi.SubscribeToTickerUpdatesAsync to SubscribeToBookTickerUpdatesAsync
    * Renamed socketClient.FuturesApi.SubscribeToMarketUpdatesAsync to SubscribeToSymbolUpdatesAsync
    * Renamed socketClient.FuturesApi.SubscribeToSystemAnnouncementsAsync to SubscribeToFundingFeeSettlementUpdatesAsync
    * Renamed socketClient.FuturesApi.SubscribeTo24HourSnapshotUpdatesAsync to SubscribeTo24HTickerUpdatesAsync
    * Removed deprecated restClient.FuturesApi.Account transfer endpoints
    * Removed deprecated restClient.SpotApi.Account.InnerTransferAsync
    * Removed deprecated restClient.SpotApi.Account.GetHistoricalDepositsAsync
    * Removed deprecated restClient.SpotApi.Account.GetHistoricalWithdrawalsAsync
    * Removed deprecated restClient.SpotApi.Account.GetDepositAddressAsync
    * Removed deprecated restClient.SpotApi.Account.CreateDepositAddressAsync
    * Removed deprecated socketClient.SpotApi.SubscribeToFundingBookUpdatesAsync

* Version 5.24.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Fixed incorrect enum value for FeeType
    * Fix Mono runtime exception on rest client construction using DI

* Version 5.23.5 - 22 Jan 2025
    * Fixed restClient.FuturesApi.Account.GetPositionHistoryAsync deserialization error and updated response model

* Version 5.23.4 - 18 Jan 2025
    * Fixed shared interfaces balance subscription providing updates for all accounts

* Version 5.23.3 - 08 Jan 2025
    * Updated Spot Shared IBalanceRestClient GetBalancesAsync implementation to only filter account types if there are both Trade and SpotHf being returned

* Version 5.23.2 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to KucoinExchange class

* Version 5.23.1 - 27 Dec 2024
    * Fixed incorrect caching of passphrase with multiple credentials

* Version 5.23.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Added TradeType property to socketClient.FuturesApi.SubscribeToOrderUpdatesAsync update
    * Improved websocket disconnect detection
    * Updated SpotApi MaxSocketConnections from 150 to 500
    * Updated restClient.FuturesApi.Trading.CancelAllOrdersAsync from V1 to V3

* Version 5.22.0 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added restClient.SpotApi.SubAccount.EnableMarginPermissionsAsync and EnableFuturesPermissionsAsync endpoints
    * Added symbol parameter to restClient.FuturesApi.Trading.CancelMultipleOrdersAsync endpoint
    * Added tradeTypes parameter to restClient.FuturesApi.Trading.GetUserTradesAsync endpoint
    * Fixed orderbook creation via KucoinOrderBookFactory

* Version 5.21.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated KucoinOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 5.20.0 - 25 Nov 2024
    * Added Option enum values for account types
    * Added quantityInBaseAsset and quantityInQuoteAsset to futures orders endpoints
    * Updated websocket connections limit from 50 to 150 for spot
    * Updated some futures response models

* Version 5.19.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to KucoinExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant KucoinSocketClient constructor

* Version 5.18.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 5.17.1 - 04 Nov 2024
    * Added restClient.SpotApi.Account.GetApiKeyInfoAsync endpoint

* Version 5.17.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to KucoinExchange class
    * Added support Side setting on SharedTrade model
    * Added KucoinTrackerFactory for creating trackers
    * Added overload to Create method on KucoinOrderBookFactory support SharedSymbol parameter

* Version 5.16.0 - 18 Oct 2024
    * Added restClient.FuturesApi.Trading.CancelMultipleOrdersAsync endpoint
    * Added restClient.SpotApi.Account.GetIsHfAccountAsync endpoint
    * Added restClient.SpotApi.ExchangeData.GetAnnouncementsAsync endpoint
    * Added AveragePrice to Futures order response model
    * Added AveragePrice setting to Shared IFuturesOrderRestClient responses
    * Updated restClient.SpotApi.Account.WithdrawAsync to V3 endpoint
    * Updated KucoinAssetNetwork response model
    * Fixed CancelAfter parameter type for restClient.SpotApi.Trading.PlaceBulkOrderAsync endpoint
    * Fixed Shared IBalanceRestClient implementation to only return spot balances

* Version 5.15.1 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization
    * Fixed restClient.SpotApi.Margin.BorrowAsync timeInForce parameter

* Version 5.15.0 - 08 Oct 2024
    * Added FuturesApi.Account.GetMarginModeAsync endpoint
    * Added FuturesApi.Account.SetMarginModeAsync endpoint
    * Added FuturesApi.Account.GetCrossMarginLeverageAsync endpoint
    * Added FuturesApi.Account.SetCrossMarginLeverageAsync endpoint
    * Added marginMode parameter to FuturesApi.Trading.PlaceOrderAsync and PlaceMultipleOrdersAsync endpoints
    * Added onWalletUpdate update handler to FuturesApi.SubscribeToBalanceUpdatesAsync stream
    * Added FuturesApi.SubscribeToMarginModeUpdatesAsync stream
    * Added FuturesApi.SubscribeToCrossMarginLeverageUpdatesAsync stream
    * Updated various order and trade response/update models with margin mode properties
    * Update position models with MarginMode, PositionSide, Leverage and PositionFunding properties
    * Fixed cancellation token not getting passed in shared ticker subscriptions

* Version 5.14.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Spot and FuturesApi Rest and Socket clients
    * Added QuoteQuantityRemaining property on KucoinStreamOrderUpdate model
    * Updated Status property type from string to OrderStatus on KucoinFuturesOrder model
    * Updated FuturesKlineInterval Enum values to match number of seconds
    * Updated Sourcelink package version
    * Fixed various endpoints returning null instead of empty collection in SpotApi.HfTrading
    * Fixed futures kline deserialization issue
    * Marked ISpotClient and IFuturesClient references as deprecated

* Version 5.13.2 - 11 Sep 2024
    * Added SpotApi.ExchangeData.GetSymbolAsync endpoint

* Version 5.13.1 - 28 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.2, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.2
    * Added FuturesApi.Trading.PlaceTpSlOrderAsync endpoint, added ClientOrderId property to futures order placement response

* Version 5.13.0 - 21 Aug 2024
    * Added sub account endpoints under SpotApi.SubAccount.*
    * Moved SpotApi.Account.GetSubUserInfoAsync to new SubAccount topic

* Version 5.12.0 - 19 Aug 2024
    * Added FuturesApi.SubscribeToKlineUpdatesAsync subscription
    * Added FuturesApi.ExchangeData.GetTickersAsync endpoint
    * Added FuturesApi.Trading.GetMaxOpenPositionSizeAsync endpoint
    * Added migration endpoints SpotApi.Account.GetHfMigrationStatusAsync and MigrateHfAccountAsync

* Version 5.11.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0
    * Updated XML code comments
    * Add caching for passphrase authentication sign
    * Renamed SpotApi.SubscribeToBestOfferUpdatesAsync to SubscribeToBookTickerUpdatesAsync
    * Fixed KucoinOrder and KucoinUserTrade model Stop property being nullable Enum

* Version 5.10.0 - 27 Jul 2024
    * Updated CryptoExchange.Net to version 7.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.10.0
    * Added SpotApi.Margin.GetMarginMarkPricesAsync endpoint
    * Updated KC-API-KEY-VERSION header from '2' to '3' (V2 keys will still work)

* Version 5.9.0 - 16 Jul 2024
    * Updated CryptoExchange.Net to version 7.9.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.9.0
    * Updated internal classes to internal access modifier

* Version 5.8.3 - 02 Jul 2024
    * Updated CryptoExchange.Net to V7.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.8.0

* Version 5.8.2 - 27 Jun 2024
    * Fixed incorrect response mapping SpotApi.HfTrading.PlaceMultipleOrdersAsync

* Version 5.8.1 - 26 Jun 2024
    * Fixed CancelAfter parameter on SpotApi.HfTrading.PlaceMultipleOrdersAsync endpoint
    * Removed symbol base parameter from SpotApi.HfTrading.PlaceMultipleOrdersAsync as its not needed

* Version 5.8.0 - 25 Jun 2024
    * Updated CryptoExchange.Net to 7.7.2, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.2
    * Added SpotApi.Margin.GetCrossMarginSymbolsAsync endpoint
    * Added SpotApi.Margin.SetLeverageMultiplierAsync
    * Added SpotApi.HfTrading.GetMarginSymbolsWithOpenOrdersAsync endpoint

* Version 5.7.0 - 23 Jun 2024
    * Updated CryptoExchange.Net to version 7.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.0
    * Added missing HF/ProAccount endpoints
    * Renamed ProAccount SpotApi topic to HFTrading
    * Added FuturesApi.Account.GetPositionHistoryAsync endpoint Added FuturesApi.Account.GetTradingFeeAsync endpoint
    * Added SpotApi.SubscribeToIsolatedMarginPositionUpdatesAsync subscription
    * Updated response models from classes to records

* Version 5.6.0 - 11 Jun 2024
    * Updated CryptoExchange.Net to v7.6.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.5.5 - 07 May 2024
    * Updated CryptoExchange.Net to v7.5.2, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.5.4 - 04 May 2024
    * Fixed CryptoExchange.Net reference

* Version 5.5.3 - 04 May 2024
    * Fixed universal transfer endpoint
    * Fixed FuturesApi.SubscribeToStopOrderUpdatesAsync deserialization
    * Updated various response models
    * Updated CryptoExchange.Net to v7.5.1, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.5.2 - 01 May 2024
    * Added SpotApi.Trading.GetOcoOrderByClientOrderIdAsync to interface
    * Updated CryptoExchange.Net to v7.5.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.5.1 - 28 Apr 2024
    * Added Url and ApiDocsUrl to KrakenExchange static info class
    * Added KucoinOrderBookFactory book creation method
    * Fixed KucoinOrderBookFactory injection issue
    * Updated CryptoExchange.Net to v7.4.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.5.0 - 23 Apr 2024
    * Added FuturesApi.ExchangeData.Get24HourTransactionVolumeAsync endpoint
    * Added FuturesApi.ExchangeData.GetFundingRateHistoryAsync endpoint
    * Added FuturesApi.Account.GetMaxWithdrawMarginAsync endpoint
    * Added FuturesApi.Account.RemoveMarginAsync endpoint
    * Added FuturesApi.Trading.PlaceTestOrderrAsync endpoint
    * Added FuturesApi.Trading.CancelOrderByClientOrderIdAsync endpoint
    * Added FuturesApi.Trading.PlaceMultipleOrdersAsync endpoint
    * Added OCO order endpoints to SpotApi.Trading
    * Added SpotApi.Margin endpoints containing Margin Trading (V3) and Margin Lending (V3) endpoints
    * Added SpotApi.SubscribeToPositionUpdatesAsync for all symbols stream
    * Added SpotApi.SubscribeToMarginPositionUpdatesAsync stream
    * Added SpotApi.SubscribeToMarginOrderUpdatesAsync stream
    * Fixed FuturesApi.ExchangeData.GetKlinesAsync parameters not getting send

* Version 5.4.1 - 18 Apr 2024
    * Fixed SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync request

* Version 5.4.0 - 18 Apr 2024
    * Updated CryptoExchange.Net to 7.3.1, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
    * Re-implemented client side ratelimiting for Spot API
    * Added client side ratelimiting for Futures API
    * Added handling of RetryAfter responses
    * Added SpotApi.ExchangeData.GetLeveragedTokensAsync endpoint
    * Added SpotApi.ExchangeData.GetCrossMarginRiskLimitAndConfig endpoint
    * Added SpotApi.ExchangeData.GetIsolatedMarginRiskLimitAndConfig endpoint
    * Added autoRepay parameter to SpotApi.Trading.PlaceMarginOrderAsync
    * Added isolatedMarginSymbol parameter to SpotApi.Account.GetTransferableAsync
    * Added SelfTradePrevention parameter to FuturesApi.Trading.PlaceOrderAsync
    * Added OneMonth to kline interval enum
    * Updated UserTrade response model
    * Updated Position response model
    * Updated WithdrawalQuota response model
    * Updated Transfer response model
    * Updated Asset Network response model
    * Updated Tickers response model
    * Fixed FuturesApi.ExchangeData.GetContractsAsync NextFundingRateTime response property
    * Removed deprecated endpoints

* Version 5.3.3 - 03 Apr 2024
    * Updated string comparision for improved performance
    * Removed pre-send symbol validation

* Version 5.3.2 - 25 Mar 2024
    * Fix deserialization issue SpotApi.ExchangeData.GetAssetsAsync

* Version 5.3.1 - 24 Mar 2024
	* Updated CryptoExchange.Net to 7.2.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	* Added DepositFeeRate and DepositMinQuantity to KucoinAssetNetwork model
	* Fixed websocket balance updates for HF/Pro trade accounts

* Version 5.3.0 - 16 Mar 2024
    * Updated CryptoExchange.Net to 7.1.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	
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

