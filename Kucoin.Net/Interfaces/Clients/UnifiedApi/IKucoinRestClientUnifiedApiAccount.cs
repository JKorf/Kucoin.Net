using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
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
        /// <param name="accountType">["<c>accountType</c>"] Account type</param>
        /// <param name="accountSubType">["<c>accountSubtype</c>"] For ISOLATED accounts, specify the trading pair name, e.g., BTC-USDT. For contract accounts, specify the contract settlement currency, e.g., USDT.</param>
        /// <param name="asset">["<c>currency</c>"] Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaBalances>> GetClassicBalancesAsync(
            UnifiedAccountType accountType,
            string? accountSubType = null,
            string? asset = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get sub account balances
        /// </summary>
        /// <param name="subAccountId">["<c>UID</c>"] Filter by sub account id</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="lastId">["<c>lastId</c>"] Filter by last id</param>
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
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="accountType">["<c>accountType</c>"] Account type</param>
        /// <param name="isolatedMarginSymbol">["<c>symbol</c>"] Isolated margin symbol, for example `ETH-USDT`</param>
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
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="transferType">["<c>type</c>"] Type of transfer</param>
        /// <param name="fromAccountType">["<c>fromAccountType</c>"] From account type</param>
        /// <param name="toAccountType">["<c>toAccountType</c>"] To account type</param>
        /// <param name="clientOrderId">["<c>clientOid</c>"] Client generated id</param>
        /// <param name="fromSubAccountId">["<c>fromUid</c>"] Source sub account id</param>
        /// <param name="toSubAccountId">["<c>toUid</c>"] Target sub account id</param>
        /// <param name="fromIsolatedMarginSymbol">["<c>fromAccountSymbol</c>"] Source isolated margin symbol</param>
        /// <param name="toIsolatedMarginSymbol">["<c>toAccountSymbol</c>"] Target isolated margin symbol</param>
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
        /// <param name="subAccountIds">["<c>subUids</c>"] Ids of sub accounts to update</param>
        /// <param name="allowSubToSub">["<c>subToSub</c>"] Whether sub account to sub account transfers are allowed</param>
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
        /// <param name="accountMode">["<c>accountType</c>"] New mode</param>
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
        /// <param name="accountType">["<c>tradeType</c>"] Account type, Spot or Futures</param>
        /// <param name="symbols">["<c>symbol</c>"] Symbols, spot max: 10, futures max: 1</param>
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
        /// <param name="accountType">["<c>accountType</c>"] Account type</param>
        /// <param name="asset">["<c>currency</c>"] Filter by assets, for example `ETH`</param>
        /// <param name="direction">["<c>direction</c>"] Filter by transfer direction</param>
        /// <param name="businessType">["<c>businessType</c>"] Filter by business type</param>
        /// <param name="lastId">["<c>lastId</c>"] Filter by last id, return ids before this</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="limit">["<c>pageSize</c>"] Max number of results</param>
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
        /// <param name="accountType">["<c>accountType</c>"] Account type</param>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="pageSize">["<c>size</c>"] Max number of results</param>
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
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDTM`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage for the symbol</param>
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
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>chain</c>"] The network, for example `eth`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaDepositAddress[]>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get API key info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-apikey-info?lang=en_US" /><br />
        /// Endpoint:<br />
        /// GET api/ua/v1/user/api-key<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaApiKeyInfo>> GetApiKeyInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-withdrawal-quotas?lang=en_US" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/withdrawals/quotas<br />
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="network">["<c>chainId</c>"] Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaWithdrawalQuota>> GetWithdrawalQuotasAsync(
            string asset,
            string network,
            CancellationToken ct = default);

        /// <summary>
        /// Withdraw from platform
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/withdrawal?lang=en_US" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/withdrawal<br />
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="toAddress">["<c>toAddress</c>"] To address</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="withdrawType">["<c>withdrawType</c>"] Withdraw type</param>
        /// <param name="network">["<c>chainId</c>"] Network id</param>
        /// <param name="memo">["<c>memo</c>"] Memo</param>
        /// <param name="isInternal">["<c>isInner</c>"] Is internal</param>
        /// <param name="feeDeductType">["<c>feeDeductType</c>"] Fee deduct type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaWithdrawResult>> WithdrawAsync(
            string asset,
            string toAddress,
            decimal quantity,
            WithdrawType withdrawType,
            string? network = null,
            string? memo = null,
            bool? isInternal = null,
            FeeDeductType? feeDeductType = null,
            CancellationToken ct = default);

        /// <summary>
        /// Set cross margin leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/modify-cross-margin-leverage-uta?lang=en_US&" /><br />
        /// Endpoint:<br />
        /// POST /api/ua/v1/{accountMode}/account/modify-leverage-margin-cross<br />
        /// </para>
        /// </summary>
        /// <param name="accountMode">["<c>accountMode</c>"] Account mode</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaLeverage>> SetCrossMarginLeverageAsync(
            UnifiedAccountMode accountMode,
            string asset,
            decimal leverage,
            CancellationToken ct = default);

        /// <summary>
        /// Get leverage settings
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-leverage?lang=en_US&" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/unified/account/leverage<br />
        /// </para>
        /// </summary>
        /// <param name="tradeType">["<c>tradeType</c>"] Trade type</param>
        /// <param name="marginMode">["<c>marginMode</c>"] Margin mode</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaLeverageSetting[]>> GetLeverageAsync(
            UnifiedSimpleAccountType tradeType,
            MarginMode marginMode,
            string? asset = null,
            string? symbol = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get funding fee history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-private-funding-fee-history?lang=en_US" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/position/funding-history<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="lastId">["<c>lastId</c>"] Previous last id for pagination</param>
        /// <param name="limit">["<c>pageSize</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFundingFeeHistory>> GetFundingFeeHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            long? lastId = null,
            int? limit = null,
            CancellationToken ct = default);

    }
}
