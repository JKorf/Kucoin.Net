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

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class KucoinClientFuturesApiExchangeData : IKucoinClientFuturesApiExchangeData
    {
        private readonly KucoinClientFuturesApi _baseClient;

        internal KucoinClientFuturesApiExchangeData(KucoinClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<IEnumerable<KucoinContract>>(_baseClient.GetUri("contracts/active"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinContract>(_baseClient.GetUri("contracts/" + symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinFuturesTick>(_baseClient.GetUri("ticker"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Order book

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinOrderBook>(_baseClient.GetUri("level2/snapshot"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 20, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<KucoinOrderBook>(_baseClient.GetUri("level2/depth" + depth), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Trade history

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _baseClient.Execute<IEnumerable<KucoinFuturesTrade>>(_baseClient.GetUri("trade/history"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Index

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<KucoinPaginatedSlider<KucoinFuturesInterest>>(_baseClient.GetUri("interest/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<KucoinPaginatedSlider<KucoinIndex>>(_baseClient.GetUri("index/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinMarkPrice>(_baseClient.GetUri($"mark-price/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endAt", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await _baseClient.Execute<KucoinPaginatedSlider<KucoinPremiumIndex>>(_baseClient.GetUri("premium/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinFundingRate>(_baseClient.GetUri($"funding-rate/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.Execute<long>(_baseClient.GetUri("timestamp"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
            return result.As(result ? new DateTime(1970, 1, 1).AddMilliseconds(result.Data) : default);
        }

        #endregion

        #region Server status

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.Execute<KucoinFuturesServiceStatus>(_baseClient.GetUri("status"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("granularity", JsonConvert.SerializeObject(interval, new FuturesKlineIntervalConverter(false)));
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _baseClient.Execute<IEnumerable<KucoinFuturesKline>>(_baseClient.GetUri("kline/query"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

    }
}
