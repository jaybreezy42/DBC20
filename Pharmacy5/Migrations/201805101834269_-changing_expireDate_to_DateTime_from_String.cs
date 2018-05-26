namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_expireDate_to_DateTime_from_String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.drugs", "ExpireDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.drugs", "ExpireDate", c => c.String());
        }
    }
}
