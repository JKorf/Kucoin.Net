using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Kucoin Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IKucoinRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get open contract list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-all-symbols" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contracts/active
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-symbol" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contracts/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the ticker for a contract
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the tickers for all contracts
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-all-tickers" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/allTickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the full order book, aggregated by price
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-full-orderbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/level2/snapshot
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-part-orderbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/level2/depth{depth}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-interest-rate-index" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/interest/query
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="pageSize">["<c>maxCount</c>"] Size of a page</param>
        /// <param name="forward">["<c>forward</c>"] Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get index list
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-spot-index-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/index/query
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="pageSize">["<c>maxCount</c>"] Size of a page</param>
        /// <param name="forward">["<c>forward</c>"] Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current mark price
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-mark-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/mark-price/{symbol}/current
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get premium index
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-premium-index" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/premium/query
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="pageSize">["<c>maxCount</c>"] Size of a page</param>
        /// <param name="forward">["<c>forward</c>"] Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current funding rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/funding-fees/get-current-funding-rate" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/funding-rate/{symbol}/current
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-trade-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/trade/history
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTrade[]>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/timestamp
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the service status
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-service-status" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/status
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-klines" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/kline/query
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="interval">["<c>granularity</c>"] Interval of the klines</param>
        /// <param name="startTime">["<c>from</c>"] Start time to retrieve klines from</param>
        /// <param name="endTime">["<c>to</c>"] End time to retrieve klines for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h transaction volume
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/market-data/get-24hr-stats" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/trade-statistics
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTransactionVolume>> Get24HourTransactionVolumeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/funding-fees/get-public-funding-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contract/funding-rates
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>from</c>"] Start time</param>
        /// <param name="endTime">["<c>to</c>"] End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default);
    }
}
