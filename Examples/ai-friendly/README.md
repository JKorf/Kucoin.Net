# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package Kucoin.Net` and it builds.
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why each line matters.
- **Idiomatic** - follows current Kucoin.Net 8.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, public ticker, authenticated balances, place limit order, query order status |
| `02-futures.cs` | Futures contracts: market data, leverage parameter, market order, position lookup, reduce-only close |
| `03-websocket.cs` | Subscribe to ticker updates, klines, private order/balance streams, with teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `WebCallResult` patterns, retry, common Kucoin symbol/credential issues |

## Running

```bash
dotnet new console -n MyKucoinApp
cd MyKucoinApp
dotnet add package Kucoin.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET / API_PASSPHRASE placeholders for private examples
dotnet run
```

