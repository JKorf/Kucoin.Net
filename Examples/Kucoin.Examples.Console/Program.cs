using Kucoin.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

// REST
var restClient = new KucoinRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETH-USDT");
Console.WriteLine($"Rest client ticker price for ETH-USDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
// Optional, manually add logging
var logFactory = new LoggerFactory();
logFactory.AddProvider(new TraceLoggerProvider());

var socketClient = new KucoinSocketClient(logFactory);
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH-USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
