namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addamount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "AmountToPay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "AmountToPay");
        }
    }
}
