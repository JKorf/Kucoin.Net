using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.UnifiedApi;
using System;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    public interface IKucoinRestClientUnifiedApi : IRestApiClient<KucoinCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IKucoinRestClientUnifiedApiAccount"/>
        IKucoinRestClientUnifiedApiAccount Account { get; }
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiExchangeData"/>
        IKucoinRestClientUnifiedApiExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IKucoinRestClientUnifiedApiTrading"/>
        IKucoinRestClientUnifiedApiTrading Trading { get; }

        /// <summary>
        /// Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IKucoinRestClientUnifiedSharedApi SharedApi { get; }
    }
}
