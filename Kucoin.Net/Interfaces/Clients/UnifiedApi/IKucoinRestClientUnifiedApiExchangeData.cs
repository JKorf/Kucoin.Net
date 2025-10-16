using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Unified exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IKucoinRestClientUnifiedApiExchangeData
    {
        /// <summary>
        /// Get announcements
        /// <para><a href="https://www.kucoin.com/docs-new/3473237e0" /></para>
        /// </summary>
        /// <param name="language">Language, for example `en_US`</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="page">Page number</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaList<KucoinUaAnnouncement[]>>> GetAnnouncementsAsync(string? language = null, AnnouncementType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? pageSize = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported spot symbols
        /// <para><a href="https://www.kucoin.com/docs-new/3473247e0" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinSpotSymbol[]>> GetSpotSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para><a href="https://www.kucoin.com/docs-new/3473247e0" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinFuturesSymbol[]>> GetFuturesSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para><a href="https://www.kucoin.com/docs-new/3473247e0" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para><a href="https://www.kucoin.com/docs-new/3473247e0" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset
        /// <para><a href="https://www.kucoin.com/docs-new/3473238e0" /></para>
        /// </summary>
        /// <param name="asset">Filter asset, for example `ETH`</param>
        /// <param name="network">Filter network, for example `eth`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAsset>> GetAssetAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price ticker info
        /// <para><a href="https://www.kucoin.com/docs-new/3473241e0" /></para>
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTicker[]>> GetTickersAsync(ProductType productType, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get snapshot of the current order book
        /// <para><a href="https://www.kucoin.com/docs-new/3473243e0" /></para>
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="limit">Number of rows, null for full book. Spot: 20 or 50, Futures: 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderBook>> GetOrderBookAsync(ProductType productType, string symbol, int? limit, CancellationToken ct = default);

        /// <summary>
        /// Get list of the most recent trades
        /// <para><a href="https://www.kucoin.com/docs-new/3473242e0" /></para>
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTrade[]>> GetRecentTradesAsync(ProductType productType, string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick/kline data
        /// <para><a href="https://www.kucoin.com/docs-new/3473244e0" /></para>
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="interval">Interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaKline[]>> GetKlinesAsync(ProductType productType, string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate
        /// <para><a href="https://www.kucoin.com/docs-new/3473245e0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para><a href="https://www.kucoin.com/docs-new/3473246e0" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFundingRateEntry[]>> GetFundingHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin configuration
        /// <para><a href="https://www.kucoin.com/docs-new/3473248e0" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaCrossMarginConfig>> GetCrossMarginConfigAsync(CancellationToken ct = default);

        /// <summary>
        /// Get service status
        /// <para><a href="https://www.kucoin.com/docs-new/3473248e0" /></para>
        /// </summary>
        /// <param name="productType">Product type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaServiceStatus>> GetServiceStatusAsync(ProductType productType, CancellationToken ct = default);

    }
}
