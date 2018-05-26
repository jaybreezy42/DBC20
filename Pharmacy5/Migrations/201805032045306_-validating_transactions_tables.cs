namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validating_transactions_tables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transactions", "clientname", c => c.String(nullable: false));
            AlterColumn("dbo.transactions", "clientphone", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transactions", "clientphone", c => c.String());
            AlterColumn("dbo.transactions", "clientname", c => c.String());
        }
    }
}
