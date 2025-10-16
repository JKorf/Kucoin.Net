using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientUnifiedApiExchangeData : IKucoinRestClientUnifiedApiExchangeData
    {
        private readonly KucoinRestClientUnifiedApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientUnifiedApiExchangeData(KucoinRestClientUnifiedApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Announcements

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaList<KucoinUaAnnouncement[]>>> GetAnnouncementsAsync(
            string? language = null,
            AnnouncementType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? pageSize = null,
            int? page = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("language", language);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("pageSize", pageSize);
            parameters.AddOptional("pageNumber", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/announcement", KucoinExchange.RateLimiter.PublicRest, 20, false);
            var result = await _baseClient.SendAsync<KucoinUaList<KucoinUaAnnouncement[]>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSpotSymbol[]>> GetSpotSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", ProductType.Spot);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinSpotSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinSpotSymbol[]>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesSymbol[]>> GetFuturesSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", ProductType.Futures);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinFuturesSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinFuturesSymbol[]>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", ProductType.CrossMargin);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinMarginSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinMarginSymbol[]>(result.Data?.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", ProductType.IsolatedMargin);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinIsolatedMarginSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinIsolatedMarginSymbol[]>(result.Data?.Data);
        }
        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaAsset>> GetAssetAsync(string? asset = null, string? network = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/currency", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaAsset>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaTicker[]>> GetTickersAsync(ProductType productType, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", productType);
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/ticker", KucoinExchange.RateLimiter.PublicRest, 15, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaTicker[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinUaTicker[]>(result.Data?.Data);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaOrderBook>> GetOrderBookAsync(ProductType productType, string symbol, int? limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", productType);
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit == null ? "FULL" : limit.ToString());
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/orderbook", KucoinExchange.RateLimiter.PublicRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinUaOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaTrade[]>> GetRecentTradesAsync(ProductType productType, string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", productType);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/trade", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaTrade[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinUaTrade[]>(result.Data?.Data);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaKline[]>> GetKlinesAsync(ProductType productType, string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", productType);
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalSecondsString("startAt", startTime);
            parameters.AddOptionalSecondsString("endAt", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/kline", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaKline[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinUaKline[]>(result.Data?.Data);
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/funding-rate", KucoinExchange.RateLimiter.PublicRest, 2, false);
            var result = await _baseClient.SendAsync<KucoinUaFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding History

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaFundingRateEntry[]>> GetFundingHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddMillisecondsString("startAt", startTime);
            parameters.AddMillisecondsString("endAt", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/funding-rate-history", KucoinExchange.RateLimiter.PublicRest, 5, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaFundingRateEntry[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As<KucoinUaFundingRateEntry[]>(result.Data?.Data);
        }

        #endregion

        #region Get Cross Margin Config

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaCrossMarginConfig>> GetCrossMarginConfigAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/market/cross-config", KucoinExchange.RateLimiter.PublicRest, 25, false);
            var result = await _baseClient.SendAsync<KucoinUaCrossMarginConfig>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Service Status

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinUaServiceStatus>> GetServiceStatusAsync(ProductType productType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("tradeType", productType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/ua/v1/server/status", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaServiceStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
