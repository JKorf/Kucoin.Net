using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Spot;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Earn endpoints
    /// </summary>
    public interface IKucoinRestClientSpotApiEarn
    {
        /// <summary>
        /// Get information on currently held assets. If no assets are currently held, an empty list is returned.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/earn/get-account-holding" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="productId">Filter by product id</param>
        /// <param name="productCategory">Filter by product category</param>
        /// <param name="page">Current request page.</param>
        /// <param name="pageSize">Number of results per request. Minimum is 10, maximum is 500.</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinEarnHolding>>> GetEarnHoldingAsync(string? asset = null, string? productId = null, EarnProductCategory? productCategory = default, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
