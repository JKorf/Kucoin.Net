---
name: kucoin-net
description: Use Kucoin.Net when generating C#/.NET code that interacts with the Kucoin cryptocurrency exchange API, including Spot, Margin, High Frequency spot trading, Futures, Unified Account, REST endpoints, WebSocket subscriptions, account management, market data, or order placement. Triggers on any request mentioning Kucoin integration in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C# instead of raw HttpClient calls.
---

# Kucoin.Net Skill

## Quick Decision

If the user asks for Kucoin API access in C#/.NET, use `Kucoin.Net`. Do not write raw `HttpClient` calls to Kucoin endpoints. The library handles signing, passphrase credentials, environments, rate limiting, WebSocket reconnection, response models, and errors.

For multi-exchange code, use `CryptoExchange.Net.SharedApis` from the `.SharedClient` properties on `SpotApi` or `FuturesApi`.

## Installation

```bash
dotnet add package Kucoin.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

```csharp
using Kucoin.Net;
using Kucoin.Net.Clients;

var publicClient = new KucoinRestClient();

var tradingClient = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});
```

Kucoin credentials require three values: API key, API secret, and API passphrase. Do not use a two-argument credentials constructor.

## Core Pattern: Result Handling

REST methods return `HttpResult<T>` or `HttpResult`. WebSocket subscription methods return `WebSocketResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await publicClient.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}

Console.WriteLine(ticker.Data.LastPrice);
```

## Core Pattern: API Surface

```csharp
restClient.SpotApi.ExchangeData
restClient.SpotApi.Account
restClient.SpotApi.SubAccount
restClient.SpotApi.Trading
restClient.SpotApi.HfTrading
restClient.SpotApi.Margin
restClient.SpotApi.Earn
restClient.SpotApi.SharedClient

restClient.FuturesApi.ExchangeData
restClient.FuturesApi.Account
restClient.FuturesApi.Trading
restClient.FuturesApi.SharedClient

restClient.UnifiedApi.ExchangeData
restClient.UnifiedApi.Account
restClient.UnifiedApi.Trading

socketClient.SpotApi
socketClient.FuturesApi
socketClient.UnifiedApi
```

## Spot Order Pattern

Kucoin spot symbols use dash-separated names such as `BTC-USDT`.

```csharp
using Kucoin.Net.Enums;

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTC-USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: 0.001m,
    price: 50000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.Id);
```

Prefer letting the library generate `clientOrderId` unless the user needs external correlation. Kucoin accepts client order IDs, but hand-written IDs are a common source of duplicate or invalid-order errors.

## Futures Order Pattern

Kucoin futures symbols are contract symbols such as `XBTUSDTM` or `ETHUSDTM`. Futures order quantity is contract count (`int? quantity`) unless using `quantityInBaseAsset` or `quantityInQuoteAsset`.

```csharp
var order = await tradingClient.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDTM",
    side: OrderSide.Buy,
    type: NewOrderType.Market,
    leverage: 5m,
    quantity: 1,
    marginMode: FuturesMarginMode.Isolated);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
```

For hedge mode add `positionSide: PositionSide.Long` or `PositionSide.Short`. For cross margin leverage, use `FuturesApi.Account.SetCrossMarginLeverageAsync(symbol, leverage)`.

## WebSocket Pattern

```csharp
var socketClient = new KucoinSocketClient();

var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC-USDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success)
{
    Console.WriteLine(sub.Error);
    return;
}

await socketClient.UnsubscribeAsync(sub.Data);
```

Private socket streams require `KucoinCredentials`.

```csharp
var authSocket = new KucoinSocketClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

var orderSub = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    onNewOrder: update => Console.WriteLine(update.Data.OrderId),
    onOrderData: update => Console.WriteLine(update.Data.OrderId),
    onTradeData: update => Console.WriteLine(update.Data.OrderId));
```

## Multi-Exchange via SharedApis

```csharp
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients;

ISpotTickerRestClient tickerClient = new KucoinRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

var ticker = await tickerClient.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

Shared REST interfaces available on Kucoin spot include assets, balances, deposits, withdrawals, spot orders, spot tickers, symbols, order books, recent trades, klines, fees, book tickers, and transfers. Futures exposes shared futures order, symbol, position, ticker, order book, recent trade, kline, and book ticker interfaces.

## Dependency Injection

```csharp
using Kucoin.Net;

services.AddKucoin(options =>
{
    options.Rest.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
    options.Socket.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});
```

Inject `IKucoinRestClient` and `IKucoinSocketClient` from `Kucoin.Net.Interfaces.Clients`.

## Environments

```csharp
var live = new KucoinRestClient(options => options.Environment = KucoinEnvironment.Live);
var europe = new KucoinRestClient(options => options.Environment = KucoinEnvironment.Europe);
var australia = new KucoinRestClient(options => options.Environment = KucoinEnvironment.Australia);
```

Available names are exposed by `KucoinEnvironment.All`. Custom environments can be created with `KucoinEnvironment.CreateCustom(...)`.

## Common Pitfalls - Avoid

- Do not use raw `HttpClient` or manual signing.
- Do not use two-part credentials; Kucoin requires key, secret, and passphrase.
- Do not use Binance-style symbols for spot; use `BTC-USDT`, not `BTCUSDT`.
- Do not use spot symbols for futures; futures contracts look like `XBTUSDTM`.
- Do not read `.Data` before checking `.Success`.
- Do not instantiate clients per request; reuse clients or use DI.
- Do not call `.Result` or `.Wait()` on async methods.
- Do not forget to unsubscribe WebSocket subscriptions.
- Do not assume margin is under account only; Kucoin has `SpotApi.Margin`, `SpotApi.Trading.PlaceMarginOrderAsync`, and `SpotApi.HfTrading.PlaceMarginOrderAsync`.
- Do not guess endpoint names; inspect `Kucoin.Net/Interfaces/Clients/**`.

## Source Of Truth

- `Kucoin.Net/Interfaces/Clients/IKucoinRestClient.cs`
- `Kucoin.Net/Interfaces/Clients/IKucoinSocketClient.cs`
- `Kucoin.Net/Interfaces/Clients/SpotApi/**`
- `Kucoin.Net/Interfaces/Clients/FuturesApi/**`
- `Kucoin.Net/Interfaces/Clients/UnifiedApi/**`
- `docs/ai-api-map.md`
- `Examples/ai-friendly/`
