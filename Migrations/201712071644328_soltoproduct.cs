namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class soltoproduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VendorProducts", "Price");
            DropColumn("dbo.VendorProducts", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VendorProducts", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.VendorProducts", "Price", c => c.Int(nullable: false));
        }
    }
}
