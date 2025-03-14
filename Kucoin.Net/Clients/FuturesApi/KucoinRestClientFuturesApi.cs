﻿using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
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

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Futures Api");

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

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => TimeSyncState.TimeOffset;

        public IKucoinRestClientFuturesApiShared SharedClient => this;
    }
}
