using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IKucoinRestClientFuturesApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient,
        IFundingRateRestClient,
        IPositionHistoryRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient,
        ILeverageRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IKucoinRestClientFuturesSharedApi :
        IGetBalancesRest,
        IGetTickerRest,
        IGetAllTickersRest,
        IGetFuturesSymbolsRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelFuturesOrderRest,
        IGetPositionsRest,
        ICloseFullPositionRest,
        IGetKlinesRest,
        IGetRecentTradesRest,
        IGetOrderBookRest,
        IGetOpenInterestRest,
        IGetFundingRateHistoryRest,
        IGetPositionHistoryRest,
        IGetFeesRest,
        IGetFuturesOrderByClientOrderIdRest,
        ICancelFuturesOrderByClientOrderIdRest,
        ISetFuturesTpSlRest,
        ICancelFuturesTpSlRest,
        IGetBookTickerRest,
        IGetLeverageRest,
        ISetLeverageRest
    {

    }
}
