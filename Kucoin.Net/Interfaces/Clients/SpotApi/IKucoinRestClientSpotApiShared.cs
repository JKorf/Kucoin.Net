using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IKucoinRestClientSpotApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        //ITradeHistoryRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        IFeeRestClient,
        ISpotOrderClientIdRestClient,
        ISpotTriggerOrderRestClient,
        IBookTickerRestClient,
        ITransferRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IKucoinRestClientSpotSharedApi :
        IGetAssetEndpoint,
        IGetAllAssetsEndpoint,
        IGetBalancesEndpoint,
        IGetKlinesEndpoint,
        IGetOrderBookEndpoint,
        IGetRecentTradesEndpoint,
        IPlaceSpotOrderEndpoint,
        IGetSpotOrderEndpoint,
        IGetOpenSpotOrdersEndpoint,
        IGetClosedSpotOrdersEndpoint,
        IGetSpotOrderTradesEndpoint,
        IGetSpotUserTradeHistoryEndpoint,
        ICancelSpotOrderEndpoint,
        IGetSpotSymbolsEndpoint,
        IGetSpotTickerEndpoint,
        IGetAllSpotTickersEndpoint,
        IGetWithdrawalHistoryEndpoint,
        IWithdrawEndpoint,
        IGetFeesEndpoint,
        IGetSpotOrderByClientOrderIdEndpoint,
        ICancelSpotOrderByClientOrderIdEndpoint,
        IGetSpotTriggerOrderEndpoint,
        IPlaceSpotTriggerOrderEndpoint,
        ICancelSpotTriggerOrderEndpoint,
        IGetBookTickerEndpoint,
        ITransferEndpoint,
        IGetDepositAddressesEndpoint,
        IGetDepositHistoryEndpoint
    {
    }
}
