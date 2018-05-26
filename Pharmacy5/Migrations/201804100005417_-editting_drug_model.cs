namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editting_drug_model : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.drugs", "ExpiryDate");
            DropColumn("dbo.drugs", "BatchNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.drugs", "BatchNo", c => c.String(nullable: false));
            AddColumn("dbo.drugs", "ExpiryDate", c => c.DateTime(nullable: false));
        }
    }
}
