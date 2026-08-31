using CryptoExchange.Net.SharedApis;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IKucoinSocketClientFuturesApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IKucoinSocketClientFuturesSharedApi :
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeBookTickerOperation,
        ISubscribeKlinesOperation,
        ISubscribeOrderBookOperation,
        ISubscribeBalancesOperation,
        ISubscribeFuturesOrdersOperation,
        ISubscribePositionsOperation
    {
    }
}
