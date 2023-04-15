---
title: IKucoinClientSpotApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`KucoinClient > SpotApi`  
*Spot API endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IKucoinClientSpotApiAccount](IKucoinClientSpotApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IKucoinClientSpotApiExchangeData](IKucoinClientSpotApiExchangeData.html) ExchangeData { get; }**  
***
*Endpoints related to orders and trades from Pro Account (High Frequency)*  
**[IKucoinClientSpotApiProAccount](IKucoinClientSpotApiProAccount.html) ProAccount { get; }**  
***
*Endpoints related to orders and trades*  
**[IKucoinClientSpotApiTrading](IKucoinClientSpotApiTrading.html) Trading { get; }**  
