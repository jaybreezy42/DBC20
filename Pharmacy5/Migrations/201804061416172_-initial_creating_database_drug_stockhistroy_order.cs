namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_creating_database_drug_stockhistroy_order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.drugs",
                c => new
                    {
                        DrugID = c.Guid(nullable: false),
                        GenericName = c.String(nullable: false),
                        BrandName = c.String(nullable: false),
                        Dose = c.Single(nullable: false),
                        DoseName = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        BatchNo = c.String(nullable: false),
                        SellingUnitPrice = c.Single(nullable: false),
                        stockhistory_HistoryID = c.Guid(),
                    })
                .PrimaryKey(t => t.DrugID)
                .ForeignKey("dbo.stockhistories", t => t.stockhistory_HistoryID)
                .Index(t => t.stockhistory_HistoryID);
            
            CreateTable(
                "dbo.stockhistories",
                c => new
                    {
                        HistoryID = c.Guid(nullable: false),
                        Quantity = c.Single(nullable: false),
                        QuantityName = c.String(),
                        DateReceived = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.drugs", "stockhistory_HistoryID", "dbo.stockhistories");
            DropIndex("dbo.drugs", new[] { "stockhistory_HistoryID" });
            DropTable("dbo.orders");
            DropTable("dbo.stockhistories");
            DropTable("dbo.drugs");
        }
    }
}
