---
title: Rest API documentation
has_children: true
---
*[generated documentation]*  
### KucoinClient  
*Client for accessing the Kucoin Spot API.*
  
***
*Futures API endpoints*  
**[IKucoinClientFuturesApi](FuturesApi/IKucoinClientFuturesApi.html) FuturesApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(KucoinApiCredentials credentials);**  
***
*Spot API endpoints*  
**[IKucoinClientSpotApi](SpotApi/IKucoinClientSpotApi.html) SpotApi { get; }**  
