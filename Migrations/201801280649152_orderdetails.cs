namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "FanID", "dbo.Fans");
            DropIndex("dbo.Orders", new[] { "FanID" });
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        PaymentVia = c.String(),
                        FanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fans", t => t.FanID, cascadeDelete: true)
                .Index(t => t.FanID);
            
            AddColumn("dbo.Orders", "OrderDetailID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderDetailID");
            AddForeignKey("dbo.Orders", "OrderDetailID", "dbo.OrderDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Orders", "Total");
            DropColumn("dbo.Orders", "PaymentVia");
            DropColumn("dbo.Orders", "FanID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "FanID", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PaymentVia", c => c.String());
            AddColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Price", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "OrderDetailID", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderDetails", "FanID", "dbo.Fans");
            DropIndex("dbo.OrderDetails", new[] { "FanID" });
            DropIndex("dbo.Orders", new[] { "OrderDetailID" });
            DropColumn("dbo.Orders", "OrderDetailID");
            DropTable("dbo.OrderDetails");
            CreateIndex("dbo.Orders", "FanID");
            AddForeignKey("dbo.Orders", "FanID", "dbo.Fans", "ID", cascadeDelete: true);
        }
    }
}
