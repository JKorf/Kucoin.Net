using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiExchangeData : IKucoinRestClientSpotApiExchangeData
    {
        private readonly KucoinRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientSpotApiExchangeData(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/timestamp", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<DateTime>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSymbol[]>> GetSymbolsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("market", market);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v2/symbols", KucoinExchange.RateLimiter.PublicRest, 4);
            return await _baseClient.SendAsync<KucoinSymbol[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v2/symbols/{symbol}", KucoinExchange.RateLimiter.PublicRest, 4);
            return await _baseClient.SendAsync<KucoinSymbol>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings) { { "symbol", symbol } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/market/orderbook/level1", KucoinExchange.RateLimiter.PublicRest, 2);
            var result = await _baseClient.SendAsync<KucoinTick>(request, parameters, ct).ConfigureAwait(false);
            if (result.Success && result.Data == null)
                return HttpResult.Fail<KucoinTick>(result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol doesn't exist")));
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/market/allTickers", KucoinExchange.RateLimiter.PublicRest, 15);
            return await _baseClient.SendAsync<KucoinTicks>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings) { { "symbol", symbol } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/market/stats", KucoinExchange.RateLimiter.PublicRest, 15);
            var result = await _baseClient.SendAsync<Kucoin24HourStat>(request, parameters, ct).ConfigureAwait(false);
            if (result.Success && result.Data.Volume == null)
                return HttpResult.Fail<Kucoin24HourStat>(result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol doesn't exist")));
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<string[]>> GetMarketsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/markets", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<string[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default)
        {
            limit.ValidateIntValues(nameof(limit), 20, 100);
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var weight = limit == 20 ? 2 : 4;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v1/market/orderbook/level2_{limit}", KucoinExchange.RateLimiter.PublicRest, weight);
            var result = await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct, weight).ConfigureAwait(false);
            if (result.Success && result.Data?.Asks == null)
                return HttpResult.Fail<KucoinOrderBook>(result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol doesn't exist")));
            return result;

        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/market/orderbook/level2", KucoinExchange.RateLimiter.SpotRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (result.Success && result.Data?.Asks == null)
                return HttpResult.Fail<KucoinOrderBook>(result, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, "Symbol doesn't exist")));
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinTrade[]>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/market/histories", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("type", interval);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToSeconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToSeconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/market/candles", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinKline[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinAssetDetails[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/currencies", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinAssetDetails[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinAssetDetails>> GetAssetAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v3/currencies/{asset}", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinAssetDetails>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, IEnumerable<string>? assets = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("base", fiatBase);
            parameters.AddOptionalParameter("currencies", assets?.Any() == true ? string.Join(",", assets) : null);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/prices", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<Dictionary<string, decimal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #region Get Announcements

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinAnnouncement>>> GetAnnouncementsAsync(int? page = null, int? pageSize = null, string? announcementType = null, string? language = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);
            parameters.Add("annType", announcementType);
            parameters.Add("lang", language);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/announcements", KucoinExchange.RateLimiter.PublicRest, 1, false);
            var result = await _baseClient.SendAsync<KucoinPaginated<KucoinAnnouncement>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Call Auction Order Book

        /// <inheritdoc />
        public async Task<HttpResult<KucoinOrderBook>> GetCallAuctionOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v1/market/orderbook/callauction/level2_{depth}", KucoinExchange.RateLimiter.PublicRest, 1, false);
            var result = await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Get Call Auction Info

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCallAuctionInfo>> GetCallAuctionInfoAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/market/callauctionData", KucoinExchange.RateLimiter.PublicRest, 1, false);
            var result = await _baseClient.SendAsync<KucoinCallAuctionInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
