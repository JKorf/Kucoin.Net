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
        #region Subscribe Balances

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);
            var result = await _api.SubscribeToBalanceUpdatesAsync(
                update =>
                {
                    // Only trade/trade_hf account updates should be passed through
                    if (!update.Data.RelationEvent.StartsWith("trade"))
                        return;

                    handler(update.ToType<SharedBalance[]>(new[] {
                        new SharedBalance(
                            SupportedTradingModes,
                            update.Data.Asset, 
                            update.Data.Available, 
                            update.Data.Total) }));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
