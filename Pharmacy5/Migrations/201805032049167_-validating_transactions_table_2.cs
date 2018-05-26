namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validating_transactions_table_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transactions", "clientname", c => c.String());
            AlterColumn("dbo.transactions", "clientphone", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transactions", "clientphone", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.transactions", "clientname", c => c.String(nullable: false));
        }
    }
}
