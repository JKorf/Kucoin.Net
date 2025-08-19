using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Linq;
using CryptoExchange.Net.Objects.Errors;

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
            decimal? leverage = null,
            int? quantity = null,

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
            FuturesMarginMode? marginMode = null,
            decimal? quantityInBaseAsset = null,
            decimal? quantityInQuoteAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("leverage", leverage?.ToString(CultureInfo.InvariantCulture));

            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("qty", quantityInBaseAsset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("valueQty", quantityInQuoteAsset?.ToString(CultureInfo.InvariantCulture));

            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalEnum("stop", stopType);
            parameters.AddOptionalEnum("stopPriceType", stopPriceType);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());
            parameters.AddOptionalEnum("stp", selfTradePrevention);
            parameters.AddOptionalEnum("marginMode", marginMode);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceTestOrderAsync(
            string symbol,
            OrderSide side,
            NewOrderType type,
            decimal? leverage = null,
            int? quantity = null,

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
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("leverage", leverage?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalEnum("stop", stopType);
            parameters.AddOptionalEnum("stopPriceType", stopPriceType);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("timeInForce", timeInForce);
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
            OrderSide? side,
            NewOrderType type,
            decimal? leverage = null,
            int? quantity = null,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            decimal? triggerStopUpPrice = null,
            decimal? triggerStopDownPrice = null,
            StopPriceType? stopPriceType = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,

            decimal? quantityInBaseAsset = null,
            decimal? quantityInQuoteAsset = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("leverage", leverage?.ToString(CultureInfo.InvariantCulture));

            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("qty", quantityInBaseAsset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("valueQty", quantityInQuoteAsset?.ToString(CultureInfo.InvariantCulture));

            parameters.AddParameter("clientOid", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("remark", remark);

            parameters.AddOptionalString("triggerStopUpPrice", triggerStopUpPrice);
            parameters.AddOptionalEnum("stopPriceType", stopPriceType);
            parameters.AddOptionalString("triggerStopDownPrice", triggerStopDownPrice);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());
            parameters.AddOptionalEnum("stp", selfTradePrevention);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/st-orders", KucoinExchange.RateLimiter.FuturesRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<KucoinFuturesOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<KucoinFuturesOrderRequestEntry> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.SetBody(orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders/multi", KucoinExchange.RateLimiter.FuturesRest, 20, true);
            var resultData = await _baseClient.SendAsync<KucoinFuturesOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<KucoinFuturesOrderResult>[]>(default);

            var result = new List<CallResult<KucoinFuturesOrderResult>>();
            foreach (var item in resultData.Data)
            {
                if (item.Message != "success")
                    result.Add(new CallResult<KucoinFuturesOrderResult>(item, null, new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message))));
                else
                    result.Add(new CallResult<KucoinFuturesOrderResult>(item));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders/" + orderId, KucoinExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrderResult[]>> CancelMultipleOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, IEnumerable<KucoinCancelRequest>? clientOrderIds = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderIdsList", orderIds?.ToArray());
            parameters.AddOptional("clientOidsList", clientOrderIds?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders/multi-cancel", KucoinExchange.RateLimiter.FuturesRest, 30, true, parameterPosition: HttpMethodParameterPosition.InBody);
            return await _baseClient.SendAsync<KucoinFuturesOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
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
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/orders", KucoinExchange.RateLimiter.FuturesRest, 30, true);
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
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type );
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
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/stopOrders", KucoinExchange.RateLimiter.FuturesRest, 6, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder[]>> GetClosedOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/recentDoneOrders", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, IEnumerable<FuturesTradeType>? tradeTypes = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            if (tradeTypes?.Any() == true)
                parameters.Add("tradeTypes", string.Join(",", tradeTypes.Select(EnumConverter.GetString)));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/fills", KucoinExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinFuturesUserTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesUserTrade[]>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/recentFills", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            return await _baseClient.SendAsync<KucoinFuturesUserTrade[]>(request, null, ct).ConfigureAwait(false);
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
