using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Clients.Rest.Futures
{
    public class KucoinClientFuturesAccount: IKucoinClientFuturesAccount
    {
        private KucoinClientFutures _baseClient;

        internal KucoinClientFuturesAccount(KucoinClientFutures baseClient)
        {
            _baseClient = baseClient;
        }


        #region Account
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.Execute<KucoinAccountOverview>(_baseClient.GetUri("account-overview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? KucoinBaseClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? KucoinBaseClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new TransactionTypeConverter(false)));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<KucoinPaginatedSlider<KucoinAccountTransaction>>(_baseClient.GetUri("transaction-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Deposit
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.Execute<KucoinDepositAddress>(_baseClient.GetUri("deposit-address"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? KucoinBaseClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? KucoinBaseClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new DepositStatusConverter(false)));
            return await _baseClient.Execute<KucoinPaginated<KucoinDeposit>>(_baseClient.GetUri("deposit-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            return await _baseClient.Execute<KucoinFuturesWithdrawalQuota>(_baseClient.GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isInner", isInner?.ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", chain);
            parameters.AddOptionalParameter("memo", memo);
            return await _baseClient.Execute<KucoinNewWithdrawal>(_baseClient.GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? KucoinBaseClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? KucoinBaseClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)));
            return await _baseClient.Execute<KucoinPaginated<KucoinWithdrawal>>(_baseClient.GetUri("withdrawal-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            return await _baseClient.Execute(_baseClient.GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }
        #endregion

        #region Transfer
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string asset, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.Execute<KucoinTransferResult>(_baseClient.GetUri("transfer-out", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? KucoinBaseClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? KucoinBaseClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinTransfer>>(_baseClient.GetUri("transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("applyId", applyId);
            return await _baseClient.Execute(_baseClient.GetUri("cancel/transfer-out", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion


        #region Positions

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinPosition>(_baseClient.GetUri("position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinPosition>>(_baseClient.GetUri("positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            return await _baseClient.Execute(_baseClient.GetUri("position/margin/auto-deposit-status"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("margin", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.Execute(_baseClient.GetUri("position/margin/deposit-margin"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Funding fees

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", startTime != null ? KucoinBaseClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? KucoinBaseClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<KucoinPaginatedSlider<KucoinFundingItem>>(_baseClient.GetUri("funding-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinOrderValuation>(_baseClient.GetUri("openOrderStatistics"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinToken>(_baseClient.GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }


    }
}
