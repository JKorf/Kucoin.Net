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
    internal class KucoinRestClientSpotSharedApi : 
        SharedApiBase,
        IKucoinRestClientSpotApiShared,
        IKucoinRestClientSpotSharedApi
    {
        private readonly KucoinRestClientSpotApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicId = "KucoinSpot";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        private static readonly HashSet<string> _exchangeFiat = ["USD", "EUR", "BRL"];

        public KucoinRestClientSpotSharedApi(KucoinRestClientSpotApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                GetBalancesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetOrderBookOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetFeeOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                TransferOptions
                );
        }

        #region Kline client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 100, false);
        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
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
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
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
                            new SharedOrderQuantity(x.Volume, x.QuoteVolume)))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client

        public SharedSymbolCatalog? SpotSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);
        public GetSpotSymbolsOptions GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

            var data = result.Data
               .Select(x => ParseSymbol(x))
               .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(data, request));
        }

        private SharedSpotSymbol ParseSymbol(KucoinSymbol s)
        {
            var result = new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.EnableTrading)
            {
                MinTradeQuantity = s.BaseMinQuantity,
                MaxTradeQuantity = s.BaseMaxQuantity,
                QuantityStep = s.BaseIncrement,
                PriceStep = s.PriceIncrement,
                MinNotionalValue = s.MinFunds,
                DisplayName = s.Name,
                BaseAssetType = SharedAssetType.Crypto
            };

            if (LibraryHelpers.IsStableCoin(result.BaseAsset))
                result.BaseAssetSubType = SharedAssetSubType.StableCoin;

            if (_exchangeFiat.Contains(result.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(result.QuoteAsset))
                    result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
        #endregion

        #region Ticker client

        public GetSpotTickerOptions GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker>> GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.Get24HourStatsAsync(symbol, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                symbol, 
                result.Data.LastPrice ?? 0,
                result.Data.HighPrice ?? 0, 
                result.Data.LowPrice ?? 0, 
                new SharedOrderQuantity(result.Data.Volume, result.Data.QuoteVolume),
                result.Data.ChangePercentage * 100)
            {
            });
        }

        Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllSpotTickersAsync(request, ct);
        GetAllSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions => GetAllSpotTickersOptions;

        public GetAllSpotTickersOptions GetAllSpotTickersOptions { get; } = new GetAllSpotTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedSpotTicker[]>> GetAllSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Data.Select(x => 
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice ?? 0, 
                    x.HighPrice ?? 0, 
                    x.LowPrice ?? 0,
                    new SharedOrderQuantity(x.Volume, x.QuoteVolume),
                    x.ChangePercentage * 100)
                {
                }).ToArray());
        }

        #endregion

        #region Book Ticker client

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var resultTicker = await _api.ExchangeData.GetTickerAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol),
                symbol,
                resultTicker.Data.BestAskPrice ?? 0,
                new SharedOrderQuantity(resultTicker.Data.BestAskQuantity),
                resultTicker.Data.BestBidPrice ?? 0,
                new SharedOrderQuantity(resultTicker.Data.BestBidQuantity)));
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

            return HttpResult.Ok(result, result.Data.Select(x =>
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, [AccountTypeFilter.Spot, AccountTypeFilter.Funding, AccountTypeFilter.Margin]);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var result = await _api.Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
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

            return HttpResult.Ok(result, data.Select(x =>
                new SharedBalance(
                    SupportedTradingModes,
                    x.Asset,
                    x.Available,
                    x.Available + x.Holds)).ToArray());
        }

        #endregion

        #region Spot Order client

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

        #region Asset client
        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetAssetAsync(request.Asset, ct: ct).ConfigureAwait(false);
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

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);

        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
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

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressesV3Async(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Select(x => new SharedDepositAddress(request.Asset, x.Address)
            {
                TagOrMemo = x.Memo,
                Network = x.Network
            }).ToArray());
        }

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetDepositsAsync(
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

            if (status == DepositStatus.Failure
                || status == DepositStatus.ComplianceVerificationFailed
                || status == DepositStatus.RollingBack
                || status == DepositStatus.RolledBack
                || status == DepositStatus.RiskManagementRejected)
            {
                return SharedTransferStatus.Failed;
            }

            if (status == DepositStatus.Processing
                || status == DepositStatus.WaitingComplianceVerification
                || status == DepositStatus.WaitingRiskManagement)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
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
                limit: request.Limit ?? 20,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(SharedQuantityType.BaseAsset, result.Data.Sequence, result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);
            
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalsAsync(
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
                        new SharedWithdrawal(
                            x.Asset,
                            x.Address,
                            x.Quantity, 
                            x.Status == WithdrawalStatus.Success, 
                            x.CreateTime,
                            GetWithdrawalStatus(x))
                        {
                            Id = x.Id,
                            Network = x.Network,
                            Tag = x.Memo,
                            TransactionId = x.WalletTransactionId,
                            Fee = x.Fee
                        }).ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(KucoinWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Failure)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawalStatus.Success)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawalStatus.Processing || x.Status == WithdrawalStatus.WalletProcessing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
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
        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await _api.Account.GetSymbolTradingFeesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.Single().MakerFeeRate * 100, result.Data.Single().TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, false)
        {
        };

        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceStopOrderAsync(
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

        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
        };
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await _api.Trading.GetStopOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
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

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelStopOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Transfer client

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin
            ]);
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.UniversalTransferAsync(
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
