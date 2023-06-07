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

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class KucoinClientFuturesApiTrading : IKucoinClientFuturesApiTrading
    {
        private readonly KucoinClientFuturesApi _baseClient;

        internal KucoinClientFuturesApiTrading(KucoinClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
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

            return await _baseClient.Execute<KucoinNewOrder>(_baseClient.GetUri("orders", 1), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("orders/" + orderId, 1), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("orders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllStopOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("stopOrders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new OrderStatusConverter(false)));
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinFuturesOrder>>(_baseClient.GetUri("orders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinFuturesOrder>>(_baseClient.GetUri("stopOrders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<IEnumerable<KucoinFuturesOrder>>(_baseClient.GetUri("recentDoneOrders"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinFuturesOrder>(_baseClient.GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            return await _baseClient.Execute<KucoinFuturesOrder>(_baseClient.GetUri("orders/byClientOid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinFuturesUserTrade>>(_baseClient.GetUri("fills"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinFuturesUserTrade>>(_baseClient.GetUri("recentFills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        #endregion

    }
}
