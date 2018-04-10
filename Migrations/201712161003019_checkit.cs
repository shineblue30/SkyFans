namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockOuts", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.StProducts", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.StockIns", "StProductID", "dbo.StProducts");
            DropForeignKey("dbo.StockOuts", "StProduct_ID", "dbo.StProducts");
            DropIndex("dbo.StockOuts", new[] { "Company_ID" });
            DropIndex("dbo.StockOuts", new[] { "StProduct_ID" });
            DropIndex("dbo.StProducts", new[] { "CompanyID" });
            DropIndex("dbo.StockIns", new[] { "StProductID" });
            DropColumn("dbo.StockOuts", "Company_ID");
            DropColumn("dbo.StockOuts", "StProduct_ID");
            DropColumn("dbo.StockIns", "StProductID");
            DropColumn("dbo.StockIns", "CompanyID");
            DropTable("dbo.Companies");
            DropTable("dbo.StProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            AddColumn("dbo.StockIns", "CompanyID", c => c.Int(nullable: false));
            AddColumn("dbo.StockIns", "StProductID", c => c.Int(nullable: false));
            AddColumn("dbo.StockOuts", "StProduct_ID", c => c.Int());
            AddColumn("dbo.StockOuts", "Company_ID", c => c.Int());
            CreateIndex("dbo.StockIns", "StProductID");
            CreateIndex("dbo.StProducts", "CompanyID");
            CreateIndex("dbo.StockOuts", "StProduct_ID");
            CreateIndex("dbo.StockOuts", "Company_ID");
            AddForeignKey("dbo.StockOuts", "StProduct_ID", "dbo.StProducts", "ID");
            AddForeignKey("dbo.StockIns", "StProductID", "dbo.StProducts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StProducts", "CompanyID", "dbo.Companies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StockOuts", "Company_ID", "dbo.Companies", "ID");
        }
    }
}
