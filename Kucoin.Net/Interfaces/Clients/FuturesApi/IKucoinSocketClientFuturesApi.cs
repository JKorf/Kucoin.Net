using CryptoExchange.Net.Objects;
using System;
using System.Threading.Tasks;
using System.Threading;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot.Socket;
using System.Collections.Generic;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Interfaces.Clients;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Futures socket api
    /// </summary>
    public interface IKucoinSocketClientFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IKucoinSocketClientFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470084w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/execution
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470084w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/execution
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470086w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/limitCandle
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example 'XBTUSDTM'</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470086w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/limitCandle
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example 'XBTUSDTM'</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamFuturesKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470080w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/tickerV2
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470080w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/tickerV2
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbol to subscribe on, for example `XBTUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamFuturesTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470082w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/level2
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470082w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/level2
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinFuturesOrderBookChange>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470083w0" /><br />
        /// <a href="https://www.kucoin.com/docs-new/3470097w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/level2Depth{limit}
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to partial order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470083w0" /><br />
        /// <a href="https://www.kucoin.com/docs-new/3470097w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/level2Depth{limit}
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470087w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/instrument
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to market data updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470087w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/instrument
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `XBTUSDTM`</param>
        /// <param name="onMarkIndexPriceUpdate">Mark/Index price update handler</param>
        /// <param name="onFundingRateUpdate">Funding price update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<KucoinStreamFuturesMarkIndexPrice>> onMarkIndexPriceUpdate,
            Action<DataEvent<KucoinStreamFuturesFundingRate>> onFundingRateUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe funding fee announcement
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470088w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/announcement
        /// </para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingFeeSettlementUpdatesAsync(Action<DataEvent<KucoinContractAnnouncement>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/transaction-statistics-timer-event" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/snapshot
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs/websocket/futures-trading/public-channels/transaction-statistics-timer-event" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/snapshot
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol, for example `XBTUSDTM`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeTo24HourSnapshotUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTransactionStatisticsUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to wallet updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470092w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractAccount/wallet
        /// </para>
        /// </summary>
        /// <param name="onOrderMarginUpdate">DEPRECATED; After the user 【First time】switches the margin mode (switching from isolated margin to cross margin), this will stop pushing and instead the onWalletUpdate event will be pushed</param>
        /// <param name="onBalanceUpdate">DEPRECATED; After the user 【First time】switches the margin mode (switching from isolated margin to cross margin), this will stop pushing and instead the onWalletUpdate event will be pushed</param>
        /// <param name="onWithdrawableUpdate">DEPRECATED; After the user 【First time】switches the margin mode (switching from isolated margin to cross margin), this will stop pushing and instead the onWalletUpdate event will be pushed</param>
        /// <param name="onWalletUpdate">Data handler for wallet update. Will be pushed once user switches margin modes for the first time</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderMarginUpdate>> onOrderMarginUpdate,
            Action<DataEvent<KucoinStreamFuturesBalanceUpdate>> onBalanceUpdate,
            Action<DataEvent<KucoinStreamFuturesWithdrawableUpdate>> onWithdrawableUpdate,
            Action<DataEvent<KucoinStreamFuturesWalletUpdate>> onWalletUpdate,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates for a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470093w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/position
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470093w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/positionAll
        /// </para>
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
        /// Subscribe to margin mode updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470095w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/marginMode
        /// </para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginModeUpdatesAsync(Action<DataEvent<Dictionary<string, FuturesMarginMode>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin leverage updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470096w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contract/crossLeverage
        /// </para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginLeverageUpdatesAsync(Action<DataEvent<Dictionary<string, KucoinLeverageUpdate>>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470090w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/tradeOrders
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470091w0" /><br />
        /// Endpoint:<br />
        /// Channel: /contractMarket/advancedOrders
        /// </para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamFuturesStopOrderUpdate>> onData, CancellationToken ct = default);
    }
}
