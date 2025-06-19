using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Kucoin Futures trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IKucoinRestClientFuturesApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/add-order" /></para>
        /// </summary>
        /// <param name="symbol">The contract for the order, for example `XBTUSDM`</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of order</param>
        /// <param name="leverage">Leverage of the order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="timeInForce">Time in force, only for limit orders</param>
        /// <param name="postOnly">Post only flag, invalid when timeInForce is IOC</param>
        /// <param name="hidden">Orders not displaying in order book. When hidden chose</param>
        /// <param name="iceberg">Only visible portion of the order is displayed in the order book</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="quantity">Quantity of contract to buy or sell, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="remark">Remark for the order</param>
        /// <param name="stopType"></param>
        /// <param name="stopPriceType"></param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="closeOrder">A mark to close the position. Set to false by default. All the positions will be closed if true</param>
        /// <param name="forceHold">A mark to forcefully hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default</param>
        /// <param name="selfTradePrevention">Self Trade Prevention mode</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="marginMode">Margin mode, defaults to Isolated</param>
        /// <param name="quantityInBaseAsset">Quantity specified in base asset, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="quantityInQuoteAsset">Quantity specified in quote asset, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        Task<WebCallResult<KucoinOrderId>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal? leverage = null,
            int? quantity = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,
            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            FuturesMarginMode? marginMode = null,
            decimal? quantityInBaseAsset = null,
            decimal? quantityInQuoteAsset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a test order. The order will not be executed or added to the order book, but can be used to verify the request parameters
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/add-order-test" /></para>
        /// </summary>
        /// <param name="symbol">The contract for the order, for example `XBTUSDM`</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of order</param>
        /// <param name="leverage">Leverage of the order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="timeInForce">Time in force, only for limit orders</param>
        /// <param name="postOnly">Post only flag, invalid when timeInForce is IOC</param>
        /// <param name="hidden">Orders not displaying in order book. When hidden chose</param>
        /// <param name="iceberg">Only visible portion of the order is displayed in the order book</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="quantity">Quantity of contract to buy or sell</param>
        /// <param name="remark">Remark for the order</param>
        /// <param name="stopType"></param>
        /// <param name="stopPriceType"></param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="closeOrder">A mark to close the position. Set to false by default. All the positions will be closed if true</param>
        /// <param name="forceHold">A mark to forcefully hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default</param>
        /// <param name="selfTradePrevention">Self Trade Prevention mode</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        Task<WebCallResult<KucoinOrderId>> PlaceTestOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal? leverage = null,
            int? quantity = null,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default);


        /// <summary>
        /// Place a new take profit / stop loss order. Note that both triggerStopUpPrice and triggerStopDownPrice should be provided or the order will execute immediately.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/add-take-profit-and-stop-loss-order" /></para>
        /// </summary>
        /// <param name="symbol">The contract for the order, for example `XBTUSDM`</param>
        /// <param name="side">Side of the order, not required when setting closeOrder to true</param>
        /// <param name="type">Type of order</param>
        /// <param name="leverage">Leverage of the order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="timeInForce">Time in force, only for limit orders</param>
        /// <param name="postOnly">Post only flag, invalid when timeInForce is IOC</param>
        /// <param name="hidden">Orders not displaying in order book. When hidden chose</param>
        /// <param name="iceberg">Only visible portion of the order is displayed in the order book</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="quantity">Quantity of contract to buy or sell, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="remark">Remark for the order</param>
        /// <param name="stopPriceType">Price type</param>
        /// <param name="triggerStopUpPrice">Up trigger price, take profit price for long positions, stop loss price for short positions</param>
        /// <param name="triggerStopDownPrice">Down trigger price, take profit price for short positions, stop loss price for long positions</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="closeOrder">A mark to close the position. Set to false by default. All the positions will be closed if true</param>
        /// <param name="forceHold">A mark to forcefully hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default</param>
        /// <param name="selfTradePrevention">Self Trade Prevention mode</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="quantityInBaseAsset">Quantity specified in base asset, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="quantityInQuoteAsset">Quantity specified in quote asset, one of `quantity`, `quantityInBaseAsset` or `quantityInQuoteAsset` should be provided</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        Task<WebCallResult<KucoinOrderId>> PlaceTpSlOrderAsync(
            string symbol,
            OrderSide? side,
            NewOrderType type,
            decimal? leverage = null,
            int? quantity = null,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            decimal? triggerStopUpPrice = null,
            decimal? triggerStopDownPrice = null,
            StopPriceType? stopPriceType = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            decimal? quantityInBaseAsset = null,
            decimal? quantityInQuoteAsset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/batch-add-orders" /></para>
        /// </summary>
        /// <param name="orders">The orders to place</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order results. Each result should be checked for success</returns>
        Task<WebCallResult<CallResult<KucoinFuturesOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<KucoinFuturesOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/cancel-order-by-orderld" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Canceled id</returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para><a href="https://www.kucoin.com/docs-new/3470241e0" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, required when specifying clientOrderIds</param>
        /// <param name="orderIds">Order ids to cancel</param>
        /// <param name="clientOrderIds">Client order ids to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesOrderResult[]>> CancelMultipleOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, IEnumerable<KucoinCancelRequest>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order by client order id
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/cancel-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="clientOrderId">Client order id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/cancel-all-orders" /></para>
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Canceled ids</returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open stop orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/cancel-all-stop-orders" /></para>
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Canceled ids</returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelAllStopOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/get-order-list" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of untriggered stop orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/get-stop-order-list" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of 1000 most recent orders in the last 24 hours
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/get-recent-closed-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinFuturesOrder[]>> GetClosedOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/orders/get-order-by-orderld" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/get-stop-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// <para><a href="https://www.kucoin.com/docs-new/est/futures-trading/orders/get-trade-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="tradeTypes">Filter by trade types</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, IEnumerable<FuturesTradeType>? tradeTypes = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of 1000 most recent user trades in the last 24 hours
        /// <para><a href="https://www.kucoin.com/docs-new/est/futures-trading/orders/get-recent-trade-history" /></para>
        /// </summary>        
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<KucoinFuturesUserTrade[]>> GetRecentUserTradesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the max position size
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-max-open-size" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="price">Price</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMaxOpenSize>> GetMaxOpenPositionSizeAsync(string symbol, decimal price, decimal leverage, CancellationToken ct = default);

    }
}
