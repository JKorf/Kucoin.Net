# Copilot Instructions for Kucoin.Net

This repository is **Kucoin.Net** - a strongly typed C#/.NET client library for the Kucoin cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Kucoin.Net, follow these conventions:

## Use Kucoin.Net, not raw HTTP

Never generate `HttpClient` calls to Kucoin REST or WebSocket endpoints. Always use `KucoinRestClient` or `KucoinSocketClient`. This ensures correct request signing, passphrase handling, rate limiting, and error handling.

## Client setup

```csharp
using Kucoin.Net;
using Kucoin.Net.Clients;

var restClient = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});
```

For public market data only, no credentials are needed: `new KucoinRestClient()`.

## Result handling

Methods return `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API structure

- `restClient.SpotApi.ExchangeData` - public spot market data
- `restClient.SpotApi.Account` - balances, deposits, withdrawals, account info
- `restClient.SpotApi.SubAccount` - sub-account endpoints
- `restClient.SpotApi.Trading` - spot orders, margin orders, stop orders, OCO
- `restClient.SpotApi.HfTrading` - high frequency spot and margin order endpoints
- `restClient.SpotApi.Margin` - margin configuration, lending, interest, mark prices
- `restClient.SpotApi.Earn` - Kucoin Earn endpoints
- `restClient.FuturesApi.ExchangeData` - futures market data
- `restClient.FuturesApi.Account` - futures account, positions, margin mode, leverage
- `restClient.FuturesApi.Trading` - futures orders
- `restClient.UnifiedApi.*` - Unified Account endpoints
- `socketClient.SpotApi`, `socketClient.FuturesApi`, `socketClient.UnifiedApi` - WebSocket streams

## Symbols and credentials

Spot symbols use dash-separated names such as `BTC-USDT`. Futures symbols use contract names such as `XBTUSDTM` or `ETHUSDTM`. Kucoin credentials require key, secret, and passphrase.

## Order placement

Let the library auto-generate `clientOrderId`. Do not pass a custom value unless required for an existing operational flow.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces accessed via `.SharedClient` properties. Kucoin exposes shared clients on `SpotApi` and `FuturesApi`.

## Avoid

- Legacy or guessed `KucoinClient` class; use `KucoinRestClient`
- Generic `ApiCredentials`; use `KucoinCredentials`
- Two-argument credentials; Kucoin requires passphrase
- Binance-style spot symbols like `BTCUSDT`; use `BTC-USDT`
- `.Result` / `.Wait()`; use `await`
- Instantiating clients per request; use DI and reuse instances
- Skipping `WebCallResult.Success`
- Manual signing or raw Kucoin URLs

## Reference

For detailed patterns and pitfalls see `CLAUDE.md`, `llms.txt`, and `llms-full.txt` in the repository root, and `Examples/ai-friendly/` for compilable examples.

