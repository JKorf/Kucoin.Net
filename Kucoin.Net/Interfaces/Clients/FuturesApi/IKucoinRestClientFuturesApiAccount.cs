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
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-futures" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/account-overview
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Get the accounts for a specific asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of accounts</returns>
        Task<WebCallResult<KucoinAccountOverview>> GetAccountOverviewAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/account-info/account-funding/get-account-ledgers-futures" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/transaction-history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `USDT`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="pageSize">["<c>maxCount</c>"] Size of a page</param>
        /// <param name="forward">["<c>forward</c>"] Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinAccountTransaction>>> GetTransactionHistoryAsync(string? asset = null, TransactionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get the total value of active orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/est/futures-trading/orders/get-open-order-value" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/openOrderStatistics
        /// </para>
        /// </summary>        
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinOrderValuation>> GetOpenOrderValueAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get details on a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-position-details" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Contract symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition[]>> GetPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get list of positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-position-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/positions
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<KucoinPosition[]>> GetPositionsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get position closure history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-positions-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/history-positions
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>from</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>to</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>pageId</c>"] Page number</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginated<KucoinPositionHistoryItem>>> GetPositionHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Enable/disable auto deposit margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs/rest/futures-trading/positions/modify-auto-deposit-margin-status" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/position/margin/auto-deposit-status
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to change for, for example `XBTUSDM`</param>
        /// <param name="enabled">["<c>status</c>"] Enable or disable</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult> ToggleAutoDepositMarginAsync(string symbol, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Manually add margin to a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/add-isolated-margin" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/position/margin/deposit-margin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="quantity">["<c>margin</c>"] Quantity to add</param>
        /// <param name="clientId">["<c>bizNo</c>"] A unique ID generated by the user, to ensure the operation is processed by the system only once</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AddMarginAsync(string symbol, decimal quantity, string? clientId = null, CancellationToken ct = default);

        /// <summary>
        /// Manually remove margin from a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/remove-isolated-margin" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/margin/withdrawMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="quantity">["<c>withdrawAmount</c>"] Quantity to remove</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> RemoveMarginAsync(string symbol, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get funding history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/funding-fees/get-private-funding-history" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/funding-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="startTime">["<c>startAt</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endAt</c>"] Filter by end time</param>
        /// <param name="offset">["<c>offset</c>"] Result offset</param>
        /// <param name="pageSize">["<c>maxCount</c>"] Size of a page</param>
        /// <param name="forward">["<c>forward</c>"] Forward or backwards direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinPaginatedSlider<KucoinFundingItem>>> GetFundingHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? pageSize = null, bool? forward = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk limit level
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-isolated-margin-risk-limit" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/contracts/risk-limit/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<Objects.Models.Futures.KucoinFuturesRiskLimit[]>> GetRiskLimitLevelAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set risk limit level
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/modify-isolated-margin-risk-limit" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/position/risk-limit-level/change
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `XBTUSDM`</param>
        /// <param name="level">["<c>level</c>"] Risk limit level</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<bool>> SetRiskLimitLevelAsync(string symbol, int level, CancellationToken ct = default);

        /// <summary>
        /// Get the maximum amount of margin that the current position supports withdrawal.
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-max-withdraw-margin" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/margin/maxWithdrawMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<decimal>> GetMaxWithdrawMarginAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trading fee for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/account-info/trade-fee/get-actual-fee-futures" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/trade-fees
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDM`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinTradeFee>> GetTradingFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the current margin mode for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-margin-mode" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/position/getMarginMode
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginMode>> GetMarginModeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set the margin mode for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/switch-margin-mode" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/position/changeMarginMode
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDTM`</param>
        /// <param name="marginMode">["<c>marginMode</c>"] The new margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginMode>> SetMarginModeAsync(string symbol, FuturesMarginMode marginMode, CancellationToken ct = default);

        /// <summary>
        /// Set the margin mode for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/batch-switch-margin-mode" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/position/batchChangeMarginMode
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] The symbols, for example `XBTUSDTM`</param>
        /// <param name="marginMode">["<c>marginMode</c>"] The new margin mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinMarginModes>> SetMarginModesAsync(IEnumerable<string> symbols, FuturesMarginMode marginMode, CancellationToken ct = default);

        /// <summary>
        /// Get the current cross margin leverage setting
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-cross-margin-leverage" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/getCrossUserLeverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDTM`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinLeverage>> GetCrossMarginLeverageAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Set a new cross margin leverage value
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/modify-cross-margin-leverage" /><br />
        /// Endpoint:<br />
        /// POST /api/v2/changeCrossUserLeverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDTM`</param>
        /// <param name="leverage">["<c>leverage</c>"] The leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetCrossMarginLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin risk limits
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.kucoin.com/docs-new/rest/futures-trading/positions/get-cross-margin-risk-limit" /><br />
        /// Endpoint:<br />
        /// GET /api/v2/batchGetCrossOrderLimit
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `XBTUSDTM`</param>
        /// <param name="totalMargin">The position opening amount, in the contract's settlement asset. Defaults to 10,000 </param>
        /// <param name="leverage">Calculates the max position size at the specified leverage. Defaults to the symbol’s max cross leverage.</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<KucoinCrossMarginRiskLimit[]>> GetCrossMarginRiskLimitAsync(string symbol, decimal? totalMargin = null, int? leverage = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin requirement
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `XBTUSDTM`</param>
        /// <param name="positionValue">["<c>positionValue</c>"] Position value</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<KucoinCrossMarginRequirement>> GetCrossMarginRequirementAsync(string symbol, decimal positionValue, int? leverage = null, CancellationToken ct = default);

        /// <summary>
        /// Set position mode
        /// <para><a href="https://www.kucoin.com/docs-new/3475097e0" /></para>
        /// </summary>
        /// <param name="positionMode">["<c>positionMode</c>"] New position mode</param>
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
