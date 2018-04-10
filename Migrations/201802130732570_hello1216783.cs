namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello1216783 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Dealer_ID", "dbo.Dealers");
            DropIndex("dbo.Orders", new[] { "Dealer_ID" });
            RenameColumn(table: "dbo.Orders", name: "Dealer_ID", newName: "DealerID");
            AlterColumn("dbo.Orders", "DealerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "DealerID");
            AddForeignKey("dbo.Orders", "DealerID", "dbo.Dealers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DealerID", "dbo.Dealers");
            DropIndex("dbo.Orders", new[] { "DealerID" });
            AlterColumn("dbo.Orders", "DealerID", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "DealerID", newName: "Dealer_ID");
            CreateIndex("dbo.Orders", "Dealer_ID");
            AddForeignKey("dbo.Orders", "Dealer_ID", "dbo.Dealers", "ID");
        }
    }
}
