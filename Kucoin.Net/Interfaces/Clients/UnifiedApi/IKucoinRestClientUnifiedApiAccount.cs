using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Unified API account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IKucoinRestClientUnifiedApiAccount
    {
        /// <summary>
        /// Get account overview for UTA account
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/account/get-account-overview-uta" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/unified/account/overview
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAccountOverview>> GetAccountOverviewAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-account-currency-assets-uta" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/unified/account/balance
        /// </para>
        /// </summary>        
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBalances>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get classic account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-account-currency-assets-classic" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/account/balance
        /// </para>
        /// </summary>
        /// <param name="accountType">Account type</param>
        /// <param name="accountSubType">For ISOLATED accounts, specify the trading pair name, e.g., BTC-USDT. For contract accounts, specify the contract settlement currency, e.g., USDT.</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBalances>> GetClassicBalancesAsync(
            UnifiedAccountType accountType,
            string? accountSubType = null,
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get sub account balances
        /// </summary>
        /// <param name="subAccountId">Filter by sub account id</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="lastId">Filter by last id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaSubAccountBalances>> GetSubAccountBalancesAsync(
            long? subAccountId = null,
            int? pageSize = null,
            long? lastId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get transfer quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-transfer-quotas" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/account/transfer-quota
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="accountType">Account type</param>
        /// <param name="isolatedMarginSymbol">Isolated margin symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTransferQuotas>> GetTransferQuotasAsync(
            string asset,
            UnifiedAccountType accountType,
            string? isolatedMarginSymbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfer between accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/flex-transfer" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/account/transfer
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="transferType">Type of transfer</param>
        /// <param name="fromAccountType">From account type</param>
        /// <param name="toAccountType">To account type</param>
        /// <param name="clientOrderId">Client generated id</param>
        /// <param name="fromSubAccountId">Source sub account id</param>
        /// <param name="toSubAccountId">Target sub account id</param>
        /// <param name="fromIsolatedMarginSymbol">Source isolated margin symbol</param>
        /// <param name="toIsolatedMarginSymbol">Target isolated margin symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaResult>> TransferAsync(
            string asset,
            decimal quantity,
            UnifiedTransferType transferType, 
            UnifiedAccountType fromAccountType,
            UnifiedAccountType toAccountType, 
            string? clientOrderId = null, 
            string? fromSubAccountId = null,
            string? toSubAccountId = null,
            string? fromIsolatedMarginSymbol = null, 
            string? toIsolatedMarginSymbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set sub to sub transfer permissions for sub accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/set-sub-account-transfer-permission" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/sub-account/canTransferOut
        /// </para>
        /// </summary>
        /// <param name="subAccountIds">Ids of sub accounts to update</param>
        /// <param name="allowSubToSub">Whether sub account to sub account transfers are allowed</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTransferPermission[]>> SetSubAccountTransferPermissionAsync(
            IEnumerable<string> subAccountIds, 
            bool allowSubToSub,
            CancellationToken ct = default);

        /// <summary>
        /// Get account mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-account-mode" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/account/mode
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAccountMode>> GetAccountModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Set account to unified mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/set-account-mode" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/account/mode
        /// </para>
        /// </summary>
        /// <param name="accountMode">New mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetAccountModeAsync(UnifiedAccountMode accountMode, CancellationToken ct = default);

        /// <summary>
        /// Get fee rates for symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-actual-fee" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/user/fee-rate
        /// </para>
        /// </summary>
        /// <param name="accountType">Account type, Spot or Futures</param>
        /// <param name="symbols">Symbols, spot max: 10, futures max: 1</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFeeRate[]>> GetFeeRateAsync(
            UnifiedAccountType accountType,
            IEnumerable<string> symbols,
            CancellationToken ct = default);

        /// <summary>
        /// Get account transfer in/out history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-account-ledger" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/account/ledger
        /// </para>
        /// </summary>
        /// <param name="accountType">Account type</param>
        /// <param name="asset">Filter by assets, for example `ETH`</param>
        /// <param name="direction">Filter by transfer direction</param>
        /// <param name="businessType">Filter by business type</param>
        /// <param name="lastId">Filter by last id, return ids before this</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAccountLedger>> GetAccountLedgerAsync(
            UnifiedAccountType accountType,
            IEnumerable<string>? asset = null,
            AccountDirection? direction = null,
            UnifiedBusinessType? businessType = null,
            long? lastId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-interest-history-uta" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/account/interest-history
        /// </para>
        /// </summary>
        /// <param name="accountType">Account type</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaInterestHistory>> GetInterestHistoryAsync(
            UnifiedAccountType accountType,
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null, 
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set leverage for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/modify-leverage-uta" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/unified/account/modify-leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDTM`</param>
        /// <param name="leverage">Leverage for the symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Get deposit addresses
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-deposit-address" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/asset/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="network">The network, for example `eth`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDepositAddress[]>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

    }
}
