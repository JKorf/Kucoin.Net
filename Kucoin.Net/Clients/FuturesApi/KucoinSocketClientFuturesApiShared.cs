using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net;

namespace Kucoin.Net.Clients.FuturesApi
{
    internal class KucoinSocketClientFuturesSharedApi:
        SharedApiBase,
        IKucoinSocketClientFuturesSharedApi,
        IKucoinSocketClientFuturesApiShared
    {
        private readonly KucoinSocketClientFuturesApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicId = "KucoinFutures";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        public KucoinSocketClientFuturesSharedApi(KucoinSocketClientFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions
                );
        }

        #region Ticker client
        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerOperation.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeTo24HTickerUpdatesAsync(symbol, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, symbol), 
                    symbol, 
                    update.Data.LastPrice,
                    null, 
                    null, 
                    new SharedOrderQuantity(update.Data.Volume, update.Data.Turnover),
                    update.Data.PriceChangePercentage * 100)
            {
            })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.ToType<SharedTrade[]>(new[] { 
                new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(contractQuantity: update.Data.Quantity), update.Data.Price, update.Data.Timestamp){
                Side = update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            } })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Book Ticker client

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToBookTickerUpdatesAsync(symbol, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(contractQuantity: update.Data.BestAskQuantity), 
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(contractQuantity: update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Kline client
        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.ToType(
                new SharedKline(
                    request.Symbol,
                    symbol, 
                    update.Data.OpenTime, 
                    update.Data.ClosePrice, 
                    update.Data.HighPrice, 
                    update.Data.LowPrice,
                    update.Data.OpenPrice,
                    new SharedOrderQuantity(contractQuantity: update.Data.Volume)))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 50 });
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 5, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.Contracts, update.SequenceNumber, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Balance client
        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);
            var result = await _api.SubscribeToBalanceUpdatesAsync(
                onBalanceUpdate: update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(SupportedTradingModes, update.Data.Asset, update.Data.AvailableBalance, update.Data.AvailableBalance + update.Data.HoldBalance) })),
                onWalletUpdate: update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(SupportedTradingModes, update.Data.Asset, update.Data.AvailableBalance, update.Data.AvailableBalance + update.Data.HoldBalance) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                null,
                update => handler(update.ToType<SharedFuturesOrderUpdate[]>(new[] { ParseOrder(update.Data) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedFuturesOrderUpdate ParseOrder(KucoinStreamFuturesOrderUpdate update)
        {
            return new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol),
                        update.Symbol,
                        update.OrderId.ToString(),
                        update.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Status, update.UpdateType),
                        update.OrderTime)
            {
                ClientOrderId = update.ClientOrderId,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: update.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: update.QuantityFilled),
                OrderPrice = update.Price == 0 ? null : update.Price,
                LastTrade = update.UpdateType != MatchUpdateType.Match ? null :
                    new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Symbol), 
                        update.Symbol, 
                        update.OrderId,
                        update.TradeId!,
                        update.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        new SharedOrderQuantity(contractQuantity: update.MatchQuantity), 
                        update.MatchPrice ?? 0,
                        update.Timestamp)
                    {
                        ClientOrderId = update.ClientOrderId,
                        Role = update.Liquidity == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker
                    }
            };
        }

        private SharedOrderStatus ParseOrderStatus(ExtendedOrderStatus status, MatchUpdateType updateType)
        {
            if (status == ExtendedOrderStatus.New || status == ExtendedOrderStatus.Open || updateType == MatchUpdateType.Open || updateType == MatchUpdateType.Received) return SharedOrderStatus.Open;
            if (updateType == MatchUpdateType.Canceled) return SharedOrderStatus.Canceled;
            if (updateType == MatchUpdateType.Filled) return SharedOrderStatus.Filled;
            return SharedOrderStatus.Unknown;
        }
        #endregion

        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToPositionUpdatesAsync(
                update => handler(update.ToType<SharedPosition[]>(new[] { 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), 
                        update.Data.Symbol,
                        new SharedOrderQuantity(contractQuantity: update.Data.CurrentQuantity),
                        update.Data.CurrentTime)
                        {
                            AverageOpenPrice = update.Data.AverageEntryPrice,
                            PositionMode = update.Data.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                            PositionSide = update.Data.CurrentQuantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                            LiquidationPrice = update.Data.LiquidationPrice,
                            Leverage = update.Data.RealLeverage,
                            UnrealizedPnl = update.Data.UnrealizedPnl
                        }})),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
