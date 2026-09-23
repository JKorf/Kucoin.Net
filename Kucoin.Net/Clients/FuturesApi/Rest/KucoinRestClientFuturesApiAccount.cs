using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects.Models.Spot;
using System.Linq;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class KucoinRestClientFuturesApiAccount : IKucoinRestClientFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientFuturesApi _baseClient;

        internal KucoinRestClientFuturesApiAccount(KucoinRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Account
        /// <inheritdoc />
        public async Task<HttpResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/account-overview", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinAccountOverview>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.Add("type", type);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/transaction-history", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinAccountTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Positions

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPosition[]>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v2/position", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPosition[]>> GetPositionsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/positions", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinPositionHistoryItem>>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("from", startTime);
            parameters.Add("to", endTime);
            parameters.Add("limit", limit);
            parameters.Add("pageId", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/history-positions", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinPositionHistoryItem>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Toggle Auto Deposit Margin

        /// <inheritdoc />
        public async Task<HttpResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/position/margin/auto-deposit-status", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Add Margin

        /// <inheritdoc />
        public async Task<HttpResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("margin", quantity.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/position/margin/deposit-margin", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Remove Margin

        /// <inheritdoc />
        public async Task<HttpResult> RemoveMarginAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("withdrawAmount", quantity.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/margin/withdrawMargin", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Funding fees

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/funding-history", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinFundingItem>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Open order value
        /// <inheritdoc />
        public async Task<HttpResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/openOrderStatistics", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<KucoinOrderValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limit Level
        /// <inheritdoc />
        public async Task<HttpResult<Objects.Models.Futures.KucoinFuturesRiskLimit[]>> GetRiskLimitLevelAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/contracts/risk-limit/" + symbol, KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<Objects.Models.Futures.KucoinFuturesRiskLimit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Risk Limit Level
        /// <inheritdoc />
        public async Task<HttpResult<bool>> SetRiskLimitLevelAsync(string symbol, int level, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("level", level);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/position/risk-limit-level/change", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync<bool>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Max Withdraw Margin
        /// <inheritdoc />
        public async Task<HttpResult<decimal>> GetMaxWithdrawMarginAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/margin/maxWithdrawMargin", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<decimal>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fee
        /// <inheritdoc />
        public async Task<HttpResult<KucoinTradeFee>> GetTradingFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/trade-fees", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            return await _baseClient.SendAsync<KucoinTradeFee>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Mode

        /// <inheritdoc />
        public async Task<HttpResult<KucoinMarginMode>> GetMarginModeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/position/getMarginMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinMarginMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<HttpResult<KucoinMarginMode>> SetMarginModeAsync(string symbol, FuturesMarginMode marginMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginMode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/position/changeMarginMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinMarginMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Margin Modes

        /// <inheritdoc />
        public async Task<HttpResult<KucoinMarginModes>> SetMarginModesAsync(IEnumerable<string> symbols, FuturesMarginMode marginMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbols", symbols.ToArray());
            parameters.Add("marginMode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/position/batchChangeMarginMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinMarginModes>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Leverage

        /// <inheritdoc />
        public async Task<HttpResult<KucoinLeverage>> GetCrossMarginLeverageAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/getCrossUserLeverage", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Cross Margin Leverage

        /// <inheritdoc />
        public async Task<HttpResult> SetCrossMarginLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/changeCrossUserLeverage", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Risk Limit

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCrossMarginRiskLimit[]>> GetCrossMarginRiskLimitAsync(string symbol, decimal? totalMargin = null, int? leverage = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("totalMargin", totalMargin);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/batchGetCrossOrderLimit", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinCrossMarginRiskLimit[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Requirement

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCrossMarginRequirement>> GetCrossMarginRequirementAsync(string symbol, decimal positionValue, int? leverage = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("positionValue", positionValue);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/getCrossModeMarginRequirement", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinCrossMarginRequirement>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPositionMode>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/position/getPositionMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinPositionMode>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("positionMode", positionMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v2/position/switchPositionMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Websocket token

        internal async Task<HttpResult<KucoinToken>> GetWebsocketTokenPublicAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/bullet-public", KucoinExchange.RateLimiter.PublicRest, 10, false);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

        internal async Task<HttpResult<KucoinToken>> GetWebsocketTokenPrivateAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v1/bullet-private", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
