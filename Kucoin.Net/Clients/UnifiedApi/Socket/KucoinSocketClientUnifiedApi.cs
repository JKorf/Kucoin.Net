using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging.Extensions;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Interfaces;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Clients.MessageHandlers;
using Kucoin.Net.Clients.UnifiedApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.UnifiedApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures.Socket;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Spot.Socket;
using Kucoin.Net.Objects.Models.Unified;
using Kucoin.Net.Objects.Options;
using Kucoin.Net.Objects.Sockets;
using Kucoin.Net.Objects.Sockets.Queries;
using Kucoin.Net.Objects.Sockets.Subscriptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    /// <inheritdoc cref="IKucoinSocketClientSpotApi" />
    internal partial class KucoinSocketClientUnifiedApi : SocketApiClient<KucoinEnvironment, KucoinUnifiedAuthenticationProvider, KucoinCredentials>, IKucoinSocketClientUnifiedApi
    {
        private readonly KucoinSocketClientUnifiedSharedApi _sharedApi;
        private readonly KucoinSocketClient _baseClient;

        /// <inheritdoc />
        public new KucoinSocketOptions ClientOptions => (KucoinSocketOptions)base.ClientOptions;

        public IKucoinSocketClientUnifiedSharedApi SharedApi => _sharedApi;

        internal KucoinSocketClientUnifiedApi(ILoggerFactory? loggerFactory, KucoinSocketClient baseClient, KucoinSocketOptions options)
            : base(loggerFactory, KucoinExchange.Metadata.Id, options.Environment.UnifiedSocketPrivateAddress, options, options.UnifiedOptions)
        {
            _baseClient = baseClient;
            _sharedApi = new KucoinSocketClientUnifiedSharedApi(this);

            RateLimiter = KucoinExchange.RateLimiter.UnifiedSocket;

            AddSystemSubscription(new KucoinUnifiedWelcomeSubscription(this, _logger));
            SetDedicatedConnection(ClientOptions.Environment.UnifiedSocketPrivateTradeAddress.AppendPath("v2/private"), true);
        }

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new KucoinSocketUnifiedMessageHandler();

        /// <inheritdoc />
        protected override KucoinUnifiedAuthenticationProvider CreateAuthenticationProvider(KucoinCredentials credentials)
            => new KucoinUnifiedAuthenticationProvider(credentials);
        
        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(
            ProductType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTickerUpdate>> onData,
            CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync(tradeType, new[] { symbol }, onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(
            ProductType tradeType,
            IEnumerable<string> symbols,
            Action<DataEvent<KucoinUaTickerUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaTickerUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaTickerUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaTickerUpdate>(_logger, this, "ticker", tradeType, null, symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(
            ProductType tradeType, string symbol, 
            KlineInterval interval,
            Action<DataEvent<KucoinUaKlineUpdate>> onData, 
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaKlineUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaKlineUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaKlineUpdate>(_logger, this, "kline", tradeType, symbol, null, internalHandler, false, interval);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(
            ProductType tradeType,
            string symbol,
            OrderBookDepth depth,
            Action<DataEvent<KucoinUaOrderBookUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaOrderBookUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(data.PushType == "snapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaOrderBookUpdate>(_logger, this, "obu", tradeType, symbol, null, internalHandler, false, depth: depth);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(
            ProductType tradeType,
            string symbol,
            Action<DataEvent<KucoinUaTradeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaTradeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaTradeUpdate>(_logger, this, "trade", tradeType, symbol, null, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(tradeType), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaFundingFeeUpdate>> onData,
            CancellationToken ct = default)
            => SubscribeToFundingFeeUpdatesAsync([symbol], onData, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            IEnumerable<string> symbols,
            Action<DataEvent<KucoinUaFundingFeeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaFundingFeeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaFundingFeeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaFundingFeeUpdate>(_logger, this, "funding-fee", ProductType.Futures, null, symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(ProductType.Futures), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingFeeUpdatesAsync(
            Action<DataEvent<KucoinUaFundingFeeUpdate[]>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaFundingFeeUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaFundingFeeUpdate[]>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaFundingFeeUpdate[]>(_logger, this, "funding-fee-all-symbols", ProductType.Futures, null, null, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(ProductType.Futures), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaMarkPriceUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaMarkPriceUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaMarkPriceUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaMarkPriceUpdate>(_logger, this, "mark-price", ProductType.Futures, symbol, null, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(ProductType.Futures), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToCallAuctionInfoUpdatesAsync(
            string symbol,
            Action<DataEvent<KucoinUaCallAuctionInfoUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaCallAuctionInfoUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaCallAuctionInfoUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaCallAuctionInfoUpdate>(_logger, this, "callAuctionInfo", ProductType.Spot, symbol, null, internalHandler, false);
            return await SubscribeAsync(GetConnectionUrl(ProductType.Spot), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(
            Action<DataEvent<KucoinUaBalanceUpdate>> onData,
            CancellationToken ct = default)
        {
            var subscription = new KucoinUnifiedBalanceSubscription(_logger, this, onData);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            Action<DataEvent<KucoinUaOrderUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaOrderUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaOrderUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaOrderUpdate>(_logger, this, "orderAll", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(
            Action<DataEvent<KucoinUaUserTradeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaUserTradeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.TradeTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaUserTradeUpdate>(_logger, this, "execution", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToLiteUserTradeUpdatesAsync(
            Action<DataEvent<KucoinUaLiteUserTradeUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaLiteUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaLiteUserTradeUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.TradeTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaLiteUserTradeUpdate>(_logger, this, "execution.lite", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
            Action<DataEvent<KucoinUaPositionUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaPositionUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaPositionUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaPositionUpdate>(_logger, this, "positionAll", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToLeverageUpdatesAsync(
            Action<DataEvent<KucoinUaLeverageUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaLeverageUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaLeverageUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.PushTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaLeverageUpdate>(_logger, this, "leverage", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToLiquidationWarningUpdatesAsync(
            Action<DataEvent<KucoinUaLiquidationWarningUpdate>> onData,
            CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, KucoinUnifiedSocketUpdate<KucoinUaLiquidationWarningUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.PushTime);

                onData.Invoke(
                    new DataEvent<KucoinUaLiquidationWarningUpdate>(KucoinExchange.ExchangeName, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Type)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data.UpdateTime, GetTimeOffset())
                    );
            });
            var subscription = new KucoinUnifiedSubscription<KucoinUaLiquidationWarningUpdate>(_logger, this, "lw", null, null, null, internalHandler, true);
            return await SubscribeAsync(ClientOptions.Environment.UnifiedSocketPrivateAddress, subscription, ct).ConfigureAwait(false);
        }

        public async Task<QueryResult<KucoinUaOrderResult>> PlaceOrderAsync(
            ProductType type,
            string symbol,
            OrderSide side,
            OrderType orderType,
            decimal quantity,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            QuantityUnit? quantityUnit = null,
            string? clientOrderId = null,
            bool? postOnly = null,
            bool? reduceOnly = null,
            SelfTradePrevention? stpMode = null,
            long? cancelAfter = null,
            decimal? triggerPrice = null,
            StopType? triggerDirection = null,
            StopPriceType? triggerPriceType = null,
            bool? autoBorrow = null,
            bool? autoRepay = null,
            PositionSide? positionSide = null,
            MarginMode? marginMode = null,
            decimal? leverage = null,
            StopPriceType? tpTriggerPriceType = null,
            decimal? tpTriggerPrice = null,
            StopPriceType? slTriggerPriceType = null,
            decimal? slTriggerPrice = null,
            bool? closeOrder = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", type);
            parameters.Add("symbol", symbol);
            parameters.Add("side", EnumConverter.GetString(side).ToUpperInvariant());
            parameters.Add("orderType", EnumConverter.GetString(orderType).ToUpperInvariant());
            parameters.Add("size", quantity);
            parameters.Add("sizeUnit", quantityUnit ?? QuantityUnit.BaseAsset);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("clientOid", clientOrderId);
            parameters.Add("postOnly", postOnly);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.Add("stp", stpMode);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("triggerPriceType", triggerPriceType);
            parameters.Add("triggerDirection", triggerDirection);
            parameters.Add("cancelAfter", cancelAfter);
            parameters.Add("autoBorrow", autoBorrow);
            parameters.Add("autoRepay", autoRepay);
            parameters.Add("positionSide", positionSide);
            parameters.Add("marginMode", marginMode);
            parameters.Add("leverage", leverage);
            parameters.Add("tpTriggerPriceType", tpTriggerPriceType);
            parameters.Add("tpTriggerPrice", tpTriggerPrice);
            parameters.Add("slTriggerPriceType", slTriggerPriceType);
            parameters.Add("slTriggerPrice", slTriggerPrice);
            parameters.Add("closeOrder", closeOrder);

            var request = new KucoinUnifiedOpRequest(ExchangeHelpers.NextId().ToString(), "uta.order", parameters);
            var query = new KucoinUnifiedOpQuery<KucoinUaOrderResult>(this, request, type == ProductType.Futures);
            var result = await QueryAsync(ClientOptions.Environment.UnifiedSocketPrivateTradeAddress.AppendPath("v2/private"), query, ct).ConfigureAwait(false);
            return result;
        }

        public async Task<QueryResult<KucoinUaOrderEditResult>> EditOrderAsync(
            string? orderId,
            string? clientOrderId,
            string symbol,
            decimal? quantity = null,
            decimal? price = null,
            QuantityUnit? quantityUnit = null,
            bool? cxlOnFail = null,
            StopPriceType? tpTriggerPriceType = null,
            decimal? tpTriggerPrice = null,
            StopPriceType? slTriggerPriceType = null,
            decimal? slTriggerPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("newSize", quantity);
            parameters.Add("sizeUnit", quantityUnit);
            parameters.Add("newPrice", price);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOid", clientOrderId);
            parameters.Add("cxlOnFail", cxlOnFail);
            parameters.Add("tpTriggerPriceType", tpTriggerPriceType);
            parameters.Add("tpTriggerPrice", tpTriggerPrice);
            parameters.Add("slTriggerPriceType", slTriggerPriceType);
            parameters.Add("slTriggerPrice", slTriggerPrice);

            var request = new KucoinUnifiedOpRequest(ExchangeHelpers.NextId().ToString(), "uta.amend", parameters);
            var query = new KucoinUnifiedOpQuery<KucoinUaOrderEditResult>(this, request, !symbol.Contains("-"));
            var result = await QueryAsync(ClientOptions.Environment.UnifiedSocketPrivateTradeAddress.AppendPath("v2/private"), query, ct).ConfigureAwait(false);
            return result;
        }

        public async Task<QueryResult<KucoinUaOrderResult>> CancelOrderAsync(
            ProductType type,
            string? symbol = null,
            string? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", type);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("clientOid", clientOrderId);

            var request = new KucoinUnifiedOpRequest(ExchangeHelpers.NextId().ToString(), "uta.cancel", parameters);
            var query = new KucoinUnifiedOpQuery<KucoinUaOrderResult>(this, request, type == ProductType.Futures);
            var result = await QueryAsync(ClientOptions.Environment.UnifiedSocketPrivateTradeAddress.AppendPath("v2/private"), query, ct).ConfigureAwait(false);
            return result;
        }

        private string GetConnectionUrl(ProductType type) => 
            type == ProductType.Futures 
                ? ClientOptions.Environment.UnifiedSocketFuturesAddress 
                : ClientOptions.Environment.UnifiedSocketSpotAddress;

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection, IMessageProcessor? processor)
        {
            ProductType type = ProductType.Futures;
            if (processor is IKucoinUnifiedOpQuery query)
                type = query.IsFutures ? ProductType.Futures : ProductType.Spot;            

            var auth = AuthenticationProvider!.GetSocketAuth(this, type);
            var authQuery = new KucoinUnifiedAuthQuery(this, auth, false);
            connection.Properties["authFutures"] = type == ProductType.Futures;
            return Task.FromResult<Query?>(authQuery);
        }

        protected override bool ConnectionCanBeUsedFor(SocketConnection connection, string address, bool authenticated, IMessageProcessor? processor, string? topic = null)
        {
            if (processor is not IKucoinUnifiedOpQuery query)
                return base.ConnectionCanBeUsedFor(connection, address, authenticated, processor, topic);

            if (!connection.Authenticated)
                return true;

            return connection.Properties.TryGetValue("authFutures", out var isAuthFutures) && (bool)isAuthFutures == query.IsFutures;
        }
    }
}
