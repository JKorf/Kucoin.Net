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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="quoteQuantity">The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order-sync" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="quoteQuantity">The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/add-order-test" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="quoteQuantity">The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/modify-order" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order to modify</param>
        /// <param name="clientOrderId">The client id of the order to modify</param>
        /// <param name="newQuantity">New order quantity</param>
        /// <param name="newPrice">New order price</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/batch-add-orders" /></para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CallResult<KucoinBulkMinimalResponseEntry>[]>> PlaceMultipleOrdersAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders and wait for and return the full order results. This is the slower version of <see cref="PlaceMultipleOrdersAsync" />
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/batch-add-orders-sync" /></para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CallResult<KucoinHfBulkOrderResponse>[]>> PlaceMultipleOrdersWaitAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order and only wait for confirmation. This is the faster version of <see cref="CancelOrderWaitAsync" />
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-orderld" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        Task<WebCallResult<KucoinOrderId>> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order and wait for and return the full order results. This is the slower version of <see cref="CancelOrderAsync" />
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-orderld-sync" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        Task<WebCallResult<KucoinHfOrder>> CancelOrderWaitAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by clientOrderId and only wait for confirmation. This is the faster version of <see cref="CancelOrderWaitAsync" />
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinClientOrderId>> CancelOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by clientOrderId and wait for and return the full order results. This is the slower version of <see cref="CancelOrderByClientOrderIdAsync" />
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-order-by-clientoid-sync" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrder>> CancelOrderByClientOrderIdWaitAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-order-by-orderld" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order by clientOrderId
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">The clientOrderId of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-all-orders-by-symbol" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllOrdersBySymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on all symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/cancel-all-orders" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledSymbols>> CancelAllOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED, use <see cref="GetOpenOrdersV2Async(string, int?, int?, CancellationToken)"/> instead
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open orders page
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-open-orders-by-page" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size, max 50</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinHfOrderDetails>>> GetOpenOrdersV2Async(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of symbols which have open orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-symbols-with-open-order" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOpenOrderSymbols>> GetSymbolsWithOpenOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of closed orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-closed-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="lastId">Last id of previous result</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-trade-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="tradeType">Filter by trade type</param>
        /// <param name="lastId">Last id of previous result</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetUserTradesAsync(string symbol, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders after a certain period. Calling this endpoint again will reset the timer. Using TimeSpan.Zero will disable the timer
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/set-dcp" /></para>
        /// </summary>
        /// <param name="cancelAfter">Cancel after this period</param>
        /// <param name="symbols">Symbols to cancel orders on, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCancelAfter>> CancelAfterAsync(TimeSpan cancelAfter, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get cancel after status
        /// <para><a href="https://www.kucoin.com/docs-new/rest/spot-trading/orders/get-dcp" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCancelAfterStatus?>> GetCancelAfterStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a new Margin Order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/add-order" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="side">The side((buy or sell) of the order</param>
        /// <param name="symbol">The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="type">The type of the order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="isIsolated">Is isolated margin</param>
        /// <param name="autoBorrow">Auto-borrow to place order.</param>
        /// <param name="autoRepay">Auto-repay to place order.</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">Quantity of base asset to buy or sell of the order</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="quoteQuantity">The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/add-order-test" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="side">The side((buy or sell) of the order</param>
        /// <param name="symbol">The symbol the order is for, for example `ETH-USDT`</param>
        /// <param name="type">The type of the order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="isIsolated">Is isolated margin</param>
        /// <param name="autoBorrow">Auto-borrow to place order.</param>
        /// <param name="autoRepay">Auto-repay to place order.</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">Quantity of base asset to buy or sell of the order</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="quoteQuantity">The quote quantity to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
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
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-order-by-orderld" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderId>> CancelMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a margin order by clientOrderId
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinClientOrderId>> CancelMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all margin orders on a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/cancel-all-orders-by-symbol" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllMarginOrdersBySymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open margin orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-open-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="type">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenMarginOrdersAsync(string symbol, TradeType type, CancellationToken ct = default);

        /// <summary>
        /// Get closed margin orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-closed-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="tradeType">Filter by trade type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="lastId">Last id of previous result</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedMarginOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a margin order by order id
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-order-by-orderld" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get a margin order by clientOrderId
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get list of margin trades
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-trade-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="tradeType">Filter by trade type</param>
        /// <param name="lastId">Last id of previous result</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetMarginUserTradesAsync(string symbol, TradeType tradeType, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, long? lastId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get symbols with active margin orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/orders/get-symbols-with-open-order" /></para>
        /// </summary>
        /// <param name="isolated">true for isolated margin, false for cross margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginOpenOrderSymbols>> GetMarginSymbolsWithOpenOrdersAsync(bool isolated, CancellationToken ct = default);
    }
}
