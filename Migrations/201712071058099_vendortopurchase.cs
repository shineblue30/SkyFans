namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vendortopurchase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendors", "Purchase_ID", "dbo.Purchases");
            DropIndex("dbo.Vendors", new[] { "Purchase_ID" });
            DropColumn("dbo.Vendors", "Purchase_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendors", "Purchase_ID", c => c.Int());
            CreateIndex("dbo.Vendors", "Purchase_ID");
            AddForeignKey("dbo.Vendors", "Purchase_ID", "dbo.Purchases", "ID");
        }
    }
}
