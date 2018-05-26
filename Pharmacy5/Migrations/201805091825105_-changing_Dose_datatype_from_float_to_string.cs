namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_Dose_datatype_from_float_to_string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.drugs", "Dose", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.drugs", "Dose", c => c.Single(nullable: false));
        }
    }
}
