namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleaning_up_transaction_and_drugs_models : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.transactions", "BrandName");
            DropColumn("dbo.transactions", "GenericName");
            DropColumn("dbo.transactions", "Dose");
            DropColumn("dbo.transactions", "DoseName");
            DropColumn("dbo.transactions", "UnitPrice");
            DropColumn("dbo.transactions", "InStock");
            DropColumn("dbo.transactions", "DrugID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.transactions", "DrugID", c => c.Guid(nullable: false));
            AddColumn("dbo.transactions", "InStock", c => c.Int(nullable: false));
            AddColumn("dbo.transactions", "UnitPrice", c => c.Single(nullable: false));
            AddColumn("dbo.transactions", "DoseName", c => c.String());
            AddColumn("dbo.transactions", "Dose", c => c.String());
            AddColumn("dbo.transactions", "GenericName", c => c.String());
            AddColumn("dbo.transactions", "BrandName", c => c.String());
        }
    }
}
