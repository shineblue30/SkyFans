namespace SkyFan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetails3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetails", "PaymentVia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "PaymentVia", c => c.String());
        }
    }
}
