namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StockOuts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        StockInID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockOuts", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.StockOuts", new[] { "EmployeeID" });
            DropTable("dbo.StockOuts");
            DropTable("dbo.Employees");
        }
    }
}
