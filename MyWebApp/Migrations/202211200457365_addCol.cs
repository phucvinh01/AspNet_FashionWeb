namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "Product_ProductID", c => c.Long());
            AddColumn("dbo.OrderDetails", "Product_ProductID", c => c.Long());
            AlterColumn("dbo.Carts", "CartID", c => c.String());
            CreateIndex("dbo.Carts", "Product_ProductID");
            CreateIndex("dbo.OrderDetails", "Product_ProductID");
            AddForeignKey("dbo.Carts", "Product_ProductID", "dbo.Products", "ProductID");
            AddForeignKey("dbo.OrderDetails", "Product_ProductID", "dbo.Products", "ProductID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Carts", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "Product_ProductID" });
            DropIndex("dbo.Carts", new[] { "Product_ProductID" });
            AlterColumn("dbo.Carts", "CartID", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Product_ProductID");
            DropColumn("dbo.Carts", "Product_ProductID");
        }
    }
}
