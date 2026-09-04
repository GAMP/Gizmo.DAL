# Schema Backlog

Outstanding database changes that have been designed or agreed but not yet built, plus the
decisions that were deliberately settled *against* a schema change so they are not raised again.

**Verified against this repository on 2026-09-04.** Item status ages; re-verify before acting:

```bash
git rev-list --left-right --count @{u}...HEAD      # "behind<TAB>ahead" — ahead 0 = nothing unpushed
grep -c 'migrationBuilder.CreateTable' Gizmo.DAL/Migrations/Gizmo.DAL.Migrations.MSSQL/*_Update2.cs
```

Never assert commit or push state from notes — run the check.

## Migration State

| Migration | MSSQL | Npgsql | Contents |
|---|---|---|---|
| `EFCore_Initial` | 20260216074339 | 20260216074229 | Baseline. |
| `Update1` | 20260630134254 | 20260630134301 | 18 performance indexes. Also carries the pre-existing `Register`→`Branch` FK `Cascade`→`Restrict` drift, intentionally. |
| `Update2` | 20260723100155 | 20260723101222 | 37 `CreateTable` — the achievement family (incl. both TPT hierarchies) and `VerificationMethod`. 3 `AddColumn` — `UserMember.IsTierExempt`, `Register.DefaultOperatorId`, `Register.ReceiptPrinterNumber`. No `InsertData`. |
| `Update3` | 20260825145608 | 20260825145544 | `PaymentIntent.RegisterId` / `.ShiftId` / `.QrDisplayNumber`, 2 indexes, 2 FKs (`NO ACTION`). |

`Update1` is understood to be the last publicly released migration, which would leave `Update2`
and `Update3` free to squash before release. **That has not been verified** — confirm before
relying on it, because it decides whether new columns fold into a regenerated `Update2` or land
in a new `Update4` (see [Batching](#batching)).

---

## Open — Columns

Each is a small additive column whose design is settled.

### `AchievementLadderLevel.Name`

```
+ Name nvarchar(45) NULL          -- SQLStringSize.TINY45, IsRequired(false), column order 7
resolution everywhere:  level.Name ?? level.UserGroup.Name
```

Optional per-level display name overriding the user group's. The group is an operational object;
the level is customer-facing. Reverses an earlier deferral that reasoned the level name should
come *from* `UserGroup.Name`.

The standing wire needs **no** change — `LadderStandingLevelModel.Name` already exists at
MessagePack key 1; only its doc comment ("the name of the user group the level maps to") becomes
wrong. The name is sourced in exactly one place, the projection in
`UserAchievementsService.GetStandingAsync`, currently a bare `ladderLevel.UserGroup.Name`.

The config wire does change: `AchievementLadderLevelModel` holds keys 0–4, so `Name` appends at
key 5 (non-breaking), in both `Gizmo.Web.Api.Models` copies. Plus the ladder service map/apply and
a Name input on the Manager ladder editor, placeholder = group name.

No uniqueness constraint — two levels sharing a display name is the operator's business, and the
existing `(LadderId, UserGroupId)` unique already blocks the case that matters.

### `AchievementLadder.ImageId`

```
+ ImageId int NULL  ->  FileImage (SET NULL)
```

The per-ladder badge **template** (the convention SVG that renders every level and switches on
rank). Distinct from the per-level **emblem**, which is the existing
`AchievementLadderLevel.ImageId`. Structure vs identity — not two mechanisms for one fact.

Blocks the ladder badge: `UserAchievementsService` currently hardcodes `TemplateGuid = null` with
a TODO pointing here. Read models already carry file GUIDs, so the consuming side is ready.

### `AchievementChallengeCompletionTimeReward.InvoiceId`

```
+ InvoiceId int NULL  ->  Invoice (RESTRICT)
```

Points snapshots reference their `PointTransaction` and product snapshots their `Invoice`; time
snapshots reference nothing. A delivered time prize leaves no ledger trail and cannot be
de-duplicated — which also makes the delivery/stamp atomicity gap undetectable for time, where
product at least leaves an invoice id in the log.

### Ladder option flags

```
AchievementLadderLevel + Options int   [Flags] AchievementLadderLevelOptionType { None, InheritRequirements = 1 }
AchievementLadder      + Options int   [Flags] AchievementLadderOptionType      { None, StepwisePromotion  = 1 }
```

For requirement inheritance (level N's effective set = own rows ∪ the chain below, while each
level carries the flag; merge overlapping achievements by `max(RequiredCount)`, flattened at
config-cache rebuild) and stepwise promotion (candidate rank clamped to
`min(highestSatisfied, periodStartRank + 1)`; promotion only, demotion uncapped).

Verify whether `AchievementLadder.Options` already exists before scaffolding — `IsStepwise` is
already on the standing model, hardcoded false. Stepwise needs no state column: the
already-promoted-this-period early-out reads `AchievementLadderEvent`. **Doc-comment that on the
event entity** — it promotes the history table to evaluator control state, so the "keep forever"
retention decision can no longer change without breaking the early-out.

### Reversal reference on the refund base

```
RefundInvoicePayment / RefundDepositPayment (common base)
+ reversal reference nvarchar NULL
```

A reversal produces a *new* reference — terminal reversal RRN, or a Stripe `re_…` refund id — that
must not overwrite `PaymentIntent.TransactionId`, which holds the original and is needed by cancel
and audit. This is the slot `PaymentReversalResult.RefundId` should fill; today it is discarded, so
a completed PSP refund records no provider reference and cannot be reconciled.

Every rail populates this one field. Writing the reference here (rather than anywhere
terminal-specific) is also what keeps a later sync→async reversal lifecycle purely additive.

### `InventoryTransferEntry` cost and expiry

```
+ UnitCost decimal NULL, TotalCost decimal NULL, ExpirationDate NULL
```

Nullable by decision, so pre-existing rows read as unknown rather than a fabricated zero.

Stamping at transfer creation removes manual inbound's estimator call, its `Product.Cost` fallback
and the `excludedQuantities` parameter entirely. It also fixes a latent collision for free:
`transferUnitCosts` / `transferExpirationDates` are keyed by **product id**, so two entries of the
same product in one transfer overwrite each other today. The FIFO arithmetic does not change, only
which side calls it.

### Fiscal identifiers for a second country

```
FiscalReceipt + MARK, UID, auth code, QR URL, series/number (+ device serial, Z number)
FiscalPrinterReceiptResult widened: DocumentNumber, ExternalId, Uid, QrUrl
```

The row carries only `Signature` and `TaxSystem`, and `DocumentNumber` is never populated. Greek
myDATA returns a MARK and QR per document, so all of it must persist.

Related and also schema-shaped: on the certified-provider route the server must assign a gapless
per-register series number **before** first transmission, stable across poller retries. No such
counter exists on `Invoice`, and the `FiscalReceipt` row is only created after a successful print.

### Emailer integration seed row

The emailer-as-integration split needs an `Integration` row seeded when `SMTPOptions.IsEnabled`.
The agreed home was the communication-methods migration, but `Update2` contains no `InsertData`
anywhere, so it was missed. Deliberately does **not** wrap the legacy SMS gateway.

---

## Open — New Schema

### Preparation stations (kitchen)

```
Station         global, named, soft-delete only (order lines keep the stamp forever)
KitchenBinding  branch + station SET + display options; Guid Uid on the wire, int Id internal
                station -> companion/printer binding per branch, with a delivery mode
ProductOL     + StationId int NULL   + index on StationId
IPreparable on ProductOL and ProductOrder: PrepareStatus, PreparedQuantity, PrepareTime
```

The `StationId` stamp at order acceptance is the load-bearing commitment: a snapshot, never
re-derived on read, so a config change mid-shift cannot re-route in-flight tickets. It is also the
only new persistence the printing half of the feature needs.

Order lines have no `BranchId` and must not gain one — branch filtering joins line→order, one
indexed FK hop. Prep status is a track separate from `IDeliverable`, not a new state machine. The
binding is the auth principal, so there is no per-device entity.

### User search

```
Option A  UserMember + one persisted/computed folded search column
Option B  UserSearchTerm (UserId, Term, FieldType) + INDEX (Term, FieldType)
```

**A before B.** A computed column is maintained by the database, so there is no sync surface and no
silently-unfindable-user risk; B's only edge is being fully B-tree-indexable on *both* engines,
which decides it only if large SQL Server installs exist. A also fixes the Turkish dotless `ı` bug
and the PostgreSQL/SQL Server result divergence for correctness alone, independent of performance.

**Gated:** get the customer's `UserMember` row count and an `EXPLAIN ANALYZE` of a real search
first. Under roughly 50k users the current query is likely under 150 ms and this backlogs.

### External signal facts

```
UserSignalFact       append-only deltas, ExternalRef idempotency, one cursor per user per collector
UserExternalAccount  keyed by account system (e.g. "steam")
```

`ISignalCollector` per game plugin writes facts here; a generic stored-signal provider reads them.
Identity is translated once at ingest. Recorded deferral to revisit at build time: a *dimensions*
column on `UserSignalFact`, if parameter-filtered external signals are ever wanted.

### Inventory count / revaluation type

No reconciliation event exists — a stock count writes a `Set` stock transaction that the cost
ledger ignores, so ledger drift never self-corrects. POS sales widen the gap; write-offs only
advance the FIFO cursor at stale prices. The designed fix is a count/revaluation inventory type
that consumes all remaining layers and reopens a fresh inbound at counted quantity × chosen
valuation, making every physical count a ledger reset.

---

## Open — Indexes

`Update1` shipped 18 indexes, all on tables of 134k rows or more with a verified consumer query.
What remains is mostly **rewrite-blocked**: the index is useless until the LINQ becomes sargable,
so these are code-and-DDL pairs, not DDL alone.

| Index | Blocked on |
|---|---|
| `ProductOrder (Status) WHERE Status IN (0,3,4)` | `ProductOrderService.ActiveAsync` filters `Status != Completed && != Canceled`. The optimizer cannot prove `!=1 && !=2` implies `IN (0,3,4)`, so it would never pick the index. Add **only** with the predicate rewritten as a positive `IN`. Completed is ~98.5% of 867k rows. |
| `UserSession (UserId, State) INCLUDE (EndTime / CreatedTime)` | ~15 remaining `State.HasFlag(Active)` call sites must first become `State != UserSessionState.Ended`. The existing `(HostId, State)` index already serves the HostId-prefiltered ones once they are sargable. Do **not** rewrite `Service.Reports` — that `HasFlag` is in-memory on an API enum. |
| Reservation gate composites | Non-sargable `Date.AddMinutes(Duration)`. Needs a persisted computed `EndDate` column on `Reservation` first — parked as a larger schema change, but it unblocks every date-range, overlap and availability query. |
| `InventoryEntry (StockId, ProductId, Id)` | Nothing. The FIFO estimator's two `SUM` aggregates still scan; this makes them index-only. No index exists on that table today and it is TPT, so the adjustment and transfer-out sums also join subtype tables. |
| Order-line `StationId` | Ships with the kitchen schema above. |

Not an index, but the same family: `NpgSqlScripts.cs` still carries `"State" & 1 = 1` in the
billing loop. SQL Server got the `IN (1,5,9,17,33)` fix (3143 logical reads → 15; 40 ms →
microseconds on 661k rows). The Postgres side should match.

---

## Open — PostgreSQL Extensions

**No extension is enabled on any install today.** `EFCore_Initial`, `Update1` and `Update2` contain
no `CREATE EXTENSION`, no `AlterDatabase` annotation and no `migrationBuilder.Sql`; `Scripts.cs` in
`Gizmo.DAL.Migrations.Npgsql` is an empty class. There is no explicit `COLLATE` on any text column.

Both items below need elevated database privileges and may be refused on a locked-down
customer-managed instance. Verify before promising either.

```sql
CREATE EXTENSION IF NOT EXISTS unaccent;   -- then unaccent(col) ILIKE unaccent(pattern)
CREATE EXTENSION IF NOT EXISTS pg_trgm;    -- then GIN on the searched columns
```

`unaccent` closes a real behavioural divergence: server-side `ILike` is case-insensitive but
accent-**sensitive**, so `Agua` misses `Água` on every PostgreSQL install. SQL Server never had the
bug — `Latin1_General_100_CI_AI_SC` is accent-insensitive. Same code, two collation tables.

`pg_trgm` is the only item on this page that changes complexity class, O(rows) → O(matches), and it
is migration-only. Caveats to carry into the decision: trigrams need 3+ characters, so short
keystrokes still sequential-scan; eight separate GIN indexes mean a BitmapOr across eight scans and
the planner correctly reverts to a sequential scan at low selectivity; GIN slows writes. There is no
SQL Server equivalent in any form — full-text search changes *what users find*, which is a product
decision, not a performance one.

---

## Open — Data Backfills

Plain SQL for both engines, not EF migrations. Each fixes rows already written.

**Transfer inbound rows carry the source stock id.** Rows created before the StockId fix point at
the source stock. Rewrite `InventoryInboundEntry` where `InventoryTransferEntryId IS NOT NULL`, and
the parent `InventoryInbound.StockId`, to the transfer's `TransferStockId`. Symptom is understated
inbound totals on transfer-destination stocks. Because the missing-inbound deficit is a permanent
offset that never decays, shallow-stocked products fall back to `Product.Cost` forever even after
correct usage.

**`ProductOrder.BranchId` nulls.** Nullable is legacy-only — pre-branch v2 data the v2→v3 migration
should have defaulted and did not. Current code always sets it. Backfilling to the default branch
helps historical branch-scoped reports and would allow tightening the column to non-null later.

**Registers orphaned by the terminal-flag change.** Replacing the global `CreditCardUseTerminal`
with `Register.PaymentTerminalNumber != null` shipped no data migration, so registers that relied on
the global flag with an empty terminal number silently lost terminal availability. Needs either a
backfill or a release note; it currently has neither.

---

## Settled — Do Not Re-Propose

| Decision | Reasoning |
|---|---|
| No unique index on `ProductOLExtended.Mark` | Uniqueness is a business rule, not a hard invariant — a constraint would block re-issued marks after voids and multi-tenant overlaps. The full-table duplicate scan in the cart is the accepted trade-off. If mark-add traffic becomes hot the next move is re-typing to `nvarchar(64)` plus a **non-unique** index, never a unique constraint. |
| Achievement filter uniqueness stays app-enforced | Under TPT the `(AchievementId, valueColumn)` composites are not expressible — `AchievementId` lives on the base table, the value on the subtype. Re-challenged and reconfirmed: every money- or state-guarding unique survives TPT, and the lost constraint only prevents harmless duplicate ANY-of values. Requirement it creates: dedupe at save time and keep all filter writes funnelled through the one service. |
| No `BranchId` on order lines | Branch filtering joins line→order — one indexed FK hop. |
| `Register`→`Branch` is `Restrict`, not `Cascade` | Pre-existing model drift the scaffolder emits, deliberately carried by `Update1`. Deleting a branch no longer cascade-deletes its registers; it is blocked. |
| Legacy time-offer seconds get no migration | Products saved during one Manager UI window hold UTC-shifted `StartSecond`/`EndSecond`. Per-product re-save through the corrected UI is the documented workaround. |

---

## House Rules

**Which checkout.** Edit the DAL submodule inside the **Gizmo.Server** working copy. The two
checkouts of this repository drift, and the migration startup projects reference the server one.

**Both providers, always.**

```
dotnet ef migrations add <Name> --project <...Migrations.MSSQL> \
    --startup-project Gizmo.DAL.Startup.MSSQL --context DefaultDbContext \
    --output-dir . --namespace Gizmo.DAL.Migrations.MSSQL
```

MSSQL keeps its files at the project root, hence the `--output-dir`/`--namespace` override; Npgsql
uses the default `Migrations/` folder. `migrations add` needs no database; `migrations remove` does.
Both migration projects have a `DesignTimeDbContextFactory`.

<a name="batching"></a>
**Batching.** Several open columns above land on tables `Update2` created. Either fold them into a
drop-regenerated `Update2` — the precedent, that is how `Achievement.ImageId` got in, at the cost of
a dev-database downgrade and now re-scaffolding `Update3` on top — or add a plain `Update4`. Update4
is cheaper now that Update3 exists, unless 2 and 3 are still to be squashed before release. Settle
once and apply to all of them.

**Index placement.** Only provider-**incompatible** index configuration lives in
`DefaultDbContext.ApplyPerformanceIndexes`: covering `INCLUDE` (`IncludeProperties` is ambiguous in
a project referencing both providers, so it must be disambiguated per provider) and filtered
indexes (the `HasFilter` SQL differs). Plain indexes go in the entity's `IEntityTypeConfiguration`
map like every other index. Maps have no provider access; the context does.

**Filtered-index predicates.** Write the consuming query as a positive equality `IN (…)` list of the
wanted values. Never `!=`, `NOT IN`, or a bitmask — non-seekable, and mis-estimated on a skewed
enum column. The predicate form *is* the fix.

**Check table size first.** Seven audit-recommended indexes were cut from `Update1` for sitting on
small tables where the optimizer sequential-scans regardless — pure write overhead. The one
exception: index a per-second timer's sargable query ahead of growth.

**Prove the consumer.** A synthetic `WHERE col BETWEEN …` measurement proves a table *can* scan, not
that any real query does. An index on `Payment(CreatedTime)` measured 28,569 reads and had no
consumer at all — the date filter was on `InvoicePayment` and `DepositPayment`.

**Identifier length.** PostgreSQL truncates at 63 bytes; EF does it deterministically and the
achievement schema relies on that, with no collisions. Two names sit at exactly 63 — do not lengthen
`FK_AchievementPaymentMethodFilter_PaymentMethod_PaymentMethodId` or
`IX_AchievementChallengeCompletion_UserId_ChallengeId_Occurrence`.

**Never inline `DateTime.UtcNow` in an EF expression.** Capture it to a local first. Inline, Npgsql
emits `now()` (`timestamptz`) against `timestamp without time zone` columns — a translation error in
`Where`, and in `ExecuteUpdateAsync` a silent non-UTC write.

**Commit order.** Bottom-up: `Gizmo.DAL.Entities` → `Gizmo.DAL` → `Gizmo.Server`, then the Manager
repository's pointers. The Manager repository holds two `Gizmo.Web.Api.Models` copies — edit the
top-level one.
