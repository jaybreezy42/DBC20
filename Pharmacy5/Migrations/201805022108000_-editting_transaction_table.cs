namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editting_transaction_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.transactions", "DrugID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.transactions", "DrugID");
        }
    }
}
