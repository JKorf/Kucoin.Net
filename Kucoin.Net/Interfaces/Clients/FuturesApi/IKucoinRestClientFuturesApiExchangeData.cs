using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Futures.Socket;
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
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-symbols-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a contract
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-symbol-detail" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the ticker for a contract
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the tickers for all contracts
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesTick>>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the full order book, aggregated by price
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-full-order-book-level-2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-part-order-book-level-2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate list
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-interest-rate-list" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
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
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-index-list" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
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
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-current-mark-price" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get premium index
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-premium-index" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
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
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/funding-fees/get-current-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-transaction-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the service status
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-service-status" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-klines" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Start time to retrieve klines from</param>
        /// <param name="endTime">End time to retrieve klines for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h transaction volume
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/market-data/get-24hour-futures-transaction-volume" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTransactionVolume>> Get24HourTransactionVolumeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for a symbol
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/funding-fees/get-public-funding-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `XBTUSDM`</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<KucoinFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, DateTime startTime, DateTime endTime, CancellationToken ct = default);
    }
}
