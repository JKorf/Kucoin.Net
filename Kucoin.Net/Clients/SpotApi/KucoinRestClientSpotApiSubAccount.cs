using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
        public async Task<WebCallResult<KucoinPaginated<KucoinSubUser>>> GetSubAccountsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v2/sub/user", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinSubUser>>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSubUser>> CreateSubAccountAsync(string subName, string password, string permissions, string? remarks = null, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v2/sub/user/created", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinSubUser>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSubUserBalances>> GetSubAccountBalancesAsync(string subAccountId, bool? includeZeroBalances = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("includeBaseAmount", includeZeroBalances);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v1/sub-accounts/{subAccountId}", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinSubUserBalances>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinSubUserBalances>>> GetSubAccountsBalancesAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currentPage", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v2/sub-accounts", KucoinExchange.RateLimiter.ManagementRest, 15, true);
            return await _baseClient.SendAsync<KucoinPaginated<KucoinSubUserBalances>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinSubUserKey>>> GetSubAccountApiKeyAsync(string subAccountName, string? apiKey = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subName", subAccountName);
            parameters.AddOptional("apiKey", apiKey);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<IEnumerable<KucoinSubUserKey>>>(request, parameters, ct).ConfigureAwait(false);

            if (!result)
                return result.AsError<IEnumerable<KucoinSubUserKey>>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<IEnumerable<KucoinSubUserKey>>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return result.AsError<IEnumerable<KucoinSubUserKey>>(new ServerError(result.Data.Message!));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSubUserKeyDetails>> CreateSubAccountApiKeyAsync(
            string subAccountName, 
            string passphrase, 
            string remark, 
            string? permissions = null, 
            string? ipWhitelist = null,
            string? expire = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("remark", remark);
            parameters.AddOptional("permissions", permissions);
            parameters.AddOptional("ipWhitelist", ipWhitelist);
            parameters.AddOptional("expire", expire);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyDetails>>(request, parameters, ct).ConfigureAwait(false);

            if(!result)
                return result.AsError<KucoinSubUserKeyDetails>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<KucoinSubUserKeyDetails>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return result.AsError<KucoinSubUserKeyDetails>(new ServerError(result.Data.Message!));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSubUserKeyEdited>> EditSubAccountApiKeyAsync(
            string subAccountName,
            string apiKey,
            string passphrase,
            string? permissions = null,
            string? ipWhitelist = null,
            string? expire = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("apiKey", apiKey);
            parameters.AddOptional("permissions", permissions);
            parameters.AddOptional("ipWhitelist", ipWhitelist);
            parameters.AddOptional("expire", expire);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v1/sub/api-key/update", KucoinExchange.RateLimiter.ManagementRest, 20, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyEdited>>(request, parameters, ct).ConfigureAwait(false);

            if (!result)
                return result.AsError<KucoinSubUserKeyEdited>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<KucoinSubUserKeyEdited>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return result.AsError<KucoinSubUserKeyEdited>(new ServerError(result.Data.Message!));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinSubUserKeyEdited>> DeleteSubAccountApiKeyAsync(
            string subAccountName,
            string apiKey,
            string passphrase,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subName", subAccountName);
            parameters.Add("passphrase", passphrase);
            parameters.Add("apiKey", apiKey);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/api/v1/sub/api-key", KucoinExchange.RateLimiter.ManagementRest, 30, true);
            var result = await _baseClient.SendRawAsync<KucoinResult<KucoinSubUserKeyEdited>>(request, parameters, ct).ConfigureAwait(false);

            if (!result)
                return result.AsError<KucoinSubUserKeyEdited>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<KucoinSubUserKeyEdited>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            if (!string.IsNullOrEmpty(result.Data.Message))
                return result.AsError<KucoinSubUserKeyEdited>(new ServerError(result.Data.Message!));

            return result.As(result.Data.Data);
        }
    }
}