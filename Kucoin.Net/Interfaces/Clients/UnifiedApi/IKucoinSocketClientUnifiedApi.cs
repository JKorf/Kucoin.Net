using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.UnifiedApi;
using Kucoin.Net.Objects.Models.Unified;

namespace Kucoin.Net.Interfaces.Clients.UnifiedApi
{
    /// <summary>
    /// Unified socket api
    /// </summary>
    public interface IKucoinSocketClientUnifiedApi : ISocketApiClient<KucoinCredentials>, IDisposable
    {
        /// <summary>
        /// Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        IKucoinSocketClientUnifiedSharedApi SharedApi { get; }

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470344w0" /><br />
        /// </para>
        /// </summary>
        /// <param name="type">["<c>tradeType</c>"] Type of trade</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>orderType</c>"] Type of order</param>
        /// <param name="quantity">["<c>size</c>"] Quantity</param>
        /// <param name="quantityUnit">["<c>sizeUnit</c>"] Unit used for the quantity</param>
        /// <param name="price">["<c>price</c>"] Order limit price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Post only order</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only order</param>
        /// <param name="stpMode">["<c>stp</c>"] Self trade prevention mode</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="triggerDirection">["<c>triggerDirection</c>"] Trigger direction</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after in seconds</param>
        /// <param name="autoBorrow">["<c>autoBorrow</c>"] Enable auto borrow (Classic account)</param>
        /// <param name="autoRepay">["<c>autoRepay</c>"] Enable auto repay (Classic account)</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side (Classic account)</param>
        /// <param name="marginMode">["<c>marginMode</c>"] Margin mode (Classic account)</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage (Classic account)</param>
        /// <param name="tpTriggerPriceType">["<c>tpTriggerPriceType</c>"] Take profit trigger price type</param>
        /// <param name="tpTriggerPrice">["<c>tpTriggerPrice</c>"] Take profit trigger price</param>
        /// <param name="slTriggerPriceType">["<c>slTriggerPriceType</c>"] Stop loss trigger price type</param>
        /// <param name="slTriggerPrice">["<c>slTriggerPrice</c>"] Stop loss trigger price</param>
        /// <param name="closeOrder">["<c>closeOrder</c>"] Close order</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult<KucoinUaOrderResult>> PlaceOrderAsync(
            UnifiedSimpleAccountType type,
            string symbol,
            OrderSide side,
            OrderType orderType,
            decimal quantity,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            QuantityUnit? quantityUnit = null,
            string? clientOrderId = null,
            bool? postOnly = null,
            bool? reduceOnly = null,
            SelfTradePrevention? stpMode = null,
            long? cancelAfter = null,
            decimal? triggerPrice = null,
            StopType? triggerDirection = null,
            StopPriceType? triggerPriceType = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            PositionSide? positionSide = null,
            MarginMode? marginMode = null,
            decimal? leverage = null,
            StopPriceType? tpTriggerPriceType = null,
            decimal? tpTriggerPrice = null,
            StopPriceType? slTriggerPriceType = null,
            decimal? slTriggerPrice = null,
            bool? closeOrder = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470409w0" /><br />
        /// </para>
        /// </summary>
        /// <param name="orderId">Find order by orderId, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Find order by clientOrderId, either this or orderId should be provided</param>
        /// <param name="symbol">The symbol, for example `ETHUSDTM`</param>
        /// <param name="quantity">The new quantity for the order</param>
        /// <param name="price">The new price for the order</param>
        /// <param name="quantityUnit">The unit of the quantity</param>
        /// <param name="cxlOnFail">Whether to cancel the order if the amendment fails</param>
        /// <param name="tpTriggerPriceType">The trigger price type for take profit</param>
        /// <param name="tpTriggerPrice">The trigger price for take profit</param>
        /// <param name="slTriggerPriceType">The trigger price type for stop loss</param>
        /// <param name="slTriggerPrice">The trigger price for stop loss</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<QueryResult<KucoinUaOrderEditResult>> EditOrderAsync(
            string? orderId,
            string? clientOrderId,
            string symbol,
            decimal? quantity = null,
            decimal? price = null,
            QuantityUnit? quantityUnit = null,
            bool? cxlOnFail = null,
            StopPriceType? tpTriggerPriceType = null,
            decimal? tpTriggerPrice = null,
            StopPriceType? slTriggerPriceType = null,
            decimal? slTriggerPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470345w0" /><br />
        /// </para>
        /// </summary>
        /// <param name="type">["<c>tradeType</c>"] Type of trade</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`, not required from Unified account Futures order</param>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<QueryResult<KucoinUaOrderResult>> CancelOrderAsync(
            UnifiedSimpleAccountType type,
            string? symbol = null,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(UnifiedAccountType tradeType, string symbol, Action<DataEvent<KucoinUaTickerUpdate>> onData, CancellationToken ct = default);

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
        /// <param name="symbols">The symbols to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(UnifiedAccountType tradeType, IEnumerable<string> symbols, Action<DataEvent<KucoinUaTickerUpdate>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            UnifiedAccountType tradeType, string symbol,
            KlineInterval interval,
            Action<DataEvent<KucoinUaKlineUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            OrderBookDepth depth,
            Action<DataEvent<KucoinUaOrderBookUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            UnifiedAccountType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTradeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding fee updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470224w0" /><br />
        /// Endpoint:<br />
        /// Channel: funding-fee
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaFundingFeeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding fee updates for symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470224w0" /><br />
        /// Endpoint:<br />
        /// Channel: funding-fee
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `ETHUSDTM`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            IEnumerable<string> symbols,
            Action<DataEvent<KucoinUaFundingFeeUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding fee updates for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470412w0" /><br />
        /// Endpoint:<br />
        /// Channel: funding-fee-all-symbols
        /// </para>
        /// </summary>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            Action<DataEvent<KucoinUaFundingFeeUpdate[]>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470272w0" /><br />
        /// Endpoint:<br />
        /// Channel: mark-price
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaMarkPriceUpdate>> onData,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to call auction info updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3470268w0" /><br />
        /// Endpoint:<br />
        /// Channel: callAuctionInfo
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETH-USDT`</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaCallAuctionInfoUpdate>> onData,
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToLiteUserTradeUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToLeverageUpdatesAsync(
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
        Task<WebSocketResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(
            UnifiedAccountType tradeType,
            Action<DataEvent<KucoinUaLiquidationWarningUpdate>> onData,
            CancellationToken ct = default);
    }
}