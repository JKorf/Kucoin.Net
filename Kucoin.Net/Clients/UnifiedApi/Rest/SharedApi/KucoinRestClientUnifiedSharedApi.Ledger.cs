using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Enums;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kucoin.Net.Clients.UnifiedApi
{
    internal partial class KucoinRestClientUnifiedSharedApi
    {
        #region Get Ledger

        async Task<IExchangeCallResult<SharedLedgerEntry[]>> IGetLedger.GetLedgerAsync(GetLedgerRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetLedgerAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetLedgerOptions GetLedgerOptions { get; } = new GetLedgerOptions(_exchangeName, false, true, true, 100)
        {
            MaxAge = TimeSpan.FromDays(7)
        };
        public async Task<HttpResult<SharedLedgerEntry[]>> GetLedgerAsync(GetLedgerRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetLedgerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLedgerEntry[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetAccountLedgerAsync(
                UnifiedAccountType.Unified,
                asset: request.Asset == null ? null : [request.Asset],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                lastId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLedgerEntry[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => result.Data.LastId == null ? null : Pagination.NextPageFromId(result.Data.LastId.Value),
                     result.Data.Items.Length,
                     result.Data.Items.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams,
                     TimeSpan.FromDays(30));

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Items, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedLedgerEntry(
                            x.Asset,
                            (x.Direction == AccountDirection.Out && x.Quantity > 0) ? -x.Quantity : x.Quantity,
                            ParseLedgerType(x.BusinessType),
                            EnumConverter.GetString(x.BusinessType),
                            x.Timestamp)
                        {
                        }).ToArray(), nextPageRequest);
        }

        private SharedLedgerEntryType ParseLedgerType(UnifiedBusinessType businessType)
        {
            return businessType switch
            {
                UnifiedBusinessType.TradeExchange => SharedLedgerEntryType.Trade,
                UnifiedBusinessType.Transfer => SharedLedgerEntryType.Transfer,
                UnifiedBusinessType.SubTransfer => SharedLedgerEntryType.Transfer,
                UnifiedBusinessType.SubToSubTransfer => SharedLedgerEntryType.Transfer,
                UnifiedBusinessType.SpotExchange => SharedLedgerEntryType.Trade,
                UnifiedBusinessType.SpotExchangeRebate => SharedLedgerEntryType.Rebate,
                UnifiedBusinessType.FuturesExchangeOpen => SharedLedgerEntryType.Trade,
                UnifiedBusinessType.FuturesExchangeClose => SharedLedgerEntryType.Trade,
                UnifiedBusinessType.FuturesExchangeRebate => SharedLedgerEntryType.Rebate,
                UnifiedBusinessType.FundingFee => SharedLedgerEntryType.FundingFee,
                _ => SharedLedgerEntryType.Unknown,
            };
        }

        #endregion

    }
}
