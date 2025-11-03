using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Enums;
using Kucoin.Net.Objects.Models;
using Kucoin.Net.Objects.Models.Futures;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Kucoin Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IKucoinRestClientFuturesApiAccount
    {
        /// <summary>
        /// Gets account overview
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-futures" /></para>
        /// </summary>
        /// <param name="asset">Get the accounts for a specific asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-ledgers-futures" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `USDT`</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the total value of active orders
        /// <para><a href="https://www.kucoin.com/docs-new/est/futures-trading/orders/get-open-order-value" /></para>
        /// </summary>        
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get details on a position
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-position-details" /></para>
        /// </summary>
        /// <param name="symbol">Contract symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition[]>> GetPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get list of positions
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-position-list" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition[]>> GetPositionsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get position closure history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-positions-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="page">Page number</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinPositionHistoryItem>>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Enable/disable auto deposit margin
        /// <para><a href="https://www.kucoin.com/docs/rest/futures-trading/positions/modify-auto-deposit-margin-status" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to change for, for example `XBTUSDM`</param>
        /// <param name="enabled">Enable or disable</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Manually add margin to a position
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/add-isolated-margin" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="clientId">A unique ID generated by the user, to ensure the operation is processed by the system only once</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default);

        /// <summary>
        /// Manually remove margin from a position
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/remove-isolated-margin" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="quantity">Quantity to remove</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RemoveMarginAsync(string symbol, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/funding-fees/get-private-funding-history" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="offset">Result offset</param>
        /// <param name="pageSize">Size of a page</param>
        /// <param name="forward">Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk limit level
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-isolated-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Objects.Models.Futures.KucoinFuturesRiskLimit[]>> GetRiskLimitLevelAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set risk limit level
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/modify-isolated-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `XBTUSDM`</param>
        /// <param name="level">Risk limit level</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<bool>> SetRiskLimitLevelAsync(string symbol, int level, CancellationToken ct = default);

        /// <summary>
        /// Get the maximum amount of margin that the current position supports withdrawal.
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-max-withdraw-margin" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<decimal>> GetMaxWithdrawMarginAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trading fee for a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/account-info/trade-fee/get-actual-fee-futures" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTradeFee>> GetTradingFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the current margin mode for a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-margin-mode" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginMode>> GetMarginModeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set the margin mode for a symbol
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/switch-margin-mode" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="marginMode">The new margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginMode>> SetMarginModeAsync(string symbol, FuturesMarginMode marginMode, CancellationToken ct = default);

        /// <summary>
        /// Set the margin mode for multiple symbols
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/batch-switch-margin-mode" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `XBTUSDTM`</param>
        /// <param name="marginMode">The new margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginModes>> SetMarginModesAsync(IEnumerable<string> symbols, FuturesMarginMode marginMode, CancellationToken ct = default);

        /// <summary>
        /// Get the current cross margin leverage setting
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-cross-margin-leverage" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinLeverage>> GetCrossMarginLeverageAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set a new cross margin leverage value
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/modify-cross-margin-leverage" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="leverage">The leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinLeverage>> SetCrossMarginLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin risk limits
        /// <para><a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-cross-margin-risk-limit" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="totalMargin">The position opening amount, in the contract's settlement asset. Defaults to 10,000 </param>
        /// <param name="leverage">Calculates the max position size at the specified leverage. Defaults to the symbol’s max cross leverage.</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinCrossMarginRiskLimit[]>> GetCrossMarginRiskLimitAsync(string symbol, decimal? totalMargin = null, int? leverage = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin requirement
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="positionValue">Position value</param>
        /// <param name="leverage">Leverage to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCrossMarginRequirement>> GetCrossMarginRequirementAsync(string symbol, decimal positionValue, int? leverage = null, CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para><a href="https://www.kucoin.com/docs-new/3475097e0" /></para>
        /// </summary>
        /// <param name="positionMode">New position mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

        /// <summary>
        /// Get current position mode
        /// <para><a href="https://www.kucoin.com/docs-new/3475216e0" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinPositionMode>> GetPositionModeAsync(CancellationToken ct = default);
    }
}
