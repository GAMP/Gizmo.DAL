# Stock Options

## StockOptionType (flags enum)

| Flag                      | Value | Description |
|---------------------------|-------|-------------|
| `None`                    | 0     | Stock tracking disabled. |
| `EnableStock`             | 1     | Enables stock tracking for the product. Required for any stock operations. |
| `DisallowSaleIfOutOfStock`| 2     | Prevents sale when `OnHand` would go below zero. |
| `Alert`                   | 4     | Enables stock alert notifications when `OnHand` falls at or below `StockAlert` threshold. |
| `TargetDifferentProduct`  | 8     | Stock transactions are recorded against a different product (`StockProductId`) instead of the product itself. See [Target Different Product](#target-different-product). |

## BundleStockOptionType (flags enum, ProductBundle only)

| Flag            | Value | Description |
|-----------------|-------|-------------|
| `None`          | 0     | Bundle stock is derived from its bundled products (default). |
| `SelfStockCount`| 1     | Bundle maintains its own independent stock count via direct stock transactions. |

## Key Entity Properties (ProductBase)

- **`StockOptions`** — flags controlling stock behavior.
- **`StockAlert`** — threshold for low-stock alert (used when `Alert` flag is set).
- **`StockProductId`** — FK to the target product (used when `TargetDifferentProduct` flag is set).
- **`StockProductAmount`** — multiplier defining how many units of the target product are consumed per one unit of this product.

## Target Different Product

When `TargetDifferentProduct` is set on a product:

1. **Stock transactions** are created against the **target product** (`StockProductId`), not the product being sold.
2. The **amount** recorded on the transaction is `StockProductAmount * quantity` (the multiplied amount in terms of the target product).
3. The transaction's `OnHand` reflects the **target product's** stock level, not the source product's.
4. The source product's effective stock is derived: `targetOnHand / StockProductAmount`.
5. The transaction records `SourceProductId`, `SourceProductAmount`, and `SourceProductOnHand` for traceability.

**Example:** Product "Glass of Juice" targets "Juice Bottle" with `StockProductAmount = 5`. Selling 2 glasses creates a transaction on "Juice Bottle" with amount = 10. If the bottle had 50 on hand, new on hand = 40. The glass's effective stock = 40 / 5 = 8.

**Constraints:**
- `StockProductId` must be set and valid.
- `StockProductAmount` must be > 0.
- Only `Sale` and `Return` transaction types are allowed (Add/Remove/Set throw `InvalidType`).
- The target product must also have `EnableStock` set, otherwise `StockDisabled` is thrown.
- **Single level only:** Targeting is strictly one level deep (A → B). The target product (B) must not itself have `TargetDifferentProduct` set. The server does not resolve chains (A → B → C) — it resolves `StockProductId` once and operates on the target directly. If a chain is misconfigured, only the first level is followed and deeper targets are ignored. The UI should prevent setting `TargetDifferentProduct` on a product that is already a target of another product, or creating chains.

## Bundle Stock Calculation

Bundles have three stock modes depending on their flags:

### Mode 1: Derived from bundled products (default)
**Flags:** `EnableStock` set, `SelfStockCount` not set, `TargetDifferentProduct` not set.

Stock level = minimum across all bundled products of `(bundledProduct.OnHand / bundledProduct.Quantity)`.

Each bundled product's on-hand is resolved through the same rules (including target different if set on the bundled product).

### Mode 2: Self stock count
**Flags:** `EnableStock` + `SelfStockCount` set.

Bundle has its own stock transactions. Works like a regular product.

### Mode 3: Target different product
**Flags:** `EnableStock` + `TargetDifferentProduct` set.

Stock is derived from the target product, same as any non-bundle product with this flag. `SelfStockCount` is ignored if both flags are set.

## Stock Transaction Types

| Type     | Effect on OnHand          | Usage |
|----------|--------------------------|-------|
| `Add`    | `previousOnHand + amount` | Inventory inbound, transfer inbound. |
| `Remove` | `previousOnHand - amount` | Inventory adjustment (write-off), transfer out. |
| `Sale`   | `previousOnHand - amount` | Order line sale. |
| `Return` | `previousOnHand + amount` | Order line return/void. Allowed even when stock is disabled. |
| `Set`    | `amount` (absolute)       | Manual stock level override. Zero is allowed. |

## Real-Time Events

- `ProductStockChangeEventMessage` is generated after a stock transaction commits, containing the new `OnHand`, `StockId`, `ProductId`, and whether `AlertLevelReached`.
- `GenerateDependentStockChangeEventsAsync` produces additional events for:
  - Products with `TargetDifferentProduct` pointing at the transacted product.
  - Bundles (without `SelfStockCount`) that contain the transacted product as a bundled product.
- **Note:** The internal `TransactionAsync` used by `InventoriesService` does not generate events. The caller is responsible for event generation after transaction commit.
