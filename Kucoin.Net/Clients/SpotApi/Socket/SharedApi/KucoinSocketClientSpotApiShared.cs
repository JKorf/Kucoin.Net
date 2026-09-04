using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot.Socket;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotSharedApi : 
        SharedApiBase,
        IKucoinSocketClientSpotApiShared,
        IKucoinSocketClientSpotSharedApi
    {
        private readonly KucoinSocketClientSpotApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicId = "KucoinSpot";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        public KucoinSocketClientSpotSharedApi(KucoinSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  [TradingMode.Spot],
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
                SubscribeSpotOrderOptions
                );
        }
    }
}
