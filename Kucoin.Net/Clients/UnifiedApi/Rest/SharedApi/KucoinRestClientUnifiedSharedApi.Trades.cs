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
        #region Get Recent Trades

        async Task<IExchangeCallResult<SharedTrade[]>> IGetRecentTrades.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
            => await ((IGetRecentTradesRest)this).GetRecentTradesAsync(request, ct).ConfigureAwait(false);

        public GetRecentTradesOptions GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 100, false);
        public async Task<HttpResult<SharedTrade[]>> GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var productType = GetProductType(request);
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetRecentTradesAsync(
                productType,
                symbol,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedTrade(
                    request.Symbol,
                    symbol,
                    new SharedOrderQuantity(
                        productType == ProductType.Spot ? x.Quantity : null,
                        null,
                        productType != ProductType.Spot ? x.Quantity : null
                        ),
                    x.Price,
                    x.Timestamp)
                {
                    Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                }
            ).ToArray());
        }

        #endregion

    }
}
