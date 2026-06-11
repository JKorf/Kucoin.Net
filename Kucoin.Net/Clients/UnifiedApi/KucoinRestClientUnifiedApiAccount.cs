using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
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
        public async Task<HttpResult<KucoinUaAccountOverview>> GetAccountOverviewAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/unified/account/overview", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountOverview>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaBalances>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/unified/account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Classic Balances

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaBalances>> GetClassicBalancesAsync(
            UnifiedAccountType accountType,
            string? accountSubType = null,
            string? asset = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("accountType", accountType);
            parameters.Add("accountSubtype", accountSubType);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Balances

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaSubAccountBalances>> GetSubAccountBalancesAsync(
            long? subAccountId = null,
            int? pageSize = null,
            long? lastId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("UID", subAccountId);
            parameters.Add("pageSize", pageSize);
            parameters.Add("lastId", lastId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/sub-account/balance", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaSubAccountBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transfer Quotas

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaTransferQuotas>> GetTransferQuotasAsync(
            string asset,
            UnifiedAccountType accountType,
            string? isolatedMarginSymbol = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("accountType", accountType);
            parameters.Add("symbol", isolatedMarginSymbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/account/transfer-quota", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync<KucoinUaTransferQuotas>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaResult>> TransferAsync(
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
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("type", transferType);
            parameters.Add("fromAccountType", fromAccountType);
            parameters.Add("toAccountType", toAccountType);
            parameters.Add("clientOid", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.Add("fromUid", fromSubAccountId);
            parameters.Add("toUid", toSubAccountId);
            parameters.Add("fromAccountSymbol", fromIsolatedMarginSymbol);
            parameters.Add("toAccountSymbol", toIsolatedMarginSymbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/account/transfer", KucoinExchange.RateLimiter.ManagementRest, 4, true);
            var result = await _baseClient.SendAsync<KucoinUaResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Sub Account Transfer Permission

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaTransferPermission[]>> SetSubAccountTransferPermissionAsync(
            IEnumerable<string> subAccountIds, 
            bool allowSubToSub,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("subUids", subAccountIds);
            parameters.Add("subToSub", allowSubToSub);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/sub-account/canTransferOut", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaTransferPermission[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Mode

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaAccountMode>> GetAccountModeAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/account/mode", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Account Mode

        /// <inheritdoc />
        public async Task<HttpResult> SetAccountModeAsync(UnifiedAccountMode accountMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("accountType", accountMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/account/mode", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fee Rate

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaFeeRate[]>> GetFeeRateAsync(
            UnifiedAccountType accountType,
            IEnumerable<string> symbols,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.AddCommaSeparated("symbol", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/user/fee-rate", KucoinExchange.RateLimiter.ManagementRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinUaFeeRates>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinUaFeeRate[]>(result);

            return HttpResult.Ok(result, result.Data.Rates);
        }

        #endregion

        #region Get Account Ledger

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaAccountLedger>> GetAccountLedgerAsync(
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
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("accountType", accountType);
            parameters.AddCommaSeparated("currency", asset);
            parameters.Add("direction", direction);
            parameters.Add("businessType", businessType);
            parameters.Add("lastId", lastId);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("pageSize", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/account/ledger", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaAccountLedger>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Interest History

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaInterestHistory>> GetInterestHistoryAsync(
            UnifiedAccountType accountType,
            string? asset = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("accountType", accountType);
            parameters.Add("currency", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/account/interest-history", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            var result = await _baseClient.SendAsync<KucoinUaInterestHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult> SetLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/unified/account/modify-leverage", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaDepositAddress[]>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/asset/deposit/address", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Api Key Info

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaApiKeyInfo>> GetApiKeyInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/ua/v1/user/api-key", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync<KucoinUaApiKeyInfo>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal Quotas

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaWithdrawalQuota>> GetWithdrawalQuotasAsync(
            string asset,
            string network,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chainId", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/withdrawals/quotas", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendAsync<KucoinUaWithdrawalQuota>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaWithdrawResult>> WithdrawAsync(
            string asset,
            string toAddress,
            decimal quantity,
            WithdrawType withdrawType,
            string? network = null,
            string? memo = null,
            bool? isInternal = null,
            FeeDeductType? feeDeductType = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("toAddress", toAddress);
            parameters.Add("amount", quantity);
            parameters.Add("withdrawType", withdrawType);
            parameters.Add("chainId", network);
            parameters.Add("memo", memo);
            parameters.Add("isInner", isInternal);
            parameters.Add("feeDeductType", feeDeductType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/withdrawal", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinUaWithdrawResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Cross Margin Leverage

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaLeverage>> SetCrossMarginLeverageAsync(
            UnifiedAccountMode accountMode,
            string asset,
            decimal leverage,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/account/modify-leverage-margin-cross",
                KucoinExchange.RateLimiter.UnifiedRest,
                20,
                true);
            var result = await _baseClient.SendAsync<KucoinUaLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaLeverageSetting[]>> GetLeverageAsync(
            UnifiedSimpleAccountType tradeType,
            MarginMode marginMode,
            string? asset = null,
            string? symbol = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", tradeType);
            parameters.Add("marginMode", marginMode);
            parameters.Add("currency", asset);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/unified/account/leverage", KucoinExchange.RateLimiter.ManagementRest, 10, true);
            var result = await _baseClient.SendAsync<KucoinUaLeverageSetting[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Fee History

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaFundingFeeHistory>> GetFundingFeeHistoryAsync(
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            long? lastId = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("lastId", lastId);
            parameters.Add("pageSize", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/position/funding-history", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            var result = await _baseClient.SendAsync<KucoinUaFundingFeeHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        internal async Task<HttpResult<KucoinToken>> GetWebsocketTokenPrivateAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v2/bullet-private", KucoinExchange.RateLimiter.ManagementRest, 10, true);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

    }
}
