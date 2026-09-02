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
        public Task<HttpResult<KucoinPaginated<KucoinEarnHolding>>> GetEarnHoldingAsync(string? asset = null, string? productId = null, EarnProductCategory? productCategory = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("productId", productId);
            parameters.Add("productCategory", productCategory);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v1/earn/hold-assets", KucoinExchange.RateLimiter.EarnRest, 5, true);
            return _baseClient.SendAsync<KucoinPaginated<KucoinEarnHolding>>(request, parameters, ct);
        }
    }
}
