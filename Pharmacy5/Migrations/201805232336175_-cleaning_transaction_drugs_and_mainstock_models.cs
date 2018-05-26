namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleaning_transaction_drugs_and_mainstock_models : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.mainstocks", "DrugID");
            RenameColumn(table: "dbo.mainstocks", name: "StockID", newName: "DrugID");
            RenameIndex(table: "dbo.mainstocks", name: "IX_StockID", newName: "IX_DrugID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.mainstocks", name: "IX_DrugID", newName: "IX_StockID");
            RenameColumn(table: "dbo.mainstocks", name: "DrugID", newName: "StockID");
            AddColumn("dbo.mainstocks", "DrugID", c => c.Guid(nullable: false));
        }
    }
}
