using CryptoExchange.Net;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Clients.Rest.Futures;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.Rest.Futures;
using Kucoin.Net.Interfaces.Clients.Rest.Spot;
using Kucoin.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.Rest.Spot
{
    public class KucoinClientFuturesMarket : RestSubClient, IKucoinClientFuturesMarket
    {
        private readonly KucoinClient _baseClient;

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        public IKucoinClientFuturesAccount Account { get; }

        public IKucoinClientFuturesExchangeData ExchangeData { get; }

        public IKucoinClientFuturesTrading Trading { get; }

        internal KucoinClientFuturesMarket(KucoinClient baseClient, KucoinClientOptions options)
            : base(options.OptionsFutures, options.OptionsFutures.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.OptionsFutures.ApiCredentials))
        {
            _baseClient = baseClient;

            Account = new KucoinClientFuturesAccount(this);
            ExchangeData = new KucoinClientFuturesExchangeData(this);
            Trading = new KucoinClientFuturesTrading(this);
        }

        internal Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
         => _baseClient.Execute(this, uri, method, ct, parameters, signed);

        internal Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1)
         => _baseClient.Execute<T>(this, uri, method, ct, parameters, signed);

        internal Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(BaseAddress.AppendPath("v" + apiVersion, path));
        }
    }
}
