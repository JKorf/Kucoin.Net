# KuCoin operational considerations

## Credentials and account selection

Use key, secret, and API passphrase from secret storage. API permissions, regional environment, and classic versus Unified account eligibility must match the workflow. Trading permission does not imply withdrawal, subaccount administration, or margin borrowing permission. Configure REST and socket credentials independently when their roles differ.

## Order execution and results

`PlaceTestOrderAsync` on classic Spot/Futures validates order syntax without placing an actual order. Test placement does not guarantee subsequent live acceptance or reserve funds. Regular Spot and HF methods have distinct account/order workflows; use the requested one. Unified order placement, including socket requests, changes live account state; do not assume a test switch exists.

A limit order can execute immediately even if its price is below a previously observed price. Cancellation races with fills; inspect status and trades for remaining exposure. Keep client order identifiers for correlation and use the matching query/cancel-by-client-id method. Bulk results may contain per-item failures despite outer success; Unified batch cancellation has its own result model. Inspect item outcomes instead of retrying the entire batch.

## Futures modes, sizing, and closes

Classic Futures contract quantity is an integer count. Use contract multiplier/lot metadata; reject unsupported fractional contract quantities instead of truncating them. Base/quote sizing parameters have distinct semantics. Shared Futures quantities are in contracts; Unified native requests use the supplied quantity unit.

Read current position mode, margin mode, and positions before deriving closing orders. For one-way mode select the opposite side from signed exposure and use `reduceOnly`; hedge-mode orders require the matching long/short position side and exchange-specific close semantics. Do not apply a one-way close example to both modes without checking. Position, margin, risk-limit, cross-margin leverage, and auto-deposit changes affect account risk and can be constrained by existing positions/orders. An accepted closing order is not proof the position has closed.

## Unified, margin, and asset movements

Unified account-mode changes, leverage, transfers, and withdrawals change live account behavior. Some classic-mode parameters only apply to classic accounts even on Unified endpoints; follow the specific method's prerequisites. HF `CancelAfterAsync` has a product/symbol scope and timeout lifetime; confirm scope before using it as protection.

Margin borrowing, repayment, lending subscription/redemption, and Earn purchase/redemption affect balances or liabilities. For transfers specify source/destination account and asset. For withdrawals confirm asset, network, address, memo/tag, internal/external withdrawal type, quantity, and fee treatment. Status/quotas/address discovery calls do not authorize a withdrawal or other funds movement.

## Uncertain submissions

A timeout or connection loss can leave an order, transfer, withdrawal, borrowing request, or mode change accepted but unconfirmed. `Error.IsTransient` alone does not justify repeating it. Reconcile client/order ids, trades, ledger, balances, withdrawal history, or current mode before deciding whether to resubmit; allow for propagation delays. Retain per-item outcomes for partial batches. Bounded backoff is suitable for transient public reads; keep generic read-retry helpers away from account-changing calls.
