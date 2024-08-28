using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Security.Cryptography;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class KucoinRestClientFuturesApiTrading : IKucoinRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientFuturesApi _baseClient;

        internal KucoinRestClientFuturesApiTrading(KucoinRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal leverage,
            int quantity,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)));
            parameters.AddParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", stopType != null ? JsonConvert.SerializeObject(stopType, new StopTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPriceType", stopPriceType != null ? JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce != null ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)): null);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());
            parameters.AddOptionalEnum("stp", selfTradePrevention);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceTestOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal leverage,
            int quantity,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)));
            parameters.AddParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", stopType != null ? JsonConvert.SerializeObject(stopType, new StopTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPriceType", stopPriceType != null ? JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce != null ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());
            parameters.AddOptionalEnum("stp", selfTradePrevention);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders/test", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceTpSlOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal leverage,
            int quantity,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            decimal? takeProfitPrice = null,
            decimal? stopLossPrice = null,
            StopPriceType? stopPriceType = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)));
            parameters.AddParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalString("triggerStopUpPrice", takeProfitPrice);
            parameters.AddOptionalParameter("stopPriceType", stopPriceType != null ? JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)) : null);
            parameters.AddOptionalString("triggerStopDownPrice", stopLossPrice);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce != null ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());
            parameters.AddOptionalEnum("stp", selfTradePrevention);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/st-orders", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesOrderResult>>> PlaceMultipleOrdersAsync(IEnumerable<KucoinFuturesOrderRequestEntry> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "<BODY>", orders }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders/multi", KucoinExchange.RateLimiter.FuturesRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesOrderResult>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders/" + orderId, KucoinExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders/client-order/" + clientOrderId, KucoinExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<KucoinCanceledOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders", KucoinExchange.RateLimiter.FuturesRest, 30, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllStopOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/stopOrders", KucoinExchange.RateLimiter.FuturesRest, 15, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new OrderStatusConverter(false)));
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/orders", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/stopOrders", KucoinExchange.RateLimiter.FuturesRest, 6, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/recentDoneOrders", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/orders/" + orderId, KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinFuturesOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/orders/byClientOid", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/fills", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinFuturesUserTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/recentFills", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesUserTrade>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Max Open Position Size

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMaxOpenSize>> GetMaxOpenPositionSizeAsync(string symbol, decimal price, decimal leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("price", price);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v2/getMaxOpenSize", KucoinExchange.RateLimiter.PublicRest, 2, true);
            return await _baseClient.SendAsync<KucoinMaxOpenSize>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
