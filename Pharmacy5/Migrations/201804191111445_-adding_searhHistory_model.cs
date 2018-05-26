namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_searhHistory_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchHistories",
                c => new
                    {
                        searchID = c.Int(nullable: false, identity: true),
                        searchitem = c.String(),
                    })
                .PrimaryKey(t => t.searchID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SearchHistories");
        }
    }
}
