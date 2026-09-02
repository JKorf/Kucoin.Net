using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class KucoinRestClientSpotApiSubAccount : IKucoinRestClientSpotApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly KucoinRestClientSpotApi _baseClient;

        internal KucoinRestClientSpotApiSubAccount(KucoinRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinSubUser>>> GetSubAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v2/sub/user", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinSubUser>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUser>> CreateSubAccountAsync(string subName, string password, string permissions, string? remarks = null, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v2/sub/user/created", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinSubUser>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUserBalances>> GetSubAccountBalancesAsync(string subAccountId, bool? includeZeroBalances = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("includeBaseAmount", includeZeroBalances);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v1/sub-accounts/{subAccountId}", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinSubUserBalances>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinPaginated<KucoinSubUserBalances>>> GetSubAccountsBalancesAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("currentPage", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v2/sub-accounts", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinSubUserBalances>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUserKey[]>> GetSubAccountApiKeyAsync(string subAccountName, string? apiKey = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountName);
            parameters.Add("apiKey", apiKey);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKey[]>>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<KucoinSubUserKey[]>(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail<KucoinSubUserKey[]>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail<KucoinSubUserKey[]>(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUserKeyDetails>> CreateSubAccountApiKeyAsync(
            string subAccountName, 
            string passphrase, 
            string remark, 
            string? permissions = null, 
            string? ipWhitelist = null,
            string? expire = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("remark", remark);
            parameters.Add("permissions", permissions);
            parameters.Add("ipWhitelist", ipWhitelist);
            parameters.Add("expire", expire);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyDetails>>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<KucoinSubUserKeyDetails>(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail<KucoinSubUserKeyDetails>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail<KucoinSubUserKeyDetails>(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUserKeyEdited>> EditSubAccountApiKeyAsync(
            string subAccountName,
            string apiKey,
            string passphrase,
            string? permissions = null,
            string? ipWhitelist = null,
            string? expire = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("apiKey", apiKey);
            parameters.Add("permissions", permissions);
            parameters.Add("ipWhitelist", ipWhitelist);
            parameters.Add("expire", expire);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v1/sub/api-key/update", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyEdited>>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<KucoinSubUserKeyEdited>> DeleteSubAccountApiKeyAsync(
            string subAccountName,
            string apiKey,
            string passphrase,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("apiKey", apiKey);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyEdited>>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail<KucoinSubUserKeyEdited>(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult> EnableMarginPermissionsAsync(string subAccountId, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/sub/user/margin/enable", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            var result = await _baseClient.SendRawAsync<KucoinResult>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result);
        }

        /// <inheritdoc />
        public async Task<HttpResult> EnableFuturesPermissionsAsync(string subAccountId, CancellationToken ct = default)
        {
            var parameters = new Parameters(KucoinExchange._parameterSerializationSettings);
            parameters.Add("subName", subAccountId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/sub/user/futures/enable", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            var result = await _baseClient.SendRawAsync<KucoinResult>(request, parameters, ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return HttpResult.Fail(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data.Message! }));

            return HttpResult.Ok(result);
        }
    }
}
