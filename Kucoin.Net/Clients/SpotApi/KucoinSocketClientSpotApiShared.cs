using Kucoin.Net;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.SubscribeModels;

namespace Kucoin.Net.Clients.SpotApi
{
    internal partial class KucoinSocketClientSpotApi : IKucoinSocketClientSpotApiShared
    {
        public string Exchange => KucoinExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToSnapshotUpdatesAsync(symbol, update => handler(update.As(new SharedTicker
            {
                Symbol = update.Data.Symbol,
                HighPrice = update.Data.HighPrice ?? 0,
                LastPrice = update.Data.LastPrice ?? 0,
                LowPrice = update.Data.LowPrice ?? 0
            })), ct).ConfigureAwait(false);

            return result;
        }
    }
}
