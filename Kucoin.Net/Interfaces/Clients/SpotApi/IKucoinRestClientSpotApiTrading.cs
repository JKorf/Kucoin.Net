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
    /// Kucoin Spot trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IKucoinRestClientSpotApiTrading
    {
        /// <summary>
        /// Places an order
        /// <para><a href="https://docs.kucoin.com/#place-a-new-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
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
        Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
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
        /// Places a margin order
        /// <para><a href="https://docs.kucoin.com/#place-a-margin-order" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="side">The side((buy or sell) of the order</param>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="type">The type of the order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="marginMode">The type of trading, including 'cross' and 'isolated'</param>
        /// <param name="autoBorrow">Auto-borrow to place order.</param>
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
            OrderSide side,
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
            MarginMode? marginMode = null,
            bool? autoBorrow = null,
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places bulk orders
        /// <para><a href="https://docs.kucoin.com/#place-bulk-orders" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orders">Up to 5 orders to be placed at the same time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of new orders</returns>
        Task<WebCallResult<KucoinBulkOrderResponse>> PlaceBulkOrderAsync(string symbol, IEnumerable<KucoinBulkOrderRequestEntry> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://docs.kucoin.com/#cancel-an-order" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of canceled orders</returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://docs.kucoin.com/#cancel-single-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client order id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of canceled orders</returns>
        Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para><a href="https://docs.kucoin.com/#cancel-all-orders" /></para>
        /// </summary>
        /// <param name="symbol">Only cancel orders for this symbol</param>
        /// <param name="tradeType">Only cancel orders for this type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of canceled orders</returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, TradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of orders
        /// <para><a href="https://docs.kucoin.com/#list-orders" /></para>
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by order status. Defaults to done</param>
        /// <param name="tradeType">The type of orders to retrieve</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, OrderStatus? status = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of max 1000 orders in the last 24 hours
        /// <para><a href="https://docs.kucoin.com/#get-v1-historical-orders-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<KucoinOrder>>> GetRecentOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order
        /// <para><a href="https://docs.kucoin.com/#get-single-active-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client order id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        Task<WebCallResult<KucoinOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific order
        /// <para><a href="https://docs.kucoin.com/#get-an-order" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of historical orders
        /// <para><a href="https://docs.kucoin.com/#get-v1-historical-orders-list" /></para>
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>> GetHistoricalOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of fills
        /// <para><a href="https://docs.kucoin.com/#list-fills" /></para>
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderId">Filter list by order id</param>
        /// <param name="tradeType">The type of orders to retrieve</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of fills</returns>
        Task<WebCallResult<KucoinPaginated<KucoinUserTrade>>> GetUserTradesAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of max 1000 fills in the last 24 hours
        /// <para><a href="https://docs.kucoin.com/#recent-fills" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of fills</returns>
        Task<WebCallResult<IEnumerable<KucoinUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default);

        /// <summary>
        /// Place a new stop order
        /// <para><a href="https://docs.kucoin.com/#place-a-new-order-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderSide">The side of the order</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="quoteQuantity">The funds to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceberg">Order is an iceberg order</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="stopCondition">Stop price condition</param>
        /// <param name="stopPrice">Price to trigger the order placement</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinNewOrder>> PlaceStopOrderAsync(
            string symbol,
            OrderSide orderSide,
            NewOrderType orderType,
            StopCondition stopCondition,
            decimal stopPrice,
            string? remark = null,
            SelfTradePrevention? selfTradePrevention = null,
            TradeType? tradeType = null,
            decimal? price = null,
            decimal? quantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,
            string? clientOrderId = null,
            decimal? quoteQuantity = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel a stop order by order id
        /// <para><a href="https://docs.kucoin.com/#cancel-an-order-2" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel a stop order by client order id
        /// <para><a href="https://docs.kucoin.com/#cancel-single-order-by-clientoid-2" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledOrder>> CancelStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all stop orders fitting the provided parameters
        /// <para><a href="https://docs.kucoin.com/#cancel-orders" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to cancel orders on</param>
        /// <param name="orderIds">Order ids of the orders to cancel</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, TradeType? tradeType = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of stop orders fitting the provided parameters
        /// <para><a href="https://docs.kucoin.com/#list-stop-orders" /></para>
        /// </summary>
        /// <param name="activeOrders">True to return active orders, false for completed orders</param>
        /// <param name="symbol">Symbol of the orders</param>
        /// <param name="side">Side of the orders</param>
        /// <param name="type">Type of the orders</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderIds">Filter list by order ids</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinStopOrder>>> GetStopOrdersAsync(bool? activeOrders = null, string? symbol = null, OrderSide? side = null,
            OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, IEnumerable<string>? orderIds = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get a stop order by id
        /// <para><a href="https://docs.kucoin.com/#get-single-order-info" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinStopOrder>> GetStopOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get a stop order by client order id
        /// <para><a href="https://docs.kucoin.com/#get-single-order-by-clientoid" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinStopOrder>>> GetStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Places a Borrow order
        /// <para><a href="https://docs.kucoin.com/#post-borrow-order" /></para>
        /// </summary>
        /// <param name="asset">Currency to Borrow e.g USDT etc</param>
        /// <param name="type">The type of the order (FOK, IOC)</param>
        /// <param name="quantity">Total size</param>
        /// <param name="maxRate">The max interest rate. All interest rates are acceptable if this field is left empty</param>
        /// <param name="term">term (Unit: Day). All terms are acceptable if this field is left empty. Please note to separate the terms via comma. For example, 7,14,28</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewBorrowOrder>> PlaceBorrowOrderAsync(
           string asset,
           BorrowOrderType type,
           decimal quantity,
           decimal? maxRate = null,
           string? term = null,
           CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific borrow order
        /// <para><a href="https://docs.kucoin.com/#get-borrow-order" /></para>
        /// </summary>
        /// <param name="orderId">The order id of the borrow order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow Order info</returns>
        Task<WebCallResult<KucoinBorrowOrder>> GetBorrowOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Repay a Single Order
        /// <para><a href="https://docs.kucoin.com/#repay-a-single-order" /></para>
        /// </summary>
        /// <param name="asset">Asset to Pay e.g USDT etc</param>
        /// <param name="tradeId">Trade ID of borrow order</param>
        /// <param name="quantity">Repayment size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepaySingleBorrowOrderAsync(
            string asset,
            string tradeId,
            decimal quantity,
            CancellationToken ct = default);

        /// <summary>
        /// Get outstanding borrow records
        /// <para><a href="https://docs.kucoin.com/#get-repay-record" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinRepayRecord>>> GetOpenBorrowRecordsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get repaid borrow records
        /// <para><a href="https://docs.kucoin.com/#get-repayment-record" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinBorrowRecord>>> GetClosedBorrowRecordsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Repay all borrowed for an asset
        /// <para><a href="https://docs.kucoin.com/#one-click-repayment" /></para>
        /// </summary>
        /// <param name="asset">Asset to repay for</param>
        /// <param name="strategy">Repayment strategy</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepayAllAsync(string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Place a new lend order
        /// <para><a href="https://docs.kucoin.com/#post-lend-order" /></para>
        /// </summary>
        /// <param name="asset">Asset to lend</param>
        /// <param name="quantity">Quantity to lend</param>
        /// <param name="dailyInterestRate">Daily interest rate. 0.01 = 1%</param>
        /// <param name="term">The term in days</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinNewOrder>> PlaceLendOrderAsync(string asset, decimal quantity, decimal dailyInterestRate, int term, CancellationToken ct = default);

        /// <summary>
        /// Cancel an active lend order
        /// <para><a href="https://docs.kucoin.com/#cancel-lend-order" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelLendOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Set up automatic lending for an asset
        /// <para><a href="https://docs.kucoin.com/#set-auto-lend" /></para>
        /// </summary>
        /// <param name="asset">The asset to lend</param>
        /// <param name="isEnabled">Is enabled or not</param>
        /// <param name="retainQuantity">Reserved quantity in main account</param>
        /// <param name="dailyInterestRate">Daily interest rate. 0.01 = 1%</param>
        /// <param name="term">The term in days</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetAutoLendAsync(string asset, bool isEnabled, decimal? retainQuantity = null, decimal? dailyInterestRate = null, int? term = null, CancellationToken ct = default);

        /// <summary>
        /// Get open lend orders
        /// <para><a href="https://docs.kucoin.com/#get-active-order" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetOpenLendOrdersAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed lend orders
        /// <para><a href="https://docs.kucoin.com/#get-lent-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetClosedLendOrdersAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get active lends
        /// <para><a href="https://docs.kucoin.com/#get-active-lend-order-list" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinOpenLend>>> GetOpenLendsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get active lends
        /// <para><a href="https://docs.kucoin.com/#get-settled-lend-order-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinClosedLend>>> GetClosedLendsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending status per asset
        /// <para><a href="https://docs.kucoin.com/#get-account-lend-record" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinLendHistory>>> GetLendingStatusAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Places an isolated Borrow order
        /// <para><a href="https://docs.kucoin.com/#isolated-margin-borrowing" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">Currency to Borrow e.g USDT etc</param>
        /// <param name="quantity">Total size</param>
        /// <param name="type">The type of the order (FOK, IOC)</param>
        /// <param name="maxRate">The max interest rate. All interest rates are acceptable if this field is left empty</param>
        /// <param name="term">term (Unit: Day). All terms are acceptable if this field is left empty. Please note to separate the terms via comma. For example, 7,14,28</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewIsolatedBorrowOrder>> PlaceIsolatedBorrowOrderAsync(
           string symbol,
           string asset,
           decimal quantity,
           BorrowOrderType type,
           decimal? maxRate = null,
           string? term = null,
           CancellationToken ct = default);

        /// <summary>
        /// Get outstanding isolated borrow records
        /// <para><a href="https://docs.kucoin.com/#get-repay-record" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinIsolatedOpenBorrowRecord>>> GetIsolatedOpenBorrowRecordsAsync(string? symbol = null, string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get repaid isolated borrow records
        /// <para><a href="https://docs.kucoin.com/#query-repayment-records" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinIsolatedClosedBorrowRecord>>> GetIsolatedClosedBorrowRecordsAsync(string? symbol = null, string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Repay all isolated borrowed for an asset
        /// <para><a href="https://docs.kucoin.com/#quick-repayment" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to repay for</param>
        /// <param name="asset">Asset to repay for</param>
        /// <param name="strategy">Repayment strategy</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepayAllIsolatedAsync(string symbol, string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a Single Order
        /// <para><a href="https://docs.kucoin.com/#single-repayment" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to Pay e.g BTC-USDT etc</param>
        /// <param name="asset">Asset to Pay e.g USDT etc</param>
        /// <param name="loanId">Loan ID of borrow order</param>
        /// <param name="quantity">Repayment size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepaySingleIsolatedBorrowOrderAsync(string symbol, string asset, decimal quantity, string loanId, CancellationToken ct = default);
        Task<WebCallResult<KucoinNewBorrow>> MarginBorrowAsync(string asset, decimal quantity, BorrowOrderType type, bool? isIsolated, string? symbol, CancellationToken ct = default);
        Task<WebCallResult<KucoinNewRepay>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated, string? symbol = null, CancellationToken ct = default);
        Task<WebCallResult<KucoinPaginated<MarginBorrowHistory>>> MarginBorrowHistoryAsync(string asset, bool? isIsolated, string? symbol = null, string? Id = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        Task<WebCallResult<KucoinPaginated<MarginRepayHistory>>> MarginRepayHistoryAsync(string asset, bool? isIsolated, string? symbol = null, string? Id = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}