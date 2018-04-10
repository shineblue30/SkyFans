namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchasebalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Balance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Balance");
        }
    }
}
