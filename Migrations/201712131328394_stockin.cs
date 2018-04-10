namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
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
                        StProductID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StProducts", t => t.StProductID, cascadeDelete: true)
                .Index(t => t.StProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockIns", "StProductID", "dbo.StProducts");
            DropForeignKey("dbo.StProducts", "CompanyID", "dbo.Companies");
            DropIndex("dbo.StockIns", new[] { "StProductID" });
            DropIndex("dbo.StProducts", new[] { "CompanyID" });
            DropTable("dbo.StockIns");
            DropTable("dbo.StProducts");
            DropTable("dbo.Companies");
        }
    }
}
