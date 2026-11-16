# Kucoin.Net API surfaces

## Client roots

`KucoinRestClient` / `IKucoinRestClient` exposes `SpotApi.{ExchangeData|Account|SubAccount|Trading|HfTrading|Margin|Earn}`, `FuturesApi.{ExchangeData|Account|Trading}`, and `UnifiedApi.{ExchangeData|Account|Trading}`. `KucoinSocketClient` / `IKucoinSocketClient` exposes methods directly on `SpotApi`, `FuturesApi`, and `UnifiedApi`. Unified socket also supports order requests. `IKucoinTrackerFactory` supplies trackers.

Configure `KucoinEnvironment.Live`, `Europe`, or `Australia` for the intended deployment. A region does not imply sandbox behavior.

## Spot REST

| User intent | Kucoin.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get spot symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync()` |
| Get one spot symbol | `client.SpotApi.ExchangeData.GetSymbolAsync("BTC-USDT")` |
| Get latest spot ticker | `client.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get 24h stats | `client.SpotApi.ExchangeData.Get24HourStatsAsync("BTC-USDT")` |
| Get markets | `client.SpotApi.ExchangeData.GetMarketsAsync()` |
| Get partial order book | `client.SpotApi.ExchangeData.GetAggregatedPartialOrderBookAsync("BTC-USDT", 20)` |
| Get full order book | `client.SpotApi.ExchangeData.GetAggregatedFullOrderBookAsync("BTC-USDT")` |
| Get recent trades | `client.SpotApi.ExchangeData.GetTradeHistoryAsync("BTC-USDT")` |
| Get spot klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("BTC-USDT", KlineInterval.OneMinute)` |
| Get assets | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get one asset | `client.SpotApi.ExchangeData.GetAssetAsync("USDT")` |
| Get fiat prices | `client.SpotApi.ExchangeData.GetFiatPricesAsync(...)` |
| Get announcements | `client.SpotApi.ExchangeData.GetAnnouncementsAsync(...)` |
| Get accounts/balances | `client.SpotApi.Account.GetAccountsAsync()` |
| Get one account | `client.SpotApi.Account.GetAccountAsync(accountId)` |
| Get user info | `client.SpotApi.Account.GetUserInfoAsync()` |
| Get trade fee | `client.SpotApi.Account.GetSymbolTradingFeesAsync("BTC-USDT")` |
| Get account ledgers | `client.SpotApi.Account.GetAccountLedgersAsync(...)` |
| Get deposit history | `client.SpotApi.Account.GetDepositsAsync(...)` |
| Get deposit addresses | `client.SpotApi.Account.GetDepositAddressesV3Async(asset, networkId)` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawalsAsync(...)` |
| Get withdrawal quota | `client.SpotApi.Account.GetWithdrawalQuotasAsync(asset, network)` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(...)` |
| Cancel withdrawal | `client.SpotApi.Account.CancelWithdrawalAsync(withdrawalId)` |
| Universal transfer | `client.SpotApi.Account.UniversalTransferAsync(...)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Place spot test order | `client.SpotApi.Trading.PlaceTestOrderAsync(...)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(orderId)` |
| Query by client order id | `client.SpotApi.Trading.GetOrderByClientOrderIdAsync(clientOrderId)` |
| Get spot orders | `client.SpotApi.Trading.GetOrdersAsync(...)` |
| Get recent spot orders | `client.SpotApi.Trading.GetRecentOrdersAsync()` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(orderId)` |
| Cancel by client order id | `client.SpotApi.Trading.CancelOrderByClientOrderIdAsync(clientOrderId)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol, tradeType)` |
| Place spot stop order | `client.SpotApi.Trading.PlaceStopOrderAsync(...)` |
| Get stop orders | `client.SpotApi.Trading.GetStopOrdersAsync(...)` |
| Cancel stop order | `client.SpotApi.Trading.CancelStopOrderAsync(orderId)` |
| Place OCO order | `client.SpotApi.Trading.PlaceOcoOrderAsync(...)` |
| Get OCO orders | `client.SpotApi.Trading.GetOcoOrdersAsync(...)` |
| Cancel OCO order | `client.SpotApi.Trading.CancelOcoOrderAsync(orderId)` |

## Spot High Frequency REST

| User intent | Kucoin.Net member |
|---|---|
| Place HF spot order | `client.SpotApi.HfTrading.PlaceOrderAsync(...)` |
| Place HF spot order and wait | `client.SpotApi.HfTrading.PlaceOrderWaitAsync(...)` |
| Place HF test order | `client.SpotApi.HfTrading.PlaceTestOrderAsync(...)` |
| Place multiple HF orders | `client.SpotApi.HfTrading.PlaceMultipleOrdersAsync(...)` |
| Cancel HF order | `client.SpotApi.HfTrading.CancelOrderAsync(symbol, orderId)` |
| Cancel HF order and wait | `client.SpotApi.HfTrading.CancelOrderWaitAsync(symbol, orderId)` |
| Get HF order | `client.SpotApi.HfTrading.GetOrderAsync(symbol, orderId)` |
| Get HF open orders | `client.SpotApi.HfTrading.GetOpenOrdersAsync(symbol)` |
| Get HF closed orders | `client.SpotApi.HfTrading.GetClosedOrdersAsync(...)` |
| Get HF user trades | `client.SpotApi.HfTrading.GetUserTradesAsync(...)` |
| Cancel all HF orders by symbol | `client.SpotApi.HfTrading.CancelAllOrdersBySymbolAsync(symbol)` |
| Configure HF cancel-after | `client.SpotApi.HfTrading.CancelAfterAsync(cancelAfter, symbols)` |
| Place HF margin order | `client.SpotApi.HfTrading.PlaceMarginOrderAsync(...)` |

## Margin, Sub-Account, Earn REST

| User intent | Kucoin.Net member |
|---|---|
| Get margin account | `client.SpotApi.Account.GetMarginAccountAsync()` |
| Get cross margin accounts | `client.SpotApi.Account.GetCrossMarginAccountsAsync(...)` |
| Get isolated margin accounts | `client.SpotApi.Account.GetIsolatedMarginAccountsAsync()` |
| Get isolated margin account | `client.SpotApi.Account.GetIsolatedMarginAccountAsync("BTC-USDT")` |
| Margin configuration | `client.SpotApi.Margin.GetMarginConfigurationAsync()` |
| Margin mark price | `client.SpotApi.Margin.GetMarginMarkPriceAsync("BTC-USDT")` |
| Margin symbols | `client.SpotApi.Margin.GetSymbolsAsync()` |
| Cross margin symbols | `client.SpotApi.Margin.GetCrossMarginSymbolsAsync(...)` |
| Margin borrow history | `client.SpotApi.Margin.GetBorrowHistoryAsync(...)` |
| Margin repay history | `client.SpotApi.Margin.GetRepayHistoryAsync(...)` |
| Margin interest history | `client.SpotApi.Margin.GetInterestHistoryAsync(...)` |
| Place margin order | `client.SpotApi.Trading.PlaceMarginOrderAsync(...)` |
| Place margin test order | `client.SpotApi.Trading.PlaceTestMarginOrderAsync(...)` |
| Get sub-accounts | `client.SpotApi.SubAccount.GetSubAccountsAsync()` |
| Get sub-account balances | `client.SpotApi.SubAccount.GetSubAccountBalancesAsync(subAccountId)` |
| Get all sub-account balances | `client.SpotApi.SubAccount.GetSubAccountsBalancesAsync(...)` |
| Get sub-account API keys | `client.SpotApi.SubAccount.GetSubAccountApiKeyAsync(subAccountName, apiKey)` |
| Get Earn holdings | `client.SpotApi.Earn.GetEarnHoldingAsync(...)` |

## Futures REST

| User intent | Kucoin.Net member |
|---|---|
| Get futures symbols/contracts | `client.FuturesApi.ExchangeData.GetSymbolsAsync()` |
| Get one futures contract | `client.FuturesApi.ExchangeData.GetContractAsync("ETHUSDTM")` |
| Get futures ticker | `client.FuturesApi.ExchangeData.GetTickerAsync("ETHUSDTM")` |
| Get all futures tickers | `client.FuturesApi.ExchangeData.GetTickersAsync()` |
| Get futures full order book | `client.FuturesApi.ExchangeData.GetAggregatedFullOrderBookAsync("ETHUSDTM")` |
| Get futures partial order book | `client.FuturesApi.ExchangeData.GetAggregatedPartialOrderBookAsync("ETHUSDTM", 20)` |
| Get futures klines | `client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDTM", FuturesKlineInterval.OneMinute)` |
| Get futures trades | `client.FuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDTM")` |
| Get current mark price | `client.FuturesApi.ExchangeData.GetCurrentMarkPriceAsync("ETHUSDTM")` |
| Get current funding rate | `client.FuturesApi.ExchangeData.GetCurrentFundingRateAsync("ETHUSDTM")` |
| Get funding history | `client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync(symbol, startTime, endTime)` |
| Get futures server time | `client.FuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get futures account overview | `client.FuturesApi.Account.GetAccountOverviewAsync()` |
| Get futures transaction history | `client.FuturesApi.Account.GetTransactionHistoryAsync(...)` |
| Get futures position | `client.FuturesApi.Account.GetPositionAsync("ETHUSDTM")` |
| Get futures positions | `client.FuturesApi.Account.GetPositionsAsync()` |
| Get position history | `client.FuturesApi.Account.GetPositionHistoryAsync(...)` |
| Get futures trading fee | `client.FuturesApi.Account.GetTradingFeeAsync("ETHUSDTM")` |
| Get margin mode | `client.FuturesApi.Account.GetMarginModeAsync("ETHUSDTM")` |
| Set margin mode | `client.FuturesApi.Account.SetMarginModeAsync("ETHUSDTM", FuturesMarginMode.Isolated)` |
| Get cross margin leverage | `client.FuturesApi.Account.GetCrossMarginLeverageAsync("ETHUSDTM")` |
| Set cross margin leverage | `client.FuturesApi.Account.SetCrossMarginLeverageAsync("ETHUSDTM", 5)` |
| Get position mode | `client.FuturesApi.Account.GetPositionModeAsync()` |
| Set position mode | `client.FuturesApi.Account.SetPositionModeAsync(PositionMode.HedgeMode)` |
| Place futures order | `client.FuturesApi.Trading.PlaceOrderAsync(...)` |
| Place futures test order | `client.FuturesApi.Trading.PlaceTestOrderAsync(...)` |
| Place futures TP/SL order | `client.FuturesApi.Trading.PlaceTpSlOrderAsync(...)` |
| Place multiple futures orders | `client.FuturesApi.Trading.PlaceMultipleOrdersAsync(...)` |
| Query futures order | `client.FuturesApi.Trading.GetOrderAsync(orderId)` |
| Get futures orders | `client.FuturesApi.Trading.GetOrdersAsync(...)` |
| Get closed futures orders | `client.FuturesApi.Trading.GetClosedOrdersAsync(symbol)` |
| Cancel futures order | `client.FuturesApi.Trading.CancelOrderAsync(orderId)` |
| Cancel futures by client order id | `client.FuturesApi.Trading.CancelOrderByClientOrderIdAsync(symbol, clientOrderId)` |
| Cancel all futures orders | `client.FuturesApi.Trading.CancelAllOrdersAsync(symbol)` |

## Unified Account REST

| User intent | Kucoin.Net member |
|---|---|
| Unified account overview | `client.UnifiedApi.Account.GetAccountOverviewAsync()` |
| Unified balances | `client.UnifiedApi.Account.GetBalancesAsync()` |
| Unified classic balances | `client.UnifiedApi.Account.GetClassicBalancesAsync(...)` |
| Unified sub-account balances | `client.UnifiedApi.Account.GetSubAccountBalancesAsync(...)` |
| Unified transfer quotas | `client.UnifiedApi.Account.GetTransferQuotasAsync(...)` |
| Unified account mode | `client.UnifiedApi.Account.GetAccountModeAsync()` |
| Unified fee rate | `client.UnifiedApi.Account.GetFeeRateAsync(...)` |
| Unified account ledger | `client.UnifiedApi.Account.GetAccountLedgerAsync(...)` |
| Unified deposit address | `client.UnifiedApi.Account.GetDepositAddressAsync(asset, network)` |
| Unified spot symbols | `client.UnifiedApi.ExchangeData.GetSpotSymbolsAsync(symbol)` |
| Unified futures symbols | `client.UnifiedApi.ExchangeData.GetFuturesSymbolsAsync(symbol)` |
| Unified assets | `client.UnifiedApi.ExchangeData.GetAssetsAsync(assets, network)` |
| Unified tickers | `client.UnifiedApi.ExchangeData.GetTickersAsync(productType, symbol)` |
| Unified order book | `client.UnifiedApi.ExchangeData.GetOrderBookAsync(productType, symbol, limit)` |
| Unified recent trades | `client.UnifiedApi.ExchangeData.GetRecentTradesAsync(productType, symbol)` |
| Unified klines | `client.UnifiedApi.ExchangeData.GetKlinesAsync(productType, symbol, interval)` |
| Unified platform statistics | `client.UnifiedApi.ExchangeData.GetPlatformStatsAsync()` |
| Unified interest rate index | `client.UnifiedApi.ExchangeData.GetInterestRateIndexAsync(...)` |
| Unified position tiers | `client.UnifiedApi.ExchangeData.GetPositionTiersAsync(...)` |
| Unified funding rate | `client.UnifiedApi.ExchangeData.GetFundingRatesAsync(symbol)` |
| Unified place order | `client.UnifiedApi.Trading.PlaceOrderAsync(...)` |
| Unified cancel order | `client.UnifiedApi.Trading.CancelOrderAsync(...)` |
| Unified cancel orders | `client.UnifiedApi.Trading.CancelOrdersAsync(...)` |
| Unified get order | `client.UnifiedApi.Trading.GetOrderAsync(...)` |
| Unified open orders | `client.UnifiedApi.Trading.GetOpenOrdersAsync(...)` |
| Unified order history | `client.UnifiedApi.Trading.GetOrderHistoryAsync(...)` |
| Unified user trades | `client.UnifiedApi.Trading.GetUserTradesAsync(...)` |
| Unified positions | `client.UnifiedApi.Trading.GetPositionsAsync(symbol: symbol)` |

## Spot WebSocket

| User intent | Kucoin.Net member |
|---|---|
| Subscribe spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe all spot tickers | `socketClient.SpotApi.SubscribeToAllTickerUpdatesAsync(handler)` |
| Subscribe spot snapshots | `socketClient.SpotApi.SubscribeToSnapshotUpdatesAsync(...)` |
| Subscribe spot aggregated order book | `socketClient.SpotApi.SubscribeToAggregatedOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.OneMinute, handler)` |
| Subscribe spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot order book | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, limit, handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(...)` |
| Subscribe spot balance updates | `socketClient.SpotApi.SubscribeToBalanceUpdatesAsync(handler)` |
| Subscribe stop order updates | `socketClient.SpotApi.SubscribeToStopOrderUpdatesAsync(handler)` |
| Subscribe margin position updates | `socketClient.SpotApi.SubscribeToMarginPositionUpdatesAsync(...)` |
| Subscribe margin order updates | `socketClient.SpotApi.SubscribeToMarginOrderUpdatesAsync(...)` |

## Futures WebSocket

| User intent | Kucoin.Net member |
|---|---|
| Subscribe futures trades | `socketClient.FuturesApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe futures klines | `socketClient.FuturesApi.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.OneMinute, handler)` |
| Subscribe futures book ticker | `socketClient.FuturesApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe futures order book | `socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe futures partial order book | `socketClient.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, limit, handler)` |
| Subscribe futures symbol updates | `socketClient.FuturesApi.SubscribeToSymbolUpdatesAsync(...)` |
| Subscribe futures 24h ticker | `socketClient.FuturesApi.SubscribeTo24HTickerUpdatesAsync(symbol, handler)` |
| Subscribe futures balance updates | `socketClient.FuturesApi.SubscribeToBalanceUpdatesAsync(...)` |
| Subscribe futures position updates | `socketClient.FuturesApi.SubscribeToPositionUpdatesAsync(...)` |
| Subscribe futures order updates | `socketClient.FuturesApi.SubscribeToOrderUpdatesAsync(...)` |
| Subscribe futures stop order updates | `socketClient.FuturesApi.SubscribeToStopOrderUpdatesAsync(handler)` |

## Unified WebSocket

| User intent | Kucoin.Net member |
|---|---|
| Subscribe unified ticker | `socketClient.UnifiedApi.SubscribeToTickerUpdatesAsync(tradeType, symbol, handler)` |
| Subscribe unified klines | `socketClient.UnifiedApi.SubscribeToKlineUpdatesAsync(...)` |
| Subscribe unified order book | `socketClient.UnifiedApi.SubscribeToOrderBookUpdatesAsync(...)` |
| Subscribe unified trades | `socketClient.UnifiedApi.SubscribeToTradeUpdatesAsync(...)` |
| Subscribe unified balances | `socketClient.UnifiedApi.SubscribeToBalanceUpdatesAsync(...)` |
| Subscribe unified orders | `socketClient.UnifiedApi.SubscribeToOrderUpdatesAsync(...)` |
| Subscribe unified user trades | `socketClient.UnifiedApi.SubscribeToUserTradeUpdatesAsync(...)` |
| Subscribe unified positions | `socketClient.UnifiedApi.SubscribeToPositionUpdatesAsync(...)` |
| Subscribe unified leverage updates | `socketClient.UnifiedApi.SubscribeToLeverageUpdatesAsync(...)` |

## Additional native workflows

Classic Spot regular, HF, and margin trading are separate workflows. `SpotApi.SubAccount` owns subaccount creation, API key management, and permissions. `SpotApi.Margin` owns borrow/repay, lending subscription/redemption, interest history, risk and collateral configuration. `SpotApi.Earn` owns product/holding discovery and purchase/redemption flows. Preserve their distinct account prerequisites.

Unified account workflows include mode discovery/change, balances, fee rates, ledger, leverage, transfers, withdrawal quotas, and withdrawals. Unified trading includes position history and order edits. Native Unified socket requests are `PlaceOrderAsync`, `EditOrderAsync`, and `CancelOrderAsync`; they return query results. Unified subscriptions include order book, trades, funding, mark price, balances, orders, positions, leverage, and liquidation warnings. Select `ProductType` or `UnifiedAccountType` according to the method signature rather than copying an enum from another call.

## Shared API V2

`IKucoinSharedApiClient` exposes these properties. Each matching native root exposes `.SharedApi`.

- `FuturesRest` (`IKucoinRestClientFuturesSharedApi`): `IGetBalancesRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetFuturesSymbolsRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `ICloseFullPositionRest`, `IGetKlinesRest`, `IGetRecentTradesRest`, `IGetOrderBookRest`, `IGetOpenInterestRest`, `IGetFundingRateHistoryRest`, `IGetPositionHistoryRest`, `IGetFeesRest`, `IGetFuturesOrderByClientOrderIdRest`, `ICancelFuturesOrderByClientOrderIdRest`, `ISetFuturesTpSlRest`, `ICancelFuturesTpSlRest`, `IGetBookTickerRest`, `IGetLeverageRest`, `ISetLeverageRest`.
- `FuturesSocket` (`IKucoinSocketClientFuturesSharedApi`): `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket`.
- `SpotRest` (`IKucoinRestClientSpotSharedApi`): `IGetAssetRest`, `IGetAllAssetsRest`, `IGetBalancesRest`, `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `IGetSpotUserTradeHistoryRest`, `ICancelSpotOrderRest`, `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetWithdrawalHistoryRest`, `IWithdrawRest`, `IGetFeesRest`, `IGetSpotOrderByClientOrderIdRest`, `ICancelSpotOrderByClientOrderIdRest`, `IGetSpotTriggerOrderRest`, `IPlaceSpotTriggerOrderRest`, `ICancelSpotTriggerOrderRest`, `IGetBookTickerRest`, `ITransferRest`, `IGetDepositAddressesRest`, `IGetDepositHistoryRest`.
- `SpotSocket` (`IKucoinSocketClientSpotSharedApi`): `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeSpotOrdersSocket`.
- `UnifiedRest` (`IKucoinRestClientUnifiedSharedApi`): `IGetTickerRest`, `IGetAllTickersRest`, `IGetAssetRest`, `IGetAllAssetsRest`, `IGetBalancesRest`, `IGetDepositAddressesRest`, `IGetDepositHistoryRest`, `IGetFeesRest`, `IGetFundingInfoRest`, `IGetFundingRateHistoryRest`, `IGetUserFundingHistoryRest`, `IGetIndexPriceRest`, `IGetAllIndexPricesRest`, `IGetMarkPriceRest`, `IGetAllMarkPricesRest`, `IGetIndexPriceKlinesRest`, `IGetMarkPriceKlinesRest`, `IGetKlinesRest`, `IGetLedgerRest`, `IGetLeverageRest`, `ISetLeverageRest`, `IGetLeverageTiersRest`, `IGetOpenInterestRest`, `IGetBookTickerRest`, `IGetOrderBookRest`, `IGetSpotSymbolsRest`, `IGetFuturesSymbolsRest`, `IGetRecentTradesRest`, `ITransferRest`, `IGetWithdrawalHistoryRest`, `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetSpotOrderByClientOrderId`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `IGetSpotUserTradeHistoryRest`, `ICancelSpotOrderRest`, `ICancelSpotOrderByClientOrderIdRest`, `ICancelAllSpotSymbolOrdersRest`, `IEditSpotOrderRest`, `IEditSpotOrderByClientOrderIdRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetFuturesOrderByClientOrderId`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `ICancelFuturesOrderByClientOrderIdRest`, `ICancelAllFuturesSymbolOrdersRest`, `IEditFuturesOrderRest`, `IEditFuturesOrderByClientOrderIdRest`, `IGetPositionsRest`, `IGetPositionHistoryRest`, `IPlaceSpotTriggerOrderRest`, `IGetSpotTriggerOrderRest`, `ICancelSpotTriggerOrderRest`, `IPlaceFuturesTriggerOrderRest`, `IGetFuturesTriggerOrderRest`, `ICancelFuturesTriggerOrderRest`.
- `UnifiedSocket` (`IKucoinSocketClientUnifiedSharedApi`): `ISubscribeKlinesSocket`, `ISubscribeTradesSocket`, `ISubscribeBookTickerSocket`, `ISubscribeOrderBookSocket`, `ISubscribeIncrementalOrderBookSocket`, `ISubscribeMarkPriceSocket`, `ISubscribeIndexPriceSocket`, `ISubscribeFundingInfoSocket`, `ISubscribeSpotOrdersSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribeUserTradesSocket`, `ISubscribeBalancesSocket`, `ISubscribePositionsSocket`, `IPlaceSpotOrderSocket`, `IEditSpotOrderSocket`, `IEditSpotOrderByClientOrderIdSocket`, `ICancelSpotOrderSocket`, `ICancelSpotOrderByClientOrderIdSocket`, `IPlaceFuturesOrderSocket`, `IEditFuturesOrderSocket`, `IEditFuturesOrderByClientOrderIdSocket`, `ICancelFuturesOrderSocket`, `ICancelFuturesOrderByClientOrderIdSocket`.

Use aggregate properties or inject narrow capabilities for known operations. For dynamic selection, call `aggregate.GetCapability(SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot)` and check for null before using `Capability` and `Options`. A descriptor identifies an operation; it does not guarantee support. `GetCapabilities` enumerates matches and `Discover` summarizes metadata. Classic and Unified surfaces can implement the same capability: explicitly select the surface appropriate to the account. With several exchanges registered, inject `IEnumerable<TCapability>` and select by exchange, supported mode, and surface options.

Unified socket supports order place/edit/cancel capabilities and several market/account streams, but does not implement `ISubscribeTickerSocket`. For a portable ticker subscription use a classic surface that declares it. Unified native ticker methods remain available. Shared capabilities do not replace native account-mode or permission checks.

## Symbols, catalogs, and quantities

Spot uses `BTC-USDT`; classic Futures uses contract identifiers such as `XBTUSDTM` and `ETHUSDTM`. The BTC/XBT mapping matters. Choose metadata from the same API/account workflow as the call. Unified native requests take explicit product types where required; account type/mode parameters are method-specific and are not interchangeable with `ProductType`.

Load `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` or `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())`, check success, then inspect `SpotSymbolCatalog` or `FuturesSymbolCatalog` on the selected capability. Symbol requests honor base/quote asset type and subtype filters. Metadata carries display names and crypto/stablecoin/fiat/TradFi classifications where populated; do not assume every surface fills every subtype. Futures metadata includes contract size, precision, lot constraints, and settlement/trading mode.

Classic Futures `quantity` is integer contracts; provide exactly one sizing form when choosing `quantity`, `quantityInBaseAsset`, or `quantityInQuoteAsset`. Use contract metadata before conversion; do not truncate fractional input to fit an integer. Classic Shared Futures orders and books use contracts; Spot books use base-asset quantities. Shared results expose `SharedOrderQuantity` with base, quote, or contracts fields; inspect quantity support/options before choosing one. Unified native `quantity` uses its `QuantityUnit`; Unified Shared Futures adapters use contract quantities. Spot market buys can use base or quote sizing where supported.

## Checkout-only sources

- [Public interfaces](../../../../Kucoin.Net/Interfaces/Clients/): exact signatures, account/product types, and cancellation tokens.
- [Client implementations](../../../../Kucoin.Net/Clients/): Shared options, conversion, and classification.
- [Complete examples](../../../../Examples/ai-friendly/): independent consumer programs.
