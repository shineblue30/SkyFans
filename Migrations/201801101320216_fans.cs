namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Picture = c.String(),
                        VendorProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VendorProducts", t => t.VendorProductID, cascadeDelete: true)
                .Index(t => t.VendorProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fans", "VendorProductID", "dbo.VendorProducts");
            DropIndex("dbo.Fans", new[] { "VendorProductID" });
            DropTable("dbo.Fans");
        }
    }
}
