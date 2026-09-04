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

        #region Get Book Ticker

        async Task<ICallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public GetBookTickerOptions GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, false);
        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await _api.ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                new SharedOrderQuantity(contractQuantity: resultTicker.Data.BestAskQuantity),
                resultTicker.Data.BestBidPrice,
                new SharedOrderQuantity(contractQuantity: resultTicker.Data.BestBidQuantity)));
        }

        #endregion

    }
}
