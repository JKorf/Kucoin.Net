using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Futures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kucoin.Net.Converters;
using Kucoin.Net.Objects.Socket;
using Kucoin.Net.Objects.Spot;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Kucoin.Net.SubClients
{
    /// <summary>
    /// Futures API endpoints
    /// </summary>
    public class KucoinClientFutures: RestClient, IKucoinClientFutures
    {
        /// <summary>
        /// Base address for the futures API
        /// </summary>
        public new string BaseAddress { get; set; }

        internal KucoinClientFutures(KucoinClientOptions options): base("Kucoin[Futures]", options, options.FuturesApiCredentials == null ? null : new KucoinAuthenticationProvider(options.FuturesApiCredentials))
        {
            BaseAddress = options.FuturesBaseAddress;
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

        #region Account
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await Execute<KucoinAccountOverview>(GetUri("account-overview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("type", type == null ? null: JsonConvert.SerializeObject(type, new TransactionTypeConverter(false)));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await Execute<KucoinPaginatedSlider<KucoinAccountTransaction>>(GetUri("transaction-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Deposit
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            return await Execute<KucoinDepositAddress>(GetUri("deposit-address"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null? null: JsonConvert.SerializeObject(status, new DepositStatusConverter(false)));
            return await Execute<KucoinPaginated<KucoinDeposit>>(GetUri("deposit-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            return await Execute<KucoinFuturesWithdrawalQuota>(GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("address", address);
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isInner", isInner?.ToString());
            parameters.AddOptionalParameter("remark",remark);
            parameters.AddOptionalParameter("chain", chain);
            parameters.AddOptionalParameter("memo", memo);
            return await Execute<KucoinNewWithdrawal>(GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)));
            return await Execute<KucoinPaginated<KucoinWithdrawal>>(GetUri("withdrawal-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            return await Execute(GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct,signed: true).ConfigureAwait(false);
        }
        #endregion

        #region Transfer
        /// <inheritdoc />
        public async Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string asset, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            return await Execute<KucoinTransferResult>(GetUri("transfer-out", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string asset, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", asset);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinTransfer>>(GetUri("transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("applyId", applyId);
            return await Execute(GetUri("cancel/transfer-out", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinNewOrder>> PlaceOrderAsync(
            string symbol, 
            OrderSide side, 
            NewOrderType type, 
            int leverage,
            decimal quantity,

            decimal? price = null,
            TimeInForce? timeInForce = null,
            bool? postOnly = null,
            bool? hidden = null,
            bool? iceberg = null,
            decimal? visibleSize = null,

            string? remark = null,
            StopType? stopType = null,
            StopPriceType? stopPriceType = null,
            decimal? stopPrice = null,
            bool? reduceOnly = null,
            bool? closeOrder = null,
            bool? forceHold = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("type", JsonConvert.SerializeObject(type, new NewOrderTypeConverter(false)));
            parameters.AddParameter("leverage", leverage.ToString(CultureInfo.InvariantCulture));
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", stopType != null ? JsonConvert.SerializeObject(stopType, new StopTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPriceType", stopPriceType != null ? JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)) : null);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());

            return await Execute<KucoinNewOrder>(GetUri("orders", 1), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinCanceledOrders>(GetUri("orders/" + orderId, 1), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCanceledOrders>(GetUri("orders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinCanceledOrders>> CancelAllOpenStopOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCanceledOrders>(GetUri("stopOrders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("status", status == null ? null: JsonConvert.SerializeObject(status, new OrderStatusConverter(false)));
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinFuturesOrder>>(GetUri("orders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinFuturesOrder>>(GetUri("stopOrders"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetCompletedOrdersAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinFuturesOrder>>(GetUri("recentDoneOrders"), HttpMethod.Get, ct, signed:true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinFuturesOrder>(GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            return await Execute<KucoinFuturesOrder>(GetUri("orders/byClientOid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, OrderSide? side = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinFuturesUserTrade>>(GetUri("fills"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinFuturesUserTrade>>(GetUri("recentFills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderValuation>> GetActiveOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderValuation>(GetUri("openOrderStatistics"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Positions

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinPosition>(GetUri("position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinPosition>>(GetUri("positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            return await Execute(GetUri("position/margin/auto-deposit-status"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("margin", quantity.ToString(CultureInfo.InvariantCulture));
            return await Execute(GetUri("position/margin/deposit-margin"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Funding fees

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await Execute<KucoinPaginatedSlider<KucoinFundingItem>>(GetUri("funding-history"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinContract>>(GetUri("contracts/active"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinContract>(GetUri("contracts/" + symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinFuturesTick>(GetUri("ticker"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Order book

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderBook>(GetUri("level2/snapshot"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 20, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderBook>(GetUri("level2/depth" + depth), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Trade history

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<IEnumerable<KucoinFuturesTrade>>(GetUri("trade/history"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Index

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinFuturesInterest>>> GetInterestRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await Execute<KucoinPaginatedSlider<KucoinFuturesInterest>>(GetUri("interest/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinIndex>>> GetIndexListAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await Execute<KucoinPaginatedSlider<KucoinIndex>>(GetUri("index/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinMarkPrice>(GetUri($"mark-price/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinPremiumIndex>>> GetPremiumIndexAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("maxCount", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("forward", forward);
            return await Execute<KucoinPaginatedSlider<KucoinPremiumIndex>>(GetUri("premium/query"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinFundingRate>(GetUri($"funding-rate/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await Execute<long>(GetUri("timestamp"), HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result ? new DateTime(1970, 1, 1).AddMilliseconds(result.Data): default);
        }

        #endregion

        #region Server status

        /// <inheritdoc />
        public async Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinFuturesServiceStatus>(GetUri("status"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<KucoinFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("granularity", JsonConvert.SerializeObject(interval, new FuturesKlineIntervalConverter(false)));
            parameters.AddOptionalParameter("from", startTime == null ? null: JsonConvert.SerializeObject(startTime, new TimestampConverter()));
            parameters.AddOptionalParameter("to", endTime == null ? null : JsonConvert.SerializeObject(endTime, new TimestampConverter()));
            return await Execute<IEnumerable<KucoinFuturesKline>>(GetUri("kline/query"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

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
                return new ServerError(result!.Code, result.Message!);
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
    }
}
