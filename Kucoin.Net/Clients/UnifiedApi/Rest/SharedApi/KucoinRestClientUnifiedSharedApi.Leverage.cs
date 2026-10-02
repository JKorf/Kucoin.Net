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

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Get Leverage

        async Task<IExchangeCallResult<SharedLeverage>> IGetLeverage.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
            => await GetLeverageAsync(request, ct).ConfigureAwait(false);

        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetLeverageAsync(
                UnifiedSimpleAccountType.Futures,
                symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            var leverage = result.Data.SingleOrDefault();
            return HttpResult.Ok(result, new SharedLeverage(leverage?.Leverage ?? 0)
            {
                Side = request.PositionSide
            });
        }

        #endregion

        #region Set Leverage

        async Task<IExchangeCallResult<SharedLeverage>> ISetLeverage.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
            => await SetLeverageAsync(request, ct).ConfigureAwait(false);

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName);
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.SetLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), request.Leverage, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage(request.Leverage));
        }

        #endregion

        #region Get Leverage Tiers

        async Task<IExchangeCallResult<SharedLeverageTier[]>> IGetLeverageTiers.GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
            => await GetLeverageTiersAsync(request, ct).ConfigureAwait(false);

        public GetLeverageTiersOptions GetLeverageTiersOptions { get; } = new GetLeverageTiersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedLeverageTier[]>> GetLeverageTiersAsync(GetLeverageTiersRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageTiersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverageTier[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetPositionTiersAsync(
                UnifiedSimpleAccountType.Futures,
                MarginMode.CrossMode,
                UnifiedAccountMode.Unified,
                symbol: request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverageTier[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x => 
                new SharedLeverageTier(request.Symbol, x.Symbol, x.Tier, null, x.MinQuantity, x.MaxQuantity, x.MaintainMarginRate, x.MaxLeverage)
            ).ToArray());
        }

        #endregion
    }
}
