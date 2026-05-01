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
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/place-order" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/{accountMode}/order/place
        /// </para>
        /// </summary>
        /// <param name="accountMode">Mode of the account</param>
        /// <param name="accountType">["<c>tradeType</c>"] Type of trade</param>
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
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/cancel-order" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/{accountMode}/order/cancel
        /// </para>
        /// </summary>
        /// <param name="accountMode">Mode of the account</param>
        /// <param name="accountType">["<c>tradeType</c>"] Type of trade</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`, not required from Unified account Futures order</param>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderResult>> CancelOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string? symbol = null, 
            string? orderId = null, 
            string? clientOrderId = null, 
            CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Cancel multiple orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/batch-cancel-order-by-id" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/{accountMode}/order/cancel-batch
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="orders">["<c>cancelOrderList</c>"] Orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBatchCancelResult>> CancelOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            IEnumerable<KucoinUaCancelOrderRequest> orders,
            CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Cancel orders on a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/batch-cancel-order-by-id" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/{accountMode}/order/cancel-all
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="marginMode">["<c>marginMode</c>"] Margin mode</param>
        /// <param name="orderFilter">["<c>orderFilter</c>"] Order filter, defaults to Normal</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBatchCancelResult>> CancelSymbolOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string symbol,
            MarginMode? marginMode = null,
            OrderFilter? orderFilter = null,
            CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get order info by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-order-details" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/order/detail
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Order id. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrder>> GetOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-open-order-list" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/order/open-list
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH-USDT`, required for Classic account spot/margin</param>
        /// <param name="orderFilter">["<c>orderFilter</c>"] Filter by order type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="page">["<c>pageNumber</c>"] Page number</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
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
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-order-history" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/order/history
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH-USDT`, required for spot/margin</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="orderFilter">["<c>orderFilter</c>"] Filter by order type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Filter by last id</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
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
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-trade-history" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/order/execution
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="accountType">["<c>tradeType</c>"] Account type</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH-USDT`, required for spot/margin</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="orderSide">["<c>side</c>"] Filter by order side</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Filter by last id</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
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
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Set disconnect protection
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/set-dcp-classic" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/dcp/set
        /// </para>
        /// </summary>
        /// <param name="tradeType">["<c>tradeType</c>"] Trade type</param>
        /// <param name="timeout">["<c>timeout</c>"] Timeout in seconds</param>
        /// <param name="symbols">["<c>symbols</c>"] Set for specific symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDcp>> SetDcpAsync(UnifiedSimpleAccountType tradeType, long timeout, string? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get disconnection protection status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-dcp-classic" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/dcp/query
        /// </para>
        /// </summary>
        /// <param name="tradeType">["<c>tradeType</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDcp>> GetDcpAsync(UnifiedSimpleAccountType tradeType, CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-position-list-uta" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/position/open-list
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDTM`</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPosition[]>> GetPositionsAsync(UnifiedAccountMode accountMode, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get position history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-position-history-uta" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/position/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDTM`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Filter by last id</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPositionHistory>> GetPositionHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            long? lastId = null,
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// [Warning: UTA/Unified API is currently in BETA phase and should not be used in product]<br />
        /// Get position tiers
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-account-position-tiers" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/{accountMode}/position/tiers
        /// </para>
        /// </summary>
        /// <param name="accountMode">Account mode</param>
        /// <param name="symbols">["<c>symbol</c>"] Symbols, for example `ETHUSDTM`</param>
        /// <param name="tradeType">["<c>tradeType</c>"] Trade type</param>
        /// <param name="marginMode">["<c>marginMode</c>"] Margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaPositionTier[]>> GetPositionTiersAsync(
            UnifiedAccountMode accountMode,
            IEnumerable<string> symbols,
            UnifiedSimpleAccountType? tradeType = null, 
            MarginMode? marginMode = null,
            CancellationToken ct = default);

    }
}
