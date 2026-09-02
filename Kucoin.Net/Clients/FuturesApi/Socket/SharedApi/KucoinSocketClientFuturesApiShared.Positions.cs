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
        #region Position client
        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToPositionUpdatesAsync(
                update => handler(update.ToType<SharedPosition[]>(new[] { 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), 
                        update.Data.Symbol,
                        new SharedOrderQuantity(contractQuantity: update.Data.CurrentQuantity),
                        update.Data.CurrentTime)
                        {
                            AverageOpenPrice = update.Data.AverageEntryPrice,
                            PositionMode = update.Data.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                            PositionSide = update.Data.CurrentQuantity < 0 ? SharedPositionSide.Short : SharedPositionSide.Long,
                            LiquidationPrice = update.Data.LiquidationPrice,
                            Leverage = update.Data.RealLeverage,
                            UnrealizedPnl = update.Data.UnrealizedPnl
                        }})),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
