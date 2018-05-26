namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editting_transaction_model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transactions", "clientphone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transactions", "clientphone", c => c.String(maxLength: 10));
        }
    }
}
