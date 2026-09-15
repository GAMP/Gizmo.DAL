# Schema Backlog

Outstanding database changes that have been designed or agreed but not yet built, plus the
decisions that were deliberately settled *against* a schema change so they are not raised again.

**Verified against this repository on 2026-09-04, corrected line by line on 2026-09-07.** Item
status ages; re-verify before acting:

```bash
git rev-list --left-right --count @{u}...HEAD      # "behind<TAB>ahead" — ahead 0 = nothing unpushed
grep -c 'migrationBuilder.CreateTable' Gizmo.DAL/Migrations/Gizmo.DAL.Migrations.MSSQL/*_Update2.cs
```

Never assert commit or push state from notes — run the check.

## Closed By Review (2026-09-07)

Listed so they are not raised again as open work:

- **Npgsql billing-loop predicate** — `NpgSqlScripts.cs` already uses `"State" IN (1, 5, 9, 17, 33)`
  (DAL `dc73adb`, 2026-06-26), mirroring the SQL Server fix.
- **Kitchen prep status** — `PrepareStatus` / `PreparedQuantity` / `PrepareTime` are mapped on both
  `ProductOL` and `ProductOrder` and shipped in `EFCore_Initial`. Only `StationId` is new persistence.
- **`AchievementLadder.Options`** — exists since `Update2` (column order 3) with
  `AchievementLadderOptionType.PromoteOnSettleOnly = 1`. Only the level-side flag column is new, and
  it is parked (see below).

## Migration State

| Migration | MSSQL | Npgsql | Contents |
|---|---|---|---|
| `EFCore_Initial` | 20260216074339 | 20260216074229 | Baseline. |
| `Update1` | 20260630134254 | 20260630134301 | 18 performance indexes. Also carries the pre-existing `Register`→`Branch` FK `Cascade`→`Restrict` drift, intentionally. |
| `Update2` | 20260723100155 | 20260723101222 | 37 `CreateTable` — the achievement family (incl. both TPT hierarchies) and `VerificationMethod`. 3 `AddColumn` — `UserMember.IsTierExempt`, `Register.DefaultOperatorId`, `Register.ReceiptPrinterNumber`. No `InsertData`. |
| `Update3` | 20260825145608 | 20260825145544 | `PaymentIntent.RegisterId` / `.ShiftId` / `.QrDisplayNumber`, 2 indexes, 2 FKs (`NO ACTION`). |

Whether `Update1` is the last publicly released migration cannot be verified from git — neither
repository carries release tags. It no longer matters for batching: `Update2` and `Update3` are on
`origin/version-3`, so regenerating `Update2` would force every checkout to downgrade. **New
columns go in `Update4`** (see [Batching](#batching)).

`Gizmo.DAL.Entities` `5760f54` (2026-09-04) renamed `AchievementChallengeRewardStatus.Granted` to
`Delivered` (same value) and added `Claimed = 4`. Enum only — no migration.

---

## Open — Columns

Each is a small additive column whose design is settled.

### `AchievementLadderLevel.Name`

```
+ Name nvarchar(45) NULL          -- SQLStringSize.TINY45 (= UserGroup.Name), IsRequired(false), column order 7
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
+ InvoiceId int NULL  ->  Invoice (RESTRICT)   -- same shape as the product snapshot's Invoice FK
```

Points snapshots reference their `PointTransaction` and product snapshots their `Invoice`; time
snapshots reference nothing. A delivered time prize leaves no ledger trail and cannot be
de-duplicated — which also makes the delivery/stamp atomicity gap undetectable for time, where
product at least leaves an invoice id in the log.

### Ladder level option flags — parked, not for the initial release

```
AchievementLadderLevel + Options int   [Flags] AchievementLadderLevelOptionType { None = 0, InheritRequirements = 1, RequirePreviousLevel = 2 }
```

Two per-level flags on different axes (decided 2026-09-07, build later):

- **`InheritRequirements`** — a *content* rule, requirements mode only. The level's effective set
  is its own rows ∪ the level below, judged on the same period's completions; the chain walks down
  while each level carries the flag, overlapping achievements merge by `max(RequiredCount)`,
  flattened at config-cache rebuild so the evaluator loop is untouched. Says nothing about speed.
- **`RequirePreviousLevel`** — a *path* rule, both modes. Entering the level requires having held
  the level below at the start of the period. Judges history, not this period's counts; the only
  flag that slows a climb (one gated rung per period). Anchored at period start on purpose — a gate
  on "currently holds the level below" is vacuous under live evaluation, which climbs rung by rung
  across consecutive ticks. Read from `AchievementLadderEvent` (`PeriodStart` = current period,
  `ToRank > FromRank`; add `CreatedTime >=` for the `(UserId, CreatedTime)` index), no state
  column. **Doc-comment on the event entity** that it is evaluator control state, so the
  "keep forever" retention decision is load-bearing.

The ladder-scoped `StepwisePromotion` flag is **dropped**: every level gated is ladder-wide
stepwise, so the per-level gate subsumes it. `AchievementLadder.Options` already exists with
`PromoteOnSettleOnly = 1` and stays ladder-scoped — cadence belongs to the ladder's clock
(`Period`, settle state, boundary sweep), entry rules belong to the rung. `StepwisePromotion`
never existed in code; the only footprint is `LadderStandingModel.IsStepwise` (key 7, reserved,
always false) and its comments — clean up with the models later.

### Reversal reference on the refund base

```
Refund (TPT base of RefundInvoicePayment / RefundDepositPayment / RefundPayment)
+ reversal reference nvarchar NULL     -- column order 8, after Note
```

A reversal produces a *new* reference — terminal reversal RRN, or a Stripe `re_…` refund id — that
must not overwrite `PaymentIntent.TransactionId`, which holds the original and is needed by cancel
and audit. This is the slot `PaymentReversalResult.RefundId` should fill; today it is discarded, so
a completed PSP refund records no provider reference and cannot be reconciled.

Every rail populates this one field. Writing the reference here (rather than anywhere
terminal-specific) is also what keeps a later sync→async reversal lifecycle purely additive.
`DepositPaymentService.VoidAsync` already carries the TODO for this column.

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
myDATA returns a MARK and QR per document, so all of it must persist. **Types matter:**
`DocumentNumber` is `int?` and a MARK is a 15-digit number, so it needs `bigint` or a string
column, not a widening of the existing one; the UID is a 40-character hex string.

Related and also schema-shaped: on the certified-provider route the server must assign a gapless
per-register series number **before** first transmission, stable across poller retries. No such
counter exists on `Invoice`, and the `FiscalReceipt` row is only created after a successful print.

### Emailer integration seed row — not a migration

The emailer-as-integration split needs an `Integration` row for `SystemSmtpEmailProvider` so
`VerificationMethod` entries can reference it. The earlier plan put it in the communication-methods
migration "when `SMTPOptions.IsEnabled`" — a migration cannot read application options, so that
was never buildable and `Update2` correctly contains no `InsertData`. It is an **idempotent startup
seeder** in the server: look up by `TypeGuid` (the index is non-unique, so the seeder checks
existence itself), insert with a fixed `PublicId` (unique index), `Name`, `CreatedTime`, and no
`ConfigJson`. Deliberately does **not** wrap the legacy SMS gateway.

---

## Open — New Schema

### Preparation stations (kitchen)

```
Station         global, named, soft-delete only (order lines keep the stamp forever)
KitchenBinding  branch + station SET + display options; Guid Uid on the wire, int Id internal
                station -> companion/printer binding per branch, with a delivery mode
ProductOL     + StationId int NULL   + index on StationId   -- the ONLY new persistence
```

The `StationId` stamp at order acceptance is the load-bearing commitment: a snapshot, never
re-derived on read, so a config change mid-shift cannot re-route in-flight tickets. It is also the
only new persistence the feature needs — the prep-status track (`PrepareStatus`, `PreparedQuantity`,
`PrepareTime`) already exists on both `ProductOL` and `ProductOrder` since `EFCore_Initial`.

Order lines have no `BranchId` and must not gain one — branch filtering joins line→order, one
indexed FK hop. Prep status is a track separate from `IDeliverable`, not a new state machine. The
binding is the auth principal, so there is no per-device entity.

### User search

```
Option A  UserMember + one persisted/computed folded search column
Option B  UserSearchTerm (UserId, Term, FieldType) + INDEX (Term, FieldType)
```

**A before B, but A is an application-maintained column, not a computed one.** The folding the
design needs (lowercase, strip diacritics, explicit map for `ı`, `ø`, `ß`, `æ`) exists only in C#:
SQL Server has no accent-folding function, and PostgreSQL's `unaccent()` is marked STABLE, so it
cannot sit in a STORED generated column or an expression index without an IMMUTABLE wrapper
function shipped by the migration. So A carries the same sync surface as B — every write path that
touches a searchable field must refresh the column (one `SaveChanges` interceptor, plus a backfill).
A still wins on size, and it fixes the Turkish dotless `ı` bug and the PostgreSQL/SQL Server result
divergence for correctness alone. B's only edge is being fully B-tree-indexable on *both* engines,
which decides it only if large SQL Server installs exist.

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
| Reservation gate composites | Non-sargable `Date.AddMinutes(Duration)`. Needs a persisted computed `EndDate` column on `Reservation` first — parked as a larger schema change, but it unblocks every date-range, overlap and availability query. The computed SQL is provider-specific (`DATEADD(minute, Duration, Date) PERSISTED` vs `"Date" + make_interval(mins => "Duration") STORED`), so it lives in the context's provider branch, like the filtered indexes. |
| `InventoryEntry (StockId, ProductId, Id DESC)` | Nothing, but the expectation was wrong. The table already has single-column FK indexes (`StockId`, `ProductId`, …); the composite supersedes `IX_InventoryEntry_StockId` (drop it, as with `StockTransaction`). It can never be index-only: the estimator's sums filter by subtype under TPT, which forces a join to the subtype table, and they aggregate `Quantity`, which the key does not carry — same shape as the `UsageTime` finding. Expect a seek plus subtype join instead of a scan; `Id DESC` also serves the newest-first layer paging. Check the row count first. |
| Order-line `StationId` | Ships with the kitchen schema above. |

The Npgsql billing-loop predicate is done — see *Closed By Review* above.

---

## Open — PostgreSQL Extensions

**No extension is enabled on any install today.** `EFCore_Initial`, `Update1` and `Update2` contain
no `CREATE EXTENSION`, no `AlterDatabase` annotation and no `migrationBuilder.Sql`; `Scripts.cs` in
`Gizmo.DAL.Migrations.Npgsql` is an empty class. There is no explicit `COLLATE` on any text column.

Both items below need elevated database privileges and may be refused on a locked-down
customer-managed instance. Verify before promising either.

```sql
CREATE EXTENSION IF NOT EXISTS unaccent;   -- then unaccent(col) ILIKE unaccent(pattern)
CREATE EXTENSION IF NOT EXISTS pg_trgm;    -- then GIN (gin_trgm_ops) on the searched columns
```

`unaccent` closes a real behavioural divergence: server-side `ILike` is case-insensitive but
accent-**sensitive**, so `Agua` misses `Água` on every PostgreSQL install. SQL Server never had the
bug — `Latin1_General_100_CI_AI_SC` is accent-insensitive. Same code, two collation tables.

Mechanics: declare both with `modelBuilder.HasPostgresExtension(...)` inside the Npgsql branch so
EF emits the `CREATE EXTENSION`, rather than `migrationBuilder.Sql`. `unaccent()` is STABLE, so a
trigram index on `unaccent(col)` needs an IMMUTABLE wrapper function created in the same migration,
and the query must use that exact wrapper expression for the index to match. The GIN opclass
(`gin_trgm_ops`) is Npgsql-only configuration and belongs in `ApplyPerformanceIndexes`.

`pg_trgm` is the only item on this page that changes complexity class, O(rows) → O(matches), and it
is migration-only. Caveats to carry into the decision: trigrams need 3+ characters, so short
keystrokes still sequential-scan; eight separate GIN indexes mean a BitmapOr across eight scans and
the planner correctly reverts to a sequential scan at low selectivity; GIN slows writes. There is no
SQL Server equivalent in any form — full-text search changes *what users find*, which is a product
decision, not a performance one.

---

## Open — Data Backfills

Plain SQL for both engines, not EF migrations. Each fixes rows already written.

**Transfer inbound rows carry the source stock id.** Rows created before server `b572c537`
(2026-07-30) point at the source stock. `StockId` lives on the TPT **base** tables: rewrite
`InventoryEntry.StockId` for rows whose `InventoryInboundEntry.InventoryTransferEntryId IS NOT NULL`
(join entry → transfer entry → its `Inventory` → `InventoryTransfer.TransferStockId`), and the
parent `Inventory.StockId` for the `InventoryInbound` row (one inbound per transfer, so it is
unambiguous). The subtype tables do not carry the column. Symptom is understated
inbound totals on transfer-destination stocks. Because the missing-inbound deficit is a permanent
offset that never decays, shallow-stocked products fall back to `Product.Cost` forever even after
correct usage.

**`ProductOrder.BranchId` nulls.** Nullable is legacy-only — pre-branch v2 data the v2→v3 migration
should have defaulted and did not. Current code always sets it. There is no "default branch" —
`Branch` has no such flag; the house rule is the branch of the register flagged
`RegisterOptions.GlobalDefault` (duplicates are tolerated there, so take the lowest `RegisterId`).
Backfilling helps historical branch-scoped reports and would allow tightening the column to
non-null later.

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
**Batching — decided: `Update4`.** Several open columns above land on tables `Update2` created.
Regenerating `Update2` was the precedent (that is how `Achievement.ImageId` got in), but `Update2`
and `Update3` are now on `origin/version-3`, so a regeneration would force every checkout to
downgrade. All additive columns go in a plain `Update4`; on `AchievementLadderLevel` the `Name`
column takes order 7 and, if the parked `Options` column ever ships with it, order 8.

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
