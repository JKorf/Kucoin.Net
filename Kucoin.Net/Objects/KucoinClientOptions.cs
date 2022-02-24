using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces.Clients;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Options for the KucoinClient
    /// </summary>
    public class KucoinClientOptions: BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinClientOptions Default { get; set; } = new KucoinClientOptions();

        private KucoinRestApiClientOptions _spotApiOptions = new KucoinRestApiClientOptions(KucoinApiAddresses.Default.SpotAddress)
        {
            RateLimiters = new List<IRateLimiter>
            {
                     new RateLimiter()
                        .AddPartialEndpointLimit("/api/v1/orders", 180, TimeSpan.FromSeconds(3), null, true, true)
                        .AddApiKeyLimit(200, TimeSpan.FromSeconds(10), true, true)
                        .AddTotalRateLimit(100, TimeSpan.FromSeconds(10))
            }
        };
        
        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinRestApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions = new KucoinRestApiClientOptions(_spotApiOptions, value);
        }

        private KucoinRestApiClientOptions _futuresApiOptions = new KucoinRestApiClientOptions(KucoinApiAddresses.Default.FuturesAddress);
        /// <summary>
        /// Futures API options
        /// </summary>
        public KucoinRestApiClientOptions FuturesApiOptions
        {
            get => _futuresApiOptions;
            set => _futuresApiOptions = new KucoinRestApiClientOptions(_futuresApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal KucoinClientOptions(KucoinClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ApiCredentials = (KucoinApiCredentials?)baseOn.ApiCredentials?.Copy();
            _spotApiOptions = new KucoinRestApiClientOptions(baseOn.SpotApiOptions, null);
            _futuresApiOptions = new KucoinRestApiClientOptions(baseOn.FuturesApiOptions, null);
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class KucoinSocketClientOptions: BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static KucoinSocketClientOptions Default { get; set; } = new KucoinSocketClientOptions() {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        private KucoinSocketApiClientOptions _spotStreamsOptions = new KucoinSocketApiClientOptions();
        /// <summary>
        /// Spot stream options
        /// </summary>
        public KucoinSocketApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions = new KucoinSocketApiClientOptions(_spotStreamsOptions, value);
        }

        private KucoinSocketApiClientOptions _futuresStreamsOptions = new KucoinSocketApiClientOptions();
        /// <summary>
        /// Futures stream options
        /// </summary>
        public KucoinSocketApiClientOptions FuturesStreamsOptions
        {
            get => _futuresStreamsOptions;
            set => _futuresStreamsOptions = new KucoinSocketApiClientOptions(_futuresStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal KucoinSocketClientOptions(KucoinSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ApiCredentials = (KucoinApiCredentials?)baseOn.ApiCredentials?.Copy();
            _spotStreamsOptions = new KucoinSocketApiClientOptions(baseOn.SpotStreamsOptions, null);
            _futuresStreamsOptions = new KucoinSocketApiClientOptions(baseOn.FuturesStreamsOptions, null);
        }
    }

    /// <summary>
    /// Kucoin rest client options
    /// </summary>
    public class KucoinRestApiClientOptions : RestApiClientOptions
    {
        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinRestApiClientOptions()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        internal KucoinRestApiClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal KucoinRestApiClientOptions(KucoinRestApiClientOptions baseOn, KucoinRestApiClientOptions? newValues) : base(baseOn, newValues)
        {
            ApiCredentials = (KucoinApiCredentials?)newValues?.ApiCredentials?.Copy() ?? (KucoinApiCredentials?)baseOn.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class KucoinSocketApiClientOptions : ApiClientOptions
    {
        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinSocketApiClientOptions()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal KucoinSocketApiClientOptions(KucoinSocketApiClientOptions baseOn, KucoinSocketApiClientOptions? newValues) : base(baseOn, newValues)
        {
            ApiCredentials = (KucoinApiCredentials?)newValues?.ApiCredentials?.Copy() ?? (KucoinApiCredentials?)baseOn.ApiCredentials?.Copy();
        }
    }

    /// <summary>
    /// Options for the KucoinSymbolOrderBook
    /// </summary>
    public class KucoinOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IKucoinSocketClient? SocketClient { get; set; }

        /// <summary>
        /// The client to use for the initial order book request
        /// </summary>
        public IKucoinClient? RestClient { get; set; }
    }
}
