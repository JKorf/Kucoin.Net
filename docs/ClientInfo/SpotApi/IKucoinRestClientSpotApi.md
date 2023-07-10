---
title: IKucoinRestClientSpotApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > SpotApi`  
*Spot API endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IKucoinRestClientSpotApiAccount](IKucoinRestClientSpotApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IKucoinRestClientSpotApiExchangeData](IKucoinRestClientSpotApiExchangeData.html) ExchangeData { get; }**  
***
*Endpoints related to orders and trades from Pro Account (High Frequency)*  
**[IKucoinRestClientSpotApiProAccount](IKucoinRestClientSpotApiProAccount.html) ProAccount { get; }**  
***
*Endpoints related to orders and trades*  
**[IKucoinRestClientSpotApiTrading](IKucoinRestClientSpotApiTrading.html) Trading { get; }**  
