---
title: Getting started
nav_order: 2
---


## Creating client
There are 2 clients available to interact with the Kucoin API, the `KucoinClient` and `KucoinSocketClient`.

*Create a new rest client*
```csharp
var kucoinClient = new KucoinClient(new KucoinClientOptions()
{
	// Set options here for this client
});
```

*Create a new socket client*
```csharp
var kucoinSocketClient = new KucoinSocketClient(new KucoinSocketClientOptions()
{
	// Set options here for this client
});
```

Different options are available to set on the clients, see this example
```csharp
var kucoinClient = new KucoinClient(new KucoinClientOptions()
{
	ApiCredentials = new KucoinApiCredentials("API-KEY", "API-SECRET", "API-PASSPHRASE"),
	LogLevel = LogLevel.Trace,
	RequestTimeout = TimeSpan.FromSeconds(60),
	FuturesApiOptions = new KucoinRestApiClientOptions
	{
		ApiCredentials = new KucoinApiCredentials("FUTURES-API-KEY", "FUTURES-API-SECRET", "FUTURES-API-PASSPHRASE"),
		AutoTimestamp = false
	}
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions`:
```csharp
KucoinClient.SetDefaultOptions(new KucoinClientOptions{
	// Set options here for all new clients
});
var kucoinClient = new KucoinClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#dependency-injection)
