namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editing_stockhistorys_model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.stockhistories", "drug_DrugID", "dbo.drugs");
            DropIndex("dbo.stockhistories", new[] { "drug_DrugID" });
            RenameColumn(table: "dbo.stockhistories", name: "drug_DrugID", newName: "DrugID");
            AlterColumn("dbo.stockhistories", "DrugID", c => c.Guid(nullable: false));
            CreateIndex("dbo.stockhistories", "DrugID");
            AddForeignKey("dbo.stockhistories", "DrugID", "dbo.drugs", "DrugID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.stockhistories", "DrugID", "dbo.drugs");
            DropIndex("dbo.stockhistories", new[] { "DrugID" });
            AlterColumn("dbo.stockhistories", "DrugID", c => c.Guid());
            RenameColumn(table: "dbo.stockhistories", name: "DrugID", newName: "drug_DrugID");
            CreateIndex("dbo.stockhistories", "drug_DrugID");
            AddForeignKey("dbo.stockhistories", "drug_DrugID", "dbo.drugs", "DrugID");
        }
    }
}
