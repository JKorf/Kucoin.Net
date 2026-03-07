using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Spot trading high frequency endpoints, placing and mananging orders.
    /// </summary>
    public interface IKucoinRestClientSpotApiHfTrading
    {
        /// <summary>
        /// Places an order and returns once the order is confirmed. This is the faster version of <see cref="PlaceOrderWaitAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] The side of the order</param>
        /// <param name="type">["<c>type</c>"] The type of the order</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">["<c>size</c>"] The quantity of the order</param>
        /// <param name="quoteQuantity">["<c>funds</c>"] The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] The time the order is in force</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after a time</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Order is post only</param>
        /// <param name="hidden">["<c>hidden</c>"] Order is hidden</param>
        /// <param name="iceBerg">["<c>iceBerg</c>"] Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">["<c>visibleSize</c>"] The maximum visible size of an iceberg order</param>
        /// <param name="remark">["<c>remark</c>"] Remark on the order</param>
        /// <param name="selfTradePrevention">["<c>stp</c>"] Self trade prevention setting</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinOrderId>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places an order and wait for and return the full order result. This is the slower version of <see cref="PlaceOrderAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order-sync" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/sync
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] The side of the order</param>
        /// <param name="type">["<c>type</c>"] The type of the order</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">["<c>size</c>"] The quantity of the order</param>
        /// <param name="quoteQuantity">["<c>funds</c>"] The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] The time the order is in force</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after a time</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Order is post only</param>
        /// <param name="hidden">["<c>hidden</c>"] Order is hidden</param>
        /// <param name="iceBerg">["<c>iceBerg</c>"] Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">["<c>visibleSize</c>"] The maximum visible size of an iceberg order</param>
        /// <param name="remark">["<c>remark</c>"] Remark on the order</param>
        /// <param name="selfTradePrevention">["<c>stp</c>"] Self trade prevention setting</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinHfOrder>> PlaceOrderWaitAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new test order. Only validates the parameters, but doesn't actually process the order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order-test" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/test
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] The side of the order</param>
        /// <param name="type">["<c>type</c>"] The type of the order</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">["<c>size</c>"] The quantity of the order</param>
        /// <param name="quoteQuantity">["<c>funds</c>"] The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] The time the order is in force</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after a time</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Order is post only</param>
        /// <param name="hidden">["<c>hidden</c>"] Order is hidden</param>
        /// <param name="iceBerg">["<c>iceBerg</c>"] Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">["<c>visibleSize</c>"] The maximum visible size of an iceberg order</param>
        /// <param name="remark">["<c>remark</c>"] Remark on the order</param>
        /// <param name="selfTradePrevention">["<c>stp</c>"] Self trade prevention setting</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderId>> PlaceTestOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);

        /// <summary>
        /// Modify an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/modify-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/alter
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The id of the order to modify</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] The client id of the order to modify</param>
        /// <param name="newQuantity">["<c>newSize</c>"] New order quantity</param>
        /// <param name="newPrice">["<c>newPrice</c>"] New order price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the modified order</returns>
        Task<WebCallResult<KucoinModifiedOrder>> EditOrderAsync(
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            decimal? newQuantity = null,
            decimal? newPrice = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders and only wait for confirmation. This is the faster version of <see cref="PlaceMultipleOrdersWaitAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/batch-add-orders" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/multi
        /// </para>
        /// </summary>
        /// <param name="orders">["<c>orderList</c>"] Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CallResult<KucoinBulkMinimalResponseEntry>[]>> PlaceMultipleOrdersAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders and wait for and return the full order results. This is the slower version of <see cref="PlaceMultipleOrdersAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/batch-add-orders-sync" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/multi/sync
        /// </para>
        /// </summary>
        /// <param name="orders">["<c>orderList</c>"] Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CallResult<KucoinHfBulkOrderResponse>[]>> PlaceMultipleOrdersWaitAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order and only wait for confirmation. This is the faster version of <see cref="CancelOrderWaitAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-orderld" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        Task<WebCallResult<KucoinOrderId>> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order and wait for and return the full order results. This is the slower version of <see cref="CancelOrderAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-orderld-sync" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders/sync/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        Task<WebCallResult<KucoinHfOrder>> CancelOrderWaitAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by clientOrderId and only wait for confirmation. This is the faster version of <see cref="CancelOrderWaitAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-clientoid" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders/client-order/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinClientOrderId>> CancelOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by clientOrderId and wait for and return the full order results. This is the slower version of <see cref="CancelOrderByClientOrderIdAsync" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-clientoid-sync" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders/sync/client-order/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrder>> CancelOrderByClientOrderIdWaitAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-order-by-orderld" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order by clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-order-by-clientoid" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/client-order/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">The clientOrderId of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-all-orders-by-symbol" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllOrdersBySymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-all-orders" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/hf/orders/cancelAll
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledSymbols>> CancelAllOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED, use <see cref="GetOpenOrdersV2Async(string, int?, int?, CancellationToken)"/> instead
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-open-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/active
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open orders page
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-open-orders-by-page" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/active/page
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="page">["<c>pageNum</c>"] Page number</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size, max 50</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinHfOrderDetails>>> GetOpenOrdersV2Async(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of symbols which have open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-symbols-with-open-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/active/symbols
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOpenOrderSymbols>> GetSymbolsWithOpenOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-closed-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/done
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Last id of previous result</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-trade-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/fills
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="tradeType">Filter by trade type</param>
        /// <param name="lastId">["<c>lastId</c>"] Last id of previous result</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetUserTradesAsync(string symbol, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders after a certain period. Calling this endpoint again will reset the timer. Using TimeSpan.Zero will disable the timer
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/set-dcp" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/hf/orders/dead-cancel-all
        /// </para>
        /// </summary>
        /// <param name="cancelAfter">["<c>timeout</c>"] Cancel after this period</param>
        /// <param name="symbols">["<c>symbols</c>"] Symbols to cancel orders on, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCancelAfter>> CancelAfterAsync(TimeSpan cancelAfter, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get cancel after status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-dcp" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/hf/orders/dead-cancel-all/query
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCancelAfterStatus?>> GetCancelAfterStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a new Margin Order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/add-order" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/hf/margin/order
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="side">["<c>side</c>"] The side((buy or sell) of the order</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="type">["<c>type</c>"] The type of the order</param>
        /// <param name="remark">["<c>remark</c>"] Remark on the order</param>
        /// <param name="selfTradePrevention">["<c>stp</c>"] Self trade prevention setting</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Is isolated margin</param>
        /// <param name="autoBorrow">["<c>autoBorrow</c>"] Auto-borrow to place order.</param>
        /// <param name="autoRepay">["<c>autoRepay</c>"] Auto-repay to place order.</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">["<c>size</c>"] Quantity of base asset to buy or sell of the order</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] The time the order is in force</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after a time</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Order is post only</param>
        /// <param name="hidden">["<c>hidden</c>"] Order is hidden</param>
        /// <param name="iceBerg">["<c>iceBerg</c>"] Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">["<c>visibleSize</c>"] The maximum visible size of an iceberg order</param>
        /// <param name="quoteQuantity">["<c>funds</c>"] The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewMarginOrder>> PlaceMarginOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            bool? isIsolated = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new test Margin Order. Will only validate the parameters but not actually process the order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/add-order-test" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/hf/margin/order/test
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client order id</param>
        /// <param name="side">["<c>side</c>"] The side((buy or sell) of the order</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="type">["<c>type</c>"] The type of the order</param>
        /// <param name="remark">["<c>remark</c>"] Remark on the order</param>
        /// <param name="selfTradePrevention">["<c>stp</c>"] Self trade prevention setting</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Is isolated margin</param>
        /// <param name="autoBorrow">["<c>autoBorrow</c>"] Auto-borrow to place order.</param>
        /// <param name="autoRepay">["<c>autoRepay</c>"] Auto-repay to place order.</param>
        /// <param name="price">["<c>price</c>"] The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">["<c>size</c>"] Quantity of base asset to buy or sell of the order</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] The time the order is in force</param>
        /// <param name="cancelAfter">["<c>cancelAfter</c>"] Cancel after a time</param>
        /// <param name="postOnly">["<c>postOnly</c>"] Order is post only</param>
        /// <param name="hidden">["<c>hidden</c>"] Order is hidden</param>
        /// <param name="iceBerg">["<c>iceBerg</c>"] Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">["<c>visibleSize</c>"] The maximum visible size of an iceberg order</param>
        /// <param name="quoteQuantity">["<c>funds</c>"] The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewMarginOrder>> PlaceTestMarginOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            bool? isIsolated = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-order-by-orderld" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/hf/margin/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderId>> CancelMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a margin order by clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-order-by-clientoid" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/hf/margin/orders/client-order/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinClientOrderId>> CancelMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all margin orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-all-orders-by-symbol" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/hf/margin/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllMarginOrdersBySymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-open-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/orders/active
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="type">["<c>tradeType</c>"] Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenMarginOrdersAsync(string symbol, TradeType type, CancellationToken ct = default);

        /// <summary>
        /// Get closed margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-closed-orders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/orders/done
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="tradeType">["<c>tradeType</c>"] Filter by trade type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Last id of previous result</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedMarginOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a margin order by order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-order-by-orderld" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/orders/{orderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get a margin order by clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-order-by-clientoid" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/orders/client-order/{clientOrderId}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get list of margin trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-trade-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/fills
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="tradeType">["<c>tradeType</c>"] Filter by trade type</param>
        /// <param name="lastId">["<c>lastId</c>"] Last id of previous result</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetMarginUserTradesAsync(string symbol, TradeType tradeType, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get symbols with active margin orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-symbols-with-open-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/hf/margin/order/active/symbols
        /// </para>
        /// </summary>
        /// <param name="isolated">["<c>tradeType</c>"] true for isolated margin, false for cross margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginOpenOrderSymbols>> GetMarginSymbolsWithOpenOrdersAsync(bool isolated, CancellationToken ct = default);
    }
}
