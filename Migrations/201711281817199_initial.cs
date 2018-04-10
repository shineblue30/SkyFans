namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        PaymentVia = c.String(),
                        VendorProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VendorProducts", t => t.VendorProductID, cascadeDelete: true)
                .Index(t => t.VendorProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "VendorProductID", "dbo.VendorProducts");
            DropIndex("dbo.Purchases", new[] { "VendorProductID" });
            DropTable("dbo.Purchases");
        }
    }
}
