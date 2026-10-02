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
        #region Subscribe Book Ticker

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await _api.SubscribeToTickerUpdatesAsync(
                request.TradingMode == TradingMode.Spot ? UnifiedAccountType.Spot : UnifiedAccountType.Futures,
                symbols,
                update => handler(update.ToType(
                    new SharedBookTicker(
                        request.Symbol, 
                        update.Data.Symbol,
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(
                            request.TradingMode == TradingMode.Spot ? update.Data.BestBidQuantity : null,
                            null,
                            request.TradingMode != TradingMode.Spot ? update.Data.BestBidQuantity : null
                            ),
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(
                            request.TradingMode == TradingMode.Spot ? update.Data.BestAskQuantity : null,
                            null,
                            request.TradingMode != TradingMode.Spot ? update.Data.BestAskQuantity : null
                            ))
            {
            })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
