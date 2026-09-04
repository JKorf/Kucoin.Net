using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot.Socket;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotSharedApi
    {

        #region Subscribe Trades

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 100
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType<SharedTrade[]>(new[] {
                new SharedTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol!,
                    new SharedOrderQuantity(update.Data.Quantity),
                    update.Data.Price,
                    update.Data.Timestamp){
                Side = update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            } })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
