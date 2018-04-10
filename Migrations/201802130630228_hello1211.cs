namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello1211 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "OrderDetailID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderDetailID", c => c.Int(nullable: false));
        }
    }
}
