using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Unified API trading endpoints, placing and managing orders.
    /// </summary>
    public interface IKucoinRestClientUnifiedApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/place-order" /></para>
        /// </summary>
        /// <param name="accountMode">Mode of the account</param>
        /// <param name="accountType">Type of trade</param>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="orderType">Type of order</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="quantityUnit">Unit used for the quantity</param>
        /// <param name="price">Order limit price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="postOnly">Post only order</param>
        /// <param name="reduceOnly">Reduce only order</param>
        /// <param name="stpMode">Self trade prevention mode</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="triggerDirection">Trigger direction</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="cancelAfter">Cancel after in seconds</param>
        /// <param name="autoBorrow">Enable auto borrow (Classic account)</param>
        /// <param name="autoRepay">Enable auto repay (Classic account)</param>
        /// <param name="positionSide">Position side (Classic account)</param>
        /// <param name="marginMode">Margin mode (Classic account)</param>
        /// <param name="leverage">Leverage (Classic account)</param>
        /// <param name="tpTriggerPriceType">Take profit trigger price type</param>
        /// <param name="tpTriggerPrice">Take profit trigger price</param>
        /// <param name="slTriggerPriceType">Stop loss trigger price type</param>
        /// <param name="slTriggerPrice">Stop loss trigger price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderResult>> PlaceOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
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
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/cancel-order" /></para>
        /// </summary>
        /// <param name="accountMode">Mode of the account</param>
        /// <param name="accountType">Type of trade</param>
        /// <param name="symbol">The symbol, for example `ETHUSDT`, not required from Unified account Futures order</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderResult>> CancelOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string? symbol = null, 
            string? orderId = null, 
            string? clientOrderId = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/batch-cancel-order-by-id" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="orders">Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBatchCancelResult>> CancelOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            IEnumerable<KucoinUaCancelOrderRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel orders on a specific symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/batch-cancel-order-by-id" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="orderFilter">Order filter, defaults to Normal</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBatchCancelResult>> CancelSymbolOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string symbol,
            MarginMode? marginMode = null,
            OrderFilter? orderFilter = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order info by id
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-order-details" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrder>> GetOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-open-order-list" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="symbol">Filter by symbol, for example `ETH-USDT`, required for Classic account spot/margin</param>
        /// <param name="orderFilter">Filter by order type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrders>> GetOpenOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null,
            OrderFilter? orderFilter = null,
            DateTime? startTime = null, 
            DateTime? endTime = null,
            int? page = null, 
            int? pageSize = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-order-history" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`, required for spot/margin</param>
        /// <param name="side">Filter by side</param>
        /// <param name="orderFilter">Filter by order type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="lastId">Filter by last id</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderHistory>> GetOrderHistoryAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null, 
            OrderSide? side = null,
            OrderFilter? orderFilter = null, 
            DateTime? startTime = null,
            DateTime? endTime = null,
            long? lastId = null,
            int? pageSize = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-trade-history" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">Account type</param>
        /// <param name="symbol">Filter by symbol, for example `ETH-USDT`, required for spot/margin</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="lastId">Filter by last id</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaUserTrades>> GetUserTradesAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null,
            long? orderId = null,
            OrderSide? orderSide = null, 
            DateTime? startTime = null,
            DateTime? endTime = null, 
            long? lastId = null, 
            int? pageSize = null, 
            CancellationToken ct = default);

        /// <summary>
        /// Set disconnect protection
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/set-dcp-classic" /></para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="timeout">Timeout in seconds</param>
        /// <param name="symbols">Set for specific symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDcp>> SetDcpAsync(UnifiedSimpleAccountType tradeType, long timeout, string? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get disconnection protection status
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-dcp-classic" /></para>
        /// </summary>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDcp>> GetDcpAsync(UnifiedSimpleAccountType tradeType, CancellationToken ct = default);

        /// <summary>
        /// Get open positions
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-position-list-uta" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPosition[]>> GetPositionsAsync(UnifiedAccountMode accountMode, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-position-history-uta" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="lastId">Filter by last id</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPositionHistory>> GetPositionHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            long? lastId = null,
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get position tiers
        /// <para><a href="https://www.kucoin.com/docs-new/rest/ua/get-account-position-tiers" /></para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="symbols">Symbols, for example `ETHUSDTM`</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPositionTier[]>> GetPositionTiersAsync(
            UnifiedAccountMode accountMode,
            IEnumerable<string> symbols,
            UnifiedSimpleAccountType? tradeType = null, 
            MarginMode? marginMode = null,
            CancellationToken ct = default);

    }
}
