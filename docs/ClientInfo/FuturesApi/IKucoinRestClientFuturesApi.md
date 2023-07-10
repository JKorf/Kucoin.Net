---
title: IKucoinRestClientFuturesApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > FuturesApi`  
*Client for accessing the Kucoin Futures API.*
  
***
*Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IKucoinRestClientFuturesApiAccount](IKucoinRestClientFuturesApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IKucoinRestClientFuturesApiExchangeData](IKucoinRestClientFuturesApiExchangeData.html) ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**[IKucoinRestClientFuturesApiTrading](IKucoinRestClientFuturesApiTrading.html) Trading { get; }**  
