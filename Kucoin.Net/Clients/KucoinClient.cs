using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinClient" />
    public class KucoinClient : BaseRestClient, IKucoinClient
    {
        /// <inheritdoc />
        public IKucoinClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IKucoinClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Create a new instance of KucoinClient using the default options
        /// </summary>
        public KucoinClient() : this(KucoinClientOptions.Default)
        {

        }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public KucoinClient(KucoinClientOptions options) : base("Kucoin", options)
        {
            SpotApi = AddApiClient(new KucoinClientSpotApi(log, this, options));
            FuturesApi = AddApiClient(new KucoinClientFuturesApi(log, this, options));

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KucoinClientOptions options)
        {
            KucoinClientOptions.Default = options;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
            {
                var errorBody = error.ToString();
                return new ServerError(string.IsNullOrEmpty(errorBody) ? "Unknown error" : errorBody);
            }

            if (error["code"] != null && error["msg"] != null)
            {
                var result = error.ToObject<KucoinResult<object>>();
                if (result == null)
                    return new ServerError(error["msg"]!.ToString());

                return new ServerError(result.Code, result.Message!);
            }

            return new ServerError(error.ToString());
        }

        internal async Task<WebCallResult> Execute(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<KucoinResult<object>>(apiClient, uri, method, ct, parameters, signed).ConfigureAwait(false);
            if (!result)
                return result.AsDatalessError(result.Error!);

            if (result.Data.Code != 200000)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.AsDataless();
        }

        internal async Task<WebCallResult<T>> Execute<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<KucoinResult<T>>(apiClient, uri, method, ct, parameters, signed, requestWeight: weight, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200000)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As(result.Data.Data);
        }
    }
}
