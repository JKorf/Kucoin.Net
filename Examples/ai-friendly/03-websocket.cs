// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions: public ticker, klines,
// authenticated order and balance streams. Includes proper teardown.
//
// Setup: dotnet add package Kucoin.Net

using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;

// ---- 1. PUBLIC SOCKET CLIENT ----
var publicSocket = new KucoinSocketClient();

// Subscription methods return WebSocketResult<UpdateSubscription>.
var tickerSub = await publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC-USDT",
    update =>
    {
        Console.WriteLine($"BTC-USDT: {update.Data.LastPrice}");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.SpotApi.SubscribeToKlineUpdatesAsync(
    "ETH-USDT",
    KlineInterval.OneMinute,
    update =>
    {
        var candle = update.Data.Candles;
        Console.WriteLine($"ETH 1m candle: O={candle.OpenPrice} H={candle.HighPrice} L={candle.LowPrice} C={candle.ClosePrice}");
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT ----
var authSocket = new KucoinSocketClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

var orderSub = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    onNewOrder: update => Console.WriteLine($"New order {update.Data.OrderId} {update.Data.Symbol}"),
    onOrderData: update => Console.WriteLine($"Order update {update.Data.OrderId}: {update.Data.Status}"),
    onTradeData: update => Console.WriteLine($"Trade {update.Data.TradeId}: {update.Data.MatchQuantity}"));

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe order updates: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

var balanceSub = await authSocket.SpotApi.SubscribeToBalanceUpdatesAsync(
    update => Console.WriteLine($"Balance {update.Data.Asset}: {update.Data.Available} available"));

if (!balanceSub.Success)
{
    Console.WriteLine($"Failed to subscribe balance updates: {balanceSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await authSocket.UnsubscribeAsync(orderSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);
await authSocket.UnsubscribeAsync(balanceSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple tickers:      SubscribeToTickerUpdatesAsync(new[] { "BTC-USDT", "ETH-USDT" }, handler)
//   Order book:            SubscribeToOrderBookUpdatesAsync(symbol, limit, handler)
//   Trades:                SubscribeToTradeUpdatesAsync(symbol, handler)
//   Futures book ticker:   authSocket.FuturesApi.SubscribeToBookTickerUpdatesAsync("ETHUSDTM", handler)
//   Unified account:       authSocket.UnifiedApi.SubscribeToOrderUpdatesAsync(...)
