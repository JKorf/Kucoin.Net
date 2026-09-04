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
    internal partial class KucoinRestClientSpotSharedApi : 
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
                  SharedTransport.Rest,
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
    }
}
