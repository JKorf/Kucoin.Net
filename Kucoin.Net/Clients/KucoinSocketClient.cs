using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Clients.FuturesApi;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinSocketClient" />
    public class KucoinSocketClient : BaseSocketClient, IKucoinSocketClient
    {
        #region Api clients

        /// <inheritdoc />
        public IKucoinSocketClientSpotStreams SpotStreams { get; }
        /// <inheritdoc />
        public IKucoinSocketClientFuturesStreams FuturesStreams { get; }

        #endregion

        /// <summary>
        /// Create a new instance of KucoinSocketClient using the default options
        /// </summary>
        public KucoinSocketClient() : this(KucoinSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public KucoinSocketClient(KucoinSocketClientOptions options) : base("Kucoin", options)
        {
            SpotStreams = AddApiClient(new KucoinSocketClientSpotStreams(log, this, options));
            FuturesStreams = AddApiClient(new KucoinSocketClientFuturesStreams(log, this, options));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            KucoinSocketClientOptions.Default = options;
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            SpotStreams.SetApiCredentials(credentials);
            FuturesStreams.SetApiCredentials(credentials);
        }
    }
}
