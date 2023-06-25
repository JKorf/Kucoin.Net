---
title: IKucoinRestClient
has_children: true
---
*[generated documentation]*  
### KucoinClient  
*Client for accessing the Kucoin Spot API.*
  
***
*Futures API endpoints*  
**IKucoinRestClientFuturesApi FuturesApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(KucoinApiCredentials credentials);**  
***
*Spot API endpoints*  
**IKucoinRestClientSpotApi SpotApi { get; }**  
