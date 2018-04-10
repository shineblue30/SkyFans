namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockoutupdate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockOuts", "StockInID", "dbo.StockIns");
            DropIndex("dbo.StockOuts", new[] { "StockInID" });
            RenameColumn(table: "dbo.StockOuts", name: "StockInID", newName: "StockIn_ID");
            AddColumn("dbo.StockOuts", "CompanyID", c => c.Int(nullable: false));
            AddColumn("dbo.StockOuts", "StProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.StockOuts", "StockIn_ID", c => c.Int());
            CreateIndex("dbo.StockOuts", "StockIn_ID");
            AddForeignKey("dbo.StockOuts", "StockIn_ID", "dbo.StockIns", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockOuts", "StockIn_ID", "dbo.StockIns");
            DropIndex("dbo.StockOuts", new[] { "StockIn_ID" });
            AlterColumn("dbo.StockOuts", "StockIn_ID", c => c.Int(nullable: false));
            DropColumn("dbo.StockOuts", "StProductID");
            DropColumn("dbo.StockOuts", "CompanyID");
            RenameColumn(table: "dbo.StockOuts", name: "StockIn_ID", newName: "StockInID");
            CreateIndex("dbo.StockOuts", "StockInID");
            AddForeignKey("dbo.StockOuts", "StockInID", "dbo.StockIns", "ID", cascadeDelete: true);
        }
    }
}
