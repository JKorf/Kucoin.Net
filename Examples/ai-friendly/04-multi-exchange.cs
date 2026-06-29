// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// The same pattern works across Kucoin, Binance, OKX, Bybit, Kraken, and other
// CryptoExchange.Net exchange libraries.
//
// Setup:
//   dotnet add package Kucoin.Net
//   dotnet add package Binance.Net  // optional, for adding another exchange

using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on supported API surfaces.
ISpotTickerRestClient kucoinShared = new KucoinRestClient().SpotApi.SharedClient;
var capabilities = kucoinShared.Discover();
Console.WriteLine($"Shared features: {capabilities.Features.Count(x => x.Supported)}");

// SharedSymbol normalizes symbols for each exchange. Kucoin spot is "BTC-USDT",
// Binance spot is "BTCUSDT", but shared APIs let you describe base/quote once.
var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(kucoinShared, btcusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- AVAILABLE SHARED INTERFACES ----
// Spot REST includes:
//   IAssetsRestClient, IBalanceRestClient, IDepositRestClient, IKlineRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, ISpotOrderRestClient
//   ISpotSymbolRestClient, ISpotTickerRestClient, IWithdrawalRestClient
//   IWithdrawRestClient, IFeeRestClient, ISpotOrderClientIdRestClient
//   ISpotTriggerOrderRestClient, IBookTickerRestClient, ITransferRestClient
// Futures REST includes:
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IPositionRestClient
//   IFuturesTickerRestClient, IOrderBookRestClient, IRecentTradeRestClient
//   IKlineRestClient, IBookTickerRestClient

// ---- WEBSOCKET EXAMPLE ----
var kucoinSocket = new KucoinSocketClient();
ITickerSocketClient tickerSocket = kucoinSocket.SpotApi.SharedClient;

var sub = await tickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{tickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await kucoinSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Multi-exchange scanner:  loop over List<ISpotTickerRestClient>
//   Cross-exchange books:    IOrderBookSocketClient on each exchange
//   Best execution:          ISpotOrderRestClient on supported exchanges
//   Futures routing:         use client.FuturesApi.SharedClient

