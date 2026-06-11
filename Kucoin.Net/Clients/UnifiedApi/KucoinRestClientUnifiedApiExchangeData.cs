using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
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
        public async Task<HttpResult<KucoinUaList<KucoinUaAnnouncement[]>>> GetAnnouncementsAsync(
            string? language = null,
            AnnouncementType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? pageSize = null,
            int? page = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("language", language);
            parameters.Add("type", type);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("pageSize", pageSize);
            parameters.Add("pageNumber", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/announcement", KucoinExchange.RateLimiter.PublicRest, 20, false);
            var result = await _baseClient.SendAsync<KucoinUaList<KucoinUaAnnouncement[]>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSpotSymbol[]>> GetSpotSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", ProductType.Spot);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinSpotSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinSpotSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinFuturesSymbol[]>> GetFuturesSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", ProductType.Futures);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinFuturesSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinFuturesSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", ProductType.CrossMargin);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinMarginSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinMarginSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", ProductType.IsolatedMargin);
            parameters.AddOptionalParameter("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/instrument", KucoinExchange.RateLimiter.PublicRest, 4);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinIsolatedMarginSymbol[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinIsolatedMarginSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaAsset>> GetAssetAsync(string? asset = null, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/currency", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaAsset>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaAsset[]>> GetAssetsAsync(IEnumerable<string>? assets = null, string? network = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("currencyList", assets);
            parameters.Add("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/asset/currencies", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaAsset[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaTicker[]>> GetTickersAsync(ProductType productType, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", productType);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/ticker", KucoinExchange.RateLimiter.PublicRest, 15, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaTicker[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinUaTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOrderBook>> GetOrderBookAsync(ProductType productType, string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", productType);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit == null ? "FULL" : limit.ToString());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/orderbook", KucoinExchange.RateLimiter.PublicRest, 3, true);
            var result = await _baseClient.SendAsync<KucoinUaOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaTrade[]>> GetRecentTradesAsync(ProductType productType, string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", productType);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/trade", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaTrade[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinUaTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaKline[]>> GetKlinesAsync(ProductType productType, string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", productType);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("startAt", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("endAt", endTime, DateTimeSerialization.SecondsString);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/kline", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaKline[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinUaKline[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/funding-rate", KucoinExchange.RateLimiter.PublicRest, 2, false);
            var result = await _baseClient.SendAsync<KucoinUaFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding History

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaFundingRateEntry[]>> GetFundingHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/funding-rate-history", KucoinExchange.RateLimiter.PublicRest, 5, false);
            var result = await _baseClient.SendAsync<KucoinUaResponse<KucoinUaFundingRateEntry[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinUaFundingRateEntry[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Cross Margin Config

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaCrossMarginConfig>> GetCrossMarginConfigAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/cross-config", KucoinExchange.RateLimiter.PublicRest, 25, false);
            var result = await _baseClient.SendAsync<KucoinUaCrossMarginConfig>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Collateral Ratio

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaCollateralRatio[]>> GetCollateralRatioAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/collateral-discount-ratio", KucoinExchange.RateLimiter.PublicRest, 10, false);
            var result = await _baseClient.SendAsync<KucoinUaCollateralRatio[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOpenInterest[]>> GetFuturesOpenInterestAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.AddCommaSeparated("symbol", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/open-interest", KucoinExchange.RateLimiter.PublicRest, 10, false);
            var result = await _baseClient.SendAsync<KucoinUaOpenInterest[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaOpenInterest[]>> GetFuturesOpenInterestHistoryAsync(
            string symbol, 
            DataPeriod interval,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("startAt", startTime);
            parameters.Add("endAt", endTime);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/market/open-interest", KucoinExchange.RateLimiter.PublicRest, 10, false);
            var result = await _baseClient.SendAsync<KucoinUaOpenInterest[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Service Status

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaServiceStatus>> GetServiceStatusAsync(ProductType productType, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("tradeType", productType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/server/status", KucoinExchange.RateLimiter.PublicRest, 3, false);
            var result = await _baseClient.SendAsync<KucoinUaServiceStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get KYC Regions

        /// <inheritdoc />
        public async Task<HttpResult<KucoinUaKYCRegion[]>> GetKYCRegionsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/ua/v1/user/kyc-region", KucoinExchange.RateLimiter.PublicRest, 20, true);
            var result = await _baseClient.SendAsync<KucoinUaKYCRegion[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
