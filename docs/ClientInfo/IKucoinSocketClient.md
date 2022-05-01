---
title: Socket API documentation
has_children: true
---
*[generated documentation]*  
### KucoinSocketClient  
*Client for accessing the Kucoin websocket API.*
  
***
*Futures streams*  
**[IKucoinSocketClientFuturesStreams](FuturesApi/IKucoinSocketClientFuturesStreams.html) FuturesStreams { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(KucoinApiCredentials credentials);**  
***
*Spot streams*  
**[IKucoinSocketClientSpotStreams](SpotApi/IKucoinSocketClientSpotStreams.html) SpotStreams { get; }**  
