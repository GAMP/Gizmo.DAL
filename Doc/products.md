# Products

## Entity Hierarchy

```
ProductBase (abstract)
├── ProductBaseExtended (abstract) — adds OrderLines, InvoiceLines
│   ├── Product — generic/simple product
│   └── ProductBundle — contains multiple bundled products
└── ProductTime — time-based product (internet/gaming time)
```

**TPT (Table-Per-Type)** inheritance — each type maps to its own database table.

## Product Types

### Product (simple/generic)
Inherits `ProductBaseExtended`. No additional properties beyond the base. Used for shop items, food, drinks, etc. Supports order lines (`ProductOLProduct`) and invoice lines (`InvoiceLineProduct`).

### ProductBundle
Inherits `ProductBaseExtended`. A product composed of multiple other products.

| Property | Type | Description |
|----------|------|-------------|
| `BundleStockOptions` | `BundleStockOptionType` | Controls how bundle stock is calculated. See [stock-options.md](stock-options.md). |
| `BundledProducts` | `ISet<BundleProduct>` | Collection of products contained in this bundle. |
| `UserPrices` | `ISet<ProductBundleUserPrice>` | Per-user-group pricing overrides for the bundle. |

**BundleProduct** (junction entity):
| Property | Type | Description |
|----------|------|-------------|
| `ProductBundleId` | `int` | FK to the parent bundle. |
| `ProductId` | `int` | FK to the bundled product. |
| `Quantity` | `decimal` | How many units of this product are in one bundle. |
| `Price` | `decimal` | Price override for this product within the bundle. |
| `DisplayOrder` | `int` | Display ordering. |
| `Options` | `ProductBundleOptionType` | Bundle product options. |

### ProductTime
Inherits `ProductBase` directly (not `ProductBaseExtended`). Represents purchasable time (internet access, gaming sessions).

| Property | Type | Description |
|----------|------|-------------|
| `Minutes` | `int` | Duration in minutes. |
| `WeekDayMaxMinutes` | `int?` | Maximum daily usage on weekdays. |
| `WeekEndMaxMinutes` | `int?` | Maximum daily usage on weekends. |
| `AppGroupId` | `int?` | Associated application group. |
| `ExpiresAfter` | `int` | Expiration period in days. |
| `ExpirationOptions` | `ProductTimeExpirationOptionType` | Expiration behavior flags. |
| `ExpireFromOptions` | `ExpireFromOptionType` | When expiration countdown begins. |
| `UsageOptions` | `ProductTimeUsageOptionType` | Usage restriction flags. |
| `UseOrder` | `int` | Consumption priority order. |
| `ExpireAfterType` | `ExpireAfterType` | Unit of the expire-after value. |
| `ExpireAtDayTimeMinute` | `int` | Minute of day for day-time expiration. |

**Expiration flags** (`ProductTimeExpirationOptionType`):
| Flag | Value | Description |
|------|-------|-------------|
| `None` | 0 | No special expiration. |
| `ExpiresAtLogout` | 1 | Time expires when user logs out. |
| `ExpireAfterTime` | 4 | Expires after a configured duration. |
| `ExpireAtDayTime` | 8 | Expires at a specific time of day. |

**Usage flags** (`ProductTimeUsageOptionType`):
| Flag | Value | Description |
|------|-------|-------------|
| `None` | 0 | No usage restrictions. |
| `HasMaximumUsage` | 1 | Enforces total maximum usage. |
| `HasMaximumDailyUsage` | 2 | Enforces daily maximum usage. |
| `Reservation` | 4 | Product is used for reservations. |

## Common Properties (ProductBase)

| Property | Type | Description |
|----------|------|-------------|
| `Name` | `string` | Product name (max 45 chars). |
| `Description` | `string` | Product description. |
| `Price` | `decimal` | Base price. |
| `Cost` | `decimal?` | Cost/wholesale price. |
| `Barcode` | `string` | Product barcode (max 255 chars). |
| `Points` | `int?` | Points awarded on purchase. |
| `PointsPrice` | `int?` | Price in loyalty points. |
| `OrderOptions` | `OrderOptionType` | Order behavior flags. |
| `PurchaseOptions` | `PurchaseOptionType` | Purchase restriction flags. |
| `StockOptions` | `StockOptionType` | Stock tracking flags. See [stock-options.md](stock-options.md). |
| `StockAlert` | `decimal` | Low stock alert threshold. |
| `StockProductId` | `int?` | FK to target product for stock. |
| `StockProductAmount` | `decimal` | Multiplier for target product stock. |
| `ProductGroupId` | `int` | FK to product group. |
| `DisplayOrder` | `int` | Display ordering. |
| `IsDeleted` | `bool` | Soft delete flag. |
| `Guid` | `Guid` | Unique identifier for replication. |

## Order Line Hierarchy

```
ProductOL (abstract base)
├── ProductOLExtended (abstract) — adds BundleLineId for bundle membership
│   ├── ProductOLProduct — order line for Product and ProductBundle
│   └── ProductOLTime — order line for ProductTime
├── ProductOLSession — order line for usage sessions
├── ProductOLTimeFixed — order line for fixed time
└── ProductOLReservationFee — order line for reservation fees
```

**Key ProductOL properties:** ProductName, Quantity, UnitPrice, UnitListPrice, UnitCost, Total, PreTaxTotal, TaxRate, TaxTotal, PointsAward, IsVoided, IsDelivered, DeliveredQuantity, ShiftId, RegisterId, PayType, ReservationId.

**ProductOLProduct** adds: `ProductId`, `Mark` (for marked/serialized products).

**ProductOLTime** adds: `ProductTimeId`.

## Invoice Line Types

```
InvoiceLine (base)
├── InvoiceLineExtended (abstract)
│   ├── InvoiceLineProduct — links to ProductOLProduct
│   └── InvoiceLineTime — links to ProductOLTime, implements ITimeDepletable
└── InvoiceLineTimeFixed — links to ProductOLTimeFixed, implements ITimeDepletable
```

## Related Entities

| Entity | Purpose |
|--------|---------|
| `ProductGroup` | Hierarchical grouping with parent/child support. |
| `ProductTax` | Associates tax rates with products. |
| `ProductUserPrice` | Per-user-group price overrides. |
| `ProductUserDisallowed` | Restricts product availability by user group. |
| `ProductHostHidden` | Hides products from specific host groups. |
| `ProductImage` | Product images. |
| `ProductBranch` | Product availability per branch. |
| `ProductPeriod` / `ProductTimePeriod` | Time-window availability restrictions. |
| `AgeRestrictionProduct` | Age restrictions on products. |
