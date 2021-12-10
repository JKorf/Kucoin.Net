using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Margin;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces
{
    /// <summary>
    /// INterface for Margin endpoints
    /// </summary>
    public interface IKucoinClientMargin : IRestClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        void SetApiCredentials(string apiKey, string apiSecret, string apiPass);

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns>The time of the server</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        #region Margin Trade

        #region Margin Info

        /// <summary>
        /// Get the index price of the specified symbol (https://docs.kucoin.com/#get-mark-price)
        /// </summary>
        /// <param name="asset">Path parameter (symbol)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mark price infos for asset</returns>
        Task<WebCallResult<KucoinMarkPrice>> GetMarkPriceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the configure info of the margin (https://docs.kucoin.com/#get-margin-configuration-info)
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginConfigurationInfo>> GetMarginConfigurationInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the info of the margin account (https://docs.kucoin.com/#get-margin-account)
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default);

        #endregion

        #region Borrow & Lend

        /// <summary>
        /// Places a Borrow order (https://docs.kucoin.com/#post-borrow-order)
        /// </summary>
        /// <param name="asset">Currency to Borrow e.g USDT etc</param>
        /// <param name="type">The type of the order (FOK, IOC)</param>
        /// <param name="quantity">Total size</param>
        /// <param name="maxRate">The max interest rate. All interest rates are acceptable if this field is left empty</param>
        /// <param name="term">term (Unit: Day). All terms are acceptable if this field is left empty. Please note to separate the terms via comma. For example, 7,14,28</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewBorrowOrder>> PlaceBorrowOrderAsync(string asset, KucoinBorrowOrderType type, decimal quantity, decimal? maxRate = null, string? term = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific borrow order (https://docs.kucoin.com/#get-borrow-order)
        /// </summary>
        /// <param name="orderId">The order id of the borrow order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow Order info</returns>
        Task<WebCallResult<KucoinBorrowOrder>> GetBorrowOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get borrow outstanding records (https://docs.kucoin.com/#get-repay-record)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated outstanding borrows</returns>
        Task<WebCallResult<KucoinPaginated<KucoinBorrowUnrepaid>>> GetUnrepaidBorrowsAsync(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow repayment records (https://docs.kucoin.com/#get-repayment-record)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated repayed borrows</returns>
        Task<WebCallResult<KucoinPaginated<KucoinBorrowRepaid>>> GetRepaidBorrowsAsync(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// One-click Repayment (https://docs.kucoin.com/#one-click-repayment)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="strategy">Repayment strategy</param>
        /// <param name="amount">Repayment size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> OneClickRepayment(string asset, KucoinRepaymentStrategy strategy, decimal amount, CancellationToken ct = default);


        /// <summary>
        /// Repay a Single Order (https://docs.kucoin.com/#repay-a-single-order)
        /// </summary>
        /// <param name="asset">Asset to Pay e.g USDT etc</param>
        /// <param name="tradeId">Trade ID of borrow order</param>
        /// <param name="quantity">Repayment size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RepaySingleBorrowOrderAsync(string asset, string tradeId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Post a Lend Order (https://docs.kucoin.com/#post-lend-order)
        /// </summary>
        /// <param name="asset">Currency to lend</param>
        /// <param name="size">Total size</param>
        /// <param name="dailyRate">Daily interest rate. e.g. 0.002 is 0.2%</param>
        /// <param name="term">Term (Unit: Day)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Kucoin Lend Order ID</returns>
        Task<WebCallResult<KucoinNewLendOrder>> PostLendOrderAsync(string asset, string size, string dailyRate, int term, CancellationToken ct = default);

        /// <summary>
        /// Cancel a Lend Order (https://docs.kucoin.com/#cancel-lend-order)
        /// </summary>
        /// <param name="orderId">Lend order ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelLendOrder(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get Active Order (https://docs.kucoin.com/#get-active-order)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated Active lend orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetOpenLendOrders(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get Lent History (https://docs.kucoin.com/#get-lent-history)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated lent orders</returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetLendOrdersHistory(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get Active Lend Order List (https://docs.kucoin.com/#get-active-lend-order-list)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated outstanding lend order list </returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendUnsettled>>> GetUnsettledLends(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get Settled Lend Order History (https://docs.kucoin.com/#get-settled-lend-order-history)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Paginated settled </returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendSettled>>> GetSettledLends(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the lending history of the main account (https://docs.kucoin.com/#get-account-lend-record)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of KucoinLendAccount</returns>
        Task<WebCallResult<IEnumerable<KucoinLendAccount>>> GetAccountLendRecord(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get the lending market data (https://docs.kucoin.com/#lending-market-data)
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="term">Term (Unit: Day)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List is sorted based on the descending sequence of the daily interest rate and terms.</returns>
        Task<WebCallResult<IEnumerable<KucoinLendingMarketData>>> GetLendingMarketData(string asset, int? term = null, CancellationToken ct = default);

        /// <summary>
        /// Get the fills in the lending and borrowing market
        /// </summary>
        /// <param name="asset">Currency</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of the last 300 fills in the lending and borrowing market. The returned value is sorted based on the descending sequence of the order execution time </returns>
        Task<WebCallResult<IEnumerable<KucoinMarginTradeData>>> GetMarginTradeData(string asset, CancellationToken ct = default);

        #endregion Borrow & Lend

        #endregion Margin Trade
    }
}
