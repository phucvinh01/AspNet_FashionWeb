namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCol2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductID");
        }
    }
}
