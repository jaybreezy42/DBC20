namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcting_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.drugs", "stockhistory_HistoryID", "dbo.stockhistories");
            DropIndex("dbo.drugs", new[] { "stockhistory_HistoryID" });
            AddColumn("dbo.stockhistories", "drug_DrugID", c => c.Guid());
            CreateIndex("dbo.stockhistories", "drug_DrugID");
            AddForeignKey("dbo.stockhistories", "drug_DrugID", "dbo.drugs", "DrugID");
            DropColumn("dbo.drugs", "stockhistory_HistoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.drugs", "stockhistory_HistoryID", c => c.Guid());
            DropForeignKey("dbo.stockhistories", "drug_DrugID", "dbo.drugs");
            DropIndex("dbo.stockhistories", new[] { "drug_DrugID" });
            DropColumn("dbo.stockhistories", "drug_DrugID");
            CreateIndex("dbo.drugs", "stockhistory_HistoryID");
            AddForeignKey("dbo.drugs", "stockhistory_HistoryID", "dbo.stockhistories", "HistoryID");
        }
    }
}
