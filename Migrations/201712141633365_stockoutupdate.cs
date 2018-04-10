namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockoutupdate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.StockOuts", "StockInID");
            AddForeignKey("dbo.StockOuts", "StockInID", "dbo.StockIns", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockOuts", "StockInID", "dbo.StockIns");
            DropIndex("dbo.StockOuts", new[] { "StockInID" });
        }
    }
}
