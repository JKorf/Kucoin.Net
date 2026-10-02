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
using Kucoin.Net.Interfaces.Clients.UnifiedApi;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinSocketClientUnifiedSharedApi :
        SharedApiBase,
        IKucoinSocketClientUnifiedSharedApi
    {
        private readonly KucoinSocketClientUnifiedApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicSpotId = "KucoinUnifiedSpot";
        private const string _topicFuturesId = "KucoinUnifiedFutures";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        public KucoinSocketClientUnifiedSharedApi(KucoinSocketClientUnifiedApi api)
            : base(
                  SharedTransport.Socket,
                  api,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeKlineOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeOrderBookOptions,
                SubscribeMarkPriceOptions,
                SubscribeIndexPriceOptions,
                SubscribeIncrementalOrderBookOptions,
                SubscribeFundingInfoOptions,
                SubscribeSpotOrderOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions,
                SubscribeBalanceOptions,
                PlaceSpotOrderOptions
                );
        }

        /// <inheritdoc />
        public Task UnsubscribeAllAsync() => _api.UnsubscribeAllAsync();
    }
}
