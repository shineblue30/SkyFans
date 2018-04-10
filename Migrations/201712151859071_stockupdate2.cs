namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockupdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockOuts", "StockIn_ID", "dbo.StockIns");
            DropIndex("dbo.StockOuts", new[] { "StockIn_ID" });
            AddColumn("dbo.StockOuts", "Company_ID", c => c.Int());
            AddColumn("dbo.StockOuts", "StProduct_ID", c => c.Int());
            CreateIndex("dbo.StockOuts", "Company_ID");
            CreateIndex("dbo.StockOuts", "StProduct_ID");
            AddForeignKey("dbo.StockOuts", "Company_ID", "dbo.Companies", "ID");
            AddForeignKey("dbo.StockOuts", "StProduct_ID", "dbo.StProducts", "ID");
            DropColumn("dbo.StockOuts", "CompanyID");
            DropColumn("dbo.StockOuts", "StProductID");
            DropColumn("dbo.StockOuts", "StockIn_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockOuts", "StockIn_ID", c => c.Int());
            AddColumn("dbo.StockOuts", "StProductID", c => c.Int(nullable: false));
            AddColumn("dbo.StockOuts", "CompanyID", c => c.Int(nullable: false));
            DropForeignKey("dbo.StockOuts", "StProduct_ID", "dbo.StProducts");
            DropForeignKey("dbo.StockOuts", "Company_ID", "dbo.Companies");
            DropIndex("dbo.StockOuts", new[] { "StProduct_ID" });
            DropIndex("dbo.StockOuts", new[] { "Company_ID" });
            DropColumn("dbo.StockOuts", "StProduct_ID");
            DropColumn("dbo.StockOuts", "Company_ID");
            CreateIndex("dbo.StockOuts", "StockIn_ID");
            AddForeignKey("dbo.StockOuts", "StockIn_ID", "dbo.StockIns", "ID");
        }
    }
}
