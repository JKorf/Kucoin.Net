using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin options
    /// </summary>
    public class KucoinOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public KucoinRestOptions Rest { get; set; } = new KucoinRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public KucoinSocketOptions Socket { get; set; } = new KucoinSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `KucoinEnvironment` to swap environment, for example `Environment = KucoinEnvironment.Live`
        /// </summary>
        public KucoinEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public KucoinApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IKucoinSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
