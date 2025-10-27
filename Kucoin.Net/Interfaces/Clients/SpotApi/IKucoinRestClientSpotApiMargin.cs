using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Margin borrow and repay endpoints
    /// </summary>
    public interface IKucoinRestClientSpotApiMargin
    {
        /// <summary>
        /// Get margin configuration
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-margin-config" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinMarginConfig>> GetMarginConfigurationAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the mark price of a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-mark-price-detail" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to retrieve, for example `USDT-BTC`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIndexBase>> GetMarginMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the mark price for all symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-mark-price-list" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinIndexBase[]>> GetMarginMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get Margin symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-symbols-cross-margin" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param> 
        /// <returns></returns>
        Task<WebCallResult<KucoinTradingPairConfiguration[]>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get cross margin risk limit and asset configuration info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/risk-limit/get-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param> 
        /// <returns></returns>
        Task<WebCallResult<KucoinCrossRiskLimitConfig[]>> GetCrossMarginRiskLimitAndConfig(CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin risk limit and asset configuration info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/risk-limit/get-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param> 
        /// <returns></returns>
        Task<WebCallResult<KucoinIsolatedRiskLimitConfig[]>> GetIsolatedMarginRiskLimitAndConfig(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Borrow an asset
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/borrow" /></para>
        /// </summary>
        /// <param name="asset">Currency to Borrow e.g USDT etc</param>
        /// <param name="timeInForce">Time in force (FOK, IOC)</param>
        /// <param name="quantity">Total size</param>
        /// <param name="isIsolated">If isolated margin</param>
        /// <param name="symbol">Isolated margin symbol</param>
        /// <param name="isHf">HighFrequency/ProAccount borrowing</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The id of the new order</returns>
        Task<WebCallResult<KucoinNewBorrowOrder>> BorrowAsync(
            string asset,
            BorrowOrderType timeInForce,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default);

        /// <summary>
        /// Repayment for previously borrowed asset
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/repay" /></para>
        /// </summary>
        /// <param name="asset">Currency to Repay e.g USDT etc</param>
        /// <param name="quantity">Total size</param>
        /// <param name="isIsolated">If isolated margin</param>
        /// <param name="symbol">Isolated margin symbol</param>
        /// <param name="isHf">HighFrequency/ProAccount repayment</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinNewBorrowOrder>> RepayAsync(
            string asset,
            decimal quantity,
            bool? isIsolated = null,
            string? symbol = null,
            bool? isHf = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get borrow history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/get-borrow-history" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="isIsolated">Filter by is isolated margin</param>
        /// <param name="symbol">Filter by isolated margin symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Filter by borrow order number</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetBorrowHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/get-repay-history" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="isIsolated">Filter by is isolated margin</param>
        /// <param name="symbol">Filter by isolated margin symbol, for example `ETH-USDT`</param>
        /// <param name="orderId">Filter by repay order number</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinBorrowOrderV3>>> GetRepayHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, string? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get margin interest records
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/get-interest-history" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="isIsolated">Filter by is isolated margin</param>
        /// <param name="symbol">Filter by isolated margin symbol, for example `ETH-USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to retrieve</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinMarginInterest>>> GetInterestHistoryAsync(string asset, bool? isIsolated = null, string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending asset info
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/get-loan-market" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinLendingAsset[]>> GetLendingAssetsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending interest rates
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/get-loan-market-interest-rate" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinLendingInterest[]>> GetInterestRatesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Initiate subscriptions of margin lending
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/purchase" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="interestRate">Interest rate</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinLendingResult>> SubscribeAsync(string asset, decimal quantity, decimal interestRate, CancellationToken ct = default);

        /// <summary>
        /// Initiate redemptions of margin lending.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/redeem" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="subscribeOrderId">Subscribe order number</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinLendingResult>> RedeemAsync(string asset, decimal quantity, string subscribeOrderId, CancellationToken ct = default);

        /// <summary>
        /// Update interest rate of a subscription order
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/modify-purchase" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="interestRate">New interest rate</param>
        /// <param name="subscribeOrderId">Subscribe order number</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> EditSubscriptionOrderAsync(string asset, decimal interestRate, string subscribeOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get redemption orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/get-redeem-orders" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="redeemOrderId">Filter by redeem order id</param>
        /// <param name="status">Status, DONE or PENDING</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinRedemption>>> GetRedemptionOrdersAsync(string asset, string status, string? redeemOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get subscription orders
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/credit/get-purchase-orders" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="status">Status, DONE or PENDING</param>
        /// <param name="purchaseOrderId">Filter by purchase order id</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinLendSubscription>>> GetSubscriptionOrdersAsync(string asset, string status, string? purchaseOrderId = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Modify the leverage multiplier for cross margin or isolated margin.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/debit/modify-leverage" /></para>
        /// </summary>
        /// <param name="leverage">New leverage multiplier. Must be greater than 1 and up to two decimal places, and cannot be less than the user's current debt leverage or greater than the system's maximum leverage</param>
        /// <param name="symbol">Symbol. Leave empty for cross margin, or specify for isolated margin, for example `ETH-USDT`</param>
        /// <param name="isolatedMargin">Is isolated margin</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> SetLeverageMultiplierAsync(decimal leverage, string? symbol = null, bool? isolatedMargin = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/margin-trading/market-data/get-symbols-cross-margin" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH-USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCrossMarginSymbol[]>> GetCrossMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow interest rates
        /// <para><a href="https://www.kucoin.com/docs-new/3474772e0" /></para>
        /// </summary>
        /// <param name="asset">Asset filter, comma separated up to 50</param>
        /// <param name="vipLevel">VIP level</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinBorrowInterestRates>> GetBorrowInterestRateAsync(string? asset = null, int? vipLevel = null, CancellationToken ct = default);

        /// <summary>
        /// Get collateral ratios
        /// <para><a href="https://www.kucoin.com/docs-new/3475578e0" /></para>
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCollateralRatios[]>> GetMarginCollateralRatioAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);
    }
}
