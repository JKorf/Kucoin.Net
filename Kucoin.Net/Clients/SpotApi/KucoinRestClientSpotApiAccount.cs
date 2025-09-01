using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiAccount : IKucoinRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientSpotApi _baseClient;

        internal KucoinRestClientSpotApiAccount(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUserInfo>> GetUserInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v2/user-info", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinUserInfo>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccount[]>> GetAccountsAsync(string? asset = null, AccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptional("type", EnumConverter.GetString(accountType)?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts/" + accountId, KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinAccountSingle>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(AssetType? assetType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currencyType", EnumConverter.GetString(assetType));

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/base-fee", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinUserFee>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default)
            => await GetSymbolTradingFeesAsync(new[] { symbol }, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbols",  string.Join(",", symbols) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/trade-fees", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinTradeFee[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? asset = null, AccountDirection? direction = null, BizTypeFilter? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalEnum("bizType", bizType);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts/ledgers", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinAccountActivity>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccountActivity[]>> GetHfAccountLedgersAsync(string? asset = null, AccountDirection? direction = null, BizTypeFilter? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? lastId = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 200);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalEnum("bizType", bizType);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("lastId", lastId);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/accounts/ledgers", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinAccountActivity[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, string? isolatedMarginSymbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "currency", asset }
            };
            parameters.AddEnum("type", accountType);
            parameters.AddOptional("tag", isolatedMarginSymbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts/transferable", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinTransferableAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUniversalTransfer>> UniversalTransferAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "fromAccountType", EnumConverter.GetString(fromAccountType) },
                { "toAccountType", EnumConverter.GetString(toAccountType)},
                { "type", EnumConverter.GetString(transferType)},
                { "amount", quantity },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString()},
            };
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("fromUserId", fromUserId);
            parameters.AddOptionalParameter("fromAccountTag", fromAccountTag);
            parameters.AddOptionalParameter("toUserId", toUserId);
            parameters.AddOptionalParameter("toAccountTag", toAccountTag);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/accounts/universal-transfer", KucoinExchange.RateLimiter.ManagementRest, 4, true);
            return await _baseClient.SendAsync<KucoinUniversalTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/deposits", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress[]>> GetDepositAddressesV3Async(string asset, string? networkId = null, decimal? quantity = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection { { "currency", asset } };
            parameters.AddOptionalParameter("chain", networkId);
            parameters.AddOptionalString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/deposit-addresses", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinDepositAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressV3Async(string asset, string? networkId = null, AccountType? toAccountType = null, decimal? quantity = null, CancellationToken ct = default)
        {
            if (toAccountType != null && (toAccountType != AccountType.Main && toAccountType != AccountType.Trade))
                throw new ArgumentException("To account type must be either Main or Trade");

            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection { { "currency", asset } };
            parameters.AddOptionalParameter("chain", networkId);
            parameters.AddOptional("to", EnumConverter.GetString(toAccountType)?.ToLower());
            parameters.AddOptionalString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/deposit-address/create", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/withdrawals", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinWithdrawal>> GetWithdrawalAsync(string id, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/withdrawals/{id}", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinWithdrawal>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/withdrawals/quotas", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinWithdrawalQuota>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(WithdrawType withdrawalType, string asset, string toAddress, decimal quantity, string? memo = null, bool isInner = false, string? remark = null, string? network = null, FeeDeductType? deductType = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            toAddress.ValidateNotNull(nameof(toAddress));
            var parameters = new ParameterCollection {
                { "currency", asset },
                { "toAddress", toAddress },
                { "amount", quantity },
            };
            parameters.AddEnum("withdrawType", withdrawalType);
            parameters.AddOptionalParameter("memo", memo);
            parameters.AddOptionalParameter("isInner", isInner);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("feeDeductType", EnumConverter.GetString(deductType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/withdrawals", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinNewWithdrawal>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            withdrawalId.ValidateNotNull(nameof(withdrawalId));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/withdrawals/{withdrawalId}", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/margin/account", KucoinExchange.RateLimiter.SpotRest, 40, true);
            return await _baseClient.SendAsync<KucoinMarginAccount>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCrossMarginAccount>> GetCrossMarginAccountsAsync(string? quoteAsset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("quoteCurrency", quoteAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/accounts", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinCrossMarginAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIsolatedMarginAccountsInfo>> GetIsolatedMarginAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/isolated/accounts", KucoinExchange.RateLimiter.SpotRest, 50, true);
            return await _baseClient.SendAsync<KucoinIsolatedMarginAccountsInfo>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/isolated/account/{symbol}", KucoinExchange.RateLimiter.SpotRest, 50, true);
            return await _baseClient.SendAsync<KucoinIsolatedMarginAccount>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMigrateStatus>> GetHfMigrationStatusAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "withAllSubs", true }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/migrate/user/account/status", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinMigrateStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMigrateResult>> MigrateHfAccountAsync(bool? withAllSubAccounts = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("withAllSubs", withAllSubAccounts);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/migrate/user/account", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinMigrateResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<bool>> GetIsHfAccountAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/accounts/opened", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<bool>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinApiKey>> GetApiKeyInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/user/api-key", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinApiKey>(request, parameters, ct).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketTokenPublicAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/bullet-public", KucoinExchange.RateLimiter.PublicRest, 10, false);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketTokenPrivateAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/bullet-private", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }
    }
}
