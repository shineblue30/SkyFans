namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        PaymentVia = c.String(),
                        FanID = c.Int(nullable: false),
                        DealerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dealers", t => t.DealerID, cascadeDelete: true)
                .ForeignKey("dbo.Fans", t => t.FanID, cascadeDelete: true)
                .Index(t => t.FanID)
                .Index(t => t.DealerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "FanID", "dbo.Fans");
            DropForeignKey("dbo.Orders", "DealerID", "dbo.Dealers");
            DropIndex("dbo.Orders", new[] { "DealerID" });
            DropIndex("dbo.Orders", new[] { "FanID" });
            DropTable("dbo.Orders");
        }
    }
}
