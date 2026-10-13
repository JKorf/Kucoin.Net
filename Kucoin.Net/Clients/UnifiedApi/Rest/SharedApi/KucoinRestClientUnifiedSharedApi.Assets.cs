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
        #region Get Asset

        async Task<IExchangeCallResult<SharedAsset>> IGetAsset.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
            => await ((IGetAssetRest)this).GetAssetAsync(request, ct).ConfigureAwait(false);

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetAssetAsync(request.Asset, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedAsset>(result);

            return HttpResult.Ok(result, new SharedAsset(result.Data.Asset)
            {
                FullName = result.Data.FullName,
                Networks = result.Data.Networks.Select(x => new SharedAssetNetwork(x.Network)
                {
                    ContractAddress = x.ContractAddress,
                    DepositEnabled = x.IsDepositEnabled,
                    FullName = x.NetworkName,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    MinConfirmations = x.Confirms,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    WithdrawEnabled = x.IsWithdrawEnabled,
                    WithdrawFee = x.MinWithdrawFee
                }).ToArray()
            });
        }


        #endregion

        #region Get All Assets

        async Task<IExchangeCallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => await ((IGetAllAssetsRest)this).GetAllAssetsAsync(request, ct).ConfigureAwait(false);

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.ExchangeData.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedAsset[]>(result);

            return HttpResult.Ok(result, result.Data.Select(x =>
                new SharedAsset(x.Asset)
                {
                    FullName = x.FullName,
                    Networks = x.Networks.Select(x => new SharedAssetNetwork(x.Network)
                    {
                        ContractAddress = x.ContractAddress,
                        DepositEnabled = x.IsDepositEnabled,
                        FullName = x.NetworkName,
                        MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                        MinConfirmations = x.Confirms,
                        MinWithdrawQuantity = x.MinWithdrawQuantity,
                        WithdrawEnabled = x.IsWithdrawEnabled,
                        WithdrawFee = x.MinWithdrawFee
                    }).ToArray()
                }
            ).ToArray());
        }

        #endregion

    }
}
