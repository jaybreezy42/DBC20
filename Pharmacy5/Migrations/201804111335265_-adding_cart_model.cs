namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_cart_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.carts",
                c => new
                    {
                        ItemID = c.Guid(nullable: false),
                        GenericName = c.String(),
                        BrandName = c.String(),
                        UnitPrice = c.Single(nullable: true),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.carts");
        }
    }
}
