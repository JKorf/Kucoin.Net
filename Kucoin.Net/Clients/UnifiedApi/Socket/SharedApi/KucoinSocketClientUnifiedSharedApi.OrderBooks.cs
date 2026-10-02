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
        #region Subscribe Incremental Order Book

        public SubscribeIncrementalOrderBookOptions SubscribeIncrementalOrderBookOptions { get; } = new SubscribeIncrementalOrderBookOptions(_exchangeName, false, [500], SharedOrderBookSubscriptionType.SnapshotThenIncremental);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedIncrementalOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeIncrementalOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToOrderBookUpdatesAsync(
                request.TradingMode == TradingMode.Spot ? UnifiedAccountType.Spot : UnifiedAccountType.Futures,
                symbol,
                OrderBookDepth.Top5,
                update => handler(update.ToType(
                    new SharedIncrementalOrderBook(
                        request.Symbol.TradingMode == TradingMode.Spot ? SharedQuantityType.BaseAsset : SharedQuantityType.Contracts,
                        update.Data.StartSequence,
                        update.Data.EndSequence,
                        update.Data.Asks,
                        update.Data.Bids
                        )
                    {
                    })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Subscribe Order Book

        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, [1, 5, 50]);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);


            var depth = request.Limit == 1 ? OrderBookDepth.BestBidOffer
                : request.Limit == 5 ? OrderBookDepth.Top5 :
                OrderBookDepth.Top50;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.SubscribeToOrderBookUpdatesAsync(
                request.TradingMode == TradingMode.Spot ? UnifiedAccountType.Spot : UnifiedAccountType.Futures,
                symbol,
                depth,
                update => handler(update.ToType(
                    new SharedOrderBook(
                        request.Symbol.TradingMode == TradingMode.Spot ? SharedQuantityType.BaseAsset : SharedQuantityType.Contracts,
                        update.Data.EndSequence,
                        update.Data.Asks,
                        update.Data.Bids
                        )
                    {
                    })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
