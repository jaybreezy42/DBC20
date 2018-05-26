namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checking_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions");
            DropIndex("dbo.drugs", new[] { "transaction_transactionID" });
            CreateTable(
                "dbo.transactiondrugs",
                c => new
                    {
                        transaction_transactionID = c.Guid(nullable: false),
                        drug_DrugID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.transaction_transactionID, t.drug_DrugID })
                .ForeignKey("dbo.transactions", t => t.transaction_transactionID, cascadeDelete: true)
                .ForeignKey("dbo.drugs", t => t.drug_DrugID, cascadeDelete: true)
                .Index(t => t.transaction_transactionID)
                .Index(t => t.drug_DrugID);
            
            AddColumn("dbo.transactions", "DoseName", c => c.String());
            AddColumn("dbo.transactions", "UnitPrice", c => c.Single(nullable: false));
            DropColumn("dbo.drugs", "transaction_transactionID");
            DropColumn("dbo.transactions", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.transactions", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.drugs", "transaction_transactionID", c => c.Guid());
            DropForeignKey("dbo.transactiondrugs", "drug_DrugID", "dbo.drugs");
            DropForeignKey("dbo.transactiondrugs", "transaction_transactionID", "dbo.transactions");
            DropIndex("dbo.transactiondrugs", new[] { "drug_DrugID" });
            DropIndex("dbo.transactiondrugs", new[] { "transaction_transactionID" });
            DropColumn("dbo.transactions", "UnitPrice");
            DropColumn("dbo.transactions", "DoseName");
            DropTable("dbo.transactiondrugs");
            CreateIndex("dbo.drugs", "transaction_transactionID");
            AddForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions", "transactionID");
        }
    }
}
