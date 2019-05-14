# ![Icon](https://github.com/JKorf/Kucoin.Net/blob/master/Resources/icon.png?raw=true) Kucoin.Net 

![Build status](https://travis-ci.org/JKorf/Kucoin.Net.svg?branch=master)

A .Net wrapper for the Kucoin API as described on [Kucoin](https://docs.kucoin.com/), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Kucoin.Net/issues)**

---
Also check out my other exchange API wrappers:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://github.com/JKorf/Binance.Net/blob/master/Resources/binance-coin.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Huobi.Net"><img src="https://github.com/JKorf/Huobi.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Huobi.Net">Huobi</a>
</td>
</table>

And other API wrappers based on CryptoExchange.Net:
<table>
<tr>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
	<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
</tr>
</table>


## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  


## Installation
![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Kucoin.Net.svg)
Available on [Nuget](https://www.nuget.org/packages/Kucoin.Net/).
```
pm> Install-Package Kucoin.Net
```
To get started with Kucoin.Net first you will need to get the library itself. The easiest way to do this is to install the package into your project using [NuGet](https://www.nuget.org/packages/Kucoin.Net/). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type'Kucoin.Net' and hit enter. The Kucoin.Net package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Kucoin.Net in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Kucoin.Net will be installed in. After selecting the correct project type  `Install-Package Kucoin.Net`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Kucoin.Net.
## Getting started
After installing it's time to actually use it. To get started you have to add the Kucoin.Net namespace: `using Kucoin.Net;`.

Kucoin.Net provides two clients to interact with the Kucoin API. The `KucoinClient` provides all rest API calls. The  `KucoinSocketClient`  provides functions to interact with the websocket provided by the Kucoin API. Both clients are disposable and as such can be used in a `using` statement.

Most API methods are available in two flavors, sync and async:
````C#
public void NonAsyncMethod()
{
    using(var client = new KucoinClient())
    {
        var result = client.GetMarkets();
    }
}

public async Task AsyncMethod()
{
    using(var client = new KucoinClient())
    {
        var result2 = await client.GetMarketsAsync();
    }
}
````

## Response handling
All API requests will respond with a (Web)CallResult object. This object contains whether the call was successful, the data returned from the call and an error if the call wasn't successful. As such, one should always check the Success flag when processing a response.
For example:
```C#
using(var client = new KucoinClient())
{
	var result = client.GetSymbols();
	if (result.Success)
		Console.WriteLine($"First symbol: {result.Data[0].Name}");
	else
		Console.WriteLine($"Error: {result.Error.Message}");
}
```
## Options & Authentication
The default behavior of the clients can be changed by providing options to the constructor, or using the `SetDefaultOptions` before creating a new client. Api credentials can be provided in the options.

## Websockets
The Kucoin.Net socket client provides several socket endpoint to which can be subscribed and follow this function structure

```C#
var client = new KucoinSocketClient();

var subscribeResult = client.SubscribeToAllTickerUpdates(data =>
{
	// handle data
});
```

**Handling socket events**

Subscribing to a socket stream returns a UpdateSubscription object. This object can be used to be notified when a socket is disconnected or reconnected:
````C#
var subscriptionResult = client.SubscribeToAllTickerUpdates(data =>
{
	Console.WriteLine("Received ticker update");
});

if(subscriptionResult.Success){
	sub.Data.Disconnected += () =>
	{
		Console.WriteLine("Socket disconnected");
	};

	sub.Data.Reconnected += (e) =>
	{
		Console.WriteLine("Socket reconnected after " + e);
	};
}
````

**Unsubscribing from socket endpoints:**

Sockets streams can be unsubscribed by using the `client.Unsubscribe` method in combination with the stream subscription received from subscribing:
```C#
var client = new KucoinSocketClient();

var successSubscribe = client.SubscribeToAllTickerUpdates((data) =>
{
	// handle data
});

client.Unsubscribe(successSubscribe.Data);
```

Additionaly, all sockets can be closed with the `UnsubscribeAll` method. Beware that when a client is disposed the sockets are automatically disposed. This means that if the code is no longer in the using statement the eventhandler won't fire anymore. To prevent this from happening make sure the code doesn't leave the using statement or don't use the socket client in a using statement:
```C#
// Doesn't leave the using block
using(var client = new KucoinSocketClient())
{
	var successSubscribe = client.SubscribeToAllTickerUpdates((data) =>
	{
		// handle data
	});

	Console.ReadLine();
}

// Without using block
var client = new KucoinSocketClient();
client.SubscribeToAllTickerUpdates((data) =>
{
	// handle data
});
```

## Release notes
* Version 0.0.2 - 14 may 2019
	* Added an order book implementation for easily keeping an updated order book
	* Added additional constructor to ApiCredentials to be able to read from file
	* Added ConfigureAwait calls to prevent deadlocks

* Version 0.0.1 - 09 may 2019
	* Initial release
