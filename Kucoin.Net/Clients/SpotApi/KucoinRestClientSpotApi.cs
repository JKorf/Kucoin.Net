using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
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

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection([

            new ErrorInfo(ErrorType.Unauthorized, false, "KYC required", "200009"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key invalid", "400003"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Passphrase invalid", "400004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "400006"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Access denied", "400007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Order placement forbidden", "400200"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Token now allowed based on region", "400500"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User frozen", "411100"),

            new ErrorInfo(ErrorType.SignatureInvalid, false, "Signature error", "400005"),

            new ErrorInfo(ErrorType.TimestampInvalid, false, "Invalid timestamp", "400002"),

            new ErrorInfo(ErrorType.SystemError, true, "Internal server error", "500000"),
            new ErrorInfo(ErrorType.SystemError, true, "System busy", "230005"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "102411", "400100"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Time range not supported", "102425"),

            new ErrorInfo(ErrorType.PriceInvalid, false, "Price too low", "102429"),

            new ErrorInfo(ErrorType.QuantityInvalid, false, "Order quantity too large", "102434"),
            new ErrorInfo(ErrorType.QuantityInvalid, false, "Order quantity too low", "102435"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "102436"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", "900001"),

            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol does not allow new orders currently", "200001"),
            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol does not allow cancellation currently", "200002"),
            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is not yet trading", "400600"),
            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is not currently trading", "600203"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate clientOrderId", "102426"),

            new ErrorInfo(ErrorType.OrderRateLimited, false, "Too many open orders", "102427", "200003"),

            new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance", "102421", "200004"),

            new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order is being processed and can't be canceled", "102428"),

            ]);

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
                return result.AsDatalessError(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        internal async Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<KucoinResult<T>>(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

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
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (code == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg), exception);
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
