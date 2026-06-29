using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.MessageHandlers;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IKucoinRestClientSpotApi" />
    internal partial class KucoinRestClientSpotApi : RestApiClient<KucoinEnvironment, KucoinAuthenticationProvider, KucoinCredentials>, IKucoinRestClientSpotApi
    {
        protected override ErrorMapping ErrorMapping => KucoinErrors.SpotErrors;

        protected override IRestMessageHandler MessageHandler { get; } = new KucoinRestMessageHandler(KucoinErrors.SpotErrors);

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

        /// <inheritdoc />
        public IKucoinRestClientSpotApiEarn Earn { get; }

        internal KucoinRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, KucoinRestClient baseClient, KucoinRestOptions options)
            : base(loggerFactory, KucoinExchange.Metadata.Id, httpClient, options.Environment.SpotAddress, options, options.SpotOptions)
        {
            Account = new KucoinRestClientSpotApiAccount(this);
            SubAccount = new KucoinRestClientSpotApiSubAccount(this);
            ExchangeData = new KucoinRestClientSpotApiExchangeData(this);
            Trading = new KucoinRestClientSpotApiTrading(this);
            HfTrading = new KucoinRestClientSpotApiHfTrading(this);
            Margin = new KucoinRestClientSpotApiMargin(this);
            Earn = new KucoinRestClientSpotApiEarn(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;

            if (options.Environment.Name == KucoinEnvironment.Australia.Name)
                StandardRequestHeaders.Add("X-SITE-TYPE", "australia");
        }

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(KucoinExchange._serializerContext));

        /// <inheritdoc />
        protected override KucoinAuthenticationProvider CreateAuthenticationProvider(KucoinCredentials credentials)
            => new KucoinAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
            => KucoinExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<KucoinResult>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<KucoinResult<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 200000 && result.Data.Code != 200)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        internal async Task<HttpResult<T>> SendRawAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        public IKucoinRestClientSpotApiShared SharedClient => this;

    }
}
