namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMS_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SMS",
                c => new
                    {
                        SMSID = c.Guid(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.SMSID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SMS");
        }
    }
}
