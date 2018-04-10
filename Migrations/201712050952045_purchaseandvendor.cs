namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchaseandvendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "VendorID", c => c.Int(nullable: false));
            AddColumn("dbo.Vendors", "Purchase_ID", c => c.Int());
            CreateIndex("dbo.Vendors", "Purchase_ID");
            AddForeignKey("dbo.Vendors", "Purchase_ID", "dbo.Purchases", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendors", "Purchase_ID", "dbo.Purchases");
            DropIndex("dbo.Vendors", new[] { "Purchase_ID" });
            DropColumn("dbo.Vendors", "Purchase_ID");
            DropColumn("dbo.Purchases", "VendorID");
        }
    }
}
