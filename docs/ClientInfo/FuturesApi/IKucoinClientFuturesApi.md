---
title: IKucoinClientFuturesApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`KucoinClient > FuturesApi`  
*Client for accessing the Kucoin Futures API.*
  
***
*Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IKucoinClientFuturesApiAccount](IKucoinClientFuturesApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IKucoinClientFuturesApiExchangeData](IKucoinClientFuturesApiExchangeData.html) ExchangeData { get; }**  
***
*The factory for creating requests. Used for unit testing*  
**IRequestFactory RequestFactory { get; set; }**  
***
*Endpoints related to orders and trades*  
**[IKucoinClientFuturesApiTrading](IKucoinClientFuturesApiTrading.html) Trading { get; }**  
