namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class search_model : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.searches");
        }
    }
}
