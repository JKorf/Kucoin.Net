// 02-futures.cs
//
// Demonstrates: Kucoin Futures contract market data, market order with leverage,
// retrieve open position, close position.
//
// Setup: dotnet add package Kucoin.Net
// Substitute API_KEY / API_SECRET / API_PASSPHRASE. The API key must have Futures trading enabled.

using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;

var client = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

// Kucoin futures symbols are contract names, not spot symbols.
const string symbol = "ETHUSDTM";

// ---- 1. READ CONTRACT / TICKER DATA ----
var contract = await client.FuturesApi.ExchangeData.GetContractAsync(symbol);
if (!contract.Success)
{
    Console.WriteLine($"Failed to get contract: {contract.Error}");
    return;
}

var ticker = await client.FuturesApi.ExchangeData.GetTickerAsync(symbol);
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"{symbol} last trade price: {ticker.Data.Price}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// Futures quantity is contract count. Use quantityInBaseAsset or quantityInQuoteAsset
// if your workflow is sized in asset/quote value instead of contracts.
var openOrder = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: NewOrderType.Market,
    leverage: 5m,
    quantity: 1,
    marginMode: FuturesMarginMode.Isolated);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened position via order {openOrder.Data.Id}");

// ---- 3. GET CURRENT POSITION ----
var positions = await client.FuturesApi.Account.GetPositionAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get position: {positions.Error}");
    return;
}

var position = positions.Data.FirstOrDefault(p => p.CurrentQuantity != 0);
if (position == null)
{
    Console.WriteLine("No open position found (may not have filled yet).");
    return;
}

Console.WriteLine($"Position: {position.CurrentQuantity} contracts at avg {position.AverageEntryPrice}");
Console.WriteLine($"Unrealized PnL: {position.UnrealizedPnl}");
Console.WriteLine($"Liquidation price: {position.LiquidationPrice}");

// ---- 4. CLOSE THE POSITION ----
// Opposite side, same absolute contract quantity, reduceOnly=true to avoid flipping position.
var closeOrder = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Sell,
    type: NewOrderType.Market,
    leverage: 5m,
    quantity: (int)Math.Abs(position.CurrentQuantity),
    reduceOnly: true,
    marginMode: FuturesMarginMode.Isolated);

if (closeOrder.Success)
{
    Console.WriteLine($"Closed position via order {closeOrder.Data.Id}");
}

// Common variations:
//   Limit order:         type: NewOrderType.Limit, add price + timeInForce
//   Stop order:          add stopType, stopPriceType, stopPrice
//   Hedge mode:          add positionSide: PositionSide.Long / PositionSide.Short
//   Cross leverage:      client.FuturesApi.Account.SetCrossMarginLeverageAsync(symbol, leverage)
//   Margin mode:         client.FuturesApi.Account.SetMarginModeAsync(symbol, FuturesMarginMode.Isolated)
