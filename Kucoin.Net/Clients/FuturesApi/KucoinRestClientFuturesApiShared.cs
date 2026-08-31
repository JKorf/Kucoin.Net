using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net;
using Kucoin.Net.Objects.Models.Futures;

namespace Kucoin.Net.Clients.FuturesApi
{
    internal class KucoinRestClientFuturesSharedApi : 
        SharedApiBase,
        IKucoinRestClientFuturesApiShared,
        IKucoinRestClientFuturesSharedApi
    {
        private readonly KucoinRestClientFuturesApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicId = "KucoinFutures";


        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        public KucoinRestClientFuturesSharedApi(KucoinRestClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBookTickerOptions,
                GetFuturesSymbolsOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                CancelFuturesOrderOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetKlinesOptions,
                GetRecentTradesOptions,
                GetOrderBookOptions,
                GetOpenInterestOptions,
                GetFundingRateHistoryOptions,
                GetPositionHistoryOptions,
                GetFeeOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions,
                GetLeverageOptions,
                SetLeverageOptions
                );
        }

        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var resultXbt = _api.Account.GetAccountOverviewAsync("XBT", ct: ct);
            var resultUsdt = _api.Account.GetAccountOverviewAsync("USDT", ct: ct);
            var resultUsdc = _api.Account.GetAccountOverviewAsync("USDC", ct: ct);
            await Task.WhenAll(resultUsdc, resultUsdt, resultXbt).ConfigureAwait(false);
            if (!resultXbt.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultXbt.Result);
            if (!resultUsdt.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultUsdt.Result);
            if (!resultUsdc.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultUsdc.Result);

            var result = new List<SharedBalance>();
            result.Add(new SharedBalance(SupportedTradingModes, resultXbt.Result.Data.Asset, resultXbt.Result.Data.AvailableBalance, resultXbt.Result.Data.AccountEquity));
            result.Add(new SharedBalance(SupportedTradingModes, resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.AvailableBalance, resultUsdt.Result.Data.AccountEquity));
            result.Add(new SharedBalance(SupportedTradingModes, resultUsdc.Result.Data.Asset, resultUsdc.Result.Data.AvailableBalance, resultUsdc.Result.Data.AccountEquity));
            return HttpResult.Ok(resultXbt.Result, result.ToArray());
        }

        #endregion

        #region Futures Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetContractAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(result);

            return HttpResult.Ok(result, new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                    result.Data.Symbol,
                    result.Data.LastTradePrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    new SharedOrderQuantity(result.Data.Volume24H, result.Data.Turnover24H),
                    result.Data.PriceChangePercentage * 100)
                {
                    IndexPrice = result.Data.IndexPrice,
                    MarkPrice = result.Data.MarkPrice,
                    FundingRate = result.Data.FundingFeeRate,
                    NextFundingTime = result.Data.NextFundingRateTime
                });
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            IEnumerable<KucoinContract> data = result.Data;
            if (request.TradingMode != null)
            {
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? (!x.IsInverse && !x.SettleDate.HasValue) :
                     request.TradingMode == TradingMode.PerpetualInverse ? (x.IsInverse && !x.SettleDate.HasValue) :
                      request.TradingMode == TradingMode.DeliveryLinear ? (!x.IsInverse && x.SettleDate.HasValue) :
                       (x.IsInverse && x.SettleDate.HasValue));
            }

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastTradePrice,
                    x.HighPrice, 
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume24H, x.Turnover24H),
                    x.PriceChangePercentage * 100)
                {
                    IndexPrice = x.IndexPrice,
                    MarkPrice = x.MarkPrice,
                    FundingRate = x.FundingFeeRate,
                    NextFundingTime = x.NextFundingRateTime
                }
            ).ToArray());
        }

        #endregion

        #region Book Ticker client

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await _api.ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                new SharedOrderQuantity(contractQuantity: resultTicker.Data.BestAskQuantity),
                resultTicker.Data.BestBidPrice,
                new SharedOrderQuantity(contractQuantity: resultTicker.Data.BestBidQuantity)));
        }

        #endregion

        #region Futures Symbol client

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(result);

            var data = result.Data
               .Select(x => ParseSymbol(x))
               .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(data, request));
        }

        private SharedFuturesSymbol ParseSymbol(KucoinContract s)
        {
            var result = new SharedFuturesSymbol(
                s.IsInverse && s.SettleDate.HasValue ? TradingMode.DeliveryInverse :
                s.IsInverse && !s.SettleDate.HasValue ? TradingMode.PerpetualInverse :
                s.SettleDate.HasValue ? TradingMode.DeliveryLinear :
                TradingMode.PerpetualLinear,
                s.BaseAsset,
                s.QuoteAsset,
                s.Symbol,
                s.Status == "Open")
            {
                MinTradeQuantity = s.LotSize,
                MaxTradeQuantity = s.MaxOrderQuantity,
                PriceStep = s.TickSize,
                QuantityStep = s.LotSize,
                ContractSize = s.Multiplier == -1 ? 1 : s.Multiplier,
                DeliveryTime = s.SettleDate,
                DisplayName = s.DisplaySymbol,
                MakerFeePercentage = s.MakerFeeRate * 100,
                TakerFeePercentage = s.TakerFeeRate * 100,
                MaxLongLeverage = s.MaxLeverage,
                MaxShortLeverage = s.MaxLeverage,
                UpperFundingCap = s.FundingRateCap,
                LowerFundingCap = s.FundingRateFloor,
                LowerPriceLimitPercentage = -s.OrderPriceRange * 100,
                UpperPriceLimitPercentage = s.OrderPriceRange * 100
            };

            if (result.TradingMode.IsInverse())
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            if (s.MarketType == MarketType.Nasdaq)
            {
                result.BaseAssetType = SharedAssetType.TradFi;
                result.BaseAssetSubType = SharedAssetSubType.Equity;
            }
            else
            {
                if (LibraryHelpers.IsCommodity(s.BaseAsset))
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
                }
                else
                {
                    result.BaseAssetType = SharedAssetType.Crypto;
                }
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
        #endregion

        #region Futures Order Client

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.InputAsset;
        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesOrderRequest.Leverage), typeof(decimal), "The leverage for opening the position", 3m)
            }
        };

        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                GetOrderSide(request.Side, request.PositionSide),
                request.OrderType == SharedOrderType.Limit ? Enums.NewOrderType.Limit : Enums.NewOrderType.Market,
                request.Leverage!.Value,
                quantity: (int?)request.Quantity?.QuantityInContracts,
                quantityInBaseAsset: request.Quantity?.QuantityInContracts == null ? request.Quantity?.QuantityInBaseAsset : null,
                price: request.Price,
                postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                reduceOnly: request.ReduceOnly,
                timeInForce: GetTimeInForce(request.TimeInForce),
                clientOrderId: request.ClientOrderId,
                marginMode: request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Isolated ? FuturesMarginMode.Isolated : FuturesMarginMode.Cross,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.CancelExist),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.ExecutedValue, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                Leverage = order.Data.Leverage,
                ReduceOnly = order.Data.ReduceOnly,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                TriggerPrice = order.Data.StopPrice,
                IsTriggerOrder = order.Data.StopPrice > 0,
                IsCloseOrder = order.Data.CloseOrder,
                PositionSide = ParsePositionSide(order.Data.PositionSide)
            });
        }

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var ordersTask = _api.Trading.GetOrdersAsync(symbol, OrderStatus.Active, ct: ct);
            var stopOrdersTask = _api.Trading.GetUntriggeredStopOrdersAsync(symbol, ct: ct);
            await Task.WhenAll(ordersTask, stopOrdersTask).ConfigureAwait(false);
            if (!ordersTask.Result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(ordersTask.Result);
            if (!stopOrdersTask.Result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(stopOrdersTask.Result);

            var orders = ordersTask.Result;
            var stopOrders = stopOrdersTask.Result;

            var result = orders.Data.Items.Concat(stopOrders.Data.Items).OrderByDescending(x => x.CreateTime);
            return HttpResult.Ok(orders, result.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.Id.ToString(),
                x.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                SharedOrderStatus.Open,
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ExecutedValue, contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                Leverage = x.Leverage,
                ReduceOnly = x.ReduceOnly,
                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                TriggerPrice = x.StopPrice,
                IsTriggerOrder = x.StopPrice > 0,
                IsCloseOrder = x.CloseOrder,
                PositionSide = ParsePositionSide(x.PositionSide)
            }).ToArray());
        }

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetOrdersAsync(request.Symbol!.GetSymbol(FormatSymbol),
                OrderStatus.Done,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                .Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        x.Id.ToString(),
                        x.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(x.Type),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status, x.CancelExist),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ExecutedValue, contractQuantity: x.QuantityFilled),
                        TimeInForce = ParseTimeInForce(x.TimeInForce),
                        UpdateTime = x.UpdateTime,
                        Leverage = x.Leverage,
                        ReduceOnly = x.ReduceOnly,
                        AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                        TriggerPrice = x.StopPrice,
                        IsTriggerOrder = x.StopPrice > 0,
                        IsCloseOrder = x.CloseOrder,						
						PositionSide = ParsePositionSide(x.PositionSide)
                    }).ToArray(), nextPageRequest);
        }

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;
        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var orders = await _api.Trading.GetUserTradesAsync(orderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Items.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.Id,
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(contractQuantity: x.Quantity),
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Liquidity == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 1000;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: TimeSpan.FromDays(7));

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                currentPage: pageParams.Page,
                pageSize: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            // Get next token
            var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
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
                        x.Id,
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: x.Quantity),
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        Role = x.Liquidity == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }).ToArray(), nextPageRequest);
        }

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var result = await _api.Account.GetPositionsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPosition[]>(result);

            IEnumerable<KucoinPosition> data = result.Data;
            if (symbol != null)
                data = data.Where(x => x.Symbol == symbol);

            return HttpResult.Ok(result, data.Select(x =>
                new SharedPosition(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                    x.Symbol,
                    new SharedOrderQuantity(contractQuantity: Math.Abs(x.CurrentQuantity)),
                    x.OpenTime)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    LiquidationPrice = x.LiquidationPrice,
                    Leverage = x.RealLeverage,
                    AverageOpenPrice = x.AverageEntryPrice,
                    PositionMode = x.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                    PositionSide = x.CurrentQuantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short,
                }).ToArray());
        }

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.PositionSide), typeof(SharedPositionSide), "Position side to close", SharedPositionSide.Short)
            }
        };
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.PositionSide == SharedPositionSide.Short ? OrderSide.Buy : OrderSide.Sell,
                NewOrderType.Market,
                0,
                0,
                closeOrder: true,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        private OrderSide GetOrderSide(SharedOrderSide side, SharedPositionSide? posSide)
        {
            if (posSide == null) return side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell;

            if (posSide == SharedPositionSide.Long)
            {
                if (side == SharedOrderSide.Buy)
                    return OrderSide.Buy;
                return OrderSide.Sell;
            }

            if (side == SharedOrderSide.Buy)
                return OrderSide.Sell;
            return OrderSide.Buy;
        }

        private TimeInForce? GetTimeInForce(SharedTimeInForce? tif)
        {
            if (tif == null)
                return null;

            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status, bool cancelExists)
        {
            if (status == OrderStatus.Active) return SharedOrderStatus.Open;
            if (cancelExists) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Done) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce? tif)
        {
            if (tif == null)
                return null;

            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private SharedPositionSide? ParsePositionSide(PositionSide? positionSide)
            => positionSide switch
            {
                PositionSide.Long => SharedPositionSide.Long,
                PositionSide.Short => SharedPositionSide.Short,
                _ => null
            };

        #endregion

        #region Futures Client Id Order Client

        public GetFuturesOrderByClientOrderIdOptions GetFuturesOrderByClientOrderIdOptions { get; } = new GetFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderByClientOrderIdAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.Id.ToString(),
                order.Data.PostOnly == true ? SharedOrderType.LimitMaker : ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.CancelExist),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.ExecutedValue, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                Leverage = order.Data.Leverage,
                ReduceOnly = order.Data.ReduceOnly,
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                TriggerPrice = order.Data.StopPrice,
                IsTriggerOrder = order.Data.StopPrice > 0,
                IsCloseOrder = order.Data.CloseOrder,
                PositionSide = ParsePositionSide(order.Data.PositionSide),
            });
        }

        public CancelFuturesOrderByClientOrderIdOptions CancelFuturesOrderByClientOrderIdOptions { get; } = new CancelFuturesOrderByClientOrderIdOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), clientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.CanceledOrderId));
        }
        #endregion

        #region Klines client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 200, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.EightHours,
            SharedKlineInterval.TwelveHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek);

        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;

            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime).Add(TimeSpan.FromSeconds(-(int)interval))),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedKline(
                            request.Symbol,
                            symbol, 
                            x.OpenTime, 
                            x.ClosePrice, 
                            x.HighPrice,
                            x.LowPrice,
                            x.OpenPrice,
                            new SharedOrderQuantity(null, x.QuoteVolume, x.Volume)))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Recent Trade client

        public GetRecentTradesOptions GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 100, false);
        public async Task<HttpResult<SharedTrade[]>> GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTradeHistoryAsync(
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Take(request.Limit ?? 100).Select(x =>
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(contractQuantity: x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Order Book client
        public GetOrderBookOptions GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, new[] { 20, 100 }, false);
        public async Task<HttpResult<SharedOrderBook>> GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await _api.ExchangeData.GetAggregatedPartialOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                depth: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(SharedQuantityType.Contracts, result.Data.Sequence, result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Open Interest client

        public GetOpenInterestOptions GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, false);
        public async Task<HttpResult<SharedOpenInterest>> GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var result = await _api.ExchangeData.GetContractAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(new SharedOrderQuantity(contractQuantity: result.Data.OpenInterest)));
        }

        #endregion

        #region Funding Rate client
        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 100, false);

        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime ?? DateTime.UtcNow.AddDays(-7),
                endTime: pageParams.EndTime ?? DateTime.UtcNow,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp)),
                     result.Data.Length,
                     result.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingRate(x.FundingRate, x.Timestamp))
                    .ToArray(), nextPageRequest);
        }
        #endregion

        #region Position History client

        public GetPositionHistoryOptions GetPositionHistoryOptions { get; } = new GetPositionHistoryOptions(_exchangeName, false, true, true, 200);
        public async Task<HttpResult<SharedPositionHistory[]>> GetPositionHistoryAsync(GetPositionHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetPositionHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPositionHistory[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetPositionHistoryAsync(
                symbol: request.Symbol?.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                page: pageParams.Page,
                limit: pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedPositionHistory[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedPositionHistory(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                            x.Symbol,
                            x.Side == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                            x.OpenPrice ?? 0,
                            x.ClosePrice ?? 0,
                            new SharedOrderQuantity(contractQuantity: x.CloseQuantity ?? 0),
                            x.ProfitAndLoss ?? 0,
                            x.CloseTime ?? x.OpenTime)
                        {
                            Leverage = x.Leverage,
                            OrderId = x.CloseId.ToString(),
                            PositionId = x.PositionId
                        }).ToArray(), nextPageRequest);
        }
        #endregion

        #region Fee Client
        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await _api.Account.GetTradingFeeAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.MakerFeeRate * 100, result.Data.TakerFeeRate * 100));
        }
        #endregion

        #region Tp/SL Client
        public SetFuturesTpSlOptions SetFuturesTpSlOptions { get; } = new SetFuturesTpSlOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetTpSlRequest.PositionSide), typeof(SharedPositionSide), "Side of the position", SharedPositionSide.Long)
            }
        };

        public async Task<HttpResult<SharedId>> SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            var validationError = SetFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                GetTpSlSide(request),
                NewOrderType.Market,
                stopType: GetStopType(request),
                stopPriceType: StopPriceType.MarkPrice,
                stopPrice: request.TriggerPrice,
                closeOrder: true,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.Id.ToString()));
        }

        private StopType GetStopType(SetTpSlRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.TpSlSide == SharedTpSlSide.TakeProfit ? StopType.Up : StopType.Down;

            return request.TpSlSide == SharedTpSlSide.TakeProfit ? StopType.Down : StopType.Up;
        }

        private OrderSide GetTpSlSide(SetTpSlRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return OrderSide.Sell;

            return OrderSide.Buy;
        }

        public CancelFuturesTpSlOptions CancelFuturesTpSlOptions { get; } = new CancelFuturesTpSlOptions(_exchangeName, true)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123")
            }
        };

        public async Task<HttpResult<bool>> CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTpSlOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<bool>(Exchange, validationError);

            var result = await _api.Trading.CancelOrderAsync(
                request.OrderId!,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<bool>(result);

            // Return
            return HttpResult.Ok(result, true);
        }

        #endregion

        #region Leverage client
        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetCrossMarginLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(result.Data.Leverage)
            {
                Side = request.PositionSide
            });
        }

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName);
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetCrossMarginLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), request.Leverage, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(request.Leverage));
        }
        #endregion

    }
}
