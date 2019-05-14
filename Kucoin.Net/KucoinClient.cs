using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Converts;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Kucoin.Net.Interfaces;

namespace Kucoin.Net
{
    public class KucoinClient: RestClient, IKucoinClient
    {
        private static KucoinClientOptions defaultOptions = new KucoinClientOptions();
        internal static KucoinClientOptions DefaultOptions => defaultOptions.Copy();

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of the KucoinClient using the default options
        /// </summary>
        public KucoinClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of the KucoinClient with the provided options
        /// </summary>
        public KucoinClient(KucoinClientOptions options) : base(options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(KucoinClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns>The time of the server</returns>
        public WebCallResult<DateTime> GetServerTime() => GetServerTimeAsync().Result;

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns>The time of the server</returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync()
        {
            var result = await Execute<long>(GetUri("timestamp")).ConfigureAwait(false);
            return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Success ? JsonConvert.DeserializeObject<DateTime>(result.Data.ToString(), new TimestampConverter()) : default(DateTime), result.Error);
        }

        /// <summary>
        /// Gets a list of symbols supported by the server
        /// </summary>
        /// <param name="market">Only get symbols for a specific market, for example 'BTC'</param>
        /// <returns>List of symbols</returns>
        public WebCallResult<KucoinSymbol[]> GetSymbols(string market = null) => GetSymbolsAsync(market).Result;

        /// <summary>
        /// Gets a list of symbols supported by the server
        /// </summary>
        /// <param name="market">Only get symbols for a specific market, for example 'BTC'</param>
        /// <returns>List of symbols</returns>
        public async Task<WebCallResult<KucoinSymbol[]>> GetSymbolsAsync(string market = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", market);
            return await Execute<KucoinSymbol[]>(GetUri("symbols"), parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ticker info of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get info for</param>
        /// <returns>Ticker info</returns>
        public WebCallResult<KucoinTick> GetTicker(string symbol) => GetTickerAsync(symbol).Result;

        /// <summary>
        /// Gets ticker info of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get info for</param>
        /// <returns>Ticker info</returns>
        public async Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await Execute<KucoinTick>(GetUri("market/orderbook/level1"), parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the ticker for all trading pairs
        /// </summary>
        /// <returns>List of tickers</returns>
        public WebCallResult<KucoinTicks> GetTickers() => GetTickersAsync().Result;

        /// <summary>
        /// Gets the ticker for all trading pairs
        /// </summary>
        /// <returns>List of tickers</returns>
        public async Task<WebCallResult<KucoinTicks>> GetTickersAsync()
        {
            return await Execute<KucoinTicks>(GetUri("market/allTickers")).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the 24 hour stats of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get stats for</param>
        /// <returns>24 hour stats</returns>
        public WebCallResult<Kucoin24HourStat> Get24HourStats(string symbol) => Get24HourStatsAsync(symbol).Result;

        /// <summary>
        /// Gets the 24 hour stats of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get stats for</param>
        /// <returns>24 hour stats</returns>
        public async Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await Execute<Kucoin24HourStat>(GetUri("market/stats"), parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of supported markets
        /// </summary>
        /// <returns>List of markets</returns>
        public WebCallResult<string[]> GetMarkets() => GetMarketsAsync().Result;

        /// <summary>
        /// Gets a list of supported markets
        /// </summary>
        /// <returns>List of markets</returns>
        public async Task<WebCallResult<string[]>> GetMarketsAsync()
        {
            return await Execute<string[]>(GetUri("markets")).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <param name="limit">The limit of results (20 / 100)</param>
        /// <returns>Partial aggregated order book</returns>
        public WebCallResult<KucoinOrderBook> GetAggregatedPartialOrderBook(string symbol, int limit) => GetAggregatedPartialOrderBookAsync(symbol, limit).Result;

        /// <summary>
        /// Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <param name="limit">The limit of results (20 / 100)</param>
        /// <returns>Partial aggregated order book</returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit)
        {
            if (limit != 20 && limit != 100)
                return WebCallResult<KucoinOrderBook>.CreateErrorResult(new ArgumentError("Limit should be either 20 or 100"));

            return await Execute<KucoinOrderBook>(GetUri($"market/orderbook/level2_{limit}?symbol={symbol}")).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a full aggregated order book for a symbol. Orders for the same price are combined.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <returns>Full aggregated order book</returns>
        public WebCallResult<KucoinOrderBook> GetAggregatedFullOrderBook(string symbol) => GetAggregatedFullOrderBookAsync(symbol).Result;

        /// <summary>
        /// Get a full aggregated order book for a symbol. Orders for the same price are combined.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <returns>Full aggregated order book</returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol)
        {
            return await Execute<KucoinOrderBook>(GetUri($"market/orderbook/level2?symbol={symbol}", 2)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a full order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <returns>Full order book</returns>
        public WebCallResult<KucoinFullOrderBook> GetFullOrderBook(string symbol) => GetFullOrderBookAsync(symbol).Result;

        /// <summary>
        /// Get a full order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <returns>Full order book</returns>
        public async Task<WebCallResult<KucoinFullOrderBook>> GetFullOrderBookAsync(string symbol)
        {
            return await Execute<KucoinFullOrderBook>(GetUri($"market/orderbook/level3?symbol={symbol}")).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the recent trade history for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trade history for</param>
        /// <returns>List of trades for the symbol</returns>
        public WebCallResult<KucoinTrade[]> GetTradeHistory(string symbol) => GetTradeHistoryAsync(symbol).Result;

        /// <summary>
        /// Gets the recent trade history for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trade history for</param>
        /// <returns>List of trades for the symbol</returns>
        public async Task<WebCallResult<KucoinTrade[]>> GetTradeHistoryAsync(string symbol)
        {
            return await Execute<KucoinTrade[]>(GetUri($"market/histories?symbol={symbol}")).ConfigureAwait(false);
        }

        /// <summary>
        /// Get kline data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get klines for</param>
        /// <param name="interval">The interval of a kline</param>
        /// <param name="startTime">The start time of the data</param>
        /// <param name="endTime">The end time of the data</param>
        /// <returns>List of klines</returns>
        public WebCallResult<KucoinKline[]> GetKlines(string symbol, KucoinKlineInterval interval, DateTime startTime, DateTime endTime) => GetKlinesAsync(symbol, interval, startTime, endTime).Result;

        /// <summary>
        /// Get kline data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get klines for</param>
        /// <param name="interval">The interval of a kline</param>
        /// <param name="startTime">The start time of the data</param>
        /// <param name="endTime">The end time of the data</param>
        /// <returns>List of klines</returns>
        public async Task<WebCallResult<KucoinKline[]>> GetKlinesAsync(string symbol, KucoinKlineInterval interval, DateTime startTime, DateTime endTime)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) },
                { "startAt", JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) },
                { "endAt", JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) },
            };
            return await Execute<KucoinKline[]>(GetUri($"market/candles"), parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        public WebCallResult<KucoinCurrency[]> GetCurrencies() => GetCurrenciesAsync().Result;

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        public async Task<WebCallResult<KucoinCurrency[]>> GetCurrenciesAsync()
        {
            return await Execute<KucoinCurrency[]>(GetUri("currencies")).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a specific currency
        /// </summary>
        /// <param name="name">The currency to get</param>
        /// <returns>Currency info</returns>
        public WebCallResult<KucoinCurrency> GetCurrency(string name) => GetCurrencyAsync(name).Result;

        /// <summary>
        /// Get info on a specific currency
        /// </summary>
        /// <param name="name">The currency to get</param>
        /// <returns>Currency info</returns>
        public async Task<WebCallResult<KucoinCurrency>> GetCurrencyAsync(string name)
        {
            return await Execute<KucoinCurrency>(GetUri($"currencies/{name}")).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of prices for all 
        /// </summary>
        /// <param name="fiatBase">The three letter code of the fiat to convert to. Defaults to USD</param>
        /// <param name="currencies">The currencies to get price for. Defaults to all</param>
        /// <returns>List of prices</returns>
        public WebCallResult<Dictionary<string, decimal>> GetFiatPrices(string fiatBase = null, string[] currencies = null) => GetFiatPricesAsync(fiatBase, currencies).Result;

        /// <summary>
        /// Gets a list of prices for all 
        /// </summary>
        /// <param name="fiatBase">The three letter code of the fiat to convert to. Defaults to USD</param>
        /// <param name="currencies">The currencies to get price for. Defaults to all</param>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string fiatBase = null, string[] currencies = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("base", fiatBase);
            parameters.AddOptionalParameter("currencies", currencies?.Length > 0 ? string.Join(",", currencies): null);

            return await Execute<Dictionary<string, decimal>>(GetUri($"prices"), parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of sub users
        /// </summary>
        /// <returns>List of sub users</returns>
        public WebCallResult<KucoinSubUser[]> GetUserInfo() => GetUserInfoAsync().Result;

        /// <summary>
        /// Gets a list of sub users
        /// </summary>
        /// <returns>List of sub users</returns>
        public async Task<WebCallResult<KucoinSubUser[]>> GetUserInfoAsync()
        {
            return await Execute<KucoinSubUser[]>(GetUri("sub/user"), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of accounts
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <returns>List of accounts</returns>
        public WebCallResult<KucoinAccount[]> GetAccounts(string currency = null, KucoinAccountType? accountType = null) => GetAccountsAsync(currency, accountType).Result;

        /// <summary>
        /// Gets a list of accounts
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <returns>List of accounts</returns>
        public async Task<WebCallResult<KucoinAccount[]>> GetAccountsAsync(string currency = null, KucoinAccountType? accountType = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("type", accountType.HasValue ? JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)): null);
            return await Execute<KucoinAccount[]>(GetUri("accounts"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get</param>
        /// <returns>Account info</returns>
        public WebCallResult<KucoinAccountSingle> GetAccount(string accountId) => GetAccountAsync(accountId).Result;

        /// <summary>
        /// Get a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get</param>
        /// <returns>Account info</returns>
        public async Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId)
        {
            return await Execute<KucoinAccountSingle>(GetUri("accounts/" + accountId), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="type">The type of the account</param>
        /// <param name="currency">The currency of the account</param>
        /// <returns>The id of the account</returns>
        public WebCallResult<KucoinNewAccount> CreateAccount(KucoinAccountType type, string currency) => GetAccountAsync(type, currency).Result;

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="type">The type of the account</param>
        /// <param name="currency">The currency of the account</param>
        /// <returns>The id of the account</returns>
        public async Task<WebCallResult<KucoinNewAccount>> GetAccountAsync(KucoinAccountType type, string currency)
        {
            var parameters = new Dictionary<string, object>
            {
                { "type", JsonConvert.SerializeObject(type, new AccountTypeConverter(false)) },
                { "currency", currency },
            };
            return await Execute<KucoinNewAccount>(GetUri("accounts"), method: Constants.PostMethod, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of account activity
        /// </summary>
        /// <param name="accountId">The account id to get the activities for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>Info on account activity</returns>
        public WebCallResult<KucoinPaginated<KucoinAccountActivity>> GetAccountLedger(string accountId, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null) => GetAccountLedgerAsync(accountId, startTime, endTime, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of account activity
        /// </summary>
        /// <param name="accountId">The account id to get the activities for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>Info on account activity</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgerAsync(string accountId, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinAccountActivity>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()): null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()): null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinAccountActivity>>(GetUri($"accounts/{accountId}/ledgers"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets hold information
        /// </summary>
        /// <param name="accountId">The account to get info for</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>Info on current holds</returns>
        public WebCallResult<KucoinPaginated<KucoinHold>> GetHolds(string accountId, int? currentPage = null, int? pageSize = null) => GetHoldsAsync(accountId, currentPage, pageSize).Result;

        /// <summary>
        /// Gets hold information
        /// </summary>
        /// <param name="accountId">The account to get info for</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>Info on current holds</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHold>>> GetHoldsAsync(string accountId, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinHold>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHold>>(GetUri($"accounts/{accountId}/holds"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of deposits
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of deposits</returns>
        public WebCallResult<KucoinPaginated<KucoinDeposit>> GetDeposits(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null) => GetDepositsAsync(currency, startTime, endTime, status, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of deposits
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of deposits</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinDeposit>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)): null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinDeposit>>(GetUri($"deposits"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of historical deposits
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical deposits</returns>
        public WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>> GetHistoricalDeposits(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null) => GetHistoricalDepositsAsync(currency, startTime, endTime, status, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of historical deposits
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical deposits</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHistoricalDeposit>>(GetUri($"hist-deposits"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to get the address for</param>
        /// <returns>The deposit address for the currency</returns>
        public WebCallResult<KucoinDepositAddress> GetDepositAddress(string currency) => GetDepositAddressAsync(currency).Result;

        /// <summary>
        /// Gets the deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to get the address for</param>
        /// <returns>The deposit address for the currency</returns>
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string currency)
        {
            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinDepositAddress>(GetUri($"deposit-addresses"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to create the address for</param>
        /// <returns>The address that was created</returns>
        public WebCallResult<KucoinDepositAddress> CreateDepositAddress(string currency) => CreateDepositAddressAsync(currency).Result;

        /// <summary>
        /// Creates a new deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to create the address for</param>
        /// <returns>The address that was created</returns>
        public async Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string currency)
        {
            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinDepositAddress>(GetUri($"deposit-addresses"), method: Constants.PostMethod, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of withdrawals
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of withdrawals</returns>
        public WebCallResult<KucoinPaginated<KucoinWithdrawal>> GetWithdrawals(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null) => GetWithdrawalsAsync(currency, startTime, endTime, status, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of withdrawals
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of withdrawals</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinWithdrawal>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinWithdrawal>>(GetUri($"withdrawals"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of historical withdrawals
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical withdrawals</returns>
        public WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>> GetHistoricalWithdrawals(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null) => GetHistoricalWithdrawalsAsync(currency, startTime, endTime, status, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of historical withdrawals
        /// </summary>
        /// <param name="currency">Filter list by currency</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by deposit status</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical withdrawals</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHistoricalWithdrawal>>(GetUri($"hist-withdrawals"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the withdrawal quota for a currency
        /// </summary>
        /// <param name="currency">The currency to get the quota for</param>
        /// <returns>Quota info</returns>
        public WebCallResult<KucoinWithdrawalQuota> GetWithdrawalQuotas(string currency) => GetWithdrawalQuotasAsync(currency).Result;

        /// <summary>
        /// Get the withdrawal quota for a currency
        /// </summary>
        /// <param name="currency">The currency to get the quota for</param>
        /// <returns>Quota info</returns>
        public async Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string currency)
        {
            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinWithdrawalQuota>(GetUri($"withdrawals/quotas"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Withdraw a currency to an address
        /// </summary>
        /// <param name="currency">The currency to withdraw</param>
        /// <param name="toAddress">The address to withdraw to</param>
        /// <param name="quantity">The quantity to withdraw</param>
        /// <param name="memo">The note that is left on the withdrawal address. When you withdraw from KuCoin to other platforms, you need to fill in memo(tag). If you don't fill in memo(tag), your withdrawal may not be available.</param>
        /// <param name="isInner">Internal withdrawal or not. Default false.</param>
        /// <param name="remark">Remark for the withdrawal</param>
        /// <param name="chain">The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is OMNI. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <returns>Id of the withdrawal</returns>
        public WebCallResult<KucoinNewWithdrawal> Withdraw(string currency, string toAddress, decimal quantity, string memo = null, bool isInner = false, string remark = null, string chain = null) => WithdrawAsync(currency, toAddress, quantity, memo, isInner, remark, chain).Result;

        /// <summary>
        /// Withdraw a currency to an address
        /// </summary>
        /// <param name="currency">The currency to withdraw</param>
        /// <param name="toAddress">The address to withdraw to</param>
        /// <param name="quantity">The quantity to withdraw</param>
        /// <param name="memo">The note that is left on the withdrawal address. When you withdraw from KuCoin to other platforms, you need to fill in memo(tag). If you don't fill in memo(tag), your withdrawal may not be available.</param>
        /// <param name="isInner">Internal withdrawal or not. Default false.</param>
        /// <param name="remark">Remark for the withdrawal</param>
        /// <param name="chain">The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is OMNI. This only apply for multi-chain currency, and there is no need for single chain currency.</param>
        /// <returns>Id of the withdrawal</returns>
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string currency, string toAddress, decimal quantity, string memo = null, bool isInner = false, string remark = null, string chain = null)
        {
            var parameters = new Dictionary<string, object> {
                { "currency", currency },
                { "address", toAddress },
                { "amount", quantity },
            };
            parameters.AddOptionalParameter("memo", memo);
            parameters.AddOptionalParameter("isInner", isInner);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", chain);
            return await Execute<KucoinNewWithdrawal>(GetUri($"withdrawals/quotas"), method: Constants.PostMethod,parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a withdrawal
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <returns>Null</returns>
        public WebCallResult<object> CancelWithdrawal(string withdrawalId) => CancelWithdrawalAsync(withdrawalId).Result;

        /// <summary>
        /// Cancel a withdrawal
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <returns>Null</returns>
        public async Task<WebCallResult<object>> CancelWithdrawalAsync(string withdrawalId)
        {
            return await Execute<object>(GetUri($"withdrawals/{withdrawalId}"), method: Constants.DeleteMethod, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="funds">The funds to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="stop">Type of stop order</param>
        /// <param name="stopPrice">The price for a stop order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <returns>The id of the new order</returns>
        public WebCallResult<KucoinNewOrder> PlaceOrder(
            string symbol,
            KucoinOrderSide side, 
            KucoinNewOrderType type, 
            decimal? price = null, 
            decimal? quantity = null, 
            decimal? funds = null,
            KucoinTimeInForce? timeInForce = null,
            DateTime? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string remark = null,
            KucoinStopCondition? stop = null, 
            decimal? stopPrice = null,
            KucoinSelfTradePrevention? selfTradePrevention = null,
            string clientOrderId = null) => PlaceOrderAsync(symbol, side, type, price, quantity, funds, timeInForce, cancelAfter, postOnly, hidden, iceBerg, visibleIceBergSize, remark, stop, stopPrice, selfTradePrevention, clientOrderId).Result;

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="funds">The funds to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceBerg">Order is an iceberg order</param>
        /// <param name="visibleIceBergSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="stop">Type of stop order</param>
        /// <param name="stopPrice">The price for a stop order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <returns>The id of the new order</returns>
        public async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
            string symbol,
            KucoinOrderSide side,
            KucoinNewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? funds = null,
            KucoinTimeInForce? timeInForce = null,
            DateTime? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string remark = null,
            KucoinStopCondition? stop = null,
            decimal? stopPrice = null,
            KucoinSelfTradePrevention? selfTradePrevention = null,
            string clientOrderId = null)
        {
            if (type == KucoinNewOrderType.Limit && !quantity.HasValue)
                return WebCallResult<KucoinNewOrder>.CreateErrorResult(new ArgumentError("Limit order needs a quantity"));

            if (type == KucoinNewOrderType.Market && !quantity.HasValue && !funds.HasValue)
                return WebCallResult<KucoinNewOrder>.CreateErrorResult(new ArgumentError("Market order needs quantity or funds specified"));

            if (type == KucoinNewOrderType.Market && quantity.HasValue && funds.HasValue)
                return WebCallResult<KucoinNewOrder>.CreateErrorResult(new ArgumentError("Market order cant have both quantity and funds specified"));

            if (stop.HasValue && stop != KucoinStopCondition.None && !stopPrice.HasValue)
                return WebCallResult<KucoinNewOrder>.CreateErrorResult(new ArgumentError("Stop orders need stop price to be specified"));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)) },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString() }
            };
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("size", quantity);
            parameters.AddOptionalParameter("funds", funds);
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce.Value, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? JsonConvert.SerializeObject(cancelAfter.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", stop.HasValue && stop != KucoinStopCondition.None ? JsonConvert.SerializeObject(stop.Value, new StopConditionConverter(false)) : null);
            parameters.AddOptionalParameter("stopPrice", stopPrice);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            return await Execute<KucoinNewOrder>(GetUri($"orders"), method: Constants.PostMethod, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns>List of cancelled orders</returns>
        public WebCallResult<KucoinCancelledOrders> CancelOrder(string orderId) => CancelOrderAsync(orderId).Result;

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <returns>List of cancelled orders</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelOrderAsync(string orderId)
        {
            return await Execute<KucoinCancelledOrders>(GetUri($"orders/{orderId}"), method: Constants.DeleteMethod, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="symbol">Only cancel orders for this symbol</param>
        /// <returns>List of cancelled orders</returns>
        public WebCallResult<KucoinCancelledOrders> CancelAllOrders(string symbol = null) => CancelAllOrdersAsync(symbol).Result;

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="symbol">Only cancel orders for this symbol</param>
        /// <returns>List of cancelled orders</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelAllOrdersAsync(string symbol = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCancelledOrders>(GetUri($"orders"), method: Constants.DeleteMethod, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by order status. Defaults to done</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of orders</returns>
        public WebCallResult<KucoinPaginated<KucoinOrder>> GetOrders(string symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, KucoinOrderStatus? status = null, int? currentPage = null, int? pageSize = null) => GetOrdersAsync(symbol, side, type, startTime, endTime, status, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="status">Filter list by order status. Defaults to done</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, KucoinOrderStatus? status = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinOrder>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new OrderStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinOrder>>(GetUri($"orders"), parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of max 1000 orders in the last 24 hours
        /// </summary>
        /// <returns>List of orders</returns>
        public WebCallResult<KucoinOrder[]> GetRecentOrders() => GetRecentOrdersAsync().Result;

        /// <summary>
        /// Gets a list of max 1000 orders in the last 24 hours
        /// </summary>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<KucoinOrder[]>> GetRecentOrdersAsync()
        {
            return await Execute<KucoinOrder[]>(GetUri($"limit/orders"), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a specific order
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <returns>Order info</returns>
        public WebCallResult<KucoinOrder> GetOrder(string orderId) => GetOrderAsync(orderId).Result;

        /// <summary>
        /// Get info on a specific order
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <returns>Order info</returns>
        public async Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId)
        {
            return await Execute<KucoinOrder>(GetUri($"orders/{orderId}"), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of historical orders
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical orders</returns>
        public WebCallResult<KucoinPaginated<KucoinHistoricalOrder>> GetHistoricalOrders(string symbol = null, KucoinOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null) => GetHistoricalOrdersAsync(symbol, side, startTime, endTime, currentPage, pageSize).Result;
        
        /// <summary>
        /// Gets a list of historical orders
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of historical orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>> GetHistoricalOrdersAsync(string symbol = null, KucoinOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinHistoricalOrder>>(GetUri($"hist-orders"), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of fills
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderId">Filter list by order id</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of fills</returns>
        public WebCallResult<KucoinPaginated<KucoinFill>> GetFills(string symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string orderId = null, int? currentPage = null, int? pageSize = null) => GetFillsAsync(symbol, side, type, startTime, endTime, orderId, currentPage, pageSize).Result;

        /// <summary>
        /// Gets a list of fills
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderId">Filter list by order id</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <returns>List of fills</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinFill>>> GetFillsAsync(string symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string orderId = null, int? currentPage = null, int? pageSize = null)
        {
            if (pageSize < 10 || pageSize > 500)
                return WebCallResult<KucoinPaginated<KucoinFill>>.CreateErrorResult(new ArgumentError("Page size should be between 10 and 500"));

            if (endTime.HasValue && startTime.HasValue && (endTime.Value - startTime.Value).TotalDays > 7)
                return WebCallResult<KucoinPaginated<KucoinFill>>.CreateErrorResult(new ArgumentError("Difference between start and end time can be a maximum of 1 week"));
            
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime.HasValue ? JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("endAt", startTime.HasValue ? JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinFill>>(GetUri($"fills"), signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of max 1000 fills in the last 24 hours
        /// </summary>
        /// <returns>List of fills</returns>
        public WebCallResult<KucoinFill[]> GetRecentFills() => GetRecentFillsAsync().Result;

        /// <summary>
        /// Gets a list of max 1000 fills in the last 24 hours
        /// </summary>
        /// <returns>List of fills</returns>
        public async Task<WebCallResult<KucoinFill[]>> GetRecentFillsAsync()
        {
            return await Execute<KucoinFill[]>(GetUri($"limit/fills"), signed: true).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketToken(bool authenticated)
        {
            return await Execute<KucoinToken>(GetUri(authenticated ? "bullet-private": "bullet-public"), method: Constants.PostMethod, signed:authenticated).ConfigureAwait(false);
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["code"] != null && error["msg"] != null)
            {
                var result = error.ToObject<KucoinResult<object>>();
                return new ServerError(result.Code, result.Message);
            }

            return new ServerError(error.ToString());
        }

        private async Task<WebCallResult<T>> Execute<T>(Uri uri, string method = Constants.GetMethod, Dictionary<string, object> parameters = null, bool signed = false)
        {
            var result = await ExecuteRequest<KucoinResult<T>>(uri, method, parameters, signed).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<T>.CreateErrorResult(result.Error);

            if (result.Data.Code != 200000)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message));

            return new WebCallResult<T>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        private Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(Path.Combine(BaseAddress, "v"+ apiVersion, path));
        }
        #endregion
    }
}
