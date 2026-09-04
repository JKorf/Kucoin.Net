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
        #region Get Balances

        async Task<ICallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var resultXbt = _api.Account.GetAccountOverviewAsync("XBT", ct: ct);
            var resultUsdt = _api.Account.GetAccountOverviewAsync("USDT", ct: ct);
            var resultUsdc = _api.Account.GetAccountOverviewAsync("USDC", ct: ct);
            await Task.WhenAll(resultUsdc, resultUsdt, resultXbt).ConfigureAwait(false);
            if (!resultXbt.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultXbt.Result);
            if (!resultUsdt.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultUsdt.Result);
            if (!resultUsdc.Result.Success)
                return HttpResult.Fail<SharedBalance[]>(resultUsdc.Result);

            var result = new List<SharedBalance>();
            result.Add(new SharedBalance(SupportedTradingModes, resultXbt.Result.Data.Asset, resultXbt.Result.Data.AvailableBalance, resultXbt.Result.Data.AccountEquity));
            result.Add(new SharedBalance(SupportedTradingModes, resultUsdt.Result.Data.Asset, resultUsdt.Result.Data.AvailableBalance, resultUsdt.Result.Data.AccountEquity));
            result.Add(new SharedBalance(SupportedTradingModes, resultUsdc.Result.Data.Asset, resultUsdc.Result.Data.AvailableBalance, resultUsdc.Result.Data.AccountEquity));
            return HttpResult.Ok(resultXbt.Result, result.ToArray());
        }

        #endregion

    }
}
