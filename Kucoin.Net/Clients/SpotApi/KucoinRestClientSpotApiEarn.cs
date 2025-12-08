using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Enums;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiEarn : IKucoinRestClientSpotApiEarn
    {
        private readonly KucoinRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new();

        internal KucoinRestClientSpotApiEarn(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public Task<WebCallResult<KucoinPaginated<KucoinEarnHolding>>> GetEarnHoldingAsync(string? asset = null, string? productId = null, EarnProductCategory? productCategory = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("productId", productId);
            parameters.AddOptionalEnum("productCategory", productCategory);
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v1/earn/hold-assets", KucoinExchange.RateLimiter.EarnRest, 5, true);
            return _baseClient.SendAsync<KucoinPaginated<KucoinEarnHolding>>(request, parameters, ct);
        }
    }
}
