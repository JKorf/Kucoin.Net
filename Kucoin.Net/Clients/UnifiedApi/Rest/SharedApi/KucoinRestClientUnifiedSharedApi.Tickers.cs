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
using CryptoExchange.Net.Objects.Errors;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Get Ticker

        async Task<IExchangeCallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedTicker>> GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetTickersAsync(
                productType,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedTicker>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedTicker(
                    request.Symbol,
                    ticker.Symbol,
                    ticker.LastPrice,
                    ticker.HighPrice,
                    ticker.LowPrice,
                    new SharedOrderQuantity(ticker.BaseVolume, ticker.QuoteVolume),
                    // Note; spot ticker percentage is a factor instead of percentage
                    ticker.PriceChangePercent * (productType == ProductType.Spot ? 100 : 1)));
        }


        #endregion

        #region Get All Tickers

        async Task<IExchangeCallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName)
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<GetTickersRequest>.Required(x => x.TradingMode)
                ]
        };
        public async Task<HttpResult<SharedTicker[]>> GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTicker[]>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetTickersAsync(
                productType,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedTicker(
                    ExchangeSymbolCache.ParseSymbol(productType == ProductType.Spot ? _topicSpotId : _topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.BaseVolume, x.QuoteVolume),
                    // Note; spot ticker percentage is a factor instead of percentage
                    x.PriceChangePercent * (productType == ProductType.Spot ? 100 : 1))
            ).ToArray());
        }

        #endregion

    }
}
