namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_imgurl_to_drug_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.drugs", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.drugs", "ImgUrl");
        }
    }
}
