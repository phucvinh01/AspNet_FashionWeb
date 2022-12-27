namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        CategoryID = c.Int(),
                        Description = c.String(),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);

            CreateTable(
                "dbo.Bag",
                c => new
                {
                    ShoppingID = c.Long(nullable: false, identity: true),
                    ProductID = c.Long(nullable: false),
                    Price = c.Int(nullable: false),
                    CustomerID = c.Int(),
                    Quantity = c.Int(),
                })
                .PrimaryKey(t => t.ShoppingID);

        }


        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Bag");
        }
    }
}
