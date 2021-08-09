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

        #region Account
        /// <summary>
        /// Gets account overview
        /// </summary>
        /// <param name="currency">Get the accounts for a specific currency</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        public async Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            return await Execute<KucoinAccountOverview>(GetUri("account-overview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get transaction history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? currency = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
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
        /// <summary>
        /// Get the deposit address for a currency
        /// </summary>
        /// <param name="currency">The currency to get deposit address for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        public async Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string currency, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            return await Execute<KucoinDepositAddress>(GetUri("deposit-address"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit history</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositHistoryAsync(string? currency = null, KucoinDepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null? null: JsonConvert.SerializeObject(status, new DepositStatusConverter(false)));
            return await Execute<KucoinPaginated<KucoinDeposit>>(GetUri("deposit-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Withdrawal

        /// <summary>
        /// Get the withdrawal limit
        /// </summary>
        /// <param name="currency">The currency to get limits for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        public async Task<WebCallResult<KucoinFuturesWithdrawalQuota>> GetWithdrawalLimitAsync(string currency, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", currency);
            return await Execute<KucoinFuturesWithdrawalQuota>(GetUri("withdrawals/quotas"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Withdraw funds
        /// </summary>
        /// <param name="currency">Currency to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="isInner">Internal transfer (default false)</param>
        /// <param name="remark">Remarks</param>
        /// <param name="chain">Chain to use</param>
        /// <param name="memo">Memo for the withdrawal</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        public async Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string currency, string address, decimal quantity, bool? isInner = null, string? remark = null, string? chain = null, string? memo = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", currency);
            parameters.AddParameter("address", address);
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isInner", isInner?.ToString());
            parameters.AddOptionalParameter("remark",remark);
            parameters.AddOptionalParameter("chain", chain);
            parameters.AddOptionalParameter("memo", memo);
            return await Execute<KucoinNewWithdrawal>(GetUri("withdrawals"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get withdraw history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Page to retrieve</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal history</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawHistoryAsync(string? currency = null, KucoinWithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)));
            return await Execute<KucoinPaginated<KucoinWithdrawal>>(GetUri("withdrawal-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a withdrawal in process
        /// </summary>
        /// <param name="withdrawalId">The id of the withdrawal to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal limit info</returns>
        public async Task<WebCallResult> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
        {
            return await Execute(GetUri($"withdrawals/{withdrawalId}"), HttpMethod.Delete, ct,signed: true).ConfigureAwait(false);
        }
        #endregion

        #region Transfer
        /// <summary>
        /// Transfer funds from futures to main account
        /// </summary>
        /// <param name="currency">Currency to withdraw</param>
        /// <param name="clientId">Client identifier for the operation, needs to be unique. Guid.NewGuid() suggested</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        public async Task<WebCallResult<KucoinTransferResult>> TransferToMainAccountAsync(string currency, decimal quantity, string? clientId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", currency);
            parameters.AddParameter("bizNo", clientId ?? Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            parameters.AddParameter("amount", quantity.ToString(CultureInfo.InvariantCulture));
            return await Execute<KucoinTransferResult>(GetUri("transfer-out", 2), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get transfer to main account history
        /// </summary>
        /// <param name="currency">Filter by currency</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinTransfer>>> GetTransferToMainAccountHistoryAsync(string currency, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("currency", currency);
            parameters.AddOptionalParameter("startAt", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endAt", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("currentPage", currentPage);
            parameters.AddOptionalParameter("pageSize", pageSize);
            return await Execute<KucoinPaginated<KucoinTransfer>>(GetUri("transfer-list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel a transfer from futures account to main account
        /// </summary>
        /// <param name="applyId">Transfer id to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal id</returns>
        public async Task<WebCallResult> CancelTransferToMainAccountAsync(string applyId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("applyId", applyId);
            return await Execute(GetUri("cancel/transfer-out", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Orders

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="symbol">The contract for the order</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of order</param>
        /// <param name="leverage">Leverage of the order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="timeInForce">Time in force, only for limit orders</param>
        /// <param name="postOnly">Post only flag, invalid when timeInForce is IOC</param>
        /// <param name="hidden">Orders not displaying in order book. When hidden chose</param>
        /// <param name="iceberg">Only visible portion of the order is displayed in the order book</param>
        /// <param name="visibleSize">The maximum visible size of an iceberg order</param>
        /// <param name="size">Amount of contract to buy or sell</param>
        /// <param name="remark">Remark for the order</param>
        /// <param name="stopType"></param>
        /// <param name="stopPriceType"></param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="closeOrder">A mark to close the position. Set to false by default. All the positions will be closed if true</param>
        /// <param name="forceHold">A mark to forcely hold the funds for an order, even though it's an order to reduce the position size. This helps the order stay on the order book and not get canceled when the position size changes. Set to false by default</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        public async Task<WebCallResult> PlaceOrderAsync(
            string symbol, 
            KucoinOrderSide side, 
            KucoinNewOrderType type, 
            int leverage,
            decimal size,

            decimal? price = null,
            KucoinTimeInForce? timeInForce = null,
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
            parameters.AddParameter("size", size.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("remark", remark);
            parameters.AddOptionalParameter("stop", JsonConvert.SerializeObject(stopType, new StopTypeConverter(false)));
            parameters.AddOptionalParameter("stopPriceType", JsonConvert.SerializeObject(stopPriceType, new StopPriceTypeConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("closeOrder", closeOrder?.ToString());
            parameters.AddOptionalParameter("forceHold", forceHold?.ToString());
            parameters.AddOptionalParameter("price", price?.ToString());
            parameters.AddOptionalParameter("timeInForce", JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("postOnly", postOnly?.ToString());
            parameters.AddOptionalParameter("hidden", hidden?.ToString());
            parameters.AddOptionalParameter("iceberg", iceberg);
            parameters.AddOptionalParameter("visibleSize", visibleSize?.ToString());

            return await Execute(GetUri("orders", 1), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled id</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinCancelledOrders>(GetUri("orders/" + orderId, 1), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled ids</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelAllOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCancelledOrders>(GetUri("orders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }


        /// <summary>
        /// Cancel all open stop orders
        /// </summary>
        /// <param name="symbol">Cancel only orders for this symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancelled ids</returns>
        public async Task<WebCallResult<KucoinCancelledOrders>> CancelAllOpenStopOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await Execute<KucoinCancelledOrders>(GetUri("stopOrders", 1), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="status">Filter by status</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetOrdersAsync(string? symbol = null, KucoinOrderStatus? status = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
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


        /// <summary>
        /// Get list of untriggered stop orders
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesOrder>>> GetUntriggeredStopOrdersAsync(string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
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

        /// <summary>
        /// Get list of 1000 most recent orders in the last 24 hours
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<IEnumerable<KucoinFuturesOrder>>> GetCompletedOrdersAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinFuturesOrder>>(GetUri("recentDoneOrders"), HttpMethod.Get, ct, signed:true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get details on an order
        /// </summary>
        /// <param name="orderId">Id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order</returns>
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            return await Execute<KucoinFuturesOrder>(GetUri("orders/" + orderId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get details on an order
        /// </summary>
        /// <param name="clientOrderId">Client order id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order</returns>
        public async Task<WebCallResult<KucoinFuturesOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("clientOid", clientOrderId);
            return await Execute<KucoinFuturesOrder>(GetUri("orders/byClientOid"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Fills

        /// <summary>
        /// Get list of user trades
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        public async Task<WebCallResult<KucoinPaginated<KucoinFuturesUserTrade>>> GetUserTradesAsync(string? orderId = null, string? symbol = null, KucoinOrderSide? side = null, KucoinOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? currentPage = null, int? pageSize = null, CancellationToken ct = default)
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

        /// <summary>
        /// Get list of 1000 most recent user trades in the last 24 hours
        /// </summary>        
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        public async Task<WebCallResult<IEnumerable<KucoinFuturesUserTrade>>> GetRecentUserTradesAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinFuturesUserTrade>>(GetUri("recentFills"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the total value of active orders
        /// </summary>        
        /// <param name="symbol">Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinOrderValuation>> GetActiveOrderValueAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderValuation>(GetUri("openOrderStatistics"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Positions

        /// <summary>
        /// Get details on a position
        /// </summary>
        /// <param name="symbol">Contract symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        public async Task<WebCallResult<KucoinPosition>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinPosition>(GetUri("position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list of positions
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        public async Task<WebCallResult<IEnumerable<KucoinPosition>>> GetPositionsAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinPosition>>(GetUri("positions"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Enable/disable auto deposit margin
        /// </summary>
        /// <param name="symbol">Symbol to change for</param>
        /// <param name="enabled">Enable or disable</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        public async Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("status", enabled.ToString());
            return await Execute(GetUri("position/margin/auto-deposit-status"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Add margin
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="clientId">A unique ID generated by the user, to ensure the operation is processed by the system only once</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get funding history
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get open contract list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<KucoinContract>>> GetOpenContractsAsync(CancellationToken ct = default)
        {
            return await Execute<IEnumerable<KucoinContract>>(GetUri("contracts/active"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinContract>> GetContractAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinContract>(GetUri("contracts/" + symbol), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Ticker

        /// <summary>
        /// Get the ticker for a contract
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinFuturesTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinFuturesTick>(GetUri("ticker"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Order book

        /// <summary>
        /// Get the full order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedFullOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderBook>(GetUri("level2/snapshot"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the partial order book, aggregated by price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="depth">Amount of rows in the book, either 20 or 100</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinOrderBook>> GetAggregatedPartialOrderBookAsync(string symbol, int depth, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 20, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<KucoinOrderBook>(GetUri("level2/depth" + depth), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Trade history

        /// <summary>
        /// Get the most recent trades
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<KucoinFuturesTrade>>> GetTradeHistoryAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await Execute<IEnumerable<KucoinFuturesTrade>>(GetUri("trade/history"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Index

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

        /// <summary>
        /// Get the current mark price
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinMarkPrice>> GetCurrentMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinMarkPrice>(GetUri($"mark-price/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

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

        /// <summary>
        /// Get the current funding rate
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            return await Execute<KucoinFundingRate>(GetUri($"funding-rate/{symbol}/current"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Time

        /// <summary>
        /// Get the server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await Execute<long>(GetUri("timestamp"), HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result ? new DateTime(1970, 1, 1).AddMilliseconds(result.Data): default);
        }

        #endregion

        #region Server status

        /// <summary>
        /// Get the service status
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<KucoinFuturesServiceStatus>> GetServiceStatusAsync(CancellationToken ct = default)
        {
            return await Execute<KucoinFuturesServiceStatus>(GetUri("status"), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Klines

        /// <summary>
        /// Get kline data
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Start time to retrieve klines from</param>
        /// <param name="endTime">End time to retrieve klines for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
    }
}
