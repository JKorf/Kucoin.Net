using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class KucoinRestClientSpotApiTrading : IKucoinRestClientSpotApiTrading
    {
        private readonly KucoinRestClientSpotApi _baseClient;

        internal KucoinRestClientSpotApiTrading(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
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
            symbol.ValidateKucoinSymbol();
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

            var parameters = new Dictionary<string, object>
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
            var result = await _baseClient.Execute<KucoinNewOrder>(_baseClient.GetUri("orders"), HttpMethod.Post, ct, parameters, true, weight: 4).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id });
            return result;
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
            SelfTradePrevention? selfTradePrevention = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
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

            var parameters = new Dictionary<string, object>
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
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            return await _baseClient.Execute<KucoinNewMarginOrder>(_baseClient.GetUri("margin/order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinBulkOrderResponse>> PlaceBulkOrderAsync(string symbol, IEnumerable<KucoinBulkOrderRequestEntry> orders, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            var orderList = orders.ToList();
            if (!orderList.Any())
                throw new ArgumentException("There should be at least one order in the bulk order");
            if (orderList.Count() > 5)
                throw new ArgumentException("There should be no more than 5 orders in the bulk order");
            if (orderList.Any(o => o.Type != NewOrderType.Limit))
                throw new ArgumentException("Only limit orders can be part of a bulk order");
            if (orderList.Any(o => o.TradeType != null && o.TradeType != TradeType.SpotTrade))
                throw new ArgumentException("Only spot orders can be part of a bulk order");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "orderList", orderList }
            };

            var result = await _baseClient.Execute<KucoinBulkOrderResponse>(_baseClient.GetUri("orders/multi"), HttpMethod.Post, ct, parameters, true, weight: 60, parameterPosition: HttpMethodParameterPosition.InBody).ConfigureAwait(false);
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
            var result = await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri($"orders/{orderId}"), HttpMethod.Delete, ct, signed: true, weight: 3).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = orderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrder>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var result = await _baseClient.Execute<KucoinCanceledOrder>(_baseClient.GetUri($"order/client-order/{clientOrderId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = clientOrderId });
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOrdersAsync(string? symbol = null, TradeType? type = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("tradeType", type);
            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("orders"), HttpMethod.Delete, ct, parameters, true, weight: 60).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string? symbol = null, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, Enums.OrderStatus? status = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new OrderStatusConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<KucoinOrder>>(_baseClient.GetUri("orders"), HttpMethod.Get, ct, parameters: parameters, signed: true, weight: 6).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinOrder>>> GetRecentOrdersAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinOrder>>(_baseClient.GetUri("limit/orders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            return await _baseClient.Execute<KucoinOrder>(_baseClient.GetUri($"order/client-order/{clientOrderId}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            return await _baseClient.Execute<KucoinOrder>(_baseClient.GetUri($"orders/{orderId}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>> GetHistoricalOrdersAsync(string? symbol = null, Enums.OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<KucoinHistoricalOrder>>(_baseClient.GetUri("hist-orders"), HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinUserTrade>>> GetUserTradesAsync(string? symbol = null, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            if (endTime.HasValue && startTime.HasValue && (endTime.Value - startTime.Value).TotalDays > 7)
                throw new ArgumentException("Difference between start and end time can be a maximum of 1 week");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<KucoinUserTrade>>(_baseClient.GetUri("fills"), HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinUserTrade>>(_baseClient.GetUri("limit/fills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewOrder>> PlaceStopOrderAsync(
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

            var parameters = new Dictionary<string, object>
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

            return await _baseClient.Execute<KucoinNewOrder>(_baseClient.GetUri("stop-order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("stop-order/" + orderId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrder>> CancelStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientOid", clientOrderId }
            };
            return await _baseClient.Execute<KucoinCanceledOrder>(_baseClient.GetUri("stop-order/cancelOrderByClientOid"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelStopOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, TradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderIds", orderIds == null ? null : string.Join(",", orderIds));
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            return await _baseClient.Execute<KucoinCanceledOrders>(_baseClient.GetUri("stop-order/cancel"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinStopOrder>>> GetStopOrdersAsync(bool? activeOrders = null, string? symbol = null, Enums.OrderSide? side = null,
            Enums.OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, IEnumerable<string>? orderIds = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
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

            return await _baseClient.Execute<KucoinPaginated<KucoinStopOrder>>(_baseClient.GetUri("stop-order"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinStopOrder>> GetStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinStopOrder>(_baseClient.GetUri("stop-order/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinStopOrder>>> GetStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientOid", clientOrderId }
            };
            return await _baseClient.Execute<IEnumerable<KucoinStopOrder>>(_baseClient.GetUri("stop-order/queryOrderByClientOid"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewBorrowOrder>> PlaceBorrowOrderAsync(
            string asset,
            BorrowOrderType type,
            decimal quantity,
            decimal? maxRate = null,
            string? term = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "type", JsonConvert.SerializeObject(type, new BorrowOrderTypeConverter(false)) },
                { "size", quantity }
            };
            parameters.AddOptionalParameter("maxRate", maxRate);
            parameters.AddOptionalParameter("term", term);
            return await _baseClient.Execute<KucoinNewBorrowOrder>(_baseClient.GetUri("margin/borrow"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewBorrow>> MarginBorrowAsync(string asset, decimal quantity, BorrowOrderType type, bool? isIsolated, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "size", quantity },
                { "timeInForce", JsonConvert.SerializeObject(type, new BorrowOrderTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinNewBorrow>(_baseClient.GetUri("margin/borrow", 3), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);

        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewRepay>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "size", quantity }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinNewRepay>(_baseClient.GetUri("margin/repay", 3), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<MarginBorrowHistory>>> MarginBorrowHistoryAsync(string asset, bool? isIsolated, string? symbol = null, string? Id = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {

            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
            };

            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderNo", Id);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", page);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<MarginBorrowHistory>>(_baseClient.GetUri("margin/borrow", 3), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);

        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<MarginRepayHistory>>> MarginRepayHistoryAsync(string asset, bool? isIsolated, string? symbol = null, string? Id = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {

            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
            };

            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderNo", Id);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("currentPage", page);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await _baseClient.Execute<KucoinPaginated<MarginRepayHistory>>(_baseClient.GetUri("margin/repay", 3), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);

        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinBorrowOrder>> GetBorrowOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var parameters = new Dictionary<string, object>
            {
                { "orderId", orderId }
            };
            return await _baseClient.Execute<KucoinBorrowOrder>(_baseClient.GetUri($"margin/borrow"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RepaySingleBorrowOrderAsync(string asset, string tradeId, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "tradeId", tradeId },
                { "size", quantity }
            };

            return await _baseClient.Execute(_baseClient.GetUri("margin/repay/single"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinRepayRecord>>> GetOpenBorrowRecordsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinRepayRecord>>(_baseClient.GetUri($"margin/borrow/outstanding"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinBorrowRecord>>> GetClosedBorrowRecordsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinBorrowRecord>>(_baseClient.GetUri($"margin/borrow/repaid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RepayAllAsync(string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "currency", asset },
                { "sequence", EnumConverter.GetString(strategy) },
                { "size", quantity }
            };
            return await _baseClient.Execute(_baseClient.GetUri($"margin/repay/all"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewOrder>> PlaceLendOrderAsync(string asset, decimal quantity, decimal dailyInterestRate, int term, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "currency", asset },
                { "dailyIntRate", dailyInterestRate },
                { "size", quantity },
                { "term", term },
            };
            return await _baseClient.Execute<KucoinNewOrder>(_baseClient.GetUri($"margin/lend"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelLendOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await _baseClient.Execute(_baseClient.GetUri($"margin/lend/{orderId}"), HttpMethod.Delete, ct, null, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> SetAutoLendAsync(string asset, bool isEnabled, decimal? retainQuantity = null, decimal? dailyInterestRate = null, int? term = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "currency", asset },
                { "isEnable", isEnabled }
            };
            parameters.AddOptionalParameter("term", term);
            parameters.AddOptionalParameter("dailyIntRate", dailyInterestRate);
            parameters.AddOptionalParameter("retainSize", retainQuantity);
            return await _baseClient.Execute(_baseClient.GetUri($"margin/toggle-auto-lend"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetOpenLendOrdersAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinLendOrder>>(_baseClient.GetUri($"margin/lend/active"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetClosedLendOrdersAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinLendOrder>>(_baseClient.GetUri($"margin/lend/done"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinOpenLend>>> GetOpenLendsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinOpenLend>>(_baseClient.GetUri($"margin/lend/trade/unsettled"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinClosedLend>>> GetClosedLendsAsync(string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinClosedLend>>(_baseClient.GetUri($"margin/lend/trade/settled"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinLendHistory>>> GetLendingStatusAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await _baseClient.Execute<IEnumerable<KucoinLendHistory>>(_baseClient.GetUri($"margin/lend/assets"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewIsolatedBorrowOrder>> PlaceIsolatedBorrowOrderAsync(
            string symbol,
            string asset,
            decimal quantity,
            BorrowOrderType type,

            decimal? maxRate = null,
            string? term = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "currency", asset },
                { "size", quantity },
                { "borrowStrategy", JsonConvert.SerializeObject(type, new BorrowOrderTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("maxRate", maxRate);
            parameters.AddOptionalParameter("term", term);
            return await _baseClient.Execute<KucoinNewIsolatedBorrowOrder>(_baseClient.GetUri("isolated/borrow"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinIsolatedOpenBorrowRecord>>> GetIsolatedOpenBorrowRecordsAsync(string? symbol = null, string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinIsolatedOpenBorrowRecord>>(_baseClient.GetUri($"isolated/borrow/outstanding"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinIsolatedClosedBorrowRecord>>> GetIsolatedClosedBorrowRecordsAsync(string? symbol = null, string? asset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await _baseClient.Execute<KucoinPaginated<KucoinIsolatedClosedBorrowRecord>>(_baseClient.GetUri($"isolated/borrow/repaid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RepayAllIsolatedAsync(string symbol, string asset, RepaymentStrategy strategy, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "currency", asset },
                { "sequence", EnumConverter.GetString(strategy) },
                { "size", quantity }
            };
            return await _baseClient.Execute(_baseClient.GetUri($"isolated/repay/all"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> RepaySingleIsolatedBorrowOrderAsync(string symbol, string asset, decimal quantity, string loanId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "currency", asset },
                { "loanId", loanId },
                { "size", quantity }
            };

            return await _baseClient.Execute(_baseClient.GetUri("isolated/repay/single"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}