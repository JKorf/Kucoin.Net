using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Interfaces.Clients.UnifiedApi
{

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IKucoinSocketClientUnifiedSharedApi :
        ISubscribeKlinesSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeOrderBookSocket,
        ISubscribeIncrementalOrderBookSocket,
        ISubscribeMarkPriceSocket,
        ISubscribeIndexPriceSocket,
        ISubscribeFundingInfoSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribeUserTradesSocket,
        ISubscribeBalancesSocket,
        ISubscribePositionsSocket,
        IPlaceSpotOrderSocket,
        IEditSpotOrderSocket,
        IEditSpotOrderByClientOrderIdSocket,
        ICancelSpotOrderSocket,
        ICancelSpotOrderByClientOrderIdSocket,
        IPlaceFuturesOrderSocket,
        IEditFuturesOrderSocket,
        IEditFuturesOrderByClientOrderIdSocket,
        ICancelFuturesOrderSocket,
        ICancelFuturesOrderByClientOrderIdSocket
    {
    }
}
