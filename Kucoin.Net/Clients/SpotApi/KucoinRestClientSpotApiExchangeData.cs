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
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects;
using Kucoin.Net.ExtensionMethods;
using System.Security.Cryptography;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class KucoinRestClientSpotApiExchangeData : IKucoinRestClientSpotApiExchangeData
    {
        private readonly KucoinRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientSpotApiExchangeData(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/timestamp", KucoinExchange.RateLimiters.PublicRest, 3);
            var result = await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
            return result.As(result ? JsonConvert.DeserializeObject<DateTime>(result.Data.ToString(), new DateTimeConverter()) : default);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinSymbol>>> GetSymbolsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("market", market);
            // Testnet doesn't support V2
            var apiVersion = _baseClient.BaseAddress == KucoinApiAddresses.TestNet.SpotAddress ? 1 : 2;
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v{apiVersion}/symbols", KucoinExchange.RateLimiters.PublicRest, 4);
            return await _baseClient.SendAsync<IEnumerable<KucoinSymbol>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/orderbook/level1", KucoinExchange.RateLimiters.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinTick>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/allTickers", KucoinExchange.RateLimiters.PublicRest, 15);
            return await _baseClient.SendAsync<KucoinTicks>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/stats", KucoinExchange.RateLimiters.PublicRest, 15);
            return await _baseClient.SendAsync<Kucoin24HourStat>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetMarketsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/markets", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<string>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 20, 100);
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            var weight = limit == 20 ? 2 : 4;
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/orderbook/level2_{limit}", KucoinExchange.RateLimiters.PublicRest, weight);
            return await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/orderbook/level2", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/histories", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<KucoinTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToSeconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/market/candles", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<KucoinKline>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinAssetDetails>>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/currencies", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<KucoinAssetDetails>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAssetDetails>> GetAssetAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/currencies/{asset}", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinAssetDetails>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, IEnumerable<string>? assets = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("base", fiatBase);
            parameters.AddOptionalParameter("currencies", assets?.Any() == true ? string.Join(",", assets) : null);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/prices", KucoinExchange.RateLimiters.PublicRest, 3);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIndexBase>> GetMarginMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/mark-price/{symbol}/current", KucoinExchange.RateLimiters.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinIndexBase>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginConfig>> GetMarginConfigurationAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/margin/config", KucoinExchange.RateLimiters.PublicRest, 25);
            return await _baseClient.SendAsync<KucoinMarginConfig>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTradingPairConfiguration>>> GetMarginTradingPairConfigurationAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/isolated/symbols", KucoinExchange.RateLimiters.SpotRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinTradingPairConfiguration>>(request, null, ct).ConfigureAwait(false);
        }
    }
}
