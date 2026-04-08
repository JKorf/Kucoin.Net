using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Unified;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Unified socket api
    /// </summary>
    public interface IKucoinSocketClientUnifiedApi : ISocketApiClient<KucoinCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470222w0" /><br />
        /// Endpoint:<br />
        /// Channel: ticker
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(UnifiedAccountType tradeType, string symbol, Action<DataEvent<KucoinUaTickerUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470222w0" /><br />
        /// Endpoint:<br />
        /// Channel: kline
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            UnifiedAccountType tradeType, string symbol,
            KlineInterval interval,
            Action<DataEvent<KucoinUaKlineUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470221w0" /><br />
        /// Endpoint:<br />
        /// Channel: obu
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="depth">Depth type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            OrderBookDepth depth,
            Action<DataEvent<KucoinUaOrderBookUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for a symbol ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470224w0" /><br />
        /// Endpoint:<br />
        /// Channel: trade
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTradeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470231w0" /><br />
        /// Endpoint:<br />
        /// Channel: balance
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaBalanceUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470228w0" /><br />
        /// Endpoint:<br />
        /// Channel: orderAll
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaOrderUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470232w0" /><br />
        /// Endpoint:<br />
        /// Channel: execution
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaUserTradeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to lite user trade updates. This update has lower latency than <see cref="SubscribeToUserTradeUpdatesAsync"/> but excludes fee info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470264w0" /><br />
        /// Endpoint:<br />
        /// Channel: execution.lite
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiteUserTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLiteUserTradeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470233w0" /><br />
        /// Endpoint:<br />
        /// Channel: positionAll
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaPositionUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to leverage change updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470237w0" /><br />
        /// Endpoint:<br />
        /// Channel: leverage
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLeverageUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLeverageUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to liquidation warnings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470236w0" /><br />
        /// Endpoint:<br />
        /// Channel: lw
        /// </para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLiquidationWarningUpdate>> onData,
            CancellationToken ct = default);
    }
}