using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures;
using System;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Futures.Socket;
using Kucoin.Net.Objects.Spot.Socket;
using CryptoExchange.Net.Interfaces;
using System.Threading;

namespace Kucoin.Net.Interfaces.Clients.Socket
{
    /// <summary>
    /// Futures subscriptions
    /// </summary>
    public interface IKucoinSocketClientFutures: ISocketClient
    {
        /// <summary>
        /// Subscribe to trade updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate, 
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe system announcement
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to wallet updates
        /// </summary>
        /// <param name="onOrderMarginUpdate">Data handler for order margin updates</param>
        /// <param name="onBalanceUpdate">Data handler for balance updates</param>
        /// <param name="onWithdrawableUpdate">Data handler for withdrawable funds updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate, 
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinPosition>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// </summary>
        /// <param name="symbol">[Optional] Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData, 
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to stop order updates
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default);
    }
}
