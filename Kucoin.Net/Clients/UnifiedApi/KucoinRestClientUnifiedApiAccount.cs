using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    /// <inheritdoc />
    internal class KucoinRestClientUnifiedApiAccount : IKucoinRestClientUnifiedApiAccount
    {
        private readonly KucoinRestClientUnifiedApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientUnifiedApiAccount(KucoinRestClientUnifiedApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Account Overview

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaAccountOverview>> GetAccountOverviewAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/unified/account/overview", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountOverview>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion


        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaBalances>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/unified/account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Classic Balances

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaBalances>> GetClassicBalancesAsync(
            UnifiedAccountType accountType,
            string? accountSubType = null,
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("accountType", accountType);
            parameters.AddOptional("accountSubtype", accountSubType);
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Balances

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaSubAccountBalances>> GetSubAccountBalancesAsync(
            long? subAccountId = null,
            int? pageSize = null,
            long? lastId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("UID", subAccountId);
            parameters.AddOptional("pageSize", pageSize);
            parameters.AddOptional("lastId", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/sub-account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaSubAccountBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transfer Quotas

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaTransferQuotas>> GetTransferQuotasAsync(
            string asset,
            UnifiedAccountType accountType,
            string? isolatedMarginSymbol = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddEnum("accountType", accountType);
            parameters.AddOptional("symbol", isolatedMarginSymbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/account/transfer-quota", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync<KucoinUaTransferQuotas>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaResult>> TransferAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.AddEnum("type", transferType);
            parameters.AddEnum("fromAccountType", fromAccountType);
            parameters.AddEnum("toAccountType", toAccountType);
            parameters.Add("clientOid", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.AddOptional("fromUid", fromSubAccountId);
            parameters.AddOptional("toUid", toSubAccountId);
            parameters.AddOptional("fromAccountSymbol", fromIsolatedMarginSymbol);
            parameters.AddOptional("toAccountSymbol", toIsolatedMarginSymbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/account/transfer", KucoinExchange.RateLimiter.ManagementRest, 4, true);
            var result = await _baseClient.SendAsync<KucoinUaResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Sub Account Transfer Permission

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaTransferPermission[]>> SetSubAccountTransferPermissionAsync(
            IEnumerable<string> subAccountIds, 
            bool allowSubToSub,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddCommaSeparated("subUids", subAccountIds);
            parameters.Add("subToSub", allowSubToSub);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/sub-account/canTransferOut", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaTransferPermission[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Mode

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaAccountMode>> GetAccountModeAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/account/mode", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Account Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetAccountModeAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/account/mode", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Account Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetAccountModeAsync(UnifiedAccountMode accountMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("accountType", accountMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/account/mode", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fee Rate

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaFeeRate[]>> GetFeeRateAsync(
            UnifiedAccountType accountType,
            IEnumerable<string> symbols,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddCommaSeparated("symbol", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/user/fee-rate", KucoinExchange.RateLimiter.ManagementRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinUaFeeRates>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinUaFeeRate[]>(result.Data?.Rates);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaAccountLedger>> GetAccountLedgerAsync(
            UnifiedAccountType accountType,
            IEnumerable<string>? asset = null,
            AccountDirection? direction = null,
            UnifiedBusinessType? businessType = null,
            long? lastId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("accountType", accountType);
            parameters.AddOptionalCommaSeparated("currency", asset);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalEnum("businessType", businessType);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptional("pageSize", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/account/ledger", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountLedger>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
        #region Get Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaInterestHistory>> GetInterestHistoryAsync(
            UnifiedAccountType accountType,
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("accountType", accountType);
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/account/interest-history", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            var result = await _baseClient.SendAsync<KucoinUaInterestHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult> SetLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddString("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/unified/account/modify-leverage", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaDepositAddress[]>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddOptional("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/asset/deposit/address", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
