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
        private const string _topicId = "KucoinSpot";
        public string Exchange => KucoinExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(false, true, true, 100, false);
        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

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
            if (!result)
                return result.AsExchangeResult<SharedKline[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client

        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var response = result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, TradingMode.Spot, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.EnableTrading)
            {
                MinTradeQuantity = s.BaseMinQuantity,
                MaxTradeQuantity = s.BaseMaxQuantity,
                QuantityStep = s.BaseIncrement,
                PriceStep = s.PriceIncrement,
                MinNotionalValue = s.MinFunds
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, response.Data);
            return response;
        }

        async Task<ExchangeResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.Get24HourStatsAsync(symbol, ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, symbol), symbol, result.Data.LastPrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0, result.Data.Volume ?? 0, result.Data.ChangePercentage * 100)
            {
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        GetTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, TradingMode.Spot, result.Data.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume ?? 0, x.ChangePercentage * 100)
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await ExchangeData.GetTickerAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, symbol),
                symbol,
                resultTicker.Data.BestAskPrice ?? 0,
                resultTicker.Data.BestAskQuantity ?? 0,
                resultTicker.Data.BestBidPrice ?? 0,
                resultTicker.Data.BestBidQuantity ?? 0));
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(100, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradeHistoryAsync(
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol.TradingMode, result.Data.Select(x =>
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions([AccountTypeFilter.Spot, AccountTypeFilter.Funding, AccountTypeFilter.Margin]);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            var result = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

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

            return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, data.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Holds)).ToArray());
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

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

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

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.Id.ToString()));
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

                if (!result)
                    return result.AsExchangeResult<SharedId>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.Id.ToString()));
            }
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
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
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
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

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                var order = await Trading.GetOrdersAsync(symbol: symbol, status: OrderStatus.Active).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

                return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, order.Data.Items.Select(x => new SharedSpotOrder(
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
                    return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, ArgumentError.Missing("Symbol", "Symbol parameter is required for HfTrading account"));

                var symbol = request.Symbol.GetSymbol(FormatSymbol);
                var order = await HfTrading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

                return order.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, order.Data.Select(x => new SharedSpotOrder(
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

        GetClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetClosedOrdersOptions(false, true, true, 100);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

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
                if (!result)
                    return result.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

                return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
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
                if (!result)
                    return result.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.LastId),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.CreateTime),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

                return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
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

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetUserTradesAsync(orderId: request.OrderId,ct: ct).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                return order.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, order.Data.Items.Select(x => new SharedUserTrade(
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
                if (!order)
                    return order.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                return order.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, order.Data.Items.Select(x => new SharedUserTrade(
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

        GetUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetUserTradesOptions(false, true, true, 100);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

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
                if (!result)
                    return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromPage(pageParams),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams);

                return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
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
                if (!result)
                    return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

                var nextPageRequest = Pagination.GetNextPageRequest(
                         () => Pagination.NextPageFromId(result.Data.LastId),
                         result.Data.Items.Length,
                         result.Data.Items.Select(x => x.Timestamp),
                         request.StartTime,
                         request.EndTime ?? DateTime.UtcNow,
                         pageParams,
                         TimeSpan.FromDays(7));

                return result.AsExchangeResult(
                    Exchange,
                    request.Symbol.TradingMode,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
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

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.CancelOrderAsync(request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await HfTrading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
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

        EndpointOptions<GetOrderRequest> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.GetOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
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
                if (!order)
                    return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
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

        EndpointOptions<CancelOrderRequest> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            if (hfAccount == false)
            {
                var order = await Trading.CancelOrderByClientOrderIdAsync(request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(request.OrderId));
            }
            else
            {
                var order = await HfTrading.CancelOrderByClientOrderIdAsync(request.Symbol!.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
                if (!order)
                    return order.AsExchangeResult<SharedId>(Exchange, null, default);

                return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
            }
        }
        #endregion

        #region Asset client
        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            return assets.AsExchangeResult<SharedAsset>(Exchange, TradingMode.Spot, new SharedAsset(assets.Data.Asset)
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

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(false);

        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult<SharedAsset[]>(Exchange, TradingMode.Spot, assets.Data.Select(x => new SharedAsset(x.Asset)
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

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressesV3Async(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, TradingMode.Spot, depositAddresses.Data.Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }).ToArray()
            );
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(false, true, true, 100);
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

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
            if (!result)
                return result.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedDeposit(
                            x.Asset,
                            x.Quantity,
                            x.Status == DepositStatus.Success,
                            x.CreateTime,
                            x.Status == DepositStatus.Success ? SharedTransferStatus.Completed
                            : x.Status == DepositStatus.Failure ? SharedTransferStatus.Failed
                            : SharedTransferStatus.InProgress)
                        {
                            Network = x.Network,
                            TransactionId = x.WalletTransactionId,
                            Tag = x.Memo
                        }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 20, 100 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetAggregatedPartialOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(false, true, true, 100);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);
            
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
            if (!result)
                return result.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromPage(pageParams),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.CreateTime, request.StartTime, request.EndTime, direction)
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

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                WithdrawType.Address,
                request.Asset,
                request.Address,
                request.Quantity,
                chain: request.Network,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.WithdrawalId));
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetSymbolTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(result.Data.Single().MakerFeeRate * 100, result.Data.Single().TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(false)
        {
        };

        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).PlaceSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes, ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

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
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.Id));
        }

        EndpointOptions<GetOrderRequest> ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
        };
        async Task<ExchangeWebResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).GetSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetStopOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotTriggerOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTriggerOrder(
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

            return SharedTriggerOrderStatus.Active;
        }

        EndpointOptions<CancelOrderRequest> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).CancelSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelStopOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(request.OrderId));
        }

        #endregion

        #region Transfer client

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions([
            SharedAccountType.Funding,
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin
            ]);
        async Task<ExchangeWebResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = ((ITransferRestClient)this).TransferOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await Account.UniversalTransferAsync(
                request.Quantity,
                fromType.Value,
                toType.Value,
                TransferType.Internal,
                request.Asset,
                ct: ct).ConfigureAwait(false);
            if (!transfer)
                return transfer.AsExchangeResult<SharedId>(Exchange, null, default);

            return transfer.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(transfer.Data.OrderId.ToString()));
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
