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
        public async Task<WebCallResult<KucoinIndexBase>> GetMarginMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/mark-price/{symbol}/current", KucoinExchange.RateLimiter.PublicRest, 2);
            return await _baseClient.SendAsync<KucoinIndexBase>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinIndexBase>>> GetMarginMarkPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/mark-price/all-symbols", KucoinExchange.RateLimiter.PublicRest, 10);
            return await _baseClient.SendAsync<IEnumerable<KucoinIndexBase>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarginConfig>> GetMarginConfigurationAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/margin/config", KucoinExchange.RateLimiter.SpotRest, 25);
            return await _baseClient.SendAsync<KucoinMarginConfig>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinTradingPairConfiguration>>> GetMarginTradingPairConfigurationAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/isolated/symbols", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinTradingPairConfiguration>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinCrossRiskLimitConfig>>> GetCrossMarginRiskLimitAndConfig(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "isIsolated", false }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/currencies", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinCrossRiskLimitConfig>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinIsolatedRiskLimitConfig>>> GetIsolatedMarginRiskLimitAndConfig(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "isIsolated", true },
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/currencies", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinIsolatedRiskLimitConfig>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewBorrowOrder>> BorrowAsync(
            string asset,
            TimeInForce timeInForce,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "currency", asset },
                { "size", quantity }
            };
            parameters.AddEnum("timeInForce", timeInForce);
            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("isHf", isHf);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/margin/borrow", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinNewBorrowOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewBorrowOrder>> RepayAsync(
            string asset,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "currency", asset },
                { "size", quantity }
            };
            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("isHf", isHf);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/margin/repay", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinNewBorrowOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetBorrowHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderNo", orderId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/borrow", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinBorrowOrderV3>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetRepayHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("orderNo", orderId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/repay", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinBorrowOrderV3>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinMarginInterest>>> GetInterestHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            parameters.AddOptional("isIsolated", isIsolated);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/interest", KucoinExchange.RateLimiter.SpotRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinMarginInterest>>(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinLendingAsset>>> GetLendingAssetsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/project/list", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinLendingAsset>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinLendingInterest>>> GetInterestRatesAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/project/marketInterestRate", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<KucoinLendingInterest>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinLendingResult>> SubscribeAsync(string asset, decimal quantity, decimal interestRate, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };
            parameters.AddString("size", quantity);
            parameters.AddString("interestRate", interestRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/purchase", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinLendingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinLendingResult>> RedeemAsync(string asset, decimal quantity, string subscribeOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "purchaseOrderNo", subscribeOrderId }
            };
            parameters.AddString("size", quantity);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/redeem", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync<KucoinLendingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> EditSubscriptionOrderAsync(string asset, decimal interestRate, string subscribeOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset },
                { "purchaseOrderNo", subscribeOrderId }
            };
            parameters.AddString("interestRate", interestRate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/lend/purchase/update", KucoinExchange.RateLimiter.SpotRest, 15, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinRedemption>>> GetRedemptionOrdersAsync(string asset, string status, string? redeemOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            parameters.AddOptional("redeemOrderNo", redeemOrderId);
            parameters.AddOptional("status", status);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/redeem/orders", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinRedemption>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinLendSubscription>>> GetSubscriptionOrdersAsync(string asset, string status, string? purchaseOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "currency", asset }
            };

            parameters.AddOptional("purchaseOrderNo", purchaseOrderId);
            parameters.AddOptional("status", status);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/purchase/orders", KucoinExchange.RateLimiter.SpotRest, 10, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinLendSubscription>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> SetLeverageMultiplierAsync(decimal leverage, string? symbol = null, bool? isolatedMargin = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddString("leverage", leverage);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptional("isIsolated", isolatedMargin);

            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/position/update-user-leverage", KucoinExchange.RateLimiter.SpotRest, 5, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinCrossMarginSymbol>>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/margin/symbols", KucoinExchange.RateLimiter.SpotRest, 5, true);
            var result = await _baseClient.SendAsync<KucoinCrossMarginSymbols>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<KucoinCrossMarginSymbol>>(result.Data?.Items);
        }

    }
}
