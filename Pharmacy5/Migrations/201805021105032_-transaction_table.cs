namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transaction_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.transactions",
                c => new
                    {
                        transactionID = c.Guid(nullable: false, identity:true),
                        BrandName = c.String(),
                        GenericName = c.String(),
                        Dose = c.String(),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        InStock = c.Int(nullable: false),
                        Status = c.String(),
                        DateOfTrans = c.DateTime(nullable: false),
                        clientname = c.String(),
                        clientphone = c.String(),
                    })
                .PrimaryKey(t => t.transactionID);
            
            AddColumn("dbo.drugs", "transaction_transactionID", c => c.Guid());
            CreateIndex("dbo.drugs", "transaction_transactionID");
            AddForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions", "transactionID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions");
            DropIndex("dbo.drugs", new[] { "transaction_transactionID" });
            DropColumn("dbo.drugs", "transaction_transactionID");
            DropTable("dbo.transactions");
        }
    }
}
