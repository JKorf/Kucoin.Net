using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models.Unified;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    /// <inheritdoc />
    internal class KucoinRestClientUnifiedApiTrading : IKucoinRestClientUnifiedApiTrading
    {
        private readonly KucoinRestClientUnifiedApi _baseClient;
        private readonly ILogger _logger;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientUnifiedApiTrading(ILogger logger, KucoinRestClientUnifiedApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        private void LogBetaWarning()
        {
            if (_baseClient.ClientOptions.DisableUnifiedProductionWarning)
                return;

            _logger.LogWarning("The Kucoin UTA/Unified API is currently in BETA phase and should not be used in the production" +
                " as things might be changed and/or break without prior notice." +
                " To disable this warning set `DisableUnifiedProductionWarning` to true in the REST client options.");
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrderResult>> PlaceOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType,
            string symbol,
            OrderSide side,
            OrderType orderType,
            decimal quantity,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            QuantityUnit? quantityUnit = null,
            string? clientOrderId = null,
            bool? postOnly = null,
            bool? reduceOnly = null,
            SelfTradePrevention? stpMode = null,
            long? cancelAfter = null,
            decimal? triggerPrice = null,
            StopType? triggerDirection = null,
            StopPriceType? triggerPriceType = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            PositionSide? positionSide = null,
            MarginMode? marginMode = null,
            decimal? leverage = null,
            StopPriceType? tpTriggerPriceType = null,
            decimal? tpTriggerPrice = null,
            StopPriceType? slTriggerPriceType = null,
            decimal? slTriggerPrice = null,
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("orderType", orderType);
            parameters.Add("size", quantity);
            parameters.Add("sizeUnit", quantityUnit);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("clientOid", clientOrderId);
            parameters.Add("postOnly", postOnly);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.Add("stp", stpMode);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("triggerPriceType", triggerPriceType);
            parameters.Add("triggerDirection", triggerDirection);
            parameters.Add("cancelAfter", cancelAfter);
            parameters.Add("autoBorrow", autoBorrow);
            parameters.Add("autoRepay", autoRepay);
            parameters.Add("positionSide", positionSide);
            parameters.Add("marginMode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.Add("tpTriggerPriceType", tpTriggerPriceType);
            parameters.Add("tpTriggerPrice", tpTriggerPrice);
            parameters.Add("slTriggerPriceType", slTriggerPriceType);
            parameters.Add("slTriggerPrice", slTriggerPrice);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/place?tradeType={EnumConverter.GetString(accountType)}",
                KucoinExchange.RateLimiter.UnifiedRest,
                1,
                true);
            var result = await _baseClient.SendAsync<KucoinUaOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrderResult>> CancelOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null, 
            string? orderId = null,
            string? clientOrderId = null, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOid", clientOrderId);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/cancel?tradeType={EnumConverter.GetString(accountType)}",
                KucoinExchange.RateLimiter.UnifiedRest, 
                1, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaBatchCancelResult>> CancelOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            IEnumerable<KucoinUaCancelOrderRequest> orders, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("cancelOrderList", orders.ToArray());
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/cancel-batch?tradeType={EnumConverter.GetString(accountType)}",
                KucoinExchange.RateLimiter.UnifiedRest,
                4,
                true);
            var result = await _baseClient.SendAsync<KucoinUaBatchCancelResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Symbol Orders

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaBatchCancelResult>> CancelSymbolOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedSimpleAccountType accountType, 
            string symbol,
            MarginMode? marginMode = null, 
            OrderFilter? orderFilter = null, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("marginMode", marginMode);
            parameters.Add("orderFilter", orderFilter);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/cancel-all",
                KucoinExchange.RateLimiter.UnifiedRest,
                20, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaBatchCancelResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrder>> GetOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string symbol, 
            string? orderId = null, 
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOid", clientOrderId);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/detail", 
                KucoinExchange.RateLimiter.UnifiedRest, 
                4,
                true);
            var result = await _baseClient.SendAsync<KucoinUaOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrders>> GetOpenOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null,
            OrderFilter? orderFilter = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("orderFilter", orderFilter);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("pageNumber", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/open-list", 
                KucoinExchange.RateLimiter.UnifiedRest,
                4, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaOrders>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrderHistory>> GetOrderHistoryAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null, 
            OrderSide? side = null,
            OrderFilter? orderFilter = null, 
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            long? lastId = null, 
            int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("orderFilter", orderFilter);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("lastId", lastId);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/history", 
                KucoinExchange.RateLimiter.UnifiedRest, 
                4,
                true);
            var result = await _baseClient.SendAsync<KucoinUaOrderHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaUserTrades>> GetUserTradesAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null,
            long? orderId = null,
            OrderSide? orderSide = null,
            DateTime? startTime = null,
            DateTime? endTime = null, 
            long? lastId = null, 
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("side", orderSide);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("lastId", lastId);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/order/execution",
                KucoinExchange.RateLimiter.UnifiedRest,
                4, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaUserTrades>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Dcp

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaDcp>> SetDcpAsync(UnifiedSimpleAccountType tradeType, long timeout, string? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", tradeType);
            parameters.Add("timeout", timeout);
            parameters.Add("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/ua/v1/dcp/set", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaDcp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Dcp

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaDcp>> GetDcpAsync(UnifiedSimpleAccountType tradeType, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/dcp/query", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaDcp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaPosition[]>> GetPositionsAsync(UnifiedAccountMode accountMode, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("page", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/position/open-list",
                KucoinExchange.RateLimiter.UnifiedRest,
                3, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position History

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaPositionHistory>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("lastId", lastId);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/position/history", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaPositionHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Tiers

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaPositionTier[]>> GetPositionTiersAsync(
            UnifiedAccountMode accountMode,
            IEnumerable<string> symbols,
            UnifiedSimpleAccountType? tradeType = null,
            MarginMode? marginMode = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("symbol", symbols.ToArray());
            parameters.Add("tradeType", tradeType);
            parameters.Add("marginMode", marginMode);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
                _baseClient.BaseAddress,
                $"/api/ua/v1/{EnumConverter.GetString(accountMode).ToLower()}/position/tiers",
                KucoinExchange.RateLimiter.ManagementRest,
                20, 
                true);
            var result = await _baseClient.SendAsync<KucoinUaPositionTier[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
