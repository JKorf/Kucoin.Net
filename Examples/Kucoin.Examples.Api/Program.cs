using Kucoin.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using Microsoft.AspNetCore.Mvc;
using Kucoin.Net.Clients;
using Kucoin.Net.Objects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Kucoin services
builder.Services.AddKucoin();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddKucoin(restOptions =>
{
    restOptions.ApiCredentials = new KucoinApiCredentials("<APIKEY>", "<APISECRET>", "<PASS>");
    restOptions.RequestTimeout = TimeSpan.FromSeconds(5);
}, socketOptions =>
{
    socketOptions.ApiCredentials = new KucoinApiCredentials("<APIKEY>", "<APISECRET>", "<PASS>");
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoints and inject the Kucoin rest client
app.MapGet("/{Symbol}", async ([FromServices] IKucoinRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.MapGet("/Balances", async ([FromServices] IKucoinRestClient client) =>
{
    var result = await client.SpotApi.Account.GetAccountsAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();