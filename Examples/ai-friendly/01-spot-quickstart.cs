// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated balances,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package Kucoin.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET / API_PASSPHRASE below
//   dotnet run

using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create per request.
var publicClient = new KucoinRestClient();

// Kucoin spot symbols use a dash: BTC-USDT, ETH-USDT, etc.
var ticker = await publicClient.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"BTC/USDT last price: {ticker.Data.LastPrice}");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
// Kucoin credentials require key, secret, and passphrase.
var tradingClient = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

var accounts = await tradingClient.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine($"Failed to get accounts: {accounts.Error}");
    return;
}

foreach (var account in accounts.Data.Where(x => x.Total > 0))
{
    Console.WriteLine($"{account.Asset} {account.Type}: {account.Available} available, {account.Holds} on hold");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.001 BTC at a price 5% below current price; likely will not fill immediately.
// Let the library generate clientOrderId unless you need external correlation.
var safePrice = Math.Round((ticker.Data.LastPrice ?? 0m) * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTC-USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: 0.001m,
    price: safePrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id} at {safePrice}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync(order.Data.Id);
if (status.Success)
{
    Console.WriteLine($"Order active: {status.Data.IsActive}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync(order.Data.Id);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.Id}");
}

// Common variations:
//   Market order by quantity: type: NewOrderType.Market, quantity: 0.001m
//   Market buy by quote:      type: NewOrderType.Market, quoteQuantity: 100m
//   Stop order:               tradingClient.SpotApi.Trading.PlaceStopOrderAsync(...)
//   OCO:                      tradingClient.SpotApi.Trading.PlaceOcoOrderAsync(...)
