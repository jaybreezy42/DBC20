namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_mainstock_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mainstocks",
                c => new
                    {
                        StockID = c.Guid(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.drugs", t => t.StockID)
                .Index(t => t.StockID);
            
            DropTable("dbo.carts");
            DropTable("dbo.orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        OrderID = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        CustomerPhoneNumber = c.String(),
                        DrugDetails = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.carts",
                c => new
                    {
                        ItemID = c.Guid(nullable: false),
                        GenericName = c.String(),
                        BrandName = c.String(),
                        UnitPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            DropForeignKey("dbo.mainstocks", "StockID", "dbo.drugs");
            DropIndex("dbo.mainstocks", new[] { "StockID" });
            DropTable("dbo.mainstocks");
        }
    }
}
