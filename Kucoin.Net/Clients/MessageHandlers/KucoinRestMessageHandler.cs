using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Kucoin.Net;
using Kucoin.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.MessageHandlers
{
    internal class KucoinRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(KucoinExchange.SerializerContext);

        public KucoinRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is not KucoinResult kucoinResult)
                return null;

            if (kucoinResult.Code == 200000 || kucoinResult.Code == 200)
                return null;

            return new ServerError(kucoinResult.Code, _errorMapping.GetErrorInfo(kucoinResult.Code.ToString(), kucoinResult.Message));
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            int? code = document!.RootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetInt32() : null;
            if(code == null)
                return new ServerError(ErrorInfo.Unknown);

            var msg = document!.RootElement.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;
            return new ServerError(code.Value!, _errorMapping.GetErrorInfo(code.Value.ToString(), msg));
        }

        public override async ValueTask<ServerRateLimitError> ParseErrorRateLimitResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return new ServerRateLimitError();

            var resetHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("gw-ratelimit-reset", StringComparison.InvariantCultureIgnoreCase));
            if (resetHeader.Value == null)
                return await base.ParseErrorRateLimitResponse(httpStatusCode, responseHeaders, responseStream).ConfigureAwait(false);

            var value = resetHeader.Value.First();
            if (!int.TryParse(value, out var milliseconds))
                return await base.ParseErrorRateLimitResponse(httpStatusCode, responseHeaders, responseStream).ConfigureAwait(false);

            return new ServerRateLimitError()
            {
                RetryAfter = DateTime.UtcNow.AddMilliseconds(milliseconds)
            };
        }
    }
}
