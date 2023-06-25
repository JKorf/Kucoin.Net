---
title: IKucoinRestClientSpotApi
has_children: true
parent: IKucoinClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinClient > SpotApi > IKucoinRestClient`  
*Spot API endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**IKucoinRestClientSpotApiAccount Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**IKucoinRestClientSpotApiExchangeData ExchangeData { get; }**  
***
*Endpoints related to orders and trades from Pro Account (High Frequency)*  
**IKucoinRestClientSpotApiProAccount ProAccount { get; }**  
***
*Endpoints related to orders and trades*  
**IKucoinRestClientSpotApiTrading Trading { get; }**  
