namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editting_drug_model_to_get_transaction_foreign_key_working : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.drugs", "transactionID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.drugs", "transactionID");
        }
    }
}
