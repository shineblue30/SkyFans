namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fansss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FanParts", "Total", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FanParts", "Total");
        }
    }
}
