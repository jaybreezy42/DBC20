namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_barcode_to_drug_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.drugs", "BarCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.drugs", "BarCode");
        }
    }
}
