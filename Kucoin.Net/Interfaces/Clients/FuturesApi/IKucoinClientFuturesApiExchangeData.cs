using System;
using System.Collections.Generic;
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
    public interface IKucoinClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get open contract list
        /// <para><a href="https://docs.kucoin.com/futures/#get-open-contract-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a contract
        /// <para><a href="https://docs.kucoin.com/futures/#get-order-info-of-the-contract" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the ticker for a contract
        /// <para><a href="https://docs.kucoin.com/futures/#get-real-time-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the full order book, aggregated by price
        /// <para><a href="https://docs.kucoin.com/futures/#get-full-order-book-level-2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// <para><a href="https://docs.kucoin.com/futures/#get-part-order-book-level-2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate list
        /// <para><a href="https://docs.kucoin.com/futures/#get-interest-rate-list" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get index list
        /// <para><a href="https://docs.kucoin.com/futures/#get-index-list" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current mark price
        /// <para><a href="https://docs.kucoin.com/futures/#get-current-mark-price" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get premium index
        /// <para><a href="https://docs.kucoin.com/futures/#get-premium-index" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current funding rate
        /// <para><a href="https://docs.kucoin.com/futures/#get-current-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades
        /// <para><a href="https://docs.kucoin.com/futures/#transaction-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para><a href="https://docs.kucoin.com/futures/#server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the service status
        /// <para><a href="https://docs.kucoin.com/futures/#get-the-service-status" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
        /// <para><a href="https://docs.kucoin.com/futures/#get-k-line-data-of-contract" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Start time to retrieve klines from</param>
        /// <param name="endTime">End time to retrieve klines for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);
    }
}
