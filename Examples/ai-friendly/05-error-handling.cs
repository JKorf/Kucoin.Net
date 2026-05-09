// 05-error-handling.cs
//
// Demonstrates: WebCallResult patterns, retry logic, and common Kucoin-specific issues.
//
// Setup: dotnet add package Kucoin.Net

using CryptoExchange.Net.Objects;
using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;

var client = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

// ---- 1. THE BASIC PATTERN ----
// Every REST method returns WebCallResult<T> or WebCallResult.
// .Success is true/false. .Data is only valid when .Success is true.
var result = await client.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
async Task<WebCallResult<T>> WithRetry<T>(
    Func<Task<WebCallResult<T>>> call,
    int maxAttempts = 3)
{
    WebCallResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success)
            return last;

        if (last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(() => client.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT"));
if (ticker.Success)
{
    Console.WriteLine($"Retried ticker: {ticker.Data.LastPrice}");
}

// ---- 3. COMMON KUCOIN ERROR SCENARIOS ----
//
// Missing or invalid passphrase:
//   Kucoin credentials are key + secret + passphrase:
//   new KucoinCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE")
//
// Wrong symbol format:
//   Spot uses BTC-USDT. Futures uses contract symbols such as XBTUSDTM or ETHUSDTM.
//
// Invalid price increment / quantity increment:
//   Fetch symbol or contract metadata before creating order quantities and prices.
//
// Insufficient balance:
//   Permanent. Surface to the caller instead of retrying.
//
// Rate limit, timeout, network, or 5xx:
//   Check result.Error?.IsTransient before retrying.

// ---- 4. ORDER PLACEMENT WITH SYMBOL METADATA ----
var symbolInfo = await client.SpotApi.ExchangeData.GetSymbolAsync("BTC-USDT");
if (!symbolInfo.Success)
{
    Console.WriteLine($"Cannot fetch symbol info: {symbolInfo.Error}");
    return;
}

decimal rawQuantity = 0.00123456m;
decimal rawPrice = 50000.1234m;

// Kucoin symbol metadata exposes increments. Use decimal rounding based on the
// retrieved values instead of hardcoding assumptions.
decimal validQuantity = RoundDownToIncrement(rawQuantity, symbolInfo.Data.BaseIncrement);
decimal validPrice = RoundDownToIncrement(rawPrice, symbolInfo.Data.PriceIncrement);

var order = await client.SpotApi.Trading.PlaceTestOrderAsync(
    symbol: "BTC-USDT",
    side: OrderSide.Buy,
    type: NewOrderType.Limit,
    quantity: validQuantity,
    price: validPrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - retry with backoff"
        : "Permanent - fix request or surface to user";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

decimal RoundDownToIncrement(decimal value, decimal increment)
{
    if (increment <= 0)
        return value;

    return Math.Floor(value / increment) * increment;
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// Kucoin.Net returns API/network/rate-limit failures via WebCallResult.Error.
// Exceptions are for cancellation, disposal, invalid local arguments, or misconfiguration.

// Common variations:
//   With CancellationToken:       pass `ct: cancellationToken`
//   Per-request timeout:          configure options.RequestTimeout
//   Futures contract metadata:    client.FuturesApi.ExchangeData.GetContractAsync("ETHUSDTM")
//   Environment selection:        options.Environment = KucoinEnvironment.Europe
