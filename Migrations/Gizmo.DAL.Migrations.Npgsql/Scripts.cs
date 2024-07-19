namespace Gizmo.DAL.Migrations.Npgsql
{
    public static class Scripts
    {
        public static readonly string BRANCH_SET = """
            UPDATE "Register" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "Asset" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "Device" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "HostGroup" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "HostLayoutGroup" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "Shift" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "StockTransaction" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
            UPDATE "Reservation" SET "BranchId" = (SELECT min("BranchId") FROM "Branch");
        """;
    }
}
