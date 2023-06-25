---
title: Socket API documentation
has_children: true
---
*[generated documentation]*  
### KucoinSocketClient  
*Client for accessing the Kucoin websocket API.*
  
***
*Futures socket api*  
**[IKucoinSocketClientFuturesApi](FuturesApi/IKucoinSocketClientFuturesApi.html) FuturesApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(KucoinApiCredentials credentials);**  
***
*Spot socket api*  
**[IKucoinSocketClientSpotApi](SpotApi/IKucoinSocketClientSpotApi.html) SpotApi { get; }**  
