using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Enums;
using CryptoExchange.Net;
using System.Linq;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotApi : IKucoinSocketClientSpotApiShared
    {
        private const string _topicId = "KucoinSpot";
        public string Exchange => KucoinExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client
        EndpointOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new EndpointOptions<SubscribeTickerRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 100
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToSnapshotUpdatesAsync(symbols, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice ?? 0, update.Data.HighPrice ?? 0, update.Data.LowPrice ?? 0, update.Data.Volume, update.Data.ChangePercentage * 100)
            {
                QuoteVolume = update.Data.VolumeValue
            })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 100
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTradeUpdatesAsync(symbols, update => handler(update.AsExchangeEvent<SharedTrade[]>(Exchange, new[] { new SharedTrade(update.Data.Quantity, update.Data.Price, update.Data.Timestamp){
                Side = update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            } })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        EndpointOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new EndpointOptions<SubscribeBookTickerRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 100
        };
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol), update.Symbol!, update.Data.BestAsk.Price, update.Data.BestAsk.Quantity, update.Data.BestBid.Price, update.Data.BestBid.Quantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false)
        {
            MaxSymbolCount = 100,
            SupportsMultipleSymbols = true
        };
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToKlineUpdatesAsync(symbols, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.Candles.OpenTime, update.Data.Candles.ClosePrice, update.Data.Candles.HighPrice, update.Data.Candles.LowPrice, update.Data.Candles.OpenPrice, update.Data.Candles.Volume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 50 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 100
        };
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToOrderBookUpdatesAsync(symbols, request.Limit ?? 5, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<ExchangeEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);
            var result = await SubscribeToBalanceUpdatesAsync(
                update =>
                {
                    // Only trade/trade_hf account updates should be passed through
                    if (!update.Data.RelationEvent.StartsWith("trade"))
                        return;

                    handler(update.AsExchangeEvent<SharedBalance[]>(Exchange, new[] { new SharedBalance(update.Data.Asset, update.Data.Available, update.Data.Total) }));
                },
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Spot Order client

        EndpointOptions<SubscribeSpotOrderRequest> ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new EndpointOptions<SubscribeSpotOrderRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<ExchangeEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                update => handler(update.AsExchangeEvent<SharedSpotOrder[]>(Exchange, new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        private SharedSpotOrder ParseOrder(KucoinStreamOrderBaseUpdate orderUpdate)
        {
            if (orderUpdate is KucoinStreamOrderNewUpdate update)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol),
                            update.Symbol,
                            update.OrderId.ToString(),
                            update.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(update.Status, update.UpdateType),
                            update.OrderTime)
                {
                    ClientOrderId = update.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(update.OriginalQuantity == 0 ? null : update.OriginalQuantity, update.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(0, 0),
                    OrderPrice = update.Price == 0 ? null : update.Price,
                    Fee = 0,
                    IsTriggerOrder = update.OrderType == OrderType.Stop || update.OrderType == OrderType.MarketStop || update.OrderType == OrderType.LimitStop
                };
            }
            if (orderUpdate is KucoinStreamOrderMatchUpdate matchUpdate)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, matchUpdate.Symbol),
                            matchUpdate.Symbol,
                            matchUpdate.OrderId.ToString(),
                            matchUpdate.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : matchUpdate.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            matchUpdate.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(matchUpdate.Status, matchUpdate.UpdateType),
                            matchUpdate.OrderTime)
                {
                    ClientOrderId = matchUpdate.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(matchUpdate.OriginalQuantity == 0 ? null : matchUpdate.OriginalQuantity, matchUpdate.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(matchUpdate.QuantityFilled, matchUpdate.OriginalValue - (matchUpdate.QuoteQuantityRemaining + matchUpdate.ValueCanceled)),
                    OrderPrice = matchUpdate.Price == 0 ? null : matchUpdate.Price,
                    UpdateTime = matchUpdate.Timestamp,
                    IsTriggerOrder = matchUpdate.OrderType == OrderType.Stop || matchUpdate.OrderType == OrderType.MarketStop || matchUpdate.OrderType == OrderType.LimitStop,
                    LastTrade = new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, matchUpdate.Symbol), matchUpdate.Symbol, matchUpdate.OrderId, matchUpdate.TradeId, matchUpdate.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, matchUpdate.MatchQuantity, matchUpdate.MatchPrice, matchUpdate.Timestamp)
                    {
                        ClientOrderId = matchUpdate.ClientOrderid,
                        Role = matchUpdate.Liquidity == LiquidityType.Taker ? SharedRole.Taker : SharedRole.Maker
                    }
                };
            }
            if (orderUpdate is KucoinStreamOrderUpdate upd)
            {
                return new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, upd.Symbol),
                            upd.Symbol,
                            upd.OrderId.ToString(),
                            upd.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : upd.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            upd.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseStatus(upd.Status, upd.UpdateType),
                            upd.OrderTime)
                {
                    ClientOrderId = upd.ClientOrderid?.ToString(),
                    OrderQuantity = new SharedOrderQuantity(upd.OriginalQuantity == 0 ? null : upd.OriginalQuantity, upd.OriginalValue),
                    QuantityFilled = new SharedOrderQuantity(upd.QuantityFilled, upd.OriginalValue - (upd.QuoteQuantityRemaining + upd.ValueCanceled)),
                    OrderPrice = upd.Price == 0 ? null : upd.Price,
                    UpdateTime = upd.Timestamp,
                    IsTriggerOrder = upd.OrderType == OrderType.Stop || upd.OrderType == OrderType.MarketStop || upd.OrderType == OrderType.LimitStop,
                };
            }

            throw new Exception("Unknown order update type");
        }

        private SharedOrderStatus ParseStatus(ExtendedOrderStatus? status, MatchUpdateType? updateType)
        {
            if (status == ExtendedOrderStatus.New)
                return SharedOrderStatus.Open;

            if (updateType == MatchUpdateType.Canceled)
                return SharedOrderStatus.Canceled;

            if (updateType == MatchUpdateType.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Open;
        }
    }
}
