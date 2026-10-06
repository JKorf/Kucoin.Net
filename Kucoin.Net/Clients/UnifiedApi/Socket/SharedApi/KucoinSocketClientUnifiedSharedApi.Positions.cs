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
        #region Subscribe Positions

        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToPositionUpdatesAsync(
                UnifiedAccountType.Unified,
                update => {
                    handler(update.ToType<SharedPosition[]>(
                        [new SharedPosition(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol,
                            new SharedOrderQuantity(null, update.Data.PositionValue, update.Data.Quantity),
                            update.Data.UpdateTime
                            )
                        {
                            AverageOpenPrice = update.Data.EntryPrice,
                            Id = update.Data.PositionId,
                            Leverage = update.Data.Leverage,
                            LiquidationPrice = update.Data.LiquidationPrice,
                            UnrealizedPnl = update.Data.UnrealizedPnl
                        }]));
                    }, ct).ConfigureAwait(false);

            return result;
        }


        #endregion
    }
}
