using Kucoin.Net.Objects;
using System;
using Kucoin.Net.Interfaces;
using Kucoin.Net.SubClients;

namespace Kucoin.Net
{
    /// <summary>
    /// Client to interact with the Kucoin REST API
    /// </summary>
    public class KucoinClient: IKucoinClient, IDisposable
    {
        private static KucoinClientOptions defaultOptions = new KucoinClientOptions();
        internal static KucoinClientOptions DefaultOptions => defaultOptions.Copy();
        internal static bool CredentialsDefaultSet => DefaultOptions.ApiCredentials != null;

        /// <inheritdoc />
        public IKucoinClientSpot Spot { get; }
        /// <inheritdoc />
        public IKucoinClientFutures Futures { get; }
        /// <inheritdoc />
        public IKucoinClientMargin Margin { get; }

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinClient using the default options
        /// </summary>
        public KucoinClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinClient with the provided options
        /// </summary>
        public KucoinClient(KucoinClientOptions options)
        {
            Spot = new KucoinClientSpot(options);
            Futures = new KucoinClientFutures(options);
            Margin = new KucoinClientMargin(options);
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinClientOptions options)
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
        #endregion


    }
}
