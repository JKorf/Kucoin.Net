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
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class KucoinRestClientSpotApiAccount : IKucoinRestClientSpotApiAccount
    {
        private readonly KucoinRestClientSpotApi _baseClient;

        internal KucoinRestClientSpotApiAccount(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinSubUser>>> GetUserInfoAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinSubUser>>(_baseClient.GetUri("sub/user"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinAccount>>> GetAccountsAsync(string? asset = null, AccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("type", accountType.HasValue ? JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)) : null);
            return await _baseClient.Execute<IEnumerable<KucoinAccount>>(_baseClient.GetUri("accounts"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            return await _baseClient.Execute<KucoinAccountSingle>(_baseClient.GetUri("accounts/" + accountId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(AssetType? assetType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currencyType", EnumConverter.GetString(assetType));

            return await _baseClient.Execute<KucoinUserFee>(_baseClient.GetUri("base-fee"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default)
            => await GetSymbolTradingFeesAsync(new[] { symbol }, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbols",  string.Join(",", symbols) }
            };
            return await _baseClient.Execute<IEnumerable<KucoinTradeFee>>(_baseClient.GetUri("trade-fees"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        [Obsolete("Prefers GetAccountLedgersAsync")]
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgerAsync(string accountId, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinAccountActivity>>(_baseClient.GetUri($"accounts/{accountId}/ledgers"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("direction", direction.HasValue ? directionString : null);
            parameters.AddOptionalParameter("bizType", bizType.HasValue ? bizTypeString : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<KucoinAccountActivity>>(_baseClient.GetUri("accounts/ledgers"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "type", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false, true))}
            };
            return await _baseClient.Execute<KucoinTransferableAccount>(_baseClient.GetUri("accounts/transferable"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinInnerTransfer>> InnerTransferAsync(string asset, AccountType from, AccountType to, decimal quantity, string? fromTag = null, string? toTag = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "from", JsonConvert.SerializeObject(from, new AccountTypeConverter(false))},
                { "to", JsonConvert.SerializeObject(to, new AccountTypeConverter(false))},
                { "amount", quantity },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString()},
            };
            parameters.AddOptionalParameter("fromTag", fromTag);
            parameters.AddOptionalParameter("toTag", toTag);

            return await _baseClient.Execute<KucoinInnerTransfer>(_baseClient.GetUri("accounts/inner-transfer", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinDeposit>>(_baseClient.GetUri("deposits"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinHistoricalDeposit>>(_baseClient.GetUri("hist-deposits"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new Dictionary<string, object> { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            return await _baseClient.Execute<KucoinDepositAddress>(_baseClient.GetUri("deposit-addresses"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new Dictionary<string, object> { { "currency", asset } };
            return await _baseClient.Execute<IEnumerable<KucoinDepositAddress>>(_baseClient.GetUri("deposit-addresses", 2), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new Dictionary<string, object> { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            return await _baseClient.Execute<KucoinDepositAddress>(_baseClient.GetUri("deposit-addresses"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinWithdrawal>>(_baseClient.GetUri("withdrawals"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinHistoricalWithdrawal>>(_baseClient.GetUri("hist-withdrawals"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string asset, string? network = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object> { { "currency", asset } };
            parameters.AddOptionalParameter("chain", network);
            return await _baseClient.Execute<KucoinWithdrawalQuota>(_baseClient.GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string toAddress, decimal quantity, string? memo = null, bool? isInner = null, string? remark = null, string? network = null, FeeDeductType? deductType = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            toAddress.ValidateNotNull(nameof(toAddress));
            var parameters = new Dictionary<string, object> {
                { "currency", asset },
                { "address", toAddress },
                { "amount", quantity },
            };
            parameters.AddOptionalParameter("memo", memo);
            parameters.AddOptionalParameter("isInner", isInner);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", network);
            parameters.AddOptionalParameter("feeDeductType", EnumConverter.GetString(deductType));
            return await _baseClient.Execute<KucoinNewWithdrawal>(_baseClient.GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<object>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            withdrawalId.ValidateNotNull(nameof(withdrawalId));
            return await _baseClient.Execute<object>(_baseClient.GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinMarginAccount>(_baseClient.GetUri($"margin/account"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinRiskLimitCrossMargin>>> GetRiskLimitCrossMarginAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinRiskLimitCrossMargin>>(_baseClient.GetUri($"risk/limit/strategy"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinRiskLimitIsolatedMargin>>> GetRiskLimitIsolatedMarginAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("marginModel", "isolated");
            return await _baseClient.Execute<IEnumerable<KucoinRiskLimitIsolatedMargin>>(_baseClient.GetUri($"risk/limit/strategy"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIsolatedMarginAccountsInfo>> GetIsolatedMarginAccountsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinIsolatedMarginAccountsInfo>(_baseClient.GetUri($"isolated/accounts"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(string symbol, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinIsolatedMarginAccount>(_baseClient.GetUri($"isolated/account/{symbol}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinToken>(_baseClient.GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }
    }
}