using CryptoExchange.Net.Interfaces;
using System;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Unified API endpoints
    /// </summary>
    public interface IKucoinRestClientUnifiedApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IKucoinRestClientSpotApiExchangeData"/>
        IKucoinRestClientUnifiedApiExchangeData ExchangeData { get; }
    }
}
