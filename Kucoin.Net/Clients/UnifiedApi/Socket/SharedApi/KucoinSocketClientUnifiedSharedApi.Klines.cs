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
        #region Subscribe Kline

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeKlinesSocket.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
            => await SubscribeToKlineUpdatesAsync(request, x => handler(x.ToType<SharedKline>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeKlineOptions SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (KlineInterval)request.Interval;
            var validationError = SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToKlineUpdatesAsync(
                request.TradingMode == TradingMode.Spot ? UnifiedAccountType.Spot : UnifiedAccountType.Futures,
                symbol,
                interval,
                update => handler(update.ToType(
                    new SharedKline(
                        request.Symbol,
                        update.Data.Symbol,
                        update.Data.OpenTime,
                        update.Data.ClosePrice,
                        update.Data.HighPrice,
                        update.Data.LowPrice,
                        update.Data.OpenPrice,
                        new SharedOrderQuantity(
                            request.Symbol.TradingMode == TradingMode.Spot ? update.Data.Volume : null,
                            update.Data.TransactionAmount,
                            request.Symbol.TradingMode != TradingMode.Spot ? update.Data.Volume : null)
                        )
                    {
                    })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
