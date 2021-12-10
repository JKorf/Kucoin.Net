using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IKucoinClientFuturesApi" />
    public class KucoinClientFuturesApi : RestApiClient, IKucoinClientFuturesApi
    {
        private readonly KucoinClient _baseClient;
        private readonly KucoinClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

        /// <inheritdoc />
        public IKucoinClientFuturesApiAccount Account { get; }

        /// <inheritdoc />
        public IKucoinClientFuturesApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IKucoinClientFuturesApiTrading Trading { get; }

        internal KucoinClientFuturesApi(Log log, KucoinClient baseClient, KucoinClientOptions options)
            : base(options, options.FuturesApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new KucoinClientFuturesApiAccount(this);
            ExchangeData = new KucoinClientFuturesApiExchangeData(this);
            Trading = new KucoinClientFuturesApiTrading(this);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new KucoinAuthenticationProvider((KucoinApiCredentials)credentials);

        internal Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
         => _baseClient.Execute(this, uri, method, ct, parameters, signed);

        internal Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1)
         => _baseClient.Execute<T>(this, uri, method, ct, parameters, signed);

        internal Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(BaseAddress.AppendPath("v" + apiVersion, path));
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.FuturesApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;
    }
}
