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
        public async Task<WebCallResult<KucoinUaOrderResult>> PlaceOrderAsync(
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

            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("orderType", orderType);
            parameters.AddString("size", quantity);
            parameters.AddOptionalEnum("sizeUnit", quantityUnit);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("clientOid", clientOrderId);
            parameters.AddOptional("postOnly", postOnly);
            parameters.AddOptional("reduceOnly", reduceOnly);
            parameters.AddOptionalEnum("stp", stpMode);
            parameters.AddOptionalString("triggerPrice", triggerPrice);
            parameters.AddOptionalEnum("triggerPriceType", triggerPriceType);
            parameters.AddOptionalEnum("triggerDirection", triggerDirection);
            parameters.AddOptional("cancelAfter", cancelAfter);
            parameters.AddOptional("autoBorrow", autoBorrow);
            parameters.AddOptional("autoRepay", autoRepay);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalEnum("marginMode", marginMode);
            parameters.AddOptionalString("leverage", leverage);
            parameters.AddOptionalEnum("tpTriggerPriceType", tpTriggerPriceType);
            parameters.AddOptionalString("tpTriggerPrice", tpTriggerPrice);
            parameters.AddOptionalEnum("slTriggerPriceType", slTriggerPriceType);
            parameters.AddOptionalString("slTriggerPrice", slTriggerPrice);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
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
        public async Task<WebCallResult<KucoinUaOrderResult>> CancelOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string? symbol = null, 
            string? orderId = null,
            string? clientOrderId = null, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("clientOid", clientOrderId);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post, 
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
        public async Task<WebCallResult<KucoinUaBatchCancelResult>> CancelOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            IEnumerable<KucoinUaCancelOrderRequest> orders, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddOptional("cancelOrderList", orders.ToArray());
            var request = _definitions.GetOrCreate(
                HttpMethod.Post,
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
        public async Task<WebCallResult<KucoinUaBatchCancelResult>> CancelSymbolOrdersAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string symbol,
            MarginMode? marginMode = null, 
            OrderFilter? orderFilter = null, 
            CancellationToken ct = default)
        {
            LogBetaWarning();

            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalEnum("marginMode", marginMode);
            parameters.AddOptionalEnum("orderFilter", orderFilter);
            var request = _definitions.GetOrCreate(
                HttpMethod.Post, 
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
        public async Task<WebCallResult<KucoinUaOrder>> GetOrderAsync(
            UnifiedAccountMode accountMode,
            UnifiedAccountType accountType, 
            string symbol, 
            string? orderId = null, 
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.Add("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("clientOid", clientOrderId);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
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
        public async Task<WebCallResult<KucoinUaOrders>> GetOpenOrdersAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("orderFilter", orderFilter);
            parameters.AddOptionalMillisecondsString("startAt", startTime);
            parameters.AddOptionalMillisecondsString("endAt", endTime);
            parameters.AddOptional("pageNumber", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
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
        public async Task<WebCallResult<KucoinUaOrderHistory>> GetOrderHistoryAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("orderFilter", orderFilter);
            parameters.AddOptionalMillisecondsString("startAt", startTime);
            parameters.AddOptionalMillisecondsString("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
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
        public async Task<WebCallResult<KucoinUaUserTrades>> GetUserTradesAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", accountType);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalEnum("side", orderSide);
            parameters.AddOptionalMillisecondsString("startAt", startTime);
            parameters.AddOptionalMillisecondsString("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
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
        public async Task<WebCallResult<KucoinUaDcp>> SetDcpAsync(UnifiedSimpleAccountType tradeType, long timeout, string? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", tradeType);
            parameters.Add("timeout", timeout);
            parameters.AddOptional("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/ua/v1/dcp/set", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaDcp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Dcp

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaDcp>> GetDcpAsync(UnifiedSimpleAccountType tradeType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", tradeType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/dcp/query", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaDcp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaPosition[]>> GetPositionsAsync(UnifiedAccountMode accountMode, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("page", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get, 
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
        public async Task<WebCallResult<KucoinUaPositionHistory>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalMillisecondsString("startAt", startTime);
            parameters.AddOptionalMillisecondsString("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/position/history", KucoinExchange.RateLimiter.UnifiedRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinUaPositionHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Tiers

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaPositionTier[]>> GetPositionTiersAsync(
            UnifiedAccountMode accountMode,
            IEnumerable<string> symbols,
            UnifiedSimpleAccountType? tradeType = null,
            MarginMode? marginMode = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddCommaSeparated("symbol", symbols.ToArray());
            parameters.AddOptionalEnum("tradeType", tradeType);
            parameters.AddOptionalEnum("marginMode", marginMode);
            var request = _definitions.GetOrCreate(
                HttpMethod.Get,
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
