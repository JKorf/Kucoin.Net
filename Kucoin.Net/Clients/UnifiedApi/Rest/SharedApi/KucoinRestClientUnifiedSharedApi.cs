using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.UnifiedApi;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi : 
        SharedApiBase,
        IKucoinRestClientUnifiedSharedApi
    {
        private readonly KucoinRestClientUnifiedApi _api;

        private const string _exchangeName = "Kucoin";
        private const string _topicSpotId = "KucoinUnifiedSpot";
        private const string _topicFuturesId = "KucoinUnifiedFutures";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(KucoinExchange.Metadata, this);

        private static readonly HashSet<string> _exchangeFiat = ["USD", "EUR", "BRL"];

        public KucoinRestClientUnifiedSharedApi(KucoinRestClientUnifiedApi api)
            : base(
                  SharedTransport.Rest,
                  api,
                  [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear, TradingMode.PerpetualInverse, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetTickerOptions,
                GetAllTickersOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetFeeOptions,
                GetFundingInfoOptions,
                GetFundingRateHistoryOptions,
                GetUserFundingHistoryOptions,
                GetIndexPriceOptions,
                GetAllIndexPricesOptions,
                GetIndexPriceKlinesOptions,
                GetMarkPriceOptions,
                GetAllMarkPricesOptions,
                GetMarkPriceKlinesOptions,
                GetKlinesOptions,
                GetLedgerOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetLeverageTiersOptions,
                GetOpenInterestOptions,
                GetBookTickerOptions,
                GetOrderBookOptions,
                GetSpotSymbolsOptions,
                GetFuturesSymbolsOptions,
                GetRecentTradesOptions,
                TransferOptions,
                GetWithdrawalHistoryOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetSpotOrderByClientOrderIdOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotUserTradeHistoryOptions,
                GetSpotOrderTradesOptions,
                CancelSpotOrderOptions,
                CancelSpotOrderByClientOrderIdOptions,
                CancelAllSpotSymbolOrdersOptions,
                EditSpotOrderOptions,
                EditSpotOrderByClientOrderIdOptions,
                GetFuturesOrderOptions,
                GetFuturesOrderByClientOrderIdOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesUserTradeHistoryOptions,
                GetFuturesOrderTradesOptions,
                CancelFuturesOrderOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                CancelAllFuturesSymbolOrdersOptions,
                EditFuturesOrderOptions,
                EditFuturesOrderByClientOrderIdOptions,
                GetPositionsOptions,
                GetPositionHistoryOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions
                );
        }

        private ProductType GetProductType(SharedRequest request)
        {
            if (request.TradingMode == null || request.TradingMode == TradingMode.Spot)
                return ProductType.Spot;

            return ProductType.Futures;
        }
    }
}
