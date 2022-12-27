namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProducIDtFromCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ProductID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "ProductID");
        }
    }
}
