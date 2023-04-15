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
    /// Kucoin Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IKucoinClientFuturesApiAccount
    {
        /// <summary>
        /// Gets account overview
        /// <para><a href="https://docs.kucoin.com/futures/#get-account-overview" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para><a href="https://docs.kucoin.com/futures/#get-transaction-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the deposit address for an asset
        /// <para><a href="https://docs.kucoin.com/futures/#get-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">The asset to get deposit address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://docs.kucoin.com/futures/#get-deposits-list" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit history</returns>
        Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the withdrawal limit
        /// <para><a href="https://docs.kucoin.com/futures/#get-withdrawal-limit" /></para>
        /// </summary>
        /// <param name="asset">The asset to get limits for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        Task<WebCallResult<KucoinFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://docs.kucoin.com/futures/#withdraw-funds" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="isInner">Internal transfer (default false)</param>
        /// <param name="remark">Remarks</param>
        /// <param name="chain">Chain to use</param>
        /// <param name="memo">Memo for the withdrawal</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdraw history
        /// <para><a href="https://docs.kucoin.com/futures/#get-withdrawal-list" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal history</returns>
        Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal in process
        /// <para><a href="https://docs.kucoin.com/futures/#cancel-withdrawal" /></para>
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Transfer funds from futures to main account
        /// <para><a href="https://docs.kucoin.com/futures/#transfer-funds-to-kucoin-main-account-2" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Cancel a transfer from futures account to main account
        /// <para><a href="https://docs.kucoin.com/futures/#cancel-transfer-out-request" /></para>
        /// </summary>
        /// <param name="applyId">Transfer id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default);

        /// <summary>
        /// Get transfer to main account history
        /// <para><a href="https://docs.kucoin.com/futures/#get-transfer-out-request-records-2" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);


        /// <summary>
        /// Get the total value of active orders
        /// <para><a href="https://docs.kucoin.com/futures/#active-order-value-calculation" /></para>
        /// </summary>        
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get details on a position
        /// <para><a href="https://docs.kucoin.com/futures/#get-position-details" /></para>
        /// </summary>
        /// <param name="symbol">Contract symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get list of positions
        /// <para><a href="https://docs.kucoin.com/futures/#get-position-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Enable/disable auto deposit margin
        /// <para><a href="https://docs.kucoin.com/futures/#enable-disable-of-auto-deposit-margin" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to change for</param>
        /// <param name="enabled">Enable or disable</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Add margin
        /// <para><a href="https://docs.kucoin.com/futures/#add-margin-manually" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="clientId">A unique ID generated by the user, to ensure the operation is processed by the system only once</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para><a href="https://docs.kucoin.com/futures/#get-funding-history" /></para>
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
        /// Get risk limit level
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Objects.Models.Futures.KucoinRiskLimit>>> GetRiskLimitLevelAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set risk limit level
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="level">Risk limit level</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<bool>> SetRiskLimitLevelAsync(string symbol, int level, CancellationToken ct = default);
    }
}
