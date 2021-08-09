using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures;
using Kucoin.Net.Objects.Spot;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// Interface for the futures client
    /// </summary>
    public interface IKucoinClientFutures : IRestClient
    {
        /// <summary>
        /// Gets account overview
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? currency = null, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? currency = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to get deposit address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string currency, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit history</returns>
        Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositHistoryAsync(string? currency = null, KucoinDepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the withdrawal limit
        /// </summary>
        /// <param name="currency">The currency to get limits for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        Task<WebCallResult<KucoinFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string currency, CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// </summary>
        /// <param name="currency">Currency to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="isInner">Internal transfer (default false)</param>
        /// <param name="remark">Remarks</param>
        /// <param name="chain">Chain to use</param>
        /// <param name="memo">Memo for the withdrawal</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string currency, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdraw history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal history</returns>
        Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawHistoryAsync(string? currency = null, KucoinWithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal in process
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Transfer funds from futures to main account
        /// </summary>
        /// <param name="currency">Currency to withdraw</param>
        /// <param name="clientId">Client identifier for the operation, needs to be unique. Guid.NewGuid() suggested</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string currency, decimal quantity, string? clientId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a transfer from futures account to main account
        /// </summary>
        /// <param name="applyId">Transfer id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default);

        /// <summary>
        /// Get transfer to main account history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string currency, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="symbol">The contract for the order</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of order</param>
        /// <param name="leverage">Leverage of the order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="timeInForce">Time in force, only for limit orders</param>
        /// <param name="postOnly">Post only flag, invalid when timeInForce is IOC</param>
        /// <param name="hidden">Orders not displaying in order book. When hidden chose</param>
        /// <param name="iceberg">Only visible portion of the order is displayed in the order book</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="size">Amount of contract to buy or sell</param>
        /// <param name="remark">Remark for the order</param>
        /// <param name="stopType"></param>
        /// <param name="stopPriceType"></param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="closeOrder">A mark to close the position. Set to false by default. All the positions will be closed if true</param>
        /// <param name="forceHold">A mark to forcely hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        Task<WebCallResult> PlaceOrderAsync(
            string symbol,
            KucoinOrderSide side,
            KucoinNewOrderType type,
            int leverage,
            decimal size,

            decimal? price = null,
            KucoinTimeInForce? timeInForce = null,
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
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled id</returns>
        Task<WebCallResult<KucoinCancelledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled ids</returns>
        Task<WebCallResult<KucoinCancelledOrders>> CancelAllOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open stop orders
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled ids</returns>
        Task<WebCallResult<KucoinCancelledOrders>> CancelAllOpenStopOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="status">Filter by status</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, KucoinOrderStatus? status = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of untriggered stop orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of 1000 most recent orders in the last 24 hours
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetCompletedOrdersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// </summary>
        /// <param name="orderId">Id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradeHistoryAsync(string? orderId = null, string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of 1000 most recent user trades in the last 24 hours
        /// </summary>        
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the total value of active orders
        /// </summary>        
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderValuation>> GetActiveOrderValueAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get details on a position
        /// </summary>
        /// <param name="symbol">Contract symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get list of positions
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Enable/disable auto deposit margin
        /// </summary>
        /// <param name="symbol">Symbol to change for</param>
        /// <param name="enabled">Enable or disable</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Add margin
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="clientId">A unique ID generated by the user, to ensure the operation is processed by the system only once</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get open contract list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the ticker for a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the full order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate list
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get index list
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current mark price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get premium index
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current funding rate
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);


        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the service status
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Start time to retrieve klines from</param>
        /// <param name="endTime">End time to retrieve klines for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);
    }
}