---
title: IKucoinRestClientSpotApiAccount
has_children: false
parent: IKucoinRestClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`KucoinRestClient > SpotApi > Account`  
*Kucoin Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## CancelWithdrawalAsync  

[https://docs.kucoin.com/#cancel-withdrawal](https://docs.kucoin.com/#cancel-withdrawal)  
<p>

*Cancel a withdrawal*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.CancelWithdrawalAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|withdrawalId|The id of the withdrawal to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateDepositAddressAsync  

[https://docs.kucoin.com/#create-deposit-address](https://docs.kucoin.com/#create-deposit-address)  
<p>

*Creates a new deposit address for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.CreateDepositAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinDepositAddress>> CreateDepositAddressAsync(string asset, string? network = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset create the address for|
|_[Optional]_ network|The network to create the address for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountAsync  

[https://docs.kucoin.com/#get-an-account](https://docs.kucoin.com/#get-an-account)  
<p>

*Get a specific account*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinAccountSingle>> GetAccountAsync(string accountId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The id of the account to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountLedgerAsync  

[https://docs.kucoin.com/#get-account-ledgers-deprecated](https://docs.kucoin.com/#get-account-ledgers-deprecated)  
<p>

*Gets a list of account activity*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetAccountLedgerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgerAsync(string accountId, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The account id to get the activities for|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountLedgersAsync  

[https://docs.kucoin.com/#get-account-ledgers](https://docs.kucoin.com/#get-account-ledgers)  
<p>

*Gets a list of account activity*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetAccountLedgersAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinAccountActivity>>> GetAccountLedgersAsync(string? asset = default, AccountDirection? direction = default, BizType? bizType = default, DateTime? startTime = default, DateTime? endTime = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|The asset to retrieve activity or null|
|_[Optional]_ direction|Side|
|_[Optional]_ bizType|Business type|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountsAsync  

[https://docs.kucoin.com/#list-accounts](https://docs.kucoin.com/#list-accounts)  
<p>

*Gets a list of accounts*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetAccountsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinAccount>>> GetAccountsAsync(string? asset = default, AccountType? accountType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Get the accounts for a specific asset|
|_[Optional]_ accountType|Filter on type of account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBasicUserFeeAsync  

[https://docs.kucoin.com/#basic-user-fee](https://docs.kucoin.com/#basic-user-fee)  
<p>

*Get the basic user fees*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetBasicUserFeeAsync();  
```  

```csharp  
Task<WebCallResult<KucoinUserFee>> GetBasicUserFeeAsync(AssetType? assetType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ assetType|The type of asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositAddressAsync  

[https://docs.kucoin.com/#get-deposit-address](https://docs.kucoin.com/#get-deposit-address)  
<p>

*Gets the deposit address for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetDepositAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinDepositAddress>> GetDepositAddressAsync(string asset, string? network = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to get the address for|
|_[Optional]_ network|The network to get the address for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositAddressesAsync  

[https://docs.kucoin.com/#get-deposit-addresses-v2](https://docs.kucoin.com/#get-deposit-addresses-v2)  
<p>

*Gets the deposit addresses for an asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetDepositAddressesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to get the address for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositsAsync  

[https://docs.kucoin.com/#get-deposit-list](https://docs.kucoin.com/#get-deposit-list)  
<p>

*Gets a list of deposits*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetDepositsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinDeposit>>> GetDepositsAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, DepositStatus? status = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter list by asset|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ status|Filter list by deposit status|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalDepositsAsync  

[https://docs.kucoin.com/#get-v1-historical-deposits-list](https://docs.kucoin.com/#get-v1-historical-deposits-list)  
<p>

*Gets a list of historical deposits*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetHistoricalDepositsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinHistoricalDeposit>>> GetHistoricalDepositsAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, DepositStatus? status = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter list by asset|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ status|Filter list by deposit status|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalWithdrawalsAsync  

[https://docs.kucoin.com/#get-v1-historical-withdrawals-list](https://docs.kucoin.com/#get-v1-historical-withdrawals-list)  
<p>

*Gets a list of historical withdrawals*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetHistoricalWithdrawalsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinHistoricalWithdrawal>>> GetHistoricalWithdrawalsAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, WithdrawalStatus? status = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter list by asset|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ status|Filter list by deposit status|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAccountAsync  

[https://docs.kucoin.com/#query-single-isolated-margin-account-info](https://docs.kucoin.com/#query-single-isolated-margin-account-info)  
<p>

*Get isolated margin account info*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAccountsAsync  

[https://docs.kucoin.com/#query-isolated-margin-account-info](https://docs.kucoin.com/#query-isolated-margin-account-info)  
<p>

*Get isolated margin account info*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginAccountsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinIsolatedMarginAccountsInfo>> GetIsolatedMarginAccountsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginAccountAsync  

[https://docs.kucoin.com/#get-margin-account](https://docs.kucoin.com/#get-margin-account)  
<p>

*Get margin account info*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetMarginAccountAsync();  
```  

```csharp  
Task<WebCallResult<KucoinMarginAccount>> GetMarginAccountAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRiskLimitCrossMarginAsync  

[https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit](https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit)  
<p>

*Get cross margin risk limit*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetRiskLimitCrossMarginAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinRiskLimitCrossMargin>>> GetRiskLimitCrossMarginAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRiskLimitIsolatedMarginAsync  

[https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit](https://docs.kucoin.com/#query-the-cross-isolated-margin-risk-limit)  
<p>

*Get isolated margin risk limit*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetRiskLimitIsolatedMarginAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinRiskLimitIsolatedMargin>>> GetRiskLimitIsolatedMarginAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolTradingFeesAsync  

[https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair](https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair)  
<p>

*Get the trading fees for symbols*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetSymbolTradingFeesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to retrieve fees for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolTradingFeesAsync  

[https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair](https://docs.kucoin.com/#actual-fee-rate-of-the-trading-pair)  
<p>

*Get the trading fees for symbols*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetSymbolTradingFeesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinTradeFee>>> GetSymbolTradingFeesAsync(IEnumerable<string> symbols, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to retrieve fees for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTransferableAsync  

[https://docs.kucoin.com/#get-the-transferable](https://docs.kucoin.com/#get-the-transferable)  
<p>

*Gets a transferable balance of a specified account.*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetTransferableAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinTransferableAccount>> GetTransferableAsync(string asset, AccountType accountType, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Get the accounts for a specific asset|
|accountType|Filter on type of account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserInfoAsync  

[https://docs.kucoin.com/#get-user-info-of-all-sub-accounts](https://docs.kucoin.com/#get-user-info-of-all-sub-accounts)  
<p>

*Gets a list of sub users*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetUserInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<KucoinSubUser>>> GetUserInfoAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawalQuotasAsync  

[https://docs.kucoin.com/#get-withdrawal-quotas](https://docs.kucoin.com/#get-withdrawal-quotas)  
<p>

*Get the withdrawal quota for a asset*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetWithdrawalQuotasAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinWithdrawalQuota>> GetWithdrawalQuotasAsync(string asset, string? network = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to get the quota for|
|_[Optional]_ network|The network name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20. This only apply for multi-chain currency, and there is no need for single chain currency.|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawalsAsync  

[https://docs.kucoin.com/#get-withdrawals-list](https://docs.kucoin.com/#get-withdrawals-list)  
<p>

*Gets a list of withdrawals*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.GetWithdrawalsAsync();  
```  

```csharp  
Task<WebCallResult<KucoinPaginated<KucoinWithdrawal>>> GetWithdrawalsAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, WithdrawalStatus? status = default, int? currentPage = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter list by asset|
|_[Optional]_ startTime|Filter list by start time|
|_[Optional]_ endTime|Filter list by end time|
|_[Optional]_ status|Filter list by deposit status|
|_[Optional]_ currentPage|The page to retrieve|
|_[Optional]_ pageSize|The amount of results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## InnerTransferAsync  

[https://docs.kucoin.com/#transfer-between-master-user-and-sub-user](https://docs.kucoin.com/#transfer-between-master-user-and-sub-user)  
<p>

*Transfers assets between the accounts of a user.*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.InnerTransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinInnerTransfer>> InnerTransferAsync(string asset, AccountType from, AccountType to, decimal quantity, string? fromTag = default, string? toTag = default, string? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Get the accounts for a specific asset|
|from|The type of the account|
|to|The type of the account|
|quantity|The quantity to transfer|
|_[Optional]_ fromTag|Trading pair, required when the payment account type is isolated, e.g.: BTC-USDT|
|_[Optional]_ toTag|Trading pair, required when the receiving account type is isolated, e.g.: BTC-USDT|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## WithdrawAsync  

[https://docs.kucoin.com/#apply-withdraw-2](https://docs.kucoin.com/#apply-withdraw-2)  
<p>

*Withdraw an asset to an address*  

```csharp  
var client = new KucoinRestClient();  
var result = await client.SpotApi.Account.WithdrawAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<KucoinNewWithdrawal>> WithdrawAsync(string asset, string toAddress, decimal quantity, string? memo = default, bool isInner, string? remark = default, string? chain = default, FeeDeductType? feeDeductType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to withdraw|
|toAddress|The address to withdraw to|
|quantity|The quantity to withdraw|
|_[Optional]_ memo|The note that is left on the withdrawal address. When you withdraw from KuCoin to other platforms, you need to fill in memo(tag). If you don't fill in memo(tag), your withdrawal may not be available.|
|isInner|Internal withdrawal or not. Default false.|
|_[Optional]_ remark|Remark for the withdrawal|
|_[Optional]_ chain|The chain name of asset, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is OMNI. This only apply for multi-chain currency, and there is no need for single chain currency.|
|_[Optional]_ feeDeductType|Fee deduction type|
|_[Optional]_ ct|Cancellation token|

</p>
