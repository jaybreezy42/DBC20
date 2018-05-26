namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editing_transaction_model0 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.transactiondrugs", "transaction_transactionID", "dbo.transactions");
            DropForeignKey("dbo.transactiondrugs", "drug_DrugID", "dbo.drugs");
            DropIndex("dbo.transactiondrugs", new[] { "transaction_transactionID" });
            DropIndex("dbo.transactiondrugs", new[] { "drug_DrugID" });
            AddColumn("dbo.drugs", "transaction_transactionID", c => c.Guid());
            AddColumn("dbo.transactions", "drug_DrugID", c => c.Guid());
            AddColumn("dbo.transactions", "drug_DrugID1", c => c.Guid());
            CreateIndex("dbo.drugs", "transaction_transactionID");
            CreateIndex("dbo.transactions", "drug_DrugID");
            CreateIndex("dbo.transactions", "drug_DrugID1");
            AddForeignKey("dbo.transactions", "drug_DrugID", "dbo.drugs", "DrugID");
            AddForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions", "transactionID");
            AddForeignKey("dbo.transactions", "drug_DrugID1", "dbo.drugs", "DrugID");
            DropTable("dbo.transactiondrugs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.transactiondrugs",
                c => new
                    {
                        transaction_transactionID = c.Guid(nullable: false),
                        drug_DrugID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.transaction_transactionID, t.drug_DrugID });
            
            DropForeignKey("dbo.transactions", "drug_DrugID1", "dbo.drugs");
            DropForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions");
            DropForeignKey("dbo.transactions", "drug_DrugID", "dbo.drugs");
            DropIndex("dbo.transactions", new[] { "drug_DrugID1" });
            DropIndex("dbo.transactions", new[] { "drug_DrugID" });
            DropIndex("dbo.drugs", new[] { "transaction_transactionID" });
            DropColumn("dbo.transactions", "drug_DrugID1");
            DropColumn("dbo.transactions", "drug_DrugID");
            DropColumn("dbo.drugs", "transaction_transactionID");
            CreateIndex("dbo.transactiondrugs", "drug_DrugID");
            CreateIndex("dbo.transactiondrugs", "transaction_transactionID");
            AddForeignKey("dbo.transactiondrugs", "drug_DrugID", "dbo.drugs", "DrugID", cascadeDelete: true);
            AddForeignKey("dbo.transactiondrugs", "transaction_transactionID", "dbo.transactions", "transactionID", cascadeDelete: true);
        }
    }
}
