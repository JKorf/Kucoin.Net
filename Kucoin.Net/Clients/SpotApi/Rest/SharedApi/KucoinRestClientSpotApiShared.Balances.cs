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
    internal partial class KucoinRestClientSpotSharedApi
    {
        #region Balance client
        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, [AccountTypeFilter.Spot, AccountTypeFilter.Funding, AccountTypeFilter.Margin]);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            var result = await _api.Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBalance[]>(result);

            var hfAccount = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "HfTrading");
            IEnumerable<KucoinAccount> data = result.Data;
            if (request.AccountType == null || request.AccountType == SharedAccountType.Spot)
            {
                if (data.Any(x => x.Type == AccountType.Trade) && data.Any(x => x.Type == AccountType.SpotHf))
                {
                    // If there are both Trade and SpotHF balance present check which to take
                    if (hfAccount == false)
                        data = result.Data.Where(x => x.Type == AccountType.Trade);
                    else
                        data = result.Data.Where(x => x.Type == AccountType.SpotHf);
                }
                else
                {
                    // If only Trade or Spot HF balance are available use that
                    data = result.Data.Where(x => x.Type == AccountType.SpotHf || x.Type == AccountType.Trade);
                }
            }
            else if (request.AccountType == SharedAccountType.Funding)
            {
                data = result.Data.Where(x => x.Type == AccountType.Main);
            }
            else
            {
                data = result.Data.Where(x => x.Type == AccountType.Margin || x.Type == AccountType.Isolated || x.Type == AccountType.IsolatedMarginHf || x.Type == AccountType.MarginHf);
            }

            return HttpResult.Ok(result, data.Select(x =>
                new SharedBalance(
                    SupportedTradingModes,
                    x.Asset,
                    x.Available,
                    x.Available + x.Holds)).ToArray());
        }

        #endregion
    }
}
