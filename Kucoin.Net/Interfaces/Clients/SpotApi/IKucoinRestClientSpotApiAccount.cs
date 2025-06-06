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
        /// Get account summary info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-summary-info" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinUserInfo>> GetUserInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of accounts
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-list-spot" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset, for example `ETH`</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<KucoinAccount[]>> GetAccountsAsync(string? asset = null, AccountType? accountType = null, CancellationToken ct = default);

        /// <summary>
        /// Get a specific account
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-detail-spot" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account info</returns>
        Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default);

        /// <summary>
        /// Get the basic user fees
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/trade-fee/get-basic-fee-spot-margin" /></para>
        /// </summary>
        /// <param name="assetType">The type of asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(AssetType? assetType = null, CancellationToken ct = default);

        /// <summary>
        /// Get the trading fees for symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/trade-fee/get-actual-fee-spot-margin" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to retrieve fees for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the trading fees for symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/trade-fee/get-actual-fee-spot-margin" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to retrieve fees for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of account activity
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-ledgers-spot-margin" /></para>
        /// </summary>
        /// <param name="asset">The asset to retrieve activity or null, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="direction">Side</param>
        /// <param name="bizType">Business type</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on account activity</returns>
        Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? asset = null, AccountDirection? direction = null, BizTypeFilter? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a transferable balance of a specified account.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/transfer/get-transfer-quotas" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset, for example `ETH`</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="isolatedMarginSymbol">Filter by isolated margin symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on transferable account balance</returns>
        Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, string? isolatedMarginSymbol = null, CancellationToken ct = default);

        /// <summary>
        /// Universal transfer between accounts
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/transfer/flex-transfer" /></para>
        /// </summary>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="fromAccountType">From account type</param>
        /// <param name="toAccountType">To account type</param>
        /// <param name="transferType">Transfer type</param>
        /// <param name="asset">Asset to transfer, for example `ETH`</param>
        /// <param name="fromUserId">Transfer out UserId， This is required when transferring sub-account to master-account. It is optional for internal transfers.</param>
        /// <param name="fromAccountTag">Symbol, required when the account type is ISOLATED or ISOLATED_V2, for example: BTC-USDT</param>
        /// <param name="toUserId">Transfer in UserId， This is required when transferring master-account to sub-account. It is optional for internal transfers.</param>
        /// <param name="toAccountTag">Symbol, required when the account type is ISOLATED or ISOLATED_V2, for example: BTC-USDT</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinUniversalTransfer>> UniversalTransferAsync(
            decimal quantity,
            TransferAccountType fromAccountType,
            TransferAccountType toAccountType,
            TransferType transferType,
            string? asset = null,
            string? fromUserId = null,
            string? fromAccountTag = null,
            string? toUserId = null,
            string? toAccountTag = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets a list of deposits
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/deposit/get-deposit-history" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset, for example `ETH`</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit addresses for an asset
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/deposit/get-deposit-address-v3" /></para>
        /// </summary>
        /// <param name="asset">The asset to get the address for, for example `ETH`</param>
        /// <param name="networkId">The id of the network to get the address for</param>
        /// <param name="quantity">Deposit amount. This parameter is only used when applying for invoices on the Lightning Network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address for the asset</returns>
        Task<WebCallResult<KucoinDepositAddress[]>> GetDepositAddressesV3Async(string asset, string? networkId = null, decimal? quantity = null, CancellationToken ct = default);

        /// <summary>
        /// Creates a new deposit address for an asset
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/deposit/add-deposit-address-v3" /></para>
        /// </summary>
        /// <param name="asset">The asset create the address for, for example `ETH`</param>
        /// <param name="networkId">The id of the network to create the address for</param>
        /// <param name="toAccount">The account deposits should be credited to. Either Main or Trade</param>
        /// <param name="quantity">Deposit amount. This parameter is only used when applying for invoices on the Lightning Network</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The address that was created</returns>
        Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressV3Async(string asset, string? networkId = null, AccountType? toAccount = null, decimal? quantity = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of withdrawals
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/withdrawals/get-withdrawal-history" /></para>
        /// </summary>
        /// <param name="asset">Filter list by asset, for example `ETH`</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a withdrawal
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/withdrawals/get-withdrawal-by-id" /></para>
        /// </summary>
        /// <param name="id">Id of the withdrawal</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinWithdrawal>> GetWithdrawalAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get the withdrawal quota for a asset
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/withdrawals/get-withdrawal-quotas" /></para>
        /// </summary>
        /// <param name="asset">The asset to get the quota for, for example `ETH`</param>
        /// <param name="network">The network name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota info</returns>
        Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset to an address
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/withdrawals/withdraw-v3" /></para>
        /// </summary>
        /// <param name="withdrawalType">Type of withdrawal</param>
        /// <param name="asset">The asset to withdraw, for example `ETH`</param>
        /// <param name="toAddress">The address to withdraw to</param>
        /// <param name="quantity">The quantity to withdraw</param>
        /// <param name="memo">The note that is left on the withdrawal address. When you withdraw from KuCoin to other platforms, you need to fill in memo(tag). If you don't fill in memo(tag), your withdrawal may not be available.</param>
        /// <param name="isInner">Internal withdrawal or not. Default false.</param>
        /// <param name="remark">Remark for the withdrawal</param>
        /// <param name="chain">The chain name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is OMNI. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <param name="feeDeductType">Fee deduction type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id of the withdrawal</returns>
        Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(WithdrawType withdrawalType, string asset, string toAddress, decimal quantity, string? memo = null, bool isInner = false, string? remark = null, string? chain = null, FeeDeductType? feeDeductType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a withdrawal
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/withdrawals/cancel-withdrawal" /></para>
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Null</returns>
        Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Get margin account info
        /// <para><a href="https://www.kucoin.com/docs/rest/funding/funding-overview/get-account-detail-margin" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get cross margin account info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-cross-margin" /></para>
        /// </summary>
        /// <param name="quoteAsset">Filter by quote asset, for example `BTC`</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCrossMarginAccount>> GetCrossMarginAccountsAsync(string? quoteAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-isolated-margin" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIsolatedMarginAccountsInfo>> GetIsolatedMarginAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-isolated-margin" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get migration status of the spot high/normal frequency accounts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMigrateStatus>> GetHfMigrationStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Migrate the spot high and normal frequency accounts into a single high frequency account. Only needed when the High Frequency API endpoints have been used previously.
        /// </summary>
        /// <param name="withAllSubAccounts">Whether to operate all sub-accounts together. If left blank, the sub-account will not be operated by default</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMigrateResult>> MigrateHfAccountAsync(bool? withAllSubAccounts = null, CancellationToken ct = default);

        /// <summary>
        /// Get whether the current account is a High-Frequency account
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<bool>> GetIsHfAccountAsync(CancellationToken ct = default);

        /// <summary>
        /// Get API key info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-apikey-info" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinApiKey>> GetApiKeyInfoAsync(CancellationToken ct = default);
    }
}
