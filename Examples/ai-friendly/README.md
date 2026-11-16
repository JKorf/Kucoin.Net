# Kucoin.Net examples

Each file is an independent console program. Install `Kucoin.Net` in a console project and copy one program to `Program.cs`.

| Program | Workflow |
|---|---|
| [01-spot-quickstart.cs](01-spot-quickstart.cs) | Public ticker, accounts, live limit order, status and cancellation |
| [02-futures.cs](02-futures.cs) | Contract data, one-way position-mode check, live order and reduce-only close |
| [03-websocket.cs](03-websocket.cs) | Spot/Futures streams and cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 ticker capability, symbol catalog and subscription |
| [05-error-handling.cs](05-error-handling.cs) | Results, public-read retries and metadata-based test order |

Replace API key, secret and passphrase placeholders for intended private operations. Spot/Futures examples submit live orders when executed. Cancellation can race with fills; read [operational considerations](../../.claude/skills/kucoin-net/references/safety.md) first. Use the [usage snippets](../../.claude/skills/kucoin-net/references/usage.md) for test placement and Unified workflows.

`AiExampleCompileTests` compiles these programs without running exchange operations.
