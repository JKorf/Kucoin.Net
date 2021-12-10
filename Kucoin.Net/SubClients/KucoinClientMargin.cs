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
using System.Linq;
using Kucoin.Net.Objects.Margin;

namespace Kucoin.Net.SubClients
{
    /// <summary>
    /// Spot endpoints
    /// </summary>
    public class KucoinClientMargin : RestClient, IKucoinClientMargin
    {

        internal KucoinClientMargin(KucoinClientOptions options) : base("Kucoin[Margin]", options, options.ApiCredentials == null ? null : new KucoinAuthenticationProvider(options.ApiCredentials))
        {
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        /// <param name="apiPass">The api passphrase</param>
        public void SetApiCredentials(string apiKey, string apiSecret, string apiPass)
        {
            SetAuthenticationProvider(new KucoinAuthenticationProvider(new KucoinApiCredentials(apiKey, apiSecret, apiPass)));
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
 

        #region Margin Trade

        #region Margin Info

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateKucoinSymbol();
            return await Execute<KucoinMarkPrice>(GetUri($"mark-price/{symbol}/current"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinMarginConfigurationInfo>> GetMarginConfigurationInfoAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinMarginConfigurationInfo>(GetUri("margin/config"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinMarginAccount>(GetUri("margin/account"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        #endregion

        #region Borrow & Lend

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinNewBorrowOrder>> PlaceBorrowOrderAsync(string asset, KucoinBorrowOrderType type, decimal quantity, decimal? maxRate = null, string? term = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "type", JsonConvert.SerializeObject(type, new BorrowOrderTypeConverter(false)) },
                { "size", quantity }
            };
            parameters.AddOptionalParameter("maxRate", maxRate);
            parameters.AddOptionalParameter("term", term);
            return await Execute<KucoinNewBorrowOrder>(GetUri("margin/borrow"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinBorrowOrder>> GetBorrowOrderAsync(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            return await Execute<KucoinBorrowOrder>(GetUri($"margin/borrow?orderId={orderId}"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinBorrowUnrepaid>>> GetUnrepaidBorrowsAsync(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinBorrowUnrepaid>>(GetUri("margin/borrow/outstanding"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinBorrowRepaid>>> GetRepaidBorrowsAsync(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinBorrowRepaid>>(GetUri("margin/borrow/repaid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult> OneClickRepayment(string asset, KucoinRepaymentStrategy strategy, decimal quantity, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            strategy.ValidateNotNull(nameof(strategy));
            quantity.ValidateNotNull(nameof(quantity));

            var parameters = new Dictionary<string, object>()
            {
                { "currency", asset },
                { "sequence", JsonConvert.SerializeObject(strategy, new RepaymentStrategyConverter(false)) },
                { "size", quantity }
            };

            return await Execute(GetUri("margin/repay/all"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult> RepaySingleBorrowOrderAsync(string asset, string tradeId, decimal quantity, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            tradeId.ValidateNotNull(nameof(tradeId));
            quantity.ValidateNotNull(nameof(quantity));

            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "tradeId", tradeId },
                { "size", quantity }
            };

            return await Execute(GetUri("margin/repay/single"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinNewLendOrder>> PostLendOrderAsync(string asset, string size, string dailyRate, int term, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            size.ValidateNotNull(nameof(size));
            dailyRate.ValidateNotNull(nameof(dailyRate));
            term.ValidateNotNull(nameof(term));

            var parameters = new Dictionary<string, object>
            {
                { "currency", asset },
                { "size", size },
                { "dailyIntRate", dailyRate },
                { "term", term }
            };

            return await Execute<KucoinNewLendOrder>(GetUri("margin/lend"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult> CancelLendOrder(string orderId, CancellationToken ct = default)
        {
            orderId.ValidateNotNull(nameof(orderId));
            return await Execute(GetUri($"margin/lend/{orderId}"), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetOpenLendOrders(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 50);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinLendOrder>>(GetUri("margin/lend/active"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinLendOrder>>> GetLendOrdersHistory(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 50);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinLendOrder>>(GetUri("margin/lend/done"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinLendUnsettled>>> GetUnsettledLends(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 50);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinLendUnsettled>>(GetUri("margin/lend/trade/unsettled"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<KucoinPaginated<KucoinLendSettled>>> GetSettledLends(string? asset = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            pageSize?.ValidateIntBetween(nameof(pageSize), 10, 50);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);

            return await Execute<KucoinPaginated<KucoinLendSettled>>(GetUri("margin/lend/trade/settled"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<IEnumerable<KucoinLendAccount>>> GetAccountLendRecord(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);

            return await Execute<IEnumerable<KucoinLendAccount>>(GetUri("margin/lend/assets"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<IEnumerable<KucoinLendingMarketData>>> GetLendingMarketData(string asset, int? term = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddOptionalParameter("term", term);

            return await Execute<IEnumerable<KucoinLendingMarketData>>(GetUri("margin/market"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<WebCallResult<IEnumerable<KucoinMarginTradeData>>> GetMarginTradeData(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);

            return await Execute<IEnumerable<KucoinMarginTradeData>>(GetUri("margin/trade/last"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }


        #endregion

        #endregion
        /*
        internal async Task<WebCallResult<KucoinToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await Execute<KucoinToken>(GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }
        */

        /*
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
                if (result == null)
                    return new ServerError(error["msg"]!.ToString());

                return new ServerError(result.Code, result.Message!);
            }

            return new ServerError(error.ToString());
        }*/

        //internal static long ToUnixTimestamp(DateTime time)
        //{
        //    return ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        //}

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
        /*
        /// <summary>
        /// Return the Kucoin trade symbol name from base and quote asset 
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + "-" + quoteAsset).ToUpperInvariant();
        */
        #endregion
    }
}
