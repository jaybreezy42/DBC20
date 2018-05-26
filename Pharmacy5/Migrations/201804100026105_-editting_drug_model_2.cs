namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editting_drug_model_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.drugs", "GenericName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.drugs", "GenericName", c => c.String(nullable: false));
        }
    }
}
