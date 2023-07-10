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
    /// Kucoin Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IKucoinRestClientSpotApiAccount
    {
        /// <summary>
        /// Gets a list of sub users
        /// <para><a href="https://docs.kucoin.com/#get-user-info-of-all-sub-accounts" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub users</returns>
        Task<WebCallResult<IEnumerable<KucoinSubUser>>> GetUserInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of accounts
        /// <para><a href="https://docs.kucoin.com/#list-accounts" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<IEnumerable<KucoinAccount>>> GetAccountsAsync(string? asset = null, AccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Get a specific account
        /// <para><a href="https://docs.kucoin.com/#get-an-account" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account info</returns>
        Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default);

        /// <summary>
        /// Get the basic user fees
        /// <para><a href="https://docs.kucoin.com/#basic-user-fee" /></para>
        /// </summary>
        /// <param name="assetType">The type of asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(AssetType? assetType = null, CancellationToken ct = default);

        /// <summary>
        /// Get the trading fees for symbols
        /// <para><a href="https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to retrieve fees for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the trading fees for symbols
        /// <para><a href="https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to retrieve fees for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of account activity
        /// <para><a href="https://docs.kucoin.com/#get-account-ledgers-deprecated" /></para>
        /// </summary>
        /// <param name="accountId">The account id to get the activities for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on account activity</returns>
        [Obsolete("Prefers GetAccountLedgersAsync")]
        Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgerAsync(string accountId, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of account activity
        /// <para><a href="https://docs.kucoin.com/#get-account-ledgers" /></para>
        /// </summary>
        /// <param name="asset">The asset to retrieve activity or null</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="direction">Side</param>
        /// <param name="bizType">Business type</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on account activity</returns>
        Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? asset = null, AccountDirection? direction = null, BizType? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a transferable balance of a specified account.
        /// <para><a href="https://docs.kucoin.com/#get-the-transferable" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on transferable account balance</returns>
        Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, CancellationToken ct = default);

        /// <summary>
        /// Transfers assets between the accounts of a user.
        /// <para><a href="https://docs.kucoin.com/#transfer-between-master-user-and-sub-user" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset</param>
        /// <param name="from">The type of the account</param>
        /// <param name="to">The type of the account</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="fromTag">Trading pair, required when the payment account type is isolated, e.g.: BTC-USDT</param>
        /// <param name="toTag">Trading pair, required when the receiving account type is isolated, e.g.: BTC-USDT</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order ID of a funds transfer</returns>
        Task<WebCallResult<KucoinInnerTransfer>> InnerTransferAsync(string asset, AccountType from, AccountType to, decimal quantity, string? fromTag = null, string? toTag = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of deposits
        /// <para><a href="https://docs.kucoin.com/#get-deposit-list" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of historical deposits
        /// <para><a href="https://docs.kucoin.com/#get-v1-historical-deposits-list" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical deposits</returns>
        Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// <para><a href="https://docs.kucoin.com/#get-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">The asset to get the address for</param>
        /// <param name="network">The network to get the address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address for the asset</returns>
        Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit addresses for an asset
        /// <para><a href="https://docs.kucoin.com/#get-deposit-addresses-v2" /></para>
        /// </summary>
        /// <param name="asset">The asset to get the address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address for the asset</returns>
        Task<WebCallResult<IEnumerable<KucoinDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Creates a new deposit address for an asset
        /// <para><a href="https://docs.kucoin.com/#create-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">The asset create the address for</param>
        /// <param name="network">The network to create the address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The address that was created</returns>
        Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of withdrawals
        /// <para><a href="https://docs.kucoin.com/#get-withdrawals-list" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of historical withdrawals
        /// <para><a href="https://docs.kucoin.com/#get-v1-historical-withdrawals-list" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical withdrawals</returns>
        Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get the withdrawal quota for a asset
        /// <para><a href="https://docs.kucoin.com/#get-withdrawal-quotas" /></para>
        /// </summary>
        /// <param name="asset">The asset to get the quota for</param>
        /// <param name="network">The network name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota info</returns>
        Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset to an address
        /// <para><a href="https://docs.kucoin.com/#apply-withdraw-2" /></para>
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="toAddress">The address to withdraw to</param>
        /// <param name="quantity">The quantity to withdraw</param>
        /// <param name="memo">The note that is left on the withdrawal address. When you withdraw from KuCoin to other platforms, you need to fill in memo(tag). If you don't fill in memo(tag), your withdrawal may not be available.</param>
        /// <param name="isInner">Internal withdrawal or not. Default false.</param>
        /// <param name="remark">Remark for the withdrawal</param>
        /// <param name="chain">The chain name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is OMNI. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <param name="feeDeductType">Fee deduction type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id of the withdrawal</returns>
        Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string toAddress, decimal quantity, string? memo = null, bool? isInner = null, string? remark = null, string? chain = null, FeeDeductType? feeDeductType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal
        /// <para><a href="https://docs.kucoin.com/#cancel-withdrawal" /></para>
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Null</returns>
        Task<WebCallResult<object>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin risk limit
        /// <para><a href="https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinRiskLimitCrossMargin>>> GetRiskLimitCrossMarginAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin risk limit
        /// <para><a href="https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinRiskLimitIsolatedMargin>>> GetRiskLimitIsolatedMarginAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin account info
        /// <para><a href="https://docs.kucoin.com/#get-margin-account" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://docs.kucoin.com/#query-isolated-margin-account-info" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIsolatedMarginAccountsInfo>> GetIsolatedMarginAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://docs.kucoin.com/#query-single-isolated-margin-account-info" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(string symbol, CancellationToken ct = default);
    }
}