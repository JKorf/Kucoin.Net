using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using Kucoin.Net.Converters;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Kucoin.Net.SubClients
{
    /// <summary>
    /// Spot endpoints
    /// </summary>
    public class KucoinClientSpot: RestClient, IKucoinClientSpot, IExchangeClient
    {
        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is cancelled via this client
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        internal KucoinClientSpot(KucoinClientOptions options): base("Kucoin[Spot]", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
        }

        /// <summary>
        /// Gets the server time
        /// </summary>
        /// <returns>The time of the server</returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await Execute<long>(GetUri("timestamp"), HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As<DateTime>(result ? JsonConvert.DeserializeObject<DateTime>(result.Data.ToString(), new TimestampConverter()) : default);
        }

        /// <summary>
        /// Gets a list of symbols supported by the server
        /// </summary>
        /// <param name="market">Only get symbols for a specific market, for example 'ALTS'</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of symbols</returns>
        public async Task<WebCallResult<IEnumerable<KucoinSymbol>>> GetSymbolsAsync(string? market = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("market", market);
            return await Execute<IEnumerable<KucoinSymbol>>(GetUri("symbols"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets ticker info of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get info for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Ticker info</returns>
        public async Task<WebCallResult<KucoinTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await Execute<KucoinTick>(GetUri("market/orderbook/level1"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the ticker for all trading pairs
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of tickers</returns>
        public async Task<WebCallResult<KucoinTicks>> GetTickersAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinTicks>(GetUri("market/allTickers"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the 24 hour stats of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get stats for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>24 hour stats</returns>
        public async Task<WebCallResult<Kucoin24HourStat>> Get24HourStatsAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            return await Execute<Kucoin24HourStat>(GetUri("market/stats"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of supported markets
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of markets</returns>
        public async Task<WebCallResult<IEnumerable<string>>> GetMarketsAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<string>>(GetUri("markets"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a partial aggregated order book for a symbol. Orders for the same price are combined and amount results are limited.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <param name="limit">The limit of results (20 / 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Partial aggregated order book</returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int limit, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            limit.ValidateIntValues(nameof(limit), 20, 100);

            return await Execute<KucoinOrderBook>(GetUri($"market/orderbook/level2_{limit}?symbol={symbol}"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a full aggregated order book for a symbol. Orders for the same price are combined.
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Full aggregated order book</returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            return await Execute<KucoinOrderBook>(GetUri($"market/orderbook/level2?symbol={symbol}", 3), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a full order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get order book for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Full order book</returns>
        public async Task<WebCallResult<KucoinFullOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            return await Execute<KucoinFullOrderBook>(GetUri($"market/orderbook/level3?symbol={symbol}", 3), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the recent trade history for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trade history for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades for the symbol</returns>
        public async Task<WebCallResult<IEnumerable<KucoinTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            return await Execute<IEnumerable<KucoinTrade>>(GetUri($"market/histories?symbol={symbol}"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get kline data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get klines for</param>
        /// <param name="interval">The interval of a kline</param>
        /// <param name="startTime">The start time of the data</param>
        /// <param name="endTime">The end time of the data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of klines</returns>
        public async Task<WebCallResult<IEnumerable<KucoinKline>>> GetKlinesAsync(string symbol, KucoinKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "type", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startAt", startTime == null ? null : JsonConvert.SerializeObject(startTime, new TimestampSecondsConverter()));
            parameters.AddOptionalParameter("endAt", endTime == null ? null : JsonConvert.SerializeObject(endTime, new TimestampSecondsConverter()));

            return await Execute<IEnumerable<KucoinKline>>(GetUri("market/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of supported currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        public async Task<WebCallResult<IEnumerable<KucoinCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinCurrency>>(GetUri("currencies"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a specific currency
        /// </summary>
        /// <param name="currency">The currency to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Currency info</returns>
        public async Task<WebCallResult<KucoinCurrency>> GetCurrencyAsync(string currency, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));
            return await Execute<KucoinCurrency>(GetUri($"currencies/{currency}"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of prices for all 
        /// </summary>
        /// <param name="fiatBase">The three letter code of the fiat to convert to. Defaults to USD</param>
        /// <param name="currencies">The currencies to get price for. Defaults to all</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<Dictionary<string, decimal>>> GetFiatPricesAsync(string? fiatBase = null, CancellationToken ct = default, params string[] currencies)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("base", fiatBase);
            parameters.AddOptionalParameter("currencies", currencies?.Length > 0 ? string.Join(",", currencies) : null);

            return await Execute<Dictionary<string, decimal>>(GetUri("prices"), HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of sub users
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub users</returns>
        public async Task<WebCallResult<IEnumerable<KucoinSubUser>>> GetUserInfoAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinSubUser>>(GetUri("sub/user"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of accounts
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        public async Task<WebCallResult<IEnumerable<KucoinAccount>>> GetAccountsAsync(string? currency = null, KucoinAccountType? accountType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("type", accountType.HasValue ? JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)) : null);
            return await Execute<IEnumerable<KucoinAccount>>(GetUri("accounts"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a specific account
        /// </summary>
        /// <param name="accountId">The id of the account to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account info</returns>
        public async Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            return await Execute<KucoinAccountSingle>(GetUri("accounts/" + accountId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="type">The type of the account</param>
        /// <param name="currency">The currency of the account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the account</returns>
        public async Task<WebCallResult<KucoinNewAccount>> CreateAccountAsync(KucoinAccountType type, string currency, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));

            var parameters = new Dictionary<string, object>
            {
                { "type", JsonConvert.SerializeObject(type, new AccountTypeConverter(false)) },
                { "currency", currency },
            };
            return await Execute<KucoinNewAccount>(GetUri("accounts"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the basic user fees
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinUserFee>(GetUri("base-fee"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the trading fees for symbols
        /// </summary>
        /// <param name="symbol">The symbol to retrieve fees for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default)
            => await GetSymbolTradingFeesAsync(new[] { symbol }, ct).ConfigureAwait(false);

        /// <summary>
        /// Get the trading fees for symbols
        /// </summary>
        /// <param name="symbols">The symbols to retrieve fees for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinTradeFee[]>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            return await Execute<KucoinTradeFee[]>(GetUri("trade-fees?symbols=" + string.Join(",", symbols)), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of account activity
        /// </summary>
        /// <param name="accountId">The account id to get the activities for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on account activity</returns>
        [Obsolete("Prefers GetAccountLedgersAsync")]
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgerAsync(string accountId, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinAccountActivity>>(GetUri($"accounts/{accountId}/ledgers"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of account activity
        /// </summary>
        /// <param name="currency">The currency to retrieve activity or null</param>
        /// <param name="direction">Side</param>
        /// <param name="bizType">Business type</param> 
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on account activity</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? currency = null, KucoinAccountDirection? direction = null, KucoinBizType? bizType = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            string bizTypeString = string.Empty;
            string directionString = string.Empty;

            if (bizType.HasValue)
            {
                bizTypeString = JsonConvert.SerializeObject(bizType, new BizTypeConverter(true));
                bizTypeString.ValidateNullOrNotEmpty(nameof(bizType));
            }

            if (direction.HasValue)
            {
                directionString = JsonConvert.SerializeObject(direction, new AccountDirectionConverter(false)).ToLowerInvariant();
            }

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("direction", direction.HasValue ? directionString : null);
            parameters.AddOptionalParameter("bizType", bizType.HasValue ? bizTypeString : null);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinAccountActivity>>(GetUri($"accounts/ledgers"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a transferable balance of a specified account.
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="accountType">Filter on type of account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on transferable account balance</returns>
        public async Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string currency, KucoinAccountType accountType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
                { "type", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false, true))}
            };
            return await Execute<KucoinTransferableAccount>(GetUri("accounts/transferable"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);

        }

        /// <summary>
        /// Transfers assets between the accounts of a user.
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="from">The type of the account</param>
        /// <param name="to">The type of the account</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order ID of a funds transfer</returns>
        public async Task<WebCallResult<KucoinInnerTransfer>> InnerTransferAsync(string currency, KucoinAccountType from, KucoinAccountType to, decimal quantity, string? clientOrderId = null, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));
            var parameters = new Dictionary<string, object>
            {
                { "currency", currency },
                { "from", JsonConvert.SerializeObject(from, new AccountTypeConverter(false))},
                { "to", JsonConvert.SerializeObject(to, new AccountTypeConverter(false))},
                { "amount", quantity },
                { "clientOid", clientOrderId ?? Guid.NewGuid().ToString()},
            };

            return await Execute<KucoinInnerTransfer>(GetUri("accounts/inner-transfer", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets hold information
        /// </summary>
        /// <param name="accountId">The account to get info for</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info on current holds</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHold>>> GetHoldsAsync(string accountId, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            accountId.ValidateNotNull(nameof(accountId));
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHold>>(GetUri($"accounts/{accountId}/holds"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinDeposit>>(GetUri("deposits"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical deposits</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string? currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinDepositStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHistoricalDeposit>>(GetUri("hist-deposits"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to get the address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address for the currency</returns>
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string currency, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));
            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinDepositAddress>(GetUri("deposit-addresses"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to create the address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The address that was created</returns>
        public async Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string currency, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));
            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinDepositAddress>(GetUri("deposit-addresses"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinWithdrawal>>(GetUri("withdrawals"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical withdrawals</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string? currency = null, DateTime? startTime = null, DateTime? endTime = null, KucoinWithdrawalStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinHistoricalWithdrawal>>(GetUri("hist-withdrawals"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the withdrawal quota for a currency
        /// </summary>
        /// <param name="currency">The currency to get the quota for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota info</returns>
        public async Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string currency, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));

            var parameters = new Dictionary<string, object> { { "currency", currency } };
            return await Execute<KucoinWithdrawalQuota>(GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id of the withdrawal</returns>
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string currency, string toAddress, decimal quantity, string? memo = null, bool isInner = false, string? remark = null, string? chain = null, CancellationToken ct = default)
        {
            currency.ValidateNotNull(nameof(currency));
            toAddress.ValidateNotNull(nameof(toAddress));
            var parameters = new Dictionary<string, object> {
                { "currency", currency },
                { "address", toAddress },
                { "amount", quantity },
            };
            parameters.AddOptionalParameter("memo", memo);
            parameters.AddOptionalParameter("isInner", isInner);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("chain", chain);
            return await Execute<KucoinNewWithdrawal>(GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a withdrawal
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Null</returns>
        public async Task<WebCallResult<object>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            withdrawalId.ValidateNotNull(nameof(withdrawalId));
            return await Execute<object>(GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
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
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        public async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
            string symbol,
            string clientOrderId,
            KucoinOrderSide side,
            KucoinNewOrderType type,
            decimal? price = null,
            decimal? quantity = null,
            decimal? funds = null,
            KucoinTimeInForce? timeInForce = null,
            TimeSpan? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceBerg = null,
            decimal? visibleIceBergSize = null,
            string? remark = null,
            KucoinSelfTradePrevention? selfTradePrevention = null,
            CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            switch (type)
            {
                case KucoinNewOrderType.Limit when !quantity.HasValue:
                    throw new ArgumentException("Limit order needs a quantity");
                case KucoinNewOrderType.Market when !quantity.HasValue && !funds.HasValue:
                    throw new ArgumentException("Market order needs quantity or funds specified");
                case KucoinNewOrderType.Market when quantity.HasValue && funds.HasValue:
                    throw new ArgumentException("Market order cant have both quantity and funds specified");
            }

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
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? (long)Math.Round(cancelAfter.Value.TotalSeconds, 0) : (long?)null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceBerg", iceBerg);
            parameters.AddOptionalParameter("visibleSize", visibleIceBergSize);
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention.Value, new SelfTradePreventionConverter(false)) : null);
            var result = await Execute<KucoinNewOrder>(GetUri("orders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (result)
                OnOrderPlaced?.Invoke(result.Data);
            return result;
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            var result = await Execute<KucoinCancelledOrders>(GetUri($"orders/{orderId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
            if (result)
                OnOrderCanceled?.Invoke(result.Data);
            return result;

        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="clientOrderId">The client order id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        public async Task<WebCallResult<KucoinCancelledOrder>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            var result = await Execute<KucoinCancelledOrder>(GetUri($"order/client-order/{clientOrderId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
            if (result)
                OnOrderCanceled?.Invoke(result.Data);
            return result;
        }

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="symbol">Only cancel orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of cancelled orders</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCancelledOrders>(GetUri("orders"), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetOrdersAsync(string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, KucoinOrderStatus? status = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status, new OrderStatusConverter(false)) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinOrder>>(GetUri("orders"), HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of max 1000 orders in the last 24 hours
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<IEnumerable<KucoinOrder>>> GetRecentOrdersAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinOrder>>(GetUri("limit/orders"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a specific order
        /// </summary>
        /// <param name="clientOrderId">The client order id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        public async Task<WebCallResult<KucoinOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            clientOrderId.ValidateNotNull(nameof(clientOrderId));
            return await Execute<KucoinOrder>(GetUri($"order/client-order/{clientOrderId}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get info on a specific order
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order info</returns>
        public async Task<WebCallResult<KucoinOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            return await Execute<KucoinOrder>(GetUri($"orders/{orderId}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
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
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of historical orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinHistoricalOrder>>> GetHistoricalOrdersAsync(string? symbol = null, KucoinOrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinHistoricalOrder>>(GetUri("hist-orders"), HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of your trades
        /// </summary>
        /// <param name="symbol">Filter list by symbol</param>
        /// <param name="type">Filter list by order type</param>
        /// <param name="side">Filter list by order side</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderId">Filter list by order id</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of fills</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinFill>>> GetUserTradesAsync(string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string? orderId = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            symbol?.ValidateKucoinSymbol();
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 500);

            if (endTime.HasValue && startTime.HasValue && (endTime.Value - startTime.Value).TotalDays > 7)
                throw new ArgumentException("Difference between start and end time can be a maximum of 1 week");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinFill>>(GetUri("fills"), HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of max 1000 fills in the last 24 hours
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of fills</returns>
        public async Task<WebCallResult<IEnumerable<KucoinFill>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinFill>>(GetUri("limit/fills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Place a new stop order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderSide">The side of the order</param>
        /// <param name="orderType">The type of the order</param>
        /// <param name="price">The price of the order. Only valid for limit orders.</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="quoteQuantity">The funds to use for the order. Only valid for market orders. If used, quantity needs to be empty</param>
        /// <param name="timeInForce">The time the order is in force</param>
        /// <param name="cancelAfter">Cancel after a time</param>
        /// <param name="postOnly">Order is post only</param>
        /// <param name="hidden">Order is hidden</param>
        /// <param name="iceberg">Order is an iceberg order</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="remark">Remark on the order</param>
        /// <param name="selfTradePrevention">Self trade prevention setting</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="stopCondition">Stop price condition</param>
        /// <param name="stopPrice">Price to trigger the order placement</param>
        /// <param name="tradeType">Trade type</param>        
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinNewOrder>> PlaceStopOrderAsync(
            string symbol,
            string clientOrderId,
            KucoinOrderSide orderSide,
            KucoinNewOrderType orderType,
            KucoinStopCondition stopCondition,
            decimal stopPrice,
            string? remark = null,
            KucoinSelfTradePrevention? selfTradePrevention = null,
            KucoinTradeType? tradeType = null,

            decimal? price = null,
            decimal? quantity = null,
            KucoinTimeInForce? timeInForce = null,
            DateTime? cancelAfter = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            decimal? quoteQuantity = null,
            CancellationToken ct = default)
        {
            if (orderType == KucoinNewOrderType.Limit && quoteQuantity != null)
                throw new ArgumentException("QuoteQuantity can only be provided for a market order", nameof(quoteQuantity));

            if ((price.HasValue || timeInForce.HasValue || cancelAfter.HasValue || postOnly.HasValue || hidden.HasValue || iceberg.HasValue || visibleSize.HasValue)
                && orderType == KucoinNewOrderType.Market)
                throw new ArgumentException("Invalid parameter(s) provided for market order type");

            if (stopCondition == KucoinStopCondition.None)
                throw new ArgumentException("Invalid stop condition", nameof(stopCondition));

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "clientOid", clientOrderId },
                { "side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(orderType, new NewOrderTypeConverter(false)) },
                { "stop", JsonConvert.SerializeObject(stopCondition,new StopConditionConverter(false)) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
            };

            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stp", selfTradePrevention.HasValue ? JsonConvert.SerializeObject(selfTradePrevention, new SelfTradePreventionConverter(false)) : null);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce.HasValue ? JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)) : null);
            parameters.AddOptionalParameter("cancelAfter", cancelAfter.HasValue ? JsonConvert.SerializeObject(cancelAfter, new TimestampSecondsConverter()) : null);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("hidden", hidden);
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString(CultureInfo.InvariantCulture));

            parameters.AddOptionalParameter("funds", quoteQuantity);

            return await Execute<KucoinNewOrder>(GetUri("stop-order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a stop order by order id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinCancelledOrders>(GetUri("stop-order/" + orderId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a stop order by client order id
        /// </summary>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinCancelledOrder>> CancelStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientOid", clientOrderId }
            };
            return await Execute<KucoinCancelledOrder>(GetUri("stop-order/cancelOrderByClientOid"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all stop orders fitting the provided parameters
        /// </summary>
        /// <param name="symbol">Symbol to cancel orders on</param>
        /// <param name="orderIds">Order ids of the orders to cancel</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelStopOrdersAsync(string? symbol = null, IEnumerable<string>? orderIds = null, KucoinTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderIds", orderIds == null ? null : string.Join(",", orderIds));
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            return await Execute<KucoinCancelledOrders>(GetUri("stop-order/cancel"), HttpMethod.Delete, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of stop orders fitting the provided parameters
        /// </summary>
        /// <param name="activeOrders">True to return active orders, false for completed orders</param>
        /// <param name="symbol">Symbol of the orders</param>
        /// <param name="side">Side of the orders</param>
        /// <param name="type">Type of the orders</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="startTime">Filter list by start time</param>
        /// <param name="endTime">Filter list by end time</param>
        /// <param name="orderIds">Filter list by order ids</param>
        /// <param name="currentPage">The page to retrieve</param>
        /// <param name="pageSize">The amount of results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinOrder>>> GetStopOrdersAsync(bool? activeOrders = null, string? symbol = null, KucoinOrderSide? side = null,
            KucoinOrderType? type = null, KucoinTradeType? tradeType = null, DateTime? startTime = null, DateTime? endTime = null, IEnumerable<string>? orderIds = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("status", activeOrders.HasValue ? (activeOrders == true ? "active" : "done") : null);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side.HasValue ? JsonConvert.SerializeObject(side, new OrderSideConverter(false)) : null);
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("orderIds", orderIds == null ? null : string.Join(",", orderIds));
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("tradeType", tradeType.HasValue ? JsonConvert.SerializeObject(tradeType, new TradeTypeConverter(false)) : null);

            return await Execute<KucoinPaginated<KucoinOrder>>(GetUri("stop-order"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }


        /// <summary>
        /// Get a stop order by id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinOrder>> GetStopOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinOrder>(GetUri("stop-order/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a stop order by client order id
        /// </summary>
        /// <param name="clientOrderId">The client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<KucoinOrder>>> GetStopOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientOid", clientOrderId }
            };
            return await Execute<IEnumerable<KucoinOrder>>(GetUri("stop-order/queryOrderByClientOid"), HttpMethod.Get, ct, parameters, signed: true).ConfigureAwait(false);
        }

        internal async Task<WebCallResult<KucoinToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await Execute<KucoinToken>(GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }



        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
            {
                var errorBody = error.ToString();
                return new ServerError(string.IsNullOrEmpty(errorBody) ? "Unknown error" : errorBody);
            }

            if (error["code"] != null && error["msg"] != null)
            {
                var result = error.ToObject<KucoinResult<object>>();
                return new ServerError(result.Code, result.Message!);
            }

            return new ServerError(error.ToString());
        }

        internal static long ToUnixTimestamp(DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        }

        internal async Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<KucoinResult<object>>(uri, method, ct, parameters, signed).ConfigureAwait(false);
            if (!result)
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return new WebCallResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);
        }

        internal async Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<KucoinResult<T>>(uri, method, ct, parameters, signed).ConfigureAwait(false);
            if (!result)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data.Code != 200000)
                return WebCallResult<T>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As<T>(result.Data.Data);
        }

        internal Uri GetUri(string path, int apiVersion = 1)
        {
            return new Uri(Path.Combine(BaseAddress, "v" + apiVersion, path));
        }

        #region common interface

        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var symbols = await GetSymbolsAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonSymbol>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var result = await GetTickerAsync(symbol).ConfigureAwait(false);
            return result.As<ICommonTicker>((ICommonTicker?)result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var symbols = await GetTickersAsync().ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonTicker>>(symbols.Data?.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (limit != null)
                return WebCallResult<IEnumerable<ICommonKline>>.CreateErrorResult(new ArgumentError(
                    $"Kucoin doesn't support the {nameof(limit)} parameter for the method {nameof(IExchangeClient.GetKlinesAsync)}"));

            var symbols = await GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime).ConfigureAwait(false);
            return symbols.As<IEnumerable<ICommonKline>>(symbols.Data);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var book = await GetOrderBookAsync(symbol).ConfigureAwait(false);
            return book.As<ICommonOrderBook>(book.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var book = await GetTradeHistoryAsync(symbol).ConfigureAwait(false);
            return book.As<IEnumerable<ICommonRecentTrade>>(book.Data);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var order = await PlaceOrderAsync(symbol,
                Guid.NewGuid().ToString(),
                side == IExchangeClient.OrderSide.Sell ? KucoinOrderSide.Sell : KucoinOrderSide.Buy,
                type == IExchangeClient.OrderType.Limit ? KucoinNewOrderType.Limit : KucoinNewOrderType.Market,
                price, quantity).ConfigureAwait(false);
            return order.As<ICommonOrderId>(order.Data);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol)
        {
            var order = await GetOrderAsync(orderId).ConfigureAwait(false);
            return order.As<ICommonOrder>(order.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol = null)
        {
            var trades = await GetUserTradesAsync(orderId: orderId).ConfigureAwait(false);
            return trades.As<IEnumerable<ICommonTrade>>(trades.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol)
        {
            var orders = await GetOrdersAsync(status: KucoinOrderStatus.Active).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol)
        {
            var orders = await GetOrdersAsync(status: KucoinOrderStatus.Done).ConfigureAwait(false);
            return orders.As<IEnumerable<ICommonOrder>>(orders.Data?.Items);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol)
        {
            var result = await CancelOrderAsync(orderId).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result.Data);
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var result = await GetAccountsAsync().ConfigureAwait(false);
            return result.As<IEnumerable<ICommonBalance>>(result.Data);
        }

        private static KucoinKlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KucoinKlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KucoinKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KucoinKlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KucoinKlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KucoinKlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return KucoinKlineInterval.TwoHours;
            if (timeSpan == TimeSpan.FromHours(4)) return KucoinKlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromHours(6)) return KucoinKlineInterval.SixHours;
            if (timeSpan == TimeSpan.FromHours(8)) return KucoinKlineInterval.EightHours;
            if (timeSpan == TimeSpan.FromHours(12)) return KucoinKlineInterval.TwelfHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KucoinKlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KucoinKlineInterval.OneWeek;

            throw new ArgumentException("Unsupported timespan for Kucoin kline interval, check supported intervals using Kucoin.Net.Objects.KucoinKlineInterval");
        }
        #endregion
    }
}
