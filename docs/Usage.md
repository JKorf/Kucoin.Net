---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Kucoin API, the `KucoinClient` and `KucoinSocketClient`. They can be created manually on the fly or be added to the dotnet DI using the `AddKucoin` extension method.

*Create a new rest client*
```csharp
var kucoinRestClient = new KucoinRestClient(options =>
{
    // Set options here for this client
});

var kucoinSocketClient = new KucoinSocketClient(options =>
{
    // Set options here for this client
});
```

*Using dotnet dependency inject*
```csharp
services.AddKucoin(
    restOptions => {
        // set options for the rest client
    },
    socketClientOptions => {
        // set options for the socket client
    }); 
    
// IKucoinRestClient, IKucoinSocketClient and IKucoinOrderBookFactory are now available for injecting
```

Different options are available to set on the clients, see this example
```csharp
var kucoinRestClient = new KucoinRestClient(options =>
{
    options.ApiCredentials = new KucoinApiCredentials("API-KEY", "API-SECRET", "API-PASSPHRASE");
    options.RequestTimeout = TimeSpan.FromSeconds(60);
    options.FuturesOptions.ApiCredentials = new KucoinApiCredentials("FUTURES-API-KEY", "FUTURES-API-SECRET", "FUTURES-API-PASSPHRASE");
    options.FuturesOptions.AutoTimestamp = false;
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions` or during the registration in the DI container:  
```csharp
KucoinRestClient.SetDefaultOptions(options => {
    // Set options here for all new clients
});
var kucoinRestClient = new KucoinRestClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Dependency%20Injection.html)
