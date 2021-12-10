using Kucoin.Net.Objects;
using Kucoin.Net.Interfaces;
using Kucoin.Net.SocketSubClients;
using System;

namespace Kucoin.Net
{
    /// <summary>
    /// Client for interacting with the Kucoin websocket API
    /// </summary>
    public class KucoinSocketClient: IKucoinSocketClient
    {
        #region fields
        private static KucoinSocketClientOptions defaultOptions = new KucoinSocketClientOptions();
        private static KucoinSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        /// <summary>
        /// Spot subscriptions
        /// </summary>
        public IKucoinSocketClientSpot Spot { get; }

        /// <summary>
        /// Futures subscriptions
        /// </summary>
        public IKucoinSocketClientFutures Futures { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the default options
        /// </summary>
        public KucoinSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinSocketClient with the provided options
        /// </summary>
        public KucoinSocketClient(KucoinSocketClientOptions options)
        {
            Spot = new KucoinSocketClientSpot(options);
            Futures = new KucoinSocketClientFutures(options);
        }
        #endregion

        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Dispose the client
        /// </summary>
        public void Dispose()
        {
            Spot.Dispose();
            Futures.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
