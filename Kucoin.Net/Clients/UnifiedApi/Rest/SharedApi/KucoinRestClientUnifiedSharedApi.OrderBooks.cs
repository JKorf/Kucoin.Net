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
        #region Get Book Ticker

        async Task<IExchangeCallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await ((IGetBookTickerRest)this).GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetTickersAsync(
                productType,
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBookTicker>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedBookTicker>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(productType == ProductType.Spot ? _topicSpotId : _topicFuturesId, _api.EnvironmentName, null, ticker.Symbol),
                    ticker.Symbol,
                    ticker.BestAskPrice ?? 0,
                    new SharedOrderQuantity(
                        productType == ProductType.Spot ? ticker.BestAskQuantity : null,
                        null,
                        productType != ProductType.Spot ? ticker.BestAskQuantity : null
                        ),
                    ticker.BestBidPrice ?? 0,
                    new SharedOrderQuantity(
                        productType == ProductType.Spot ? ticker.BestBidQuantity : null,
                        null,
                        productType != ProductType.Spot ? ticker.BestBidQuantity : null
                        )
                    ));
        }


        #endregion

        #region Get Order Book

        async Task<IExchangeCallResult<SharedOrderBook>> IGetOrderBook.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
            => await ((IGetOrderBookRest)this).GetOrderBookAsync(request, ct).ConfigureAwait(false);

        public GetOrderBookOptions GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, [20, 100], false);
        public async Task<HttpResult<SharedOrderBook>> GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetOrderBookAsync(
                productType,
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, 
                new SharedOrderBook(
                    productType == ProductType.Spot ? SharedQuantityType.BaseAsset : SharedQuantityType.Contracts,
                    result.Data.Sequence,
                    result.Data.Asks,
                    result.Data.Bids
                ));
        }


        #endregion
    }
}
