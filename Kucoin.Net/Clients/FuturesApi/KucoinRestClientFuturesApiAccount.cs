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
        public async Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/account-overview", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinAccountOverview>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/transaction-history", KucoinExchange.RateLimiter.ManagementRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinAccountTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Positions

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/position", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPosition>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/positions", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinPosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinPositionHistoryItem>>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalMilliseconds("from", startTime);
            parameters.AddOptionalMilliseconds("to", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("pageId", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/history-positions", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinPositionHistoryItem>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        /// <inheritdoc />
        public async Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/position/margin/auto-deposit-status", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("margin", quantity.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/position/margin/deposit-margin", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RemoveMarginAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("withdrawAmount", quantity.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/margin/withdrawMargin", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }


        #region Funding fees

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/funding-history", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinFundingItem>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Open order value
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/openOrderStatistics", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<KucoinOrderValuation>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Risk Limit Level
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<Objects.Models.Futures.KucoinRiskLimit>>> GetRiskLimitLevelAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contracts/risk-limit/" + symbol, KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<Objects.Models.Futures.KucoinRiskLimit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Risk Limit Level
        /// <inheritdoc />
        public async Task<WebCallResult<bool>> SetRiskLimitLevelAsync(string symbol, int level, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("level", level);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/position/risk-limit-level/change", KucoinExchange.RateLimiter.FuturesRest, 4, true);
            return await _baseClient.SendAsync<bool>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Max Withdraw Margin
        /// <inheritdoc />
        public async Task<WebCallResult<decimal>> GetMaxWithdrawMarginAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/margin/maxWithdrawMargin", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<decimal>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trading Fee
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTradeFee>> GetTradingFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/trade-fees", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            return await _baseClient.SendAsync<KucoinTradeFee>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Mode

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginMode>> GetMarginModeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/position/getMarginMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinMarginMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginMode>> SetMarginModeAsync(string symbol, FuturesMarginMode marginMode, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("marginMode", marginMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/position/changeMarginMode", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinMarginMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Cross Margin Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinLeverage>> GetCrossMarginLeverageAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v2/getCrossUserLeverage", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Cross Margin Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinLeverage>> SetCrossMarginLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v2/changeCrossUserLeverage", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Websocket token

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketTokenPublicAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/bullet-public", KucoinExchange.RateLimiter.PublicRest, 10, false);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketTokenPrivateAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/bullet-private", KucoinExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<KucoinToken>(request, null, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
