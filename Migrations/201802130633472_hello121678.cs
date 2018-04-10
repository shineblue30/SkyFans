namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello121678 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "DealerID", "dbo.Dealers");
            DropIndex("dbo.Orders", new[] { "DealerID" });
            RenameColumn(table: "dbo.Orders", name: "DealerID", newName: "Dealer_ID");
            AlterColumn("dbo.Orders", "Dealer_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Dealer_ID");
            AddForeignKey("dbo.Orders", "Dealer_ID", "dbo.Dealers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Dealer_ID", "dbo.Dealers");
            DropIndex("dbo.Orders", new[] { "Dealer_ID" });
            AlterColumn("dbo.Orders", "Dealer_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "Dealer_ID", newName: "DealerID");
            CreateIndex("dbo.Orders", "DealerID");
            AddForeignKey("dbo.Orders", "DealerID", "dbo.Dealers", "ID", cascadeDelete: true);
        }
    }
}
