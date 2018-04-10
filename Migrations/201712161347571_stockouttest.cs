namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockouttest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockOuts", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.StockOuts", "VendorProduct_ID", "dbo.VendorProducts");
            DropIndex("dbo.StockOuts", new[] { "Vendor_ID" });
            DropIndex("dbo.StockOuts", new[] { "VendorProduct_ID" });
            RenameColumn(table: "dbo.StockOuts", name: "Vendor_ID", newName: "VendorID");
            RenameColumn(table: "dbo.StockOuts", name: "VendorProduct_ID", newName: "VendorProductID");
            AlterColumn("dbo.StockOuts", "VendorID", c => c.Int(nullable: false));
            AlterColumn("dbo.StockOuts", "VendorProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.StockOuts", "VendorID");
            CreateIndex("dbo.StockOuts", "VendorProductID");
            AddForeignKey("dbo.StockOuts", "VendorID", "dbo.Vendors", "ID", cascadeDelete: false);
            AddForeignKey("dbo.StockOuts", "VendorProductID", "dbo.VendorProducts", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockOuts", "VendorProductID", "dbo.VendorProducts");
            DropForeignKey("dbo.StockOuts", "VendorID", "dbo.Vendors");
            DropIndex("dbo.StockOuts", new[] { "VendorProductID" });
            DropIndex("dbo.StockOuts", new[] { "VendorID" });
            AlterColumn("dbo.StockOuts", "VendorProductID", c => c.Int());
            AlterColumn("dbo.StockOuts", "VendorID", c => c.Int());
            RenameColumn(table: "dbo.StockOuts", name: "VendorProductID", newName: "VendorProduct_ID");
            RenameColumn(table: "dbo.StockOuts", name: "VendorID", newName: "Vendor_ID");
            CreateIndex("dbo.StockOuts", "VendorProduct_ID");
            CreateIndex("dbo.StockOuts", "Vendor_ID");
            AddForeignKey("dbo.StockOuts", "VendorProduct_ID", "dbo.VendorProducts", "ID");
            AddForeignKey("dbo.StockOuts", "Vendor_ID", "dbo.Vendors", "ID");
        }
    }
}
