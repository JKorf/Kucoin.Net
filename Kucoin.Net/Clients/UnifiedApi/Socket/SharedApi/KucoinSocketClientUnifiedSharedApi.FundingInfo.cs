using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinSocketClientUnifiedSharedApi
    {
        #region Subscribe Funding Info

        public SubscribeFundingInfoOptions SubscribeFundingInfoOptions { get; } = new SubscribeFundingInfoOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingInfoUpdatesAsync(SubscribeFundingInfoRequest request, Action<DataEvent<SharedFundingInfo>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFundingInfoOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToFundingFeeUpdatesAsync(
                symbol,
                update => handler(update.ToType<SharedFundingInfo>(
                    
                    new SharedFundingInfo(
                        update.Data.FundingFeeRate,
                        update.Data.NextFundingTime,
                        (int?)(update.Data.FundingInterval / 1000)
                        )
                    
                )
            )
            , ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
