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
        #region Subscribe Index Price

        public SubscribeIndexPriceOptions SubscribeIndexPriceOptions { get; } = new SubscribeIndexPriceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(SubscribeIndexPriceRequest request, Action<DataEvent<SharedIndexPrice>> handler, CancellationToken ct)
        {
            var validationError = SubscribeIndexPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToMarkPriceUpdatesAsync(
                symbol,
                update => handler(update.ToType(
                    new SharedIndexPrice(
                        request.Symbol, 
                        symbol,
                        update.Data.IndexPrice)
            {
            })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
