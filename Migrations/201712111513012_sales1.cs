namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sales1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dealers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DealerProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DealerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dealers", t => t.DealerID, cascadeDelete: true)
                .Index(t => t.DealerID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        AmountToPay = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        PaymentVia = c.String(),
                        DealerProductID = c.Int(nullable: false),
                        DealerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DealerProducts", t => t.DealerProductID, cascadeDelete: true)
                .Index(t => t.DealerProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "DealerProductID", "dbo.DealerProducts");
            DropForeignKey("dbo.DealerProducts", "DealerID", "dbo.Dealers");
            DropIndex("dbo.Sales", new[] { "DealerProductID" });
            DropIndex("dbo.DealerProducts", new[] { "DealerID" });
            DropTable("dbo.Sales");
            DropTable("dbo.DealerProducts");
            DropTable("dbo.Dealers");
        }
    }
}
