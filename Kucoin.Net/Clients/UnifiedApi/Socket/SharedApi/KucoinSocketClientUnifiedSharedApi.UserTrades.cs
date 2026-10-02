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
        #region Subscribe User Trades

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.SubscribeToUserTradeUpdatesAsync(
                UnifiedAccountType.Unified,
                update => {
                    if (update.Data.TradeType != UnifiedTradeType.Normal)
                        return;

                    var sharedSymbol = ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, update.Data.Symbol)
                        ?? ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, update.Data.Symbol);
                    handler(update.ToType<SharedUserTrade[]>(
                        [new SharedUserTrade(
                            sharedSymbol,
                            update.Data.Symbol,
                            update.Data.OrderId,
                            update.Data.TradeId!.ToString()!,
                            update.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
#warning check quantity unit
                            new SharedOrderQuantity(update.Data.Quantity),
                            update.Data.Price,
                            update.Data.TradeTime)
                        {
                            Role = update.Data.TradeRole == LiquidityType.Maker ? SharedRole.Maker : SharedRole.Taker,
                            FeeAsset = update.Data.FeeAsset,
                            Fee = update.Data.TotalSettleFee,
                            ClientOrderId = update.Data.ClientOrderId
                        }]));
                    }, ct).ConfigureAwait(false);

            return result;
        }


        #endregion
    }
}
