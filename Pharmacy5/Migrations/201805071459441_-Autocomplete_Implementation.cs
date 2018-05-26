namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autocomplete_Implementation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrugAutoCompletes",
                c => new
                    {
                        DrugID = c.Guid(nullable: false),
                        BrandName = c.String(),
                        GenericName = c.String(),
                    })
                .PrimaryKey(t => t.DrugID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DrugAutoCompletes");
        }
    }
}
