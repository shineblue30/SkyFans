namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetails1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderDetailID", "dbo.OrderDetails");
            DropIndex("dbo.Orders", new[] { "OrderDetailID" });
            CreateTable(
                "dbo.OrderDetailOrders",
                c => new
                    {
                        OrderDetail_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderDetail_ID, t.Order_ID })
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetail_ID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID, cascadeDelete: true)
                .Index(t => t.OrderDetail_ID)
                .Index(t => t.Order_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetailOrders", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetailOrders", "OrderDetail_ID", "dbo.OrderDetails");
            DropIndex("dbo.OrderDetailOrders", new[] { "Order_ID" });
            DropIndex("dbo.OrderDetailOrders", new[] { "OrderDetail_ID" });
            DropTable("dbo.OrderDetailOrders");
            CreateIndex("dbo.Orders", "OrderDetailID");
            AddForeignKey("dbo.Orders", "OrderDetailID", "dbo.OrderDetails", "ID", cascadeDelete: true);
        }
    }
}
