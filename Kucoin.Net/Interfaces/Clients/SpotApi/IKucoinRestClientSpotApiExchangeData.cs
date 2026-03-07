using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IKucoinRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Gets the server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/timestamp
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The time of the server</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of symbols supported by the server
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-all-symbols" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/symbols
        /// </para>
        /// </summary>
        /// <param name="market">["<c>market</c>"] Only get symbols for a specific market, for example 'ALTS'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<KucoinSymbol[]>> GetSymbolsAsync(string? market = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-symbol" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/symbols/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets ticker info of a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/orderbook/level1
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get info for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Ticker info</returns>
        Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the ticker for all trading pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-all-tickers" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/allTickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of tickers</returns>
        Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the 24 hour stats of a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-all-tickers" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/stats
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get stats for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>24 hour stats</returns>
        Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported markets
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-market-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/markets
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of markets</returns>
        Task<WebCallResult<string[]>> GetMarketsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-part-orderbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/orderbook/level2_{limit}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get order book for, for example `ETH-USDT`</param>
        /// <param name="limit">The limit of results (20 / 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Partial aggregated order book</returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default);

        /// <summary>
        /// Get a full aggregated order book for a symbol. Orders for the same price are combined.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-full-orderbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/market/orderbook/level2
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get order book for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Full aggregated order book</returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trade history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-trade-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/histories
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get trade history for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades for the symbol</returns>
        Task<WebCallResult<KucoinTrade[]>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline data for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-klines" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/candles
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get klines for, for example `ETH-USDT`</param>
        /// <param name="interval">["<c>type</c>"] The interval of a kline</param>
        /// <param name="startTime">["<c>startAt</c>"] The start time of the data</param>
        /// <param name="endTime">["<c>endAt</c>"] The end time of the data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of klines</returns>
        Task<WebCallResult<KucoinKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-all-currencies" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/currencies
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of currencies</returns>
        Task<WebCallResult<KucoinAssetDetails[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-currency" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/currencies/{asset}
        /// </para>
        /// </summary>
        /// <param name="asset">The asset to get, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset info</returns>
        Task<WebCallResult<KucoinAssetDetails>> GetAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of prices for all 
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-fiat-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/prices
        /// </para>
        /// </summary>
        /// <param name="fiatBase">["<c>base</c>"] The three letter code of the fiat to convert to. Defaults to USD</param>
        /// <param name="assets">["<c>currencies</c>"] The assets to get price for. Defaults to all, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, IEnumerable<string>? assets = null, CancellationToken ct = default);

        /// <summary>
        /// Get leveraged token information
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-etf-info" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/etf/info
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param> 
        /// <returns></returns>
        Task<WebCallResult<KucoinLeveragedToken[]>> GetLeveragedTokensAsync(CancellationToken ct = default);

        /// <summary>
        /// Get system announcements
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-announcements" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/announcements
        /// </para>
        /// </summary>
        /// <param name="page">["<c>currentPage</c>"] Current page</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="announcementType">["<c>annType</c>"] Filter by announcement type</param>
        /// <param name="language">["<c>lang</c>"] Language, defaults to en_US</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinPaginated<KucoinAnnouncement>>> GetAnnouncementsAsync(int? page = null, int? pageSize = null, string? announcementType = null, string? language = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get Call Auction order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-call-auction-part-orderbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/orderbook/callauction/level2_{depth}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETH-USDT`</param>
        /// <param name="depth">Depth of the book, 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinOrderBook>> GetCallAuctionOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get call auction info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/spot-trading/market-data/get-call-auction-info" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/market/callauctionData
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinCallAuctionInfo>> GetCallAuctionInfoAsync(string symbol, CancellationToken ct = default);
    }
}
