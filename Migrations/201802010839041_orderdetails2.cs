namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetails2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetailOrders", "OrderDetail_ID", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderDetailOrders", "Order_ID", "dbo.Orders");
            DropIndex("dbo.OrderDetailOrders", new[] { "OrderDetail_ID" });
            DropIndex("dbo.OrderDetailOrders", new[] { "Order_ID" });
            AddColumn("dbo.OrderDetails", "Order_ID", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Order_ID");
            AddForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders", "ID");
            DropTable("dbo.OrderDetailOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetailOrders",
                c => new
                    {
                        OrderDetail_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderDetail_ID, t.Order_ID });
            
            DropForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_ID" });
            DropColumn("dbo.OrderDetails", "Order_ID");
            CreateIndex("dbo.OrderDetailOrders", "Order_ID");
            CreateIndex("dbo.OrderDetailOrders", "OrderDetail_ID");
            AddForeignKey("dbo.OrderDetailOrders", "Order_ID", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetailOrders", "OrderDetail_ID", "dbo.OrderDetails", "ID", cascadeDelete: true);
        }
    }
}
