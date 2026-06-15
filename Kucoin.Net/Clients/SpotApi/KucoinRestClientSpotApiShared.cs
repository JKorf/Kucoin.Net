using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using CryptoExchange.Net;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinRestClientSpotApi : IKucoinRestClientSpotApiShared
    {
        private const string _exchangeName = "Kucoin";
        private const string _topicId = "KucoinSpot";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 100, false);
        async Task<HttpResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SharedClient.GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client

        GetSpotSymbolsOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);
        async Task<HttpResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

            var response = HttpResult.Ok(result, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.EnableTrading)
            {
                MinTradeQuantity = s.BaseMinQuantity,
                MaxTradeQuantity = s.BaseMaxQuantity,
                QuantityStep = s.BaseIncrement,
                PriceStep = s.PriceIncrement,
                MinNotionalValue = s.MinFunds
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data!);
            return response;
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetSpotTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.Get24HourStatsAsync(symbol, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, result.Data.LastPrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0, result.Data.Volume ?? 0, result.Data.ChangePercentage * 100)
            {
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        GetSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetSpotTickersOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0, x.ChangePercentage * 100)
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        GetBookTickerOptions IBookTickerRestClient.GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        async Task<HttpResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetTickerAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
                symbol,
                resultTicker.Data.BestAskPrice ?? 0,
                resultTicker.Data.BestAskQuantity ?? 0,
                resultTicker.Data.BestBidPrice ?? 0,
                resultTicker.Data.BestBidQuantity ?? 0));
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 100, false);

        async Task<HttpResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradeHistoryAsync(
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, [AccountTypeFilter.Spot, AccountTypeFilter.Funding, AccountTypeFilter.Margin]);

        async Task<HttpResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBalance[]>(result);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            IEnumerable<KucoinAccount> data = result.Data;
            if (request.AccountType == null || request.AccountType == SharedAccountType.Spot)
            {
                if (data.Any(x => x.Type == AccountType.Trade) && data.Any(x => x.Type == AccountType.SpotHf))
                {
                    // If there are both Trade and SpotHF balance present check which to take
                    if (hfAccount == false)
                        data = result.Data.Where(x => x.Type == AccountType.Trade);
                    else
                        data = result.Data.Where(x => x.Type == AccountType.SpotHf);
                }
                else
                {
                    // If only Trade or Spot HF balance are available use that
                    data = result.Data.Where(x => x.Type == AccountType.SpotHf || x.Type == AccountType.Trade);
                }
            }
            else if (request.AccountType == SharedAccountType.Funding)
            {
                data = result.Data.Where(x => x.Type == AccountType.Main);
            }
            else
            {
                data = result.Data.Where(x => x.Type == AccountType.Margin || x.Type == AccountType.Isolated || x.Type == AccountType.IsolatedMarginHf || x.Type == AccountType.MarginHf);
            }

            return HttpResult.Ok(result, data.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Holds)).ToArray());
        }

        #endregion

        #region Spot Order client

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };

        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var result = await Trading.PlaceOrderAsync(
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
                var result = await HfTrading.PlaceOrderAsync(
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

        GetSpotOrderOptions ISpotOrderRestClient.GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    Fee = order.Data.Fee,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                    FeeAsset = order.Data.FeeAsset,
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
            else
            {
                var order = await HfTrading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    Fee = order.Data.Fee,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                    FeeAsset = order.Data.FeeAsset,
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
        }

        GetOpenSpotOrdersOptions ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var order = await Trading.GetOrdersAsync(symbol: symbol, status: OrderStatus.Active).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                    x.Symbol,
                    x.Id.ToString(),
                    ParseOrderType(x.Type, x.PostOnly),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId,
                    Fee = x.Fee,
                    OrderPrice = x.Price == 0 ? null : x.Price,
                    OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(x.TimeInForce),
                    FeeAsset = x.FeeAsset,
                    TriggerPrice = x.StopPrice,
                    IsTriggerOrder = x.StopPrice > 0
                }).ToArray());
            }
            else
            {
                if (request.Symbol == null)
                    return HttpResult.Fail<SharedSpotOrder[]>(Exchange, ArgumentError.Missing("Symbol", "Symbol parameter is required for HfTrading account"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var order = await HfTrading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder[]>(order);

                return HttpResult.Ok(order, order.Data.Select(x => new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                    x.Symbol,
                    x.Id.ToString(),
                    ParseOrderType(x.Type, x.PostOnly),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                    x.CreateTime)
                {
                    ClientOrderId = x.ClientOrderId,
                    Fee = x.Fee,
                    OrderPrice = x.Price == 0 ? null : x.Price,
                    OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(x.TimeInForce),
                    FeeAsset = x.FeeAsset,
                    TriggerPrice = x.StopPrice,
                    IsTriggerOrder = x.StopPrice > 0
                }).ToArray());
            }
        }

        GetSpotClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedSpotOrdersOptions.ValidateRequest(request, this);
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
                var result = await Trading.GetOrdersAsync(
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
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                            x.Symbol,
                            x.Id.ToString(),
                            ParseOrderType(x.Type, x.PostOnly),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            Fee = x.Fee,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                            TimeInForce = ParseTimeInForce(x.TimeInForce),
                            FeeAsset = x.FeeAsset,
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
                var result = await HfTrading.GetClosedOrdersAsync(
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
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                            x.Symbol,
                            x.Id.ToString(),
                            ParseOrderType(x.Type, x.PostOnly),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.IsActive ?? true, x.CancelExist),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            Fee = x.Fee,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                            TimeInForce = ParseTimeInForce(x.TimeInForce),
                            FeeAsset = x.FeeAsset,
                            TriggerPrice = x.StopPrice,
                            IsTriggerOrder = x.StopPrice > 0
                        }).ToArray(), nextPageRequest);
            }
        }

        GetSpotOrderTradesOptions ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetUserTradesAsync(orderId: request.OrderId,ct: ct).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                    x.Symbol,
                    x.OrderId.ToString(),
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
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
                var order = await HfTrading.GetUserTradesAsync(symbol, orderId: request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedUserTrade[]>(order);

                return HttpResult.Ok(order, order.Data.Items.Select(x => new SharedUserTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                    x.Symbol,
                    x.OrderId.ToString(),
                    x.Id.ToString(),
                    x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    x.Quantity,
                    x.Price,
                    x.Timestamp)
                {
                    Fee = x.Fee,
                    FeeAsset = x.FeeAsset,
                    Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                }).ToArray());
            }
        }

        GetSpotUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetSpotUserTradesOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotUserTradesOptions.ValidateRequest(request, this);
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
                var result = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
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
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            x.Quantity,
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
                var result = await HfTrading.GetUserTradesAsync(
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
                            ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), 
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.Id.ToString(),
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            x.Quantity,
                            x.Price,
                            x.Timestamp)
                        {
                            Fee = x.Fee,
                            FeeAsset = x.FeeAsset,
                            Role = x.ForceTaker ? SharedRole.Taker : SharedRole.Taker
                        }).ToArray(), nextPageRequest);
            }
        }

        CancelSpotOrderOptions ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.CancelOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await HfTrading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }

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

        #endregion

        #region Spot Client Id Order Client

        GetSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    Fee = order.Data.Fee,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                    FeeAsset = order.Data.FeeAsset,
                    TriggerPrice = order.Data.StopPrice,
                    IsTriggerOrder = order.Data.StopPrice > 0
                });
            }
            else
            {
                var order = await HfTrading.GetOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedSpotOrder>(order);

                return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                    order.Data.Symbol,
                    order.Data.Id.ToString(),
                    ParseOrderType(order.Data.Type, order.Data.PostOnly),
                    order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                    ParseOrderStatus(order.Data.IsActive ?? true, order.Data.CancelExist),
                    order.Data.CreateTime)
                {
                    ClientOrderId = order.Data.ClientOrderId,
                    Fee = order.Data.Fee,
                    OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                    OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                    QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                    TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                    FeeAsset = order.Data.FeeAsset,
                    TriggerPrice = order.Data.StopPrice
                });
            }
        }

        CancelSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.CancelOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
            else
            {
                var order = await HfTrading.CancelOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order.Success)
                    return HttpResult.Fail<SharedId>(order);

                return HttpResult.Ok(order, new SharedId(request.OrderId));
            }
        }
        #endregion

        #region Asset client
        GetAssetOptions IAssetsRestClient.GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        async Task<HttpResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            return HttpResult.Ok(assets, new SharedAsset(assets.Data.Asset)
            {
                FullName = assets.Data.Name,
                Networks = assets.Data.Networks?.Select(x => new SharedAssetNetwork(x.NetworkId)
                {
                    FullName = x.NetworkName,
                    MinConfirmations = x.Confirms,
                    DepositEnabled = x.IsDepositEnabled,
                    MinWithdrawQuantity = x.WithdrawalMinQuantity,
                    WithdrawEnabled = x.IsWithdrawEnabled,
                    WithdrawFee = x.WithdrawalMinFee,
                    ContractAddress = x.ContractAddress
                }).ToArray()
            });
        }

        GetAssetsOptions IAssetsRestClient.GetAssetsOptions { get; } = new GetAssetsOptions(_exchangeName, false);

        async Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name,
                Networks = x.Networks?.Select(x => new SharedAssetNetwork(x.NetworkId)
                {
                    FullName = x.NetworkName,
                    MinConfirmations = x.Confirms,
                    DepositEnabled = x.IsDepositEnabled,
                    MinWithdrawQuantity = x.WithdrawalMinQuantity,
                    WithdrawEnabled = x.IsWithdrawEnabled,
                    WithdrawFee = x.WithdrawalMinFee,
                    ContractAddress = x.ContractAddress
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client

        GetDepositAddressesOptions IDepositRestClient.GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        async Task<HttpResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressesV3Async(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetDepositsAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Success,
                            x.CreateTime,
                            ParseTransferStatus(x.Status))
                        {
                            Network = x.Network,
                            TransactionId = x.WalletTransactionId,
                            Tag = x.Memo
                        }).ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Success)
                return SharedTransferStatus.Completed;

            if (status == DepositStatus.Failure)
                return SharedTransferStatus.Failed;

            if (status == DepositStatus.Processing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, new[] { 20, 100 }, false);
        async Task<HttpResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetAggregatedPartialOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetWithdrawalsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);
            
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetWithdrawalsAsync(
                request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Success, x.CreateTime)
                        {
                            Id = x.Id,
                            Network = x.Network,
                            Tag = x.Memo,
                            TransactionId = x.WalletTransactionId,
                            Fee = x.Fee
                        }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);

        async Task<HttpResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                WithdrawType.Address,
                request.Asset,
                request.Address,
                request.Quantity,
                chain: request.Network,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

        #region Fee Client
        GetFeeOptions IFeeRestClient.GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        async Task<HttpResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetSymbolTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.Single().MakerFeeRate * 100, result.Data.Single().TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, false)
        {
        };

        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceStopOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                request.OrderPrice == null ? NewOrderType.Market : NewOrderType.Limit,
                request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopCondition.Entry : StopCondition.Loss,
                request.TriggerPrice,
                price: request.OrderPrice,
                clientOrderId: request.ClientOrderId,
                quantity: request.Quantity.QuantityInBaseAsset,
                quoteQuantity: request.Quantity.QuantityInQuoteAsset,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.Id));
        }

        GetSpotTriggerOrderOptions ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
        };
        async Task<HttpResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetStopOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                order.Data.Symbol!,
                order.Data.Id,
                order.Data.Type == OrderType.Market ? SharedOrderType.Market: SharedOrderType.Limit,
                order.Data.Side == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data),
                order.Data.StopPrice ?? 0,
                order.Data.CreateTime)
            {
                Fee = order.Data.Fee,
				OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                FeeAsset = order.Data.FeeAsset,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(KucoinStopOrder data)
        {
            if (data.Status == StopOrderStatus.New)
                return SharedTriggerOrderStatus.Active;

            if (data.CancelExist)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (data.IsActive == false)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == StopOrderStatus.Triggered)
                return SharedTriggerOrderStatus.Active;

            return SharedTriggerOrderStatus.Unknown;
        }

        CancelSpotTriggerOrderOptions ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelStopOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Transfer client

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin
            ]);
        async Task<HttpResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await Account.UniversalTransferAsync(
                request.Quantity,
                fromType.Value,
                toType.Value,
                TransferType.Internal,
                request.Asset,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.OrderId.ToString()));
        }

        private TransferAccountType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Funding) return TransferAccountType.Main;
            if (type == SharedAccountType.Spot) return TransferAccountType.Trade;
            if (type.IsFuturesAccount()) return TransferAccountType.Contract;
            if (type == SharedAccountType.CrossMargin) return TransferAccountType.Margin;
            if (type == SharedAccountType.IsolatedMargin) return TransferAccountType.Isolated;

            return null;
        }

        #endregion
    }
}
