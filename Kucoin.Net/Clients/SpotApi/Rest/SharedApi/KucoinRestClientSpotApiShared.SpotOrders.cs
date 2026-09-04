using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinRestClientSpotSharedApi
    {

        #region Place Spot Order

        async Task<ICallResult<SharedId>> IPlaceSpotOrder.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
            => await PlaceSpotOrderAsync(request, ct).ConfigureAwait(false);

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceSpotOrderOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var result = await _api.Trading.PlaceOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    GetPlaceOrderType(request.OrderType),
                    request.Quantity?.QuantityInBaseAsset,
                    request.Price,
                    request.Quantity?.QuantityInQuoteAsset,
                    timeInForce: GetTimeInForce(request.TimeInForce),
                    postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                    clientOrderId: request.ClientOrderId).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            }
            else
            {
                var result = await _api.HfTrading.PlaceOrderAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                    GetPlaceOrderType(request.OrderType),
                    request.Quantity?.QuantityInBaseAsset,
                    request.Price,
                    request.Quantity?.QuantityInQuoteAsset,
                    timeInForce: GetTimeInForce(request.TimeInForce),
                    postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                    clientOrderId: request.ClientOrderId).ConfigureAwait(false);

                if (!result.Success)
                    return HttpResult.Fail<SharedId>(result);

                return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
            }
        }

        #endregion

        #region Get Spot Order

        async Task<ICallResult<SharedSpotOrder>> IGetSpotOrder.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderOptions GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await _api.Trading.GetOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = order.Data.Fee,
                    FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
            else
            {
                var order = await _api.HfTrading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    FeeAsset = order.Data.FeeAsset,
                    Fee = order.Data.Fee,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
        }

        #endregion

        #region Get Open Spot Orders

        async Task<ICallResult<SharedSpotOrder[]>> IGetOpenSpotOrders.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenSpotOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenSpotOrdersOptions GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder[]>> GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var order = await _api.Trading.GetOrdersAsync(symbol: symbol, status: OrderStatus.Active).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.Id.ToString(),
                    ParseOrderType(x.Type, x.PostOnly),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId,
                    OrderPrice = x.Price == 0 ? null : x.Price,
                    OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(x.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = x.StopPrice,
                    IsTriggerOrder = x.StopPrice > 0
                }).ToArray());
            }
            else
            {
                if (request.Symbol == null)
                    return HttpResult.Fail<SharedSpotOrder[]>(Exchange, ArgumentError.Missing("Symbol", "Symbol parameter is required for HfTrading account"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var order = await _api.HfTrading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(order);

                return HttpResult.Ok(order, order.Data.Select(x => new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.Id.ToString(),
                    ParseOrderType(x.Type, x.PostOnly),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId,
                    OrderPrice = x.Price == 0 ? null : x.Price,
                    OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(x.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = x.StopPrice,
                    IsTriggerOrder = x.StopPrice > 0
                }).ToArray());
            }
        }

        #endregion

        #region Get Closed Spot Orders

        async Task<ICallResult<SharedSpotOrder[]>> IGetClosedSpotOrders.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedSpotOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetSpotClosedOrdersOptions GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedSpotOrder[]>> GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                // Determine page token
                int limit = request.Limit ?? 500;
                var direction = DataDirection.Descending;
                var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

                // Get data
                var result = await _api.Trading.GetOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    status: OrderStatus.Done,
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    currentPage: pageParams.Page,
                    pageSize: pageParams.Limit,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.Id.ToString(),
                            ParseOrderType(x.Type, x.PostOnly),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                            TimeInForce = ParseTimeInForce(x.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                            TriggerPrice = x.StopPrice,
                            IsTriggerOrder = x.StopPrice > 0
                        }).ToArray(), nextPageRequest);
            }
            else
            {
                // Determine page token
                int limit = request.Limit ?? 100;
                var direction = DataDirection.Descending;
                var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

                // Get data
                var result = await _api.HfTrading.GetClosedOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    lastId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.LastId),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.Id.ToString(),
                            ParseOrderType(x.Type, x.PostOnly),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                            TimeInForce = ParseTimeInForce(x.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                            TriggerPrice = x.StopPrice,
                            IsTriggerOrder = x.StopPrice > 0
                        }).ToArray(), nextPageRequest);
            }
        }

        #endregion

        #region Get Spot Order Trades

        async Task<ICallResult<SharedUserTrade[]>> IGetSpotOrderTrades.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetSpotOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderTradesOptions GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await _api.Trading.GetUserTradesAsync(orderId: request.OrderId, ct: ct).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.OrderId.ToString(),
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    x.Price,
                    x.Timestamp)
                {
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                }).ToArray());
            }
            else
            {
                var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                var order = await _api.HfTrading.GetUserTradesAsync(symbol, orderId: request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.OrderId.ToString(),
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    x.Price,
                    x.Timestamp)
                {
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                }).ToArray());
            }
        }

        #endregion

        #region Get Spot User Trade History

        async Task<ICallResult<SharedUserTrade[]>> IGetSpotUserTradeHistory.GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetSpotUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetSpotUserTradeHistoryAsync(request, pageRequest, ct);
        GetSpotUserTradeHistoryOptions ISpotOrderRestClient.GetSpotUserTradesOptions => GetSpotUserTradeHistoryOptions;

        public GetSpotUserTradeHistoryOptions GetSpotUserTradeHistoryOptions { get; } = new GetSpotUserTradeHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedUserTrade[]>> GetSpotUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetSpotUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                // Determine page token
                int limit = request.Limit ?? 500;
                var direction = DataDirection.Descending;
                var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

                // Get data
                var result = await _api.Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    currentPage: pageParams.Page,
                    pageSize: pageParams.Limit,
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                        }).ToArray(), nextPageRequest);
            }
            else
            {
                // Determine page token
                int limit = request.Limit ?? 100;
                var direction = DataDirection.Descending;
                var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

                // Get data
                var result = await _api.HfTrading.GetUserTradesAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    startTime: pageParams.StartTime,
                    endTime: pageParams.EndTime,
                    limit: pageParams.Limit,
                    lastId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                    ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.LastId),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                        }).ToArray(), nextPageRequest);
            }
        }

        #endregion

        #region Cancel Spot Order

        async Task<ICallResult<SharedId>> ICancelSpotOrder.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderOptions CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await _api.Trading.CancelOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await _api.HfTrading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(bool active, bool canceled)
        {
            if (canceled) return SharedOrderStatus.Canceled;
            if (active) return SharedOrderStatus.Open;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type, bool? postOnly)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.Limit && postOnly == true) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? tif)
        {
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;
            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private NewOrderType GetPlaceOrderType(SharedOrderType type)
        {
            if (type == SharedOrderType.Market) return NewOrderType.Market;

            return NewOrderType.Limit;
        }

        private TimeInForce? GetTimeInForce(SharedTimeInForce? tif)
        {
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;

            return null;
        }

        #region Get Spot Order By Client Order Id

        async Task<ICallResult<SharedSpotOrder>> IGetSpotOrderByClientOrderId.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
            => await GetSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public GetSpotOrderByClientOrderIdOptions GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotOrder>> GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await _api.Trading.GetOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = order.Data.Fee,
                    FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
            else
            {
                var order = await _api.HfTrading.GetOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
#pragma warning disable CS0618 // Type or member is obsolete
                    Fee = order.Data.Fee,
                    FeeAsset = order.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                    TriggerPrice = order.Data.StopPrice
                });
            }
        }

        #endregion

        #region Cancel Spot Order By Client Order Id

        async Task<ICallResult<SharedId>> ICancelSpotOrderByClientOrderId.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderByClientOrderIdAsync(request, ct).ConfigureAwait(false);

        public CancelSpotOrderByClientOrderIdOptions CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await _api.Trading.CancelOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await _api.HfTrading.CancelOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

        #endregion
    }
}
