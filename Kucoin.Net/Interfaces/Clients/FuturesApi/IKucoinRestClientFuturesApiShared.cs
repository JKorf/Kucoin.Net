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
        IBookTickerRestClient
    {
    }
}
