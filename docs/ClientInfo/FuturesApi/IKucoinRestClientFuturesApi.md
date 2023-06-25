---
title: IKucoinRestClientFuturesApi
has_children: true
parent: IKucoinClientFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinClient > FuturesApi > IKucoinRestClient`  
*Client for accessing the Kucoin Futures API.*
  
***
*Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**IKucoinRestClientFuturesApiAccount Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**IKucoinRestClientFuturesApiExchangeData ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**IKucoinRestClientFuturesApiTrading Trading { get; }**  
