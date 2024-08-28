using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
        public async Task<WebCallResult<IEnumerable<KucoinAccount>>> GetAccountsAsync(string? asset = null, AccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("type", accountType.HasValue ? JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)) : null);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinAccount>>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default)
            => await GetSymbolTradingFeesAsync(new[] { symbol }, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbols",  string.Join(",", symbols) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/trade-fees", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinTradeFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? asset = null, AccountDirection? direction = null, BizType? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            string bizTypeString = string.Empty;
            string directionString = string.Empty;

            if (bizType.HasValue)
            {
                bizTypeString = JsonConvert.SerializeObject(bizType, new BizTypeConverter(true));
                bizTypeString.ValidateNullOrNotEmpty(nameof(bizType));
            }

            if (direction.HasValue)
            {
                directionString = JsonConvert.SerializeObject(direction, new AccountDirectionConverter(false)).ToLowerInvariant();
            }

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("direction", direction.HasValue ? directionString : null);
            parameters.AddOptionalParameter("bizType", bizType.HasValue ? bizTypeString : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/accounts/ledgers", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinAccountActivity>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, string? isolatedMarginSymbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "currency", asset },
                { "type", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false, true))}
            };
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
        public async Task<WebCallResult<KucoinInnerTransfer>> InnerTransferAsync(string asset, AccountType from, AccountType to, decimal quantity, string? fromTag = null, string? toTag = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection
            {
                { "currency", asset },
                { "from", JsonConvert.SerializeObject(from, new AccountTypeConverter(false))},
                { "to", JsonConvert.SerializeObject(to, new AccountTypeConverter(false))},
                { "amount", quantity },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString()},
            };
            parameters.AddOptionalParameter("fromTag", fromTag);
            parameters.AddOptionalParameter("toTag", toTag);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v2/accounts/inner-transfer", KucoinExchange.RateLimiter.ManagementRest, 10, true);
            return await _baseClient.SendAsync<KucoinInnerTransfer>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/deposits", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hist-deposits", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinHistoricalDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/deposit-addresses", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<KucoinDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection { { "currency", asset } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v2/deposit-addresses", KucoinExchange.RateLimiter.ManagementRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/deposit-addresses", KucoinExchange.RateLimiter.ManagementRest, 20, true);
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
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/withdrawals", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hist-withdrawals", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinHistoricalWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string toAddress, decimal quantity, string? memo = null, bool isInner = false, string? remark = null, string? network = null, FeeDeductType? deductType = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            toAddress.ValidateNotNull(nameof(toAddress));
            var parameters = new ParameterCollection {
                { "currency", asset },
                { "address", toAddress },
                { "amount", quantity },
            };
            parameters.AddOptionalParameter("memo", memo);
            parameters.AddOptionalParameter("isInner", isInner);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("feeDeductType", EnumConverter.GetString(deductType));
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/withdrawals", KucoinExchange.RateLimiter.ManagementRest, 5, true);
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