using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Interfaces.Clients.SpotApi;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class KucoinClientSpotApiExchangeData : IKucoinClientSpotApiExchangeData
    {
        private readonly KucoinClientSpotApi _baseClient;
        internal KucoinClientSpotApiExchangeData(KucoinClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.Execute<long>(_baseClient.GetUri("timestamp"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
            return result.As(result ? JsonConvert.DeserializeObject<DateTime>(result.Data.ToString(), new DateTimeConverter()) : default);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinSymbol>>> GetSymbolsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", market);
            return await _baseClient.Execute<IEnumerable<KucoinSymbol>>(_baseClient.GetUri("symbols"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await _baseClient.Execute<KucoinTick>(_baseClient.GetUri("market/orderbook/level1"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinTicks>(_baseClient.GetUri("market/allTickers"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await _baseClient.Execute<Kucoin24HourStat>(_baseClient.GetUri("market/stats"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetMarketsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<string>>(_baseClient.GetUri("markets"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 20, 100);
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await _baseClient.Execute<KucoinOrderBook>(_baseClient.GetUri($"market/orderbook/level2_{limit}"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            return await _baseClient.Execute<KucoinOrderBook>(_baseClient.GetUri($"market/orderbook/level2", 3), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFullOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            return await _baseClient.Execute<KucoinFullOrderBook>(_baseClient.GetUri($"market/orderbook/level3", 3), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            return await _baseClient.Execute<IEnumerable<KucoinTrade>>(_baseClient.GetUri($"market/histories"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToSeconds(endTime));

            return await _baseClient.Execute<IEnumerable<KucoinKline>>(_baseClient.GetUri("market/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinAsset>>(_baseClient.GetUri("currencies"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAsset>> GetAssetAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            return await _baseClient.Execute<KucoinAsset>(_baseClient.GetUri($"currencies/{asset}"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, IEnumerable<string>? assets = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("base", fiatBase);
            parameters.AddOptionalParameter("currencies", assets?.Any() == true ? string.Join(",", assets) : null);

            return await _baseClient.Execute<Dictionary<string, decimal>>(_baseClient.GetUri("prices"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

    }
}
