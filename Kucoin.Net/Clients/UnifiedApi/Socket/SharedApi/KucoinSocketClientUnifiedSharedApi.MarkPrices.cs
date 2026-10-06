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
        #region Subscribe Mark Price

        public SubscribeMarkPriceOptions SubscribeMarkPriceOptions { get; } = new SubscribeMarkPriceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(SubscribeMarkPriceRequest request, Action<DataEvent<SharedMarkPrice>> handler, CancellationToken ct)
        {
            var validationError = SubscribeMarkPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToMarkPriceUpdatesAsync(
                symbol,
                update => handler(update.ToType(
                    new SharedMarkPrice(
                        request.Symbol, 
                        symbol,
                        update.Data.MarkPrice)
            {
            })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
