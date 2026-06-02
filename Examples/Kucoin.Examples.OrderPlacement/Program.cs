using Kucoin.Net;
using Kucoin.Net.Clients;
using Kucoin.Net.Enums;

const string spotSymbol = "BTC-USDT";
const string futuresSymbol = "XBTUSDM";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";
var apiPass = "PASS";

Console.WriteLine("Kucoin.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinCredentials(apiKey, apiSecret, apiPass);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesReduceOnlyOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(KucoinRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.SpotApi.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    if (ticker.Data.LastPrice is null)
    {
        Console.WriteLine("Failed to get spot ticker: last price is missing");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice.Value * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: NewOrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.Id}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: active: {orderStatus.Data.IsActive}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.Id}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesReduceOnlyOrderExampleAsync(KucoinRestClient client)
{
    Console.WriteLine($"Placing futures reduce-only limit sell order for {futuresSymbol}...");

    var ticker = await client.FuturesApi.ExchangeData.GetTickerAsync(futuresSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.Price * 1.05m, 1);
    var order = await client.FuturesApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: OrderSide.Sell,
        type: NewOrderType.Limit,
        quantity: 1,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled,
        reduceOnly: true);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.Id}");

    var orderStatus = await client.FuturesApi.Trading.GetOrderAsync(order.Data.Id);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.FuturesApi.Trading.CancelOrderAsync(order.Data.Id);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.Id}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
