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
        IGetBalancesEndpoint,
        IGetFuturesTickerEndpoint,
        IGetAllFuturesTickersEndpoint,
        IGetFuturesSymbolsEndpoint,
        IPlaceFuturesOrderEndpoint,
        IGetFuturesOrderEndpoint,
        IGetOpenFuturesOrdersEndpoint,
        IGetClosedFuturesOrdersEndpoint,
        IGetFuturesOrderTradesEndpoint,
        IGetFuturesUserTradeHistoryEndpoint,
        ICancelFuturesOrderEndpoint,
        IGetPositionsEndpoint,
        IClosePositionEndpoint,
        IGetKlinesEndpoint,
        IGetRecentTradesEndpoint,
        IGetOrderBookEndpoint,
        IGetOpenInterestEndpoint,
        IGetFundingRateHistoryEndpoint,
        IGetPositionHistoryEndpoint,
        IGetFeesEndpoint,
        IGetFuturesOrderByClientOrderIdEndpoint,
        ICancelFuturesOrderByClientOrderIdEndpoint,
        ISetFuturesTpSlEndpoint,
        ICancelFuturesTpSlEndpoint,
        IGetBookTickerEndpoint,
        IGetLeverageEndpoint,
        ISetLeverageEndpoint
    {

    }
}
