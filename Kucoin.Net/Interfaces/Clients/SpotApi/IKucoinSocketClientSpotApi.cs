using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot socket api
    /// </summary>
    public interface IKucoinSocketClientSpotApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IKucoinSocketClientSpotApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para><a href="https://www.kucoin.com/docs-new/3470063w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para><a href="https://www.kucoin.com/docs-new/3470063w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for all symbol tickers
        /// <para><a href="https://www.kucoin.com/docs-new/3470064w0" /></para>
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<KucoinStreamTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// <para><a href="https://www.kucoin.com/docs-new/3470066w0" /></para>
        /// </summary>
        /// <param name="symbolOrMarket">The symbol (ie KCS-BTC) or market (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(string symbolOrMarket,
            Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for symbol or market snapshots
        /// <para><a href="https://www.kucoin.com/docs-new/3470066w0" /></para>
        /// </summary>
        /// <param name="symbolOrMarkets">The symbols (ie KCS-BTC) or markets (ie BTC) to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSnapshotUpdatesAsync(
            IEnumerable<string> symbolOrMarkets, Action<DataEvent<KucoinStreamSnapshot>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470068w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470068w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470072w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470072w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamMatch>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470071w0" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example `ETH-USDT`</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470071w0" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<KucoinStreamCandle>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best ask/bid) updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470067w0" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamBestOffers>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker (best ask/bid) updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470067w0" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamBestOffers>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470069w0" /></para>
        /// <para><a href="https://www.kucoin.com/docs-new/3470070w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `ETH-USDT`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int limit,
            Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to full order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470069w0" /></para>
        /// <para><a href="https://www.kucoin.com/docs-new/3470070w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="limit">The amount of levels to receive, either 5 or 50</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int limit, Action<DataEvent<KucoinStreamOrderBookChanged>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates for your own orders 
        /// <para><a href="https://www.kucoin.com/docs-new/3470073w0" /></para>
        /// </summary>
        /// <param name="onNewOrder">Data handler for new order updates</param>
        /// <param name="onOrderData">Data handler for order updates</param>
        /// <param name="onTradeData">Data handler for trade updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            Action<DataEvent<KucoinStreamOrderNewUpdate>>? onNewOrder = null,
            Action<DataEvent<KucoinStreamOrderUpdate>>? onOrderData = null,
            Action<DataEvent<KucoinStreamOrderMatchUpdate>>? onTradeData = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470075w0" /></para>
        /// </summary>
        /// <param name="onBalanceChange">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<KucoinBalanceUpdate>> onBalanceChange, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for stop orders
        /// <para><a href="https://www.kucoin.com/docs/websocket/spot-trading/private-channels/stop-order-event" /></para>
        /// </summary>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToStopOrderUpdatesAsync(Action<DataEvent<KucoinStreamStopOrderUpdateBase>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470076w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `USDT-BTC`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470076w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `USDT-BTC`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470077w0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `USDT-BTC`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470077w0" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe, for example `USDT-BTC`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamIndicatorPrice>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to call auction order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470137w0" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionOrderBookUpdatesAsync(string symbol, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to call auction order book updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470137w0" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinStreamOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to call auction info updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470138w0" /></para>
        /// </summary>
        /// <param name="symbol">Symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(string symbol, Action<DataEvent<KucoinCallAuctionInfo>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to call auction info updates
        /// <para><a href="https://www.kucoin.com/docs-new/3470138w0" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `ETH-USDT`</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        Task<CallResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<KucoinCallAuctionInfo>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to cross margin position events
        /// <para><a href="https://www.kucoin.com/docs-new/3470078w0" /></para>
        /// </summary>
        /// <param name="onDebtRatioChange">Data handler for debt ratio change evens. The system will push the current debt message periodically when there is a liability.</param>
        /// <param name="onPositionStatusChange">Data handler for position status change events. The system will push the change event when the position status changes.</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginPositionUpdatesAsync(Action<DataEvent<KucoinMarginDebtRatioUpdate>> onDebtRatioChange, Action<DataEvent<KucoinMarginPositionStatusUpdate>> onPositionStatusChange, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to margin order updates for an asset
        /// <para><a href="https://www.kucoin.com/docs/websocket/margin-trading/private-channels/margin-trade-order-event" /></para>
        /// </summary>
        /// <param name="symbol">Asset, for example `ETH-USDT`</param>
        /// <param name="onOrderPlaced">Data handler for order placement updates</param>
        /// <param name="onOrderUpdate">Data handler for order updates</param>
        /// <param name="onOrderDone">Data handler for order done updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginOrderUpdatesAsync(string symbol, Action<DataEvent<KucoinMarginOrderUpdate>>? onOrderPlaced = null, Action<DataEvent<KucoinMarginOrderUpdate>>? onOrderUpdate = null, Action<DataEvent<KucoinMarginOrderDoneUpdate>>? onOrderDone = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to isolated margin order updates for a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/3470079w0" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onPositionChange">Position change update handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginPositionUpdatesAsync(string symbol, Action<DataEvent<KucoinIsolatedMarginPositionUpdate>> onPositionChange, CancellationToken ct = default);
    }
}