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

namespace Kucoin.Net.Clients.FuturesApi
{
    internal partial class KucoinSocketClientFuturesSharedApi
    {
        #region Subscribe Balances

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);
            var result = await _api.SubscribeToBalanceUpdatesAsync(
                onBalanceUpdate: update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(SupportedTradingModes, update.Data.Asset, update.Data.AvailableBalance, update.Data.AvailableBalance + update.Data.HoldBalance) })),
                onWalletUpdate: update => handler(update.ToType<SharedBalance[]>(new[] { 
                    new SharedBalance(SupportedTradingModes, update.Data.Asset, update.Data.AvailableBalance, update.Data.AvailableBalance + update.Data.HoldBalance) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
