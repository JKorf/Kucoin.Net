using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
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

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinRestClientFuturesApi" />
    internal partial class KucoinRestClientFuturesApi : RestApiClient, IKucoinRestClientFuturesApi
    {
        private readonly KucoinRestClient _baseClient;
        private readonly KucoinRestOptions _options;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Futures Api");

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection([
            new ErrorInfo(ErrorType.Unauthorized, false, "Not allowed to place orders based on region", "40010"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key does not exist", "400003"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Passphrase invalid", "400004"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "400006"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "400007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User frozen", "411100"),

            new ErrorInfo(ErrorType.SystemError, true, "Internal server error", "500000"),

            new ErrorInfo(ErrorType.SignatureInvalid, false, "Signature invalid", "400005"),

            new ErrorInfo(ErrorType.TimestampInvalid, false, "Invalid timestamp", "400002"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "100001", "300000", "400100"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage too high", "300016"),

            new ErrorInfo(ErrorType.PriceInvalid, false, "Price worse than liquidation price", "300007"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Price too high", "300011"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Price too low", "300012"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "100003", "200003"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "300018"),

            new ErrorInfo(ErrorType.NoPosition, false, "No open position to close", "300009"),

            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Order placement/cancellation suspended", "300002"),
            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is in settlement and does not accept orders", "300015"),

            new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance", "200005", "300003"),

            new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order not in cancelable state", "100004"),
            new ErrorInfo(ErrorType.TargetIncorrectState, false, "Position in liquidation", "300014"),

            new ErrorInfo(ErrorType.RiskError, false, "Risk limit exceeded", "300005"),

            new ErrorInfo(ErrorType.OrderRateLimited, false, "Too many open orders", "300001"),
            new ErrorInfo(ErrorType.OrderRateLimited, false, "Too many stop orders", "300004"),

            new ErrorInfo(ErrorType.RequestRateLimited, false, "Too many requests, IP blocked for 30 seconds", "1015"),
            new ErrorInfo(ErrorType.RequestRateLimited, false, "Too many requests, blocked for 10 seconds", "200002"),
            new ErrorInfo(ErrorType.RequestRateLimited, false, "Too many requests", "429000"),
            ]);

        /// <inheritdoc />
        public string ExchangeName => "Kucoin";

        /// <inheritdoc />
        public IKucoinRestClientFuturesApiAccount Account { get; }

        /// <inheritdoc />
        public IKucoinRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IKucoinRestClientFuturesApiTrading Trading { get; }

        internal KucoinRestClientFuturesApi(ILogger logger, HttpClient? httpClient, KucoinRestClient baseClient, KucoinRestOptions options)
            : base(logger, httpClient, options.Environment.FuturesAddress, options, options.FuturesOptions)
        {
            _baseClient = baseClient;
            _options = options;

            Account = new KucoinRestClientFuturesApiAccount(this);
            ExchangeData = new KucoinRestClientFuturesApiExchangeData(this);
            Trading = new KucoinRestClientFuturesApiTrading(this);

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

        protected override Error? TryParseError(KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (code == null)
                return new ServerError(ErrorInfo.Unknown);

            if (code == 200 || code == 200000)
                return null;

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg));
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

        public IKucoinRestClientFuturesApiShared SharedClient => this;
    }
}
