namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bags",
                c => new
                    {
                        ShoppingID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        Total = c.Double(),
                        Quantity = c.Int(),
                        IDCustomer = c.String(),
                    })
                .PrimaryKey(t => t.ShoppingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bags");
        }
    }
}
