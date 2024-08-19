using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using System.Security.Cryptography;
using Kucoin.Net.Objects.Models.Futures.Socket;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class KucoinRestClientFuturesApiExchangeData : IKucoinRestClientFuturesApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientFuturesApi _baseClient;

        internal KucoinRestClientFuturesApiExchangeData(KucoinRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contracts/active", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<KucoinContract>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contracts/" + symbol, KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinContract>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/ticker", KucoinExchange.RateLimiter.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinFuturesTick>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesTick>>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/allTickers", KucoinExchange.RateLimiter.PublicRest, 15);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesTick>>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Order book

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/level2/snapshot", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 20, 100);

            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);

            var weight = depth == 20 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/level2/depth" + depth, KucoinExchange.RateLimiter.PublicRest, weight);
            return await _baseClient.SendAsync<KucoinOrderBook>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Trade history

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/trade/history", KucoinExchange.RateLimiter.PublicRest, 5);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Index

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/interest/query", KucoinExchange.RateLimiter.PublicRest, 5);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinFuturesInterest>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/index/query", KucoinExchange.RateLimiter.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinIndex>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/mark-price/{symbol}/current", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinMarkPrice>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/premium/query", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<KucoinPaginatedSlider<KucoinPremiumIndex>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/funding-rate/{symbol}/current", KucoinExchange.RateLimiter.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinFundingRate>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/timestamp", KucoinExchange.RateLimiter.PublicRest, 2);
            var result = await _baseClient.SendAsync<long>(request, null, ct).ConfigureAwait(false);
            return result.As(result ? new DateTime(1970, 1, 1).AddMilliseconds(result.Data) : default);
        }

        #endregion

        #region Server status

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/status", KucoinExchange.RateLimiter.PublicRest, 4);
            return await _baseClient.SendAsync<KucoinFuturesServiceStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("granularity", JsonConvert.SerializeObject(interval, new FuturesKlineIntervalConverter(false)));
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/kline/query", KucoinExchange.RateLimiter.PublicRest, 3);
            return await _baseClient.SendAsync<IEnumerable<KucoinFuturesKline>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get 24h Transaction Volume

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransactionVolume>> Get24HourTransactionVolumeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/trade-statistics", KucoinExchange.RateLimiter.FuturesRest, 3, true);
            return await _baseClient.SendAsync<KucoinTransactionVolume>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddMilliseconds("from", startTime);
            parameters.AddMilliseconds("to", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/contract/funding-rates", KucoinExchange.RateLimiter.PublicRest, 5);
            return await _baseClient.SendAsync<IEnumerable<KucoinFundingRateHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
