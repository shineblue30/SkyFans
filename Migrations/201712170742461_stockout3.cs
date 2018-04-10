namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockout3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockOuts", "RemQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockOuts", "RemQuantity");
        }
    }
}
