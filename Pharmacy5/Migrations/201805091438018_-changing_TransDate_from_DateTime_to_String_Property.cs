namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_TransDate_from_DateTime_to_String_Property : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transactions", "DateOfTrans", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transactions", "DateOfTrans", c => c.DateTime(nullable: false));
        }
    }
}
