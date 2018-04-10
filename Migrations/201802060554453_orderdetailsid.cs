namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetailsid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_ID" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_ID", newName: "OrderID");
            AlterColumn("dbo.OrderDetails", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderID");
            AddForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            AlterColumn("dbo.OrderDetails", "OrderID", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "OrderID", newName: "Order_ID");
            CreateIndex("dbo.OrderDetails", "Order_ID");
            AddForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders", "ID");
        }
    }
}
