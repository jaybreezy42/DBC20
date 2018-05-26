namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editing_expireDate_to_making_it_nulluable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.drugs", "ExpireDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.drugs", "ExpireDate", c => c.DateTime(nullable: false));
        }
    }
}
