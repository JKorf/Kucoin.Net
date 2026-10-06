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
        #region Get Mark Price

        async Task<IExchangeCallResult<SharedMarkPrice>> IGetMarkPrice.GetMarkPriceAsync(GetMarkPriceRequest request, CancellationToken ct)
            => await ((IGetMarkPrice)this).GetMarkPriceAsync(request, ct).ConfigureAwait(false);

        public GetMarkPriceOptions GetMarkPriceOptions { get; } = new GetMarkPriceOptions(_exchangeName, false);
        public async Task<HttpResult<SharedMarkPrice>> GetMarkPriceAsync(GetMarkPriceRequest request, CancellationToken ct)
        {
            var validationError = GetMarkPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedMarkPrice>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetTickersAsync(
                productType,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedMarkPrice>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedMarkPrice>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedMarkPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, ticker.Symbol),
                    ticker.Symbol,
                    ticker.MarkPrice ?? 0));
        }


        #endregion

        #region Get All Mark Prices

        async Task<IExchangeCallResult<SharedMarkPrice[]>> IGetAllMarkPrices.GetAllMarkPricesAsync(GetAllMarkPricesRequest request, CancellationToken ct)
            => await ((IGetAllMarkPrices)this).GetAllMarkPricesAsync(request, ct).ConfigureAwait(false);

        public GetAllMarkPricesOptions GetAllMarkPricesOptions { get; } = new GetAllMarkPricesOptions(_exchangeName, false);
        public async Task<HttpResult<SharedMarkPrice[]>> GetAllMarkPricesAsync(GetAllMarkPricesRequest request, CancellationToken ct)
        {
            var validationError = GetAllMarkPricesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedMarkPrice[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetTickersAsync(
                ProductType.Futures,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedMarkPrice[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedMarkPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.MarkPrice ?? 0)
            ).ToArray());
        }

        #endregion

        #region Get Mark Price Klines

        async Task<IExchangeCallResult<SharedFuturesKline[]>> IGetMarkPriceKlines.GetMarkPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetMarkPriceKlinesAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetMarkPriceKlinesOptions GetMarkPriceKlinesOptions { get; } = new GetMarkPriceKlinesOptions(_exchangeName, false, true, true, 200, false);

        public async Task<HttpResult<SharedFuturesKline[]>> GetMarkPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;

            var validationError = GetMarkPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            int limit = request.Limit ?? 200;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetKlinesAsync(
                productType,
                symbol,
                interval,
                type: KlineType.MarkPrice,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime).Add(TimeSpan.FromSeconds(-(int)interval))),
                     result.Data.Length,
                     result.Data.Select(x => x.OpenTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFuturesKline(
                            request.Symbol,
                            symbol,
                            x.OpenTime,
                            x.ClosePrice,
                            x.HighPrice,
                            x.LowPrice,
                            x.OpenPrice))
                    .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
