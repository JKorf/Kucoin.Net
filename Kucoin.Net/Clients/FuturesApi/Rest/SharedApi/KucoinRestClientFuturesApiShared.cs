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
    internal partial class KucoinRestClientFuturesSharedApi : 
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
                  SharedTransport.Rest,
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
    }
}
