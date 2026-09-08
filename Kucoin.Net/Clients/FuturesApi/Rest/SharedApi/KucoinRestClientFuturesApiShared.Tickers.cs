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
    internal partial class KucoinRestClientFuturesSharedApi
    {

        #region Get Ticker

        async Task<ICallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker>(result);

            return HttpResult.Ok<SharedTicker>(result, result.Data);
        }

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetContractAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(result);

            return HttpResult.Ok(result, new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                    result.Data.Symbol,
                    result.Data.LastTradePrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    new SharedOrderQuantity(result.Data.Volume24H, result.Data.Turnover24H),
                    result.Data.PriceChangePercentage * 100)
                {
                    IndexPrice = result.Data.IndexPrice,
                    MarkPrice = result.Data.MarkPrice,
                    FundingRate = result.Data.FundingFeeRate,
                    NextFundingTime = result.Data.NextFundingRateTime
                });
        }

        #endregion

        #region Get All Tickers

        async Task<ICallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var result = await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            return HttpResult.Ok<SharedTicker[]>(result, result.Data);
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(result);

            IEnumerable<KucoinContract> data = result.Data;
            if (request.TradingMode != null)
            {
                data = data.Where(x =>
                    request.TradingMode == TradingMode.PerpetualLinear ? (!x.IsInverse && !x.SettleDate.HasValue) :
                     request.TradingMode == TradingMode.PerpetualInverse ? (x.IsInverse && !x.SettleDate.HasValue) :
                      request.TradingMode == TradingMode.DeliveryLinear ? (!x.IsInverse && x.SettleDate.HasValue) :
                       (x.IsInverse && x.SettleDate.HasValue));
            }

            return HttpResult.Ok(result, data.Select(x =>
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastTradePrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume24H, x.Turnover24H),
                    x.PriceChangePercentage * 100)
                {
                    IndexPrice = x.IndexPrice,
                    MarkPrice = x.MarkPrice,
                    FundingRate = x.FundingFeeRate,
                    NextFundingTime = x.NextFundingRateTime
                }
            ).ToArray());
        }

        #endregion

    }
}
