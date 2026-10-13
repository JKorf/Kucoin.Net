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
        #region Transfer

        async Task<IExchangeCallResult<SharedId>> ITransfer.TransferAsync(TransferRequest request, CancellationToken ct)
            => await ((ITransferRest)this).TransferAsync(request, ct).ConfigureAwait(false);

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Unified,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin,
            SharedAccountType.Spot
            ]);
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var productType = GetProductType(request);
            var result = await _api.Account.TransferAsync(
                request.Asset,
                request.Quantity,
                UnifiedTransferType.Internal,
                GetAccountType(request.FromAccountType),
                GetAccountType(request.ToAccountType),
                fromIsolatedMarginSymbol: request.FromSymbol,
                toIsolatedMarginSymbol: request.ToSymbol,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.OrderId));
        }

        private UnifiedAccountType GetAccountType(SharedAccountType accountType) => accountType switch
        {
            SharedAccountType.Unified => UnifiedAccountType.Unified,
            SharedAccountType.Funding => UnifiedAccountType.Funding,
            SharedAccountType.PerpetualLinearFutures => UnifiedAccountType.Futures,
            SharedAccountType.PerpetualInverseFutures => UnifiedAccountType.Futures,
            SharedAccountType.DeliveryLinearFutures => UnifiedAccountType.Futures,
            SharedAccountType.DeliveryInverseFutures => UnifiedAccountType.Futures,
            SharedAccountType.CrossMargin => UnifiedAccountType.Cross,
            SharedAccountType.IsolatedMargin => UnifiedAccountType.Isolated,
            SharedAccountType.Spot => UnifiedAccountType.Spot,
            _ => throw new ArgumentException($"Unsupported account type: {accountType}"),
        };

        #endregion

    }
}
