using Kucoin.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace Kucoin.Net
{
    /// <inheritdoc/>
    public class KucoinUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public KucoinUserSpotDataTracker(
            ILogger<KucoinUserSpotDataTracker> logger,
            IKucoinRestClient restClient,
            IKucoinSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig config) : base(
                logger,
                restClient.SpotApi.SharedClient,
                null,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                null,
                userIdentifier,
                config)
        {
        }
    }

    /// <inheritdoc/>
    public class KucoinUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public KucoinUserFuturesDataTracker(
            ILogger<KucoinUserFuturesDataTracker> logger,
            IKucoinRestClient restClient,
            IKucoinSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config) : base(logger,
                restClient.FuturesApi.SharedClient,
                null,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                null,
                socketClient.FuturesApi.SharedClient,
                userIdentifier,
                config)
        {
        }
    }
}
