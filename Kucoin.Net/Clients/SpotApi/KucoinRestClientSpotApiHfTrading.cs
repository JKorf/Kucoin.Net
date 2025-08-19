using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiHfTrading : IKucoinRestClientSpotApiHfTrading
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientSpotApi _baseClient;

        internal KucoinRestClientSpotApiHfTrading(KucoinRestClientSpotApi baseClient)
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
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalEnum("stp", selfTradePrevention);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrder>> PlaceOrderWaitAsync(
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
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalEnum("stp", selfTradePrevention);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/sync", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinHfOrder>(request, parameters, ct).ConfigureAwait(false);
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
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalEnum("stp", selfTradePrevention);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/test", KucoinExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<KucoinBulkMinimalResponseEntry>[]>> PlaceMultipleOrdersAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default)
        {
            var orderList = orders.ToList();
            if (!orderList.Any())
                throw new ArgumentException("There should be at least one order in the bulk order");
            if (orderList.Count() > 20)
                throw new ArgumentException("There should be no more than 20 orders in the bulk order");
            if (orderList.Any(o => o.Type != NewOrderType.Limit))
                throw new ArgumentException("Only limit orders can be part of a bulk order");

            var parameters = new ParameterCollection
            {
                { "orderList", orderList.ToArray() }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/multi", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var resultData = await _baseClient.SendAsync<KucoinBulkMinimalResponseEntry[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<KucoinBulkMinimalResponseEntry>[]>(default);

            var result = new List<CallResult<KucoinBulkMinimalResponseEntry>>();
            foreach (var item in resultData.Data)
            {
                if (!string.IsNullOrEmpty(item.Error))
                    result.Add(new CallResult<KucoinBulkMinimalResponseEntry>(item, null, new ServerError(ErrorInfo.Unknown with { Message = item.Error! })));
                else
                    result.Add(new CallResult<KucoinBulkMinimalResponseEntry>(item));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<KucoinHfBulkOrderResponse>[]>> PlaceMultipleOrdersWaitAsync(IEnumerable<KucoinHfBulkOrderRequestEntry> orders, CancellationToken ct = default)
        {
            var orderList = orders.ToList();
            if (!orderList.Any())
                throw new ArgumentException("There should be at least one order in the bulk order");
            if (orderList.Count() > 20)
                throw new ArgumentException("There should be no more than 20 orders in the bulk order");
            if (orderList.Any(o => o.Type != NewOrderType.Limit))
                throw new ArgumentException("Only limit orders can be part of a bulk order");

            var parameters = new ParameterCollection
            {
                { "orderList", orderList.ToArray() }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/multi/sync", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var resultData = await _baseClient.SendAsync<KucoinHfBulkOrderResponse[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData)
                return resultData.As<CallResult<KucoinHfBulkOrderResponse>[]>(default);

            var result = new List<CallResult<KucoinHfBulkOrderResponse>>();
            foreach (var item in resultData.Data)
            {
                if (!string.IsNullOrEmpty(item.ErrorMessage))
                    result.Add(new CallResult<KucoinHfBulkOrderResponse>(item, null, new ServerError(ErrorInfo.Unknown with { Message = item.ErrorMessage! })));
                else
                    result.Add(new CallResult<KucoinHfBulkOrderResponse>(item));
            }

            if (result.All(x => !x.Success))
                return resultData.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return resultData.As(result.ToArray());
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinModifiedOrder>> EditOrderAsync(
            string symbol,
            string? orderId = null,
            string? clientOrderId = null,
            decimal? newQuantity = null,
            decimal? newPrice = null,
            CancellationToken ct = default)
        {
            if (!newQuantity.HasValue && !newPrice.HasValue)
                throw new ArgumentException("Must choose order parameter to edit");
            
            if ((orderId is not null && clientOrderId is not null) || (orderId is null && clientOrderId is null))
                throw new ArgumentException("Must choose one order id");
            
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("newPrice", newPrice);
            parameters.AddOptionalParameter("newSize", newQuantity);
            
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/alter", KucoinExchange.RateLimiter.SpotRest, 1, true);
            return await _baseClient.SendAsync<KucoinModifiedOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrder>> CancelOrderWaitAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders/sync/{orderId}", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinHfOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinClientOrderId>> CancelOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinClientOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrder>> CancelOrderByClientOrderIdWaitAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders/sync/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 1, true);
            var result = await _baseClient.SendAsync<KucoinHfOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinHfOrderDetails>(request, parameters, ct).ConfigureAwait(false);
            if (result.Data == null)
                return result.AsError<KucoinHfOrderDetails>(new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Order not found")));
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinHfOrderDetails>(request, parameters, ct).ConfigureAwait(false);
            if (result.Data == null)
                return result.AsError<KucoinHfOrderDetails>(new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Order not found")));
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersBySymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledSymbols>> CancelAllOrdersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v1/hf/orders/cancelAll", KucoinExchange.RateLimiter.SpotRest, 30, true);
            return await _baseClient.SendAsync<KucoinCanceledSymbols>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/active", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinHfOrderDetails[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? Array.Empty<KucoinHfOrderDetails>());
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinHfOrderDetails>>> GetOpenOrdersV2Async(string symbol, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("pageNum", page);
            parameters.AddOptionalParameter("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/active/page", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinPaginated<KucoinHfOrderDetails>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result;

            if (result.Data.Items == null)
                result.Data.Items = new KucoinHfOrderDetails[0];

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOpenOrderSymbols>> GetSymbolsWithOpenOrdersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/active/symbols", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinOpenOrderSymbols>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/done", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinHfPaginated<KucoinHfOrderDetails>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? new KucoinHfPaginated<KucoinHfOrderDetails> { Items = Array.Empty<KucoinHfOrderDetails>(), LastId = 0 });
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetUserTradesAsync(string symbol, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, TradeType? tradeType = null, long? lastId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/fills", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinHfPaginated<KucoinUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? new KucoinHfPaginated<KucoinUserTrade> { Items = Array.Empty<KucoinUserTrade>(), LastId = 0 });
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCancelAfter>> CancelAfterAsync(TimeSpan cancelAfter, IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("timeout", (int)cancelAfter.TotalSeconds);
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v1/hf/orders/dead-cancel-all", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinCancelAfter>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCancelAfterStatus?>> GetCancelAfterStatusAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/hf/orders/dead-cancel-all/query", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinCancelAfterStatus?>(request, parameters, ct).ConfigureAwait(false);
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
            bool? isIsolated = null,
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

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptionalParameter("autoBorrow", autoBorrow);
            parameters.AddOptionalParameter("autoRepay", autoRepay);
            parameters.AddOptionalEnum("stp", selfTradePrevention);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/hf/margin/order", KucoinExchange.RateLimiter.SpotRest, 2, true);
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
            bool? isIsolated = null,
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

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", quoteQuantity);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptionalParameter("autoBorrow", autoBorrow);
            parameters.AddOptionalParameter("autoRepay", autoRepay);
            parameters.AddOptionalEnum("stp", selfTradePrevention);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/hf/margin/order/test", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinNewMarginOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderId>> CancelMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/hf/margin/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            var result = await _baseClient.SendAsync<KucoinOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinClientOrderId>> CancelMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/hf/margin/orders/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 2, true);
            return await _baseClient.SendAsync<KucoinClientOrderId>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllMarginOrdersBySymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/hf/margin/orders", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails[]>> GetOpenMarginOrdersAsync(string symbol, TradeType type, CancellationToken ct = default)
        {
            if (type == TradeType.SpotTrade)
                throw new ArgumentException("Type should be MarginTrade or IsolatedMarginTrade", nameof(type));

            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("tradeType", type);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/orders/active", KucoinExchange.RateLimiter.SpotRest, 4, true);
            return await _baseClient.SendAsync<KucoinHfOrderDetails[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfPaginated<KucoinHfOrderDetails>>> GetClosedMarginOrdersAsync(string symbol, OrderSide? side = null, OrderType? type = null, TradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, long? lastId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalEnum("tradeType", tradeType);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("limit", limit);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/orders/done", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinHfPaginated<KucoinHfOrderDetails>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/orders/{orderId}", KucoinExchange.RateLimiter.SpotRest, 4, true);
            return await _baseClient.SendAsync<KucoinHfOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfOrderDetails>> GetMarginOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/orders/client-order/{clientOrderId}", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinHfOrderDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinHfPaginated<KucoinUserTrade>>> GetMarginUserTradesAsync(string symbol, TradeType tradeType, Enums.OrderSide? side = null, Enums.OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, long? lastId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddEnum("tradeType", tradeType);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalMilliseconds("startAt", startTime);
            parameters.AddOptionalMilliseconds("endAt", endTime);
            parameters.AddOptional("lastId", lastId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/fills", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinHfPaginated<KucoinUserTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginOpenOrderSymbols>> GetMarginSymbolsWithOpenOrdersAsync(bool isolated, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("tradeType", isolated ? "MARGIN_ISOLATED_TRADE" : "MARGIN_TRADE");
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/hf/margin/order/active/symbols", KucoinExchange.RateLimiter.SpotRest, 4, true);
            return await _baseClient.SendAsync<KucoinMarginOpenOrderSymbols>(request, parameters, ct).ConfigureAwait(false);
        }

    }
}
