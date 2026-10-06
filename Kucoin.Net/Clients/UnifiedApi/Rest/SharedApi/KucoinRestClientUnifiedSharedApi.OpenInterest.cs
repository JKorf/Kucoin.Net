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
        #region Get Open Interest

        async Task<IExchangeCallResult<SharedOpenInterest>> IGetOpenInterest.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
            => await ((IGetOpenInterestRest)this).GetOpenInterestAsync(request, ct).ConfigureAwait(false);

        public GetOpenInterestOptions GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, false);
        public async Task<HttpResult<SharedOpenInterest>> GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var result = await _api.ExchangeData.GetFuturesOpenInterestAsync(
                [request.Symbol!.GetSymbol(FormatSymbol)],
                ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            var ticker = result.Data.SingleOrDefault();
            if (ticker == null)
                return HttpResult.Fail<SharedOpenInterest>(result, new ServerError(ErrorType.UnknownSymbol, "No ticker found for symbol"));

            return HttpResult.Ok(result, new SharedOpenInterest(new SharedOrderQuantity(null, null, ticker.OpenInterest)));
        }


        #endregion

    }
}
