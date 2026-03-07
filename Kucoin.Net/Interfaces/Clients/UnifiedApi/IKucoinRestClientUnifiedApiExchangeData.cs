using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Unified;
using System;
using System.Collections.Generic;
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473237e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/announcement
        /// </para>
        /// </summary>
        /// <param name="language">["<c>language</c>"] Language, for example `en_US`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="page">["<c>pageNumber</c>"] Page number</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaList<KucoinUaAnnouncement[]>>> GetAnnouncementsAsync(string? language = null, AnnouncementType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? pageSize = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported spot symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473247e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/instrument
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinSpotSymbol[]>> GetSpotSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473247e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/instrument
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinFuturesSymbol[]>> GetFuturesSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473247e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/instrument
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473247e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/instrument
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473238e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/currency
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter asset, for example `ETH`</param>
        /// <param name="network">["<c>chain</c>"] Filter network, for example `eth`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAsset>> GetAssetAsync(string asset, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get assets
        /// </summary>
        /// <param name="assets">["<c>currencyList</c>"] Assets filter, null for all assets</param>
        /// <param name="network">["<c>chain</c>"] Filter network, for example `eth`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaAsset[]>> GetAssetsAsync(IEnumerable<string>? assets = null, string? network = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price ticker info
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473241e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/ticker
        /// </para>
        /// </summary>
        /// <param name="productType">["<c>tradeType</c>"] Product type</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTicker[]>> GetTickersAsync(ProductType productType, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get snapshot of the current order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473243e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/orderbook
        /// </para>
        /// </summary>
        /// <param name="productType">["<c>tradeType</c>"] Product type</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows, 20, 100 or null for full book</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOrderBook>> GetOrderBookAsync(ProductType productType, string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of the most recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473242e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/trade
        /// </para>
        /// </summary>
        /// <param name="productType">["<c>tradeType</c>"] Product type</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaTrade[]>> GetRecentTradesAsync(ProductType productType, string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick/kline data
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473244e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/kline
        /// </para>
        /// </summary>
        /// <param name="productType">["<c>tradeType</c>"] Product type</param>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>interval</c>"] Interval</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaKline[]>> GetKlinesAsync(ProductType productType, string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473245e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/funding-rate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473246e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/funding-rate-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaFundingRateEntry[]>> GetFundingHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473248e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/cross-config
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaCrossMarginConfig>> GetCrossMarginConfigAsync(CancellationToken ct = default);

        /// <summary>
        /// Get collateral discount ratio
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-collateral-ratio" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/collateral-discount-ratio
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaCollateralRatio[]>> GetCollateralRatioAsync(CancellationToken ct = default);

        /// <summary>
        /// Get open interest for futures symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-futures-open-interset" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/open-interest
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbol</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOpenInterest[]>> GetFuturesOpenInterestAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/ua/get-futures-open-interset" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/market/open-interest
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDTM`</param>
        /// <param name="interval">["<c>interval</c>"] Interval</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaOpenInterest[]>> GetFuturesOpenInterestHistoryAsync(
            string symbol,
            DataPeriod interval,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get service status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/3473248e0" /><br />
        /// Endpoint:<br />
        /// GET /api/ua/v1/server/status
        /// </para>
        /// </summary>
        /// <param name="productType">["<c>tradeType</c>"] Product type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinUaServiceStatus>> GetServiceStatusAsync(ProductType productType, CancellationToken ct = default);

    }
}
