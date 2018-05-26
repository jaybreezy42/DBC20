namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editing_drugs_model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.transactions", "drug_DrugID", "dbo.drugs");
            DropForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions");
            DropForeignKey("dbo.transactions", "drug_DrugID1", "dbo.drugs");
            DropIndex("dbo.drugs", new[] { "transaction_transactionID" });
            DropIndex("dbo.transactions", new[] { "drug_DrugID" });
            DropIndex("dbo.transactions", new[] { "drug_DrugID1" });
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
            
            DropColumn("dbo.drugs", "transactionID");
            DropColumn("dbo.drugs", "transaction_transactionID");
            DropColumn("dbo.transactions", "drug_DrugID");
            DropColumn("dbo.transactions", "drug_DrugID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.transactions", "drug_DrugID1", c => c.Guid());
            AddColumn("dbo.transactions", "drug_DrugID", c => c.Guid());
            AddColumn("dbo.drugs", "transaction_transactionID", c => c.Guid());
            AddColumn("dbo.drugs", "transactionID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.transactiondrugs", "drug_DrugID", "dbo.drugs");
            DropForeignKey("dbo.transactiondrugs", "transaction_transactionID", "dbo.transactions");
            DropIndex("dbo.transactiondrugs", new[] { "drug_DrugID" });
            DropIndex("dbo.transactiondrugs", new[] { "transaction_transactionID" });
            DropTable("dbo.transactiondrugs");
            CreateIndex("dbo.transactions", "drug_DrugID1");
            CreateIndex("dbo.transactions", "drug_DrugID");
            CreateIndex("dbo.drugs", "transaction_transactionID");
            AddForeignKey("dbo.transactions", "drug_DrugID1", "dbo.drugs", "DrugID");
            AddForeignKey("dbo.drugs", "transaction_transactionID", "dbo.transactions", "transactionID");
            AddForeignKey("dbo.transactions", "drug_DrugID", "dbo.drugs", "DrugID");
        }
    }
}
