namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fans1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "Qty", c => c.Int(nullable: false));
            AddColumn("dbo.Fans", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fans", "Price");
            DropColumn("dbo.Fans", "Qty");
        }
    }
}
