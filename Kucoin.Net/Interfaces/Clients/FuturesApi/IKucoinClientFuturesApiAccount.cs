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
        /// Transfer funds from futures to main account
        /// <para><a href="https://docs.kucoin.com/futures/#transfer-to-main-or-trade-account" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="receiveAccountType">Receiving account type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string asset, decimal quantity, AccountType receiveAccountType, CancellationToken ct = default);

        /// <summary>
        /// Transfer funds from main or trade account to futures
        /// <para><a href="https://docs.kucoin.com/futures/#transfer-to-futures-account" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="payAccountType">Account to move funds from</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> TransferToFuturesAccountAsync(string asset, decimal quantity, AccountType payAccountType, CancellationToken ct = default);

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
        /// <param name="status">Filter by status</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);


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
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(string? asset = null, CancellationToken ct = default);

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
