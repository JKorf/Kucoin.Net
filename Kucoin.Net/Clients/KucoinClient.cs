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
    public class KucoinClient : BaseRestClient, IKucoinClient
    {
        public IKucoinClientSpotApi SpotApi { get; }
        public IKucoinClientFuturesApi FuturesApi { get; }

        public KucoinClient() : this(KucoinClientOptions.Default)
        {

        }

        public KucoinClient(KucoinClientOptions options) : base("Kucoin", options)
        {
            SpotApi = new KucoinClientSpotApi(this, options);
            FuturesApi = new KucoinClientFuturesApi(this, options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
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
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }

        internal async Task<WebCallResult<T>> Execute<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1)
        {
            var result = await SendRequestAsync<KucoinResult<T>>(apiClient, uri, method, ct, parameters, signed, requestWeight: weight).ConfigureAwait(false);
            if (!result)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As(result.Data.Data);
        }

        public override void Dispose()
        {
            SpotApi.Dispose();
            FuturesApi.Dispose();
            base.Dispose();
        }
    }
}
