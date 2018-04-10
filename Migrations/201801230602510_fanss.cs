namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanss : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fans", "VendorProductID", "dbo.VendorProducts");
            DropIndex("dbo.Fans", new[] { "VendorProductID" });
            CreateTable(
                "dbo.FanParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        VendorProductID = c.Int(nullable: false),
                        FanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fans", t => t.FanID, cascadeDelete: true)
                .ForeignKey("dbo.VendorProducts", t => t.VendorProductID, cascadeDelete: true)
                .Index(t => t.VendorProductID)
                .Index(t => t.FanID);
            
            DropColumn("dbo.Fans", "Qty");
            DropColumn("dbo.Fans", "Price");
            DropColumn("dbo.Fans", "VendorProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fans", "VendorProductID", c => c.Int(nullable: false));
            AddColumn("dbo.Fans", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Fans", "Qty", c => c.Int(nullable: false));
            DropForeignKey("dbo.FanParts", "VendorProductID", "dbo.VendorProducts");
            DropForeignKey("dbo.FanParts", "FanID", "dbo.Fans");
            DropIndex("dbo.FanParts", new[] { "FanID" });
            DropIndex("dbo.FanParts", new[] { "VendorProductID" });
            DropTable("dbo.FanParts");
            CreateIndex("dbo.Fans", "VendorProductID");
            AddForeignKey("dbo.Fans", "VendorProductID", "dbo.VendorProducts", "ID", cascadeDelete: true);
        }
    }
}
