namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editing_drugs_and_mainstock_models : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.drugs", "ExpireDate", c => c.String(nullable: true));
            AddColumn("dbo.mainstocks", "DrugID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.mainstocks", "DrugID");
            DropColumn("dbo.drugs", "ExpireDate");
        }
    }
}
