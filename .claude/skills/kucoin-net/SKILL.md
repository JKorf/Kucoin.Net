---
name: kucoin-net
description: Generate and troubleshoot C#/.NET code consuming Kucoin.Net for Spot, Margin, high-frequency trading, Futures, Unified accounts, market data, account operations, REST, WebSocket, and Shared API V2 integrations.
---

# Kucoin.Net

Install:

```sh
dotnet add package Kucoin.Net
```

Use `KucoinRestClient` and `KucoinSocketClient`. Both expose `SpotApi`, `FuturesApi`, and `UnifiedApi`; REST methods are grouped by purpose and socket methods are directly on each root. Reuse and dispose clients, await calls, and retain successful subscriptions for unsubscribe cleanup.

Credentials are `KucoinCredentials(key, secret, passphrase)`. All three values are required for signed workflows. Configure the environment and API permissions for the intended region, account, and product; public market data needs no credentials.

Spot symbols use `BTC-USDT`; classic Futures contract identifiers include `XBTUSDTM` and `ETHUSDTM`. Use metadata or `KucoinExchange.FormatSymbol` for reusable formatting. Classic Futures `quantity` is an integer contract count; native base/quote sizing parameters and Unified quantity units have distinct semantics.

Check `Success` before `Data`. REST returns `HttpResult`, socket order requests on `UnifiedApi` return `QueryResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and some Shared helpers return `ExchangeCallResult`, with generic variants where applicable. Inspect nested batch outcomes.

Use `.SharedApi` and narrow V2 capabilities for portable code. `IKucoinSharedApiClient` groups six surfaces, including `UnifiedRest` and `UnifiedSocket`. Support differs by surface: Unified socket trading does not imply classic socket trading, nor support for every subscription. Use native methods for account modes, HF, Earn, detailed margin operations, and exchange-specific options.

## Read when needed

- [API surfaces](references/api-surfaces.md): roots, endpoint routing, Unified workflows, Shared capabilities, symbols, and quantities.
- [Usage](references/usage.md): setup, credentials, account reads, test orders, subscriptions, Shared API V2, and DI.
- [Operational considerations](references/safety.md): execution, modes, leverage, borrowing, movements, and uncertain submissions.

For work on the library itself, use [repository instructions](../../../AGENTS.md) (checkout-only).
