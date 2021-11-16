using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Interfaces.Clients.Rest.Futures
{
    public interface IKucoinClientFuturesExchangeData
    {

        /// <summary>
        /// Get open contract list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the ticker for a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the full order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate list
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
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get premium index
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
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);


        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the service status
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
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
