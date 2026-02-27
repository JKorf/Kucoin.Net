using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    public interface IKucoinRestClientUnifiedApi : IRestApiClient, IDisposable
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
    }
}
