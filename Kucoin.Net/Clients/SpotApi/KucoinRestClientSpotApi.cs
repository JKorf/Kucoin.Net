using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinRestClientSpotApi" />
    internal partial class KucoinRestClientSpotApi : RestApiClient, IKucoinRestClientSpotApi
    {
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        /// <inheritdoc />
        public string ExchangeName => "Kucoin";

        /// <inheritdoc />
        public IKucoinRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IKucoinRestClientSpotApiSubAccount SubAccount { get; }

        /// <inheritdoc />
        public IKucoinRestClientSpotApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IKucoinRestClientSpotApiTrading Trading { get; }

        /// <inheritdoc />
        public IKucoinRestClientSpotApiHfTrading HfTrading { get; }

        /// <inheritdoc />
        public IKucoinRestClientSpotApiMargin Margin { get; }

        internal KucoinRestClientSpotApi(ILogger logger, HttpClient? httpClient, KucoinRestClient baseClient, KucoinRestOptions options)
            : base(logger, httpClient, options.Environment.SpotAddress, options, options.SpotOptions)
        {
            Account = new KucoinRestClientSpotApiAccount(this);
            SubAccount = new KucoinRestClientSpotApiSubAccount(this);
            ExchangeData = new KucoinRestClientSpotApiExchangeData(this);
            Trading = new KucoinRestClientSpotApiTrading(this);
            HfTrading = new KucoinRestClientSpotApiHfTrading(this);
            Margin = new KucoinRestClientSpotApiMargin(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange.SerializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        internal async Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<KucoinResult>(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDatalessError(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.AsDataless();
        }

        internal async Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<KucoinResult<T>>(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<T>> SendRawAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            return result.As(result.Data);
        }

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("gw-ratelimit-reset", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return base.ParseRateLimitResponse(httpStatusCode, responseHeaders, accessor);

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var milliseconds))
                return base.ParseRateLimitResponse(httpStatusCode, responseHeaders, accessor);

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            return new ServerRateLimitError(msg!)
            {
                RetryAfter = DateTime.UtcNow.AddMilliseconds(milliseconds)
            };
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsJson)
                return new ServerError(null, "Unknown request error", exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(null, "Unknown request error", exception: exception);

            if (code == null)
                return new ServerError(null, msg, exception);

            return new ServerError(code.Value, msg, exception);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        public IKucoinRestClientSpotApiShared SharedClient => this;

    }
}
