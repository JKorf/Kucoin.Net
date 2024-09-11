using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Objects.Models.Spot;
using Kucoin.Net.Objects.Models.Futures;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Kucoin Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IKucoinRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Gets the server time
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The time of the server</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of symbols supported by the server
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-symbols-list" /></para>
        /// </summary>
        /// <param name="market">Only get symbols for a specific market, for example 'ALTS'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        Task<WebCallResult<IEnumerable<KucoinSymbol>>> GetSymbolsAsync(string? market = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-symbol-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets ticker info of a symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get info for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Ticker info</returns>
        Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the ticker for all trading pairs
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-all-tickers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of tickers</returns>
        Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the 24 hour stats of a symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-24hr-stats" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get stats for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>24 hour stats</returns>
        Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported markets
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-market-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of markets</returns>
        Task<WebCallResult<IEnumerable<string>>> GetMarketsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-part-order-book-aggregated-" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get order book for, for example `ETH-USDT`</param>
        /// <param name="limit">The limit of results (20 / 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Partial aggregated order book</returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default);

        /// <summary>
        /// Get a full aggregated order book for a symbol. Orders for the same price are combined.
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-full-order-book-aggregated-" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get order book for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Full aggregated order book</returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trade history for a symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-trade-histories" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trade history for, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades for the symbol</returns>
        Task<WebCallResult<IEnumerable<KucoinTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline data for a symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-klines" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get klines for, for example `ETH-USDT`</param>
        /// <param name="interval">The interval of a kline</param>
        /// <param name="startTime">The start time of the data</param>
        /// <param name="endTime">The end time of the data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of klines</returns>
        Task<WebCallResult<IEnumerable<KucoinKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-currency-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of currencies</returns>
        Task<WebCallResult<IEnumerable<KucoinAssetDetails>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific asset
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-currency-detail" /></para>
        /// </summary>
        /// <param name="asset">The asset to get, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset info</returns>
        Task<WebCallResult<KucoinAssetDetails>> GetAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of prices for all 
        /// <para><a href="https://www.kucoin.com/docs/rest/spot-trading/market-data/get-fiat-price" /></para>
        /// </summary>
        /// <param name="fiatBase">The three letter code of the fiat to convert to. Defaults to USD</param>
        /// <param name="assets">The assets to get price for. Defaults to all, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, IEnumerable<string>? assets = null, CancellationToken ct = default);

        /// <summary>
        /// Get leveraged token information
        /// <para><a href="https://www.kucoin.com/docs/rest/margin-trading/margin-info/get-leveraged-token-info" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param> 
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinLeveragedToken>>> GetLeveragedTokensAsync(CancellationToken ct = default);
    }
}
