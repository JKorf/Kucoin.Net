using CryptoExchange.Net.Objects;
using System;
using System.Threading.Tasks;
using System.Threading;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Interfaces;
using System.Collections.Generic;
using CryptoExchange.Net.Objects.Sockets;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures socket api
    /// </summary>
    public interface IKucoinSocketClientFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/match-execution-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/match-execution-data" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example 'XBTUSDTM'</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example 'XBTUSDTM'</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/get-ticker-v2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/get-ticker-v2" /></para>
        /// </summary>
        /// <param name="symbols">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-market-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-market-data" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-5-best-ask-bid-orders" /></para>
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-50-best-ask-bid-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-5-best-ask-bid-orders" /></para>
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/level2-50-best-ask-bid-orders" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/contract-market-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/contract-market-data" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarketUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe system announcement
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/funding-fee-settlement" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSystemAnnouncementsAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/transaction-statistics-timer-event" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/transaction-statistics-timer-event" /></para>
        /// </summary>
        /// <param name="symbols">Symbol, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to wallet updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/private-channels/account-balance-events" /></para>
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
        /// Subscribe to position updates for a specific symbol
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/private-channels/position-change-events" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDTM`</param>
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
        /// Subscribe to position updates. Note that this overrides any symbol specific position subscriptions
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/private-channels/all-position-change-events" /></para>
        /// </summary>
        /// <param name="onPositionUpdate">Handler for position changes</param>
        /// <param name="onMarkPriceUpdate">Handler for update when position change due to mark price changes</param>
        /// <param name="onFundingSettlementUpdate">Handler for funding settlement updates</param>
        /// <param name="onRiskAdjustUpdate">Handler for risk adjust updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            Action<DataEvent<KucoinPositionUpdate>>? onPositionUpdate = null,
            Action<DataEvent<KucoinPositionMarkPriceUpdate>>? onMarkPriceUpdate = null,
            Action<DataEvent<KucoinPositionFundingSettlementUpdate>>? onFundingSettlementUpdate = null,
            Action<DataEvent<KucoinPositionRiskAdjustResultUpdate>>? onRiskAdjustUpdate = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/private-channels/trade-orders" /></para>
        /// </summary>
        /// <param name="symbol">[Optional] Symbol, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol,
            Action<DataEvent<KucoinStreamFuturesOrderUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to stop order updates
        /// <para><a href="https://www.kucoin.com/docs/websocket/futures-trading/private-channels/stop-order-lifecycle-event" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamFuturesStopOrderUpdate>> onData, CancellationToken ct = default);
    }
}
