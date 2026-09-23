using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Enums;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using System.Collections.Generic;
using Kucoin.Net.Objects.Models.Futures;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiMargin : IKucoinRestClientSpotApiMargin
    {
        private readonly KucoinRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientSpotApiMargin(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinIndexBase>> GetMarginMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v1/mark-price/{symbol}/current", KucoinExchange.RateLimiter.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinIndexBase>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinIndexBase[]>> GetMarginMarkPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/mark-price/all-symbols", KucoinExchange.RateLimiter.PublicRest, 10);
            return await _baseClient.SendAsync<KucoinIndexBase[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinMarginConfig>> GetMarginConfigurationAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/margin/config", KucoinExchange.RateLimiter.SpotRest, 25);
            return await _baseClient.SendAsync<KucoinMarginConfig>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinTradingPairConfiguration[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/isolated/symbols", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<KucoinTradingPairConfiguration[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCrossRiskLimitConfig[]>> GetCrossMarginRiskLimitAndConfig(CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "isIsolated", false }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/currencies", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<KucoinCrossRiskLimitConfig[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinIsolatedRiskLimitConfig[]>> GetIsolatedMarginRiskLimitAndConfig(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "isIsolated", true },
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/currencies", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<KucoinIsolatedRiskLimitConfig[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinNewBorrowOrder>> BorrowAsync(
            string asset,
            BorrowOrderType timeInForce,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset },
                { "size", quantity }
            };
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("isIsolated", isIsolated);
            parameters.Add("symbol", symbol);
            parameters.Add("isHf", isHf);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/margin/borrow", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinNewBorrowOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinNewBorrowOrder>> RepayAsync(
            string asset,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset },
                { "size", quantity }
            };
            parameters.Add("isIsolated", isIsolated);
            parameters.Add("symbol", symbol);
            parameters.Add("isHf", isHf);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/margin/repay", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinNewBorrowOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetBorrowHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            parameters.Add("isIsolated", isIsolated);
            parameters.Add("symbol", symbol);
            parameters.Add("orderNo", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/borrow", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinBorrowOrderV3>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetRepayHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            parameters.Add("isIsolated", isIsolated);
            parameters.Add("symbol", symbol);
            parameters.Add("orderNo", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/repay", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinBorrowOrderV3>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinMarginInterest>>> GetInterestHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            parameters.Add("isIsolated", isIsolated);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/interest", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinMarginInterest>>(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<HttpResult<KucoinLendingAsset[]>> GetLendingAssetsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/project/list", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinLendingAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinLendingInterest[]>> GetInterestRatesAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/project/marketInterestRate", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinLendingInterest[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinLendingResult>> SubscribeAsync(string asset, decimal quantity, decimal interestRate, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };
            parameters.Add("size", quantity);
            parameters.Add("interestRate", interestRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/purchase", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinLendingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinLendingResult>> RedeemAsync(string asset, decimal quantity, string subscribeOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset },
                { "purchaseOrderNo", subscribeOrderId }
            };
            parameters.Add("size", quantity);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/redeem", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinLendingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult> EditSubscriptionOrderAsync(string asset, decimal interestRate, string subscribeOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset },
                { "purchaseOrderNo", subscribeOrderId }
            };
            parameters.Add("interestRate", interestRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/lend/purchase/update", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinRedemption>>> GetRedemptionOrdersAsync(string asset, string status, string? redeemOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            parameters.Add("redeemOrderNo", redeemOrderId);
            parameters.Add("status", status);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/redeem/orders", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinRedemption>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinLendSubscription>>> GetSubscriptionOrdersAsync(string asset, string status, string? purchaseOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings)
            {
                { "currency", asset }
            };

            parameters.Add("purchaseOrderNo", purchaseOrderId);
            parameters.Add("status", status);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/purchase/orders", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinLendSubscription>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult> SetLeverageMultiplierAsync(decimal leverage, string? symbol = null, bool? isolatedMargin = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("leverage", leverage);
            parameters.Add("symbol", symbol);
            parameters.Add("isIsolated", isolatedMargin);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/position/update-user-leverage", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCrossMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/symbols", KucoinExchange.RateLimiter.SpotRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinCrossMarginSymbols>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<KucoinCrossMarginSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Items);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinBorrowInterestRates>> GetBorrowInterestRateAsync(string? asset = null, int? vipLevel = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("vipLevel", vipLevel);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/borrowRate", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<KucoinBorrowInterestRates>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinCollateralRatios[]>> GetMarginCollateralRatioAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currencyList", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/margin/collateralRatio", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinCollateralRatios[]>(request, parameters, ct).ConfigureAwait(false);
        }
    }
}
