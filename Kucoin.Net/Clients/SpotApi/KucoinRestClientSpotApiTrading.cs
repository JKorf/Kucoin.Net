using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Kucoin.Net.ExtensionMethods;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiTrading : IKucoinRestClientSpotApiTrading
    {
        private readonly KucoinRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientSpotApiTrading(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            switch (type)
            {
                case NewOrderType.Limit when !quantity.HasValue:
                    throw new ArgumentException("Limit order needs a quantity");
                case NewOrderType.Limit when !price.HasValue:
                    throw new ArgumentException("Limit order needs a price");
                case NewOrderType.Market when !quantity.HasValue && !quoteQuantity.HasValue:
                    throw new ArgumentException("Market order needs quantity or quoteQuantity specified");
                case NewOrderType.Market when quantity.HasValue && quoteQuantity.HasValue:
                    throw new ArgumentException("Market order cant have both quantity and quoteQuantity specified");
            }

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce.Value, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceTestOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            string? clientOrderId = null,
            SelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            switch (type)
            {
                case NewOrderType.Limit when !quantity.HasValue:
                    throw new ArgumentException("Limit order needs a quantity");
                case NewOrderType.Limit when !price.HasValue:
                    throw new ArgumentException("Limit order needs a price");
                case NewOrderType.Market when !quantity.HasValue && !quoteQuantity.HasValue:
                    throw new ArgumentException("Market order needs quantity or quoteQuantity specified");
                case NewOrderType.Market when quantity.HasValue && quoteQuantity.HasValue:
                    throw new ArgumentException("Market order cant have both quantity and quoteQuantity specified");
            }

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce.Value, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders/test", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewMarginOrder>> PlaceMarginOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            MarginMode? marginMode = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            switch (type)
            {
                case NewOrderType.Limit when !quantity.HasValue:
                    throw new ArgumentException("Limit order needs a quantity");
                case NewOrderType.Market when !quantity.HasValue && !quoteQuantity.HasValue:
                    throw new ArgumentException("Market order needs quantity or quoteQuantity specified");
                case NewOrderType.Market when quantity.HasValue && quoteQuantity.HasValue:
                    throw new ArgumentException("Market order cant have both quantity and quoteQuantity specified");
            }

            if (marginMode.HasValue && marginMode.Value != MarginMode.CrossMode)
                throw new ArgumentException("Currently, the platform only supports the cross mode");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce.Value, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("marginMode", marginMode.HasValue ? JsonConvert.SerializeObject(marginMode.Value, new MarginModeConverter(false)) : null);
            parameters.AddOptionalParameter("autoBorrow", autoBorrow);
            parameters.AddOptionalParameter("autoRepay", autoRepay);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/margin/order", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinNewMarginOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewMarginOrder>> PlaceTestMarginOrderAsync(
            string symbol,
            Enums.OrderSide side,
            NewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            MarginMode? marginMode = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            switch (type)
            {
                case NewOrderType.Limit when !quantity.HasValue:
                    throw new ArgumentException("Limit order needs a quantity");
                case NewOrderType.Market when !quantity.HasValue && !quoteQuantity.HasValue:
                    throw new ArgumentException("Market order needs quantity or quoteQuantity specified");
                case NewOrderType.Market when quantity.HasValue && quoteQuantity.HasValue:
                    throw new ArgumentException("Market order cant have both quantity and quoteQuantity specified");
            }

            if (marginMode.HasValue && marginMode.Value != MarginMode.CrossMode)
                throw new ArgumentException("Currently, the platform only supports the cross mode");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce.Value, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("marginMode", marginMode.HasValue ? JsonConvert.SerializeObject(marginMode.Value, new MarginModeConverter(false)) : null);
            parameters.AddOptionalParameter("autoBorrow", autoBorrow);
            parameters.AddOptionalParameter("autoRepay", autoRepay);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/margin/order/test", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinNewMarginOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceOcoOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            decimal price,
            decimal stopPrice,
            decimal limitPrice,
            TradeType? tradeType = null,
            string? remark = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddString("price", price);
            parameters.AddString("size", quantity);
            parameters.AddString("stopPrice", stopPrice);
            parameters.AddString("limitPrice", limitPrice);
            parameters.AddOptional("remark", remark);
            parameters.AddOptionalEnum("tradeType", tradeType);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/oco/order", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinBulkOrderResponse>> PlaceBulkOrderAsync(string symbol, IEnumerable<KucoinBulkOrderRequestEntry> orders, CancellationToken ct = default)
        {
            var orderList = orders.ToList();
            if (!orderList.Any())
                throw new ArgumentException("There should be at least one order in the bulk order");
            if (orderList.Count() > 5)
                throw new ArgumentException("There should be no more than 5 orders in the bulk order");
            if (orderList.Any(o => o.Type != NewOrderType.Limit))
                throw new ArgumentException("Only limit orders can be part of a bulk order");
            if (orderList.Any(o => o.TradeType != null && o.TradeType != TradeType.SpotTrade))
                throw new ArgumentException("Only spot orders can be part of a bulk order");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "orderList", orderList }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/orders/multi", KucoinExchange.RateLimiter.SpotRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinBulkOrderResponse>(request, parameters, ct).ConfigureAwait(false);
            if (result)
            {
                foreach (var order in result.Data.Orders.Where(o => o.Status == BulkOrderCreationStatus.Success))
                {
                    _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = order, Id = order.Id });
                }
            }
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = orderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOcoOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/oco/order/{orderId}", KucoinExchange.RateLimiter.SpotRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = orderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOcoOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "orderIds", string.Join(",", orderIds) }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/oco/orders", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/order/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinCanceledOrder>(request, null, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = clientOrderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOcoOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/oco/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = clientOrderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, TradeType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("tradeType", EnumConverter.GetString(type));
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/orders", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string? symbol = null, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, Enums.OrderStatus? status = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new OrderStatusConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/orders", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinOcoOrder>>> GetOcoOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptional("orderIds", orderIds == null ? null: string.Join(",", orderIds));
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/oco/orders", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinOcoOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinOrder>>> GetRecentOrdersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/limit/orders", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinOrder>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/order/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOcoOrder>> GetOcoOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/oco/order/{orderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOcoOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOcoOrder>> GetOcoOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/oco/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOcoOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOcoOrderDetails>> GetOcoOrderDetailsAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/oco/order/details/{orderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOcoOrderDetails>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinUserTrade>>> GetUserTradesAsync(string? symbol = null, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            if (endTime.HasValue && startTime.HasValue && (endTime.Value - startTime.Value).TotalDays > 7)
                throw new ArgumentException("Difference between start and end time can be a maximum of 1 week");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/fills", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinUserTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/limit/fills", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinUserTrade>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> PlaceStopOrderAsync(
            string symbol,
            Enums.OrderSide orderSide,
            NewOrderType orderType,
            StopCondition stopCondition,
            decimal stopPrice,
            string? remark = null,
            SelfTradePrevention? selfTradePrevention = null,
            TradeType? tradeType = null,

            decimal? price = null,
            decimal? quantity = null,
            TimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? clientOrderId = null,
            decimal? quoteQuantity = null,
            CancellationToken ct = default)
        {
            if (orderType == NewOrderType.Limit && quoteQuantity != null)
                throw new ArgumentException("QuoteQuantity can only be provided for a market order", nameof(quoteQuantity));

            if ((price.HasValue || timeInForce.HasValue || cancelAfter.HasValue || postOnly.HasValue || hidden.HasValue || iceberg.HasValue || visibleSize.HasValue)
                && orderType == NewOrderType.Market)
            {
                throw new ArgumentException("Invalid parameter(s) provided for market order type");
            }

            if (stopCondition == StopCondition.None)
                throw new ArgumentException("Invalid stop condition", nameof(stopCondition));

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() },
                { "side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(orderType, new NewOrderTypeConverter(false)) },
                { "stop", EnumConverter.GetString(stopCondition) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
            };

            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention, new SelfTradePreventionConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString(CultureInfo.InvariantCulture));

            parameters.AddOptionalParameter("funds", quoteQuantity);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/stop-order", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/stop-order/" + orderId, KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrder>> CancelStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "clientOid", clientOrderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/stop-order/cancelOrderByClientOid", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinCanceledOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, TradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderIds", orderIds == null ? null : string.Join(",", orderIds));
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/stop-order/cancel", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinCanceledOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinStopOrder>>> GetStopOrdersAsync(bool? activeOrders = null, string? symbol = null, Enums.OrderSide? side = null,
            Enums.OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, IEnumerable<string>? orderIds = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("status", activeOrders.HasValue ? activeOrders == true ? "active" : "done" : null);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("orderIds", orderIds == null ? null : string.Join(",", orderIds));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/stop-order", KucoinExchange.RateLimiter.SpotRest, 8, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinStopOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinStopOrder>> GetStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/stop-order/" + orderId, KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<KucoinStopOrder>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinStopOrder>>> GetStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "clientOid", clientOrderId }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/stop-order/queryOrderByClientOid", KucoinExchange.RateLimiter.SpotRest, 3, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinStopOrder>>(request, parameters, ct).ConfigureAwait(false);
        }
    }
}