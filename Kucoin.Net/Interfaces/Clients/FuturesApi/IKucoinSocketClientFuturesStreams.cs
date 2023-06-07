using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Threading.Tasks;
using System.Threading;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Interfaces;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures streams
    /// </summary>
    public interface IKucoinSocketClientFuturesStreams : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://docs.kucoin.com/futures/#execution-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://docs.kucoin.com/futures/#get-real-time-symbol-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://docs.kucoin.com/futures/#level-2-market-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// <para><a href="https://docs.kucoin.com/futures/#message-channel-for-the-5-best-ask-bid-full-data-of-level-2" /></para>
        /// <para><a href="https://docs.kucoin.com/futures/#message-channel-for-the-50-best-ask-bid-full-data-of-level-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// <para><a href="https://docs.kucoin.com/futures/#contract-market-data" /></para>
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
        /// <para><a href="https://docs.kucoin.com/futures/#system-annoucements" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// <para><a href="https://docs.kucoin.com/futures/#transaction-statistics-timer-event" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to wallet updates
        /// <para><a href="https://docs.kucoin.com/futures/#account-balance-events" /></para>
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
        /// <para><a href="https://docs.kucoin.com/futures/#position-change-events" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onPositionUpdate">Handler for position changes</param>
        /// <param name="onMarkPriceUpdate">Handler for update when position change due to mark price changes</param>
        /// <param name="onFundingSettlementUpdate">Handler for funding settlement updates</param>
        /// <param name="onRiskAdjustUpdate">Handler for risk adjust updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinPositionUpdate>> onPositionUpdate,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>> onMarkPriceUpdate,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>> onFundingSettlementUpdate,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>> onRiskAdjustUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para><a href="https://docs.kucoin.com/futures/#trade-orders" /></para>
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
        /// <para><a href="https://docs.kucoin.com/futures/#stop-order-lifecycle-event" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default);
    }
}
