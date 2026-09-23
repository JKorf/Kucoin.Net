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
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
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
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.FuturesApi.SharedApi,

                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.FuturesApi.SharedApi,
                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.FuturesApi.SharedApi,
                null,

                restClient.FuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}
