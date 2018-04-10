namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockout1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockOuts", "Vendor_ID", c => c.Int());
            AddColumn("dbo.StockOuts", "VendorProduct_ID", c => c.Int());
            CreateIndex("dbo.StockOuts", "Vendor_ID");
            CreateIndex("dbo.StockOuts", "VendorProduct_ID");
            AddForeignKey("dbo.StockOuts", "Vendor_ID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.StockOuts", "VendorProduct_ID", "dbo.VendorProducts", "ID");
            DropTable("dbo.StockIns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockIns",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        AmountToPay = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        PaymentVia = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.StockOuts", "VendorProduct_ID", "dbo.VendorProducts");
            DropForeignKey("dbo.StockOuts", "Vendor_ID", "dbo.Vendors");
            DropIndex("dbo.StockOuts", new[] { "VendorProduct_ID" });
            DropIndex("dbo.StockOuts", new[] { "Vendor_ID" });
            DropColumn("dbo.StockOuts", "VendorProduct_ID");
            DropColumn("dbo.StockOuts", "Vendor_ID");
        }
    }
}
