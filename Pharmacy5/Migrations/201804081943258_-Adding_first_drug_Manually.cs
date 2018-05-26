namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_first_drug_Manually : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.searches");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.searches",
                c => new
                    {
                        SearchID = c.Guid(nullable: false),
                        Search = c.String(),
                    })
                .PrimaryKey(t => t.SearchID);
            
        }
    }
}
