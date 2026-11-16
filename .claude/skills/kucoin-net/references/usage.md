# Kucoin.Net usage

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
```

## Public setup and result handling

```csharp
using var client = new KucoinRestClient();
var ticker = await client.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}
Console.WriteLine(ticker.Data.LastPrice);
```

## Credentials and account read

```csharp
var key = Environment.GetEnvironmentVariable("KUCOIN_KEY")
    ?? throw new InvalidOperationException("API key is required");
var secret = Environment.GetEnvironmentVariable("KUCOIN_SECRET")
    ?? throw new InvalidOperationException("API secret is required");
var pass = Environment.GetEnvironmentVariable("KUCOIN_PASSPHRASE")
    ?? throw new InvalidOperationException("API passphrase is required");
using var client = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials(key, secret, pass);
});
var accounts = await client.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine(accounts.Error);
    return;
}
foreach (var account in accounts.Data.Where(x => x.Total > 0))
    Console.WriteLine($"{account.Asset} {account.Type}: {account.Available}");
```

## Spot test order

This and the classic Futures test snippet assume the authenticated client above. See [operational considerations](safety.md#order-execution-and-results) before adapting them to live placement.

```csharp
var result = await client.SpotApi.Trading.PlaceTestOrderAsync(
    symbol: "BTC-USDT", side: OrderSide.Buy, type: NewOrderType.Limit,
    quantity: 0.001m, price: 50000m, timeInForce: TimeInForce.GoodTillCanceled);
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine("Spot test order accepted");
```

## Classic Futures metadata and test order

```csharp
var contract = await client.FuturesApi.ExchangeData.GetContractAsync("ETHUSDTM");
if (!contract.Success)
{
    Console.WriteLine(contract.Error);
    return;
}
Console.WriteLine(contract.Data.Symbol);
var result = await client.FuturesApi.Trading.PlaceTestOrderAsync(
    symbol: "ETHUSDTM", side: OrderSide.Buy, type: NewOrderType.Market,
    leverage: 5m, quantity: 1, marginMode: FuturesMarginMode.Isolated);
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine("Futures test order accepted");
```

Read positions before deriving a close; mode-specific closing considerations are in [Futures modes](safety.md#futures-modes-sizing-and-closes).

```csharp
var positions = await client.FuturesApi.Account.GetPositionAsync("ETHUSDTM");
if (!positions.Success)
{
    Console.WriteLine(positions.Error);
    return;
}
foreach (var position in positions.Data.Where(x => x.CurrentQuantity != 0))
    Console.WriteLine($"{position.Symbol}: {position.CurrentQuantity} contracts");
```

## Unified account read

Use a credentialed client for the intended Unified account:

```csharp
var result = await client.UnifiedApi.Account.GetBalancesAsync();
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data);
```

## Subscription lifecycle

```csharp
using var socketClient = new KucoinSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC-USDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Unified native ticker streams use a product type:

```csharp
using var socketClient = new KucoinSocketClient();
var subscription = await socketClient.UnifiedApi.SubscribeToTickerUpdatesAsync(
    ProductType.Spot, "BTC-USDT", update => Console.WriteLine(update.Data));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Private order/balance streams use credentialed socket options with the same `KucoinCredentials` shape. Unified socket place/edit/cancel calls return query results, not subscription handles.

## Shared API V2

Use a narrow capability on the selected surface. This example uses Unified REST:

```csharp
IGetTickerRest ticker = client.UnifiedApi.SharedApi;
var result = await ticker.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Load the symbol catalog on the same surface before inspecting it:

```csharp
IGetSpotSymbolsRest symbolsClient = client.UnifiedApi.SharedApi;
var result = await symbolsClient.GetSpotSymbolsAsync(new GetSymbolsRequest());
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var symbol in result.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");
var catalog = symbolsClient.SpotSymbolCatalog;
```

## Dependency injection and selection

```csharp
var services = new ServiceCollection();
services.AddKucoin();
using var provider = services.BuildServiceProvider();
var aggregate = provider.GetRequiredService<IKucoinSharedApiClient>();
IGetTickerRest selected = aggregate.UnifiedRest;
var result = await selected.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
var matches = aggregate.GetCapabilities(
    SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot);
```

Configure `services.AddKucoin(options => { options.ApiCredentials = credentials; })` for authenticated consumers; `options.Rest.ApiCredentials` and `options.Socket.ApiCredentials` can be set independently. Inject `IKucoinRestClient`, `IKucoinSocketClient`, the aggregate, or a registered narrow capability. [API routing](api-surfaces.md#shared-api-v2) covers dynamic lookup and multiple implementations; select the intended classic or Unified account surface.

## Complete programs (checkout-only)

[Examples](../../../../Examples/ai-friendly/README.md) contains independent Spot, Futures, socket, Shared API V2, and public-read retry programs. These links require a checkout; the snippets above work with the installed package.
