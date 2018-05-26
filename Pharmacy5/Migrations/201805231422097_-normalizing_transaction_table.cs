namespace Pharmacy5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizing_transaction_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.clientinfoes",
                c => new
                    {
                        clientID = c.Guid(nullable: false),
                        clientname = c.String(),
                        clientphone = c.String(),
                    })
                .PrimaryKey(t => t.clientID);
            
            AddColumn("dbo.transactions", "clientID", c => c.Guid(nullable: false));
            CreateIndex("dbo.transactions", "clientID");
            AddForeignKey("dbo.transactions", "clientID", "dbo.clientinfoes", "clientID", cascadeDelete: true);
            DropColumn("dbo.transactions", "clientname");
            DropColumn("dbo.transactions", "clientphone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.transactions", "clientphone", c => c.String());
            AddColumn("dbo.transactions", "clientname", c => c.String());
            DropForeignKey("dbo.transactions", "clientID", "dbo.clientinfoes");
            DropIndex("dbo.transactions", new[] { "clientID" });
            DropColumn("dbo.transactions", "clientID");
            DropTable("dbo.clientinfoes");
        }
    }
}
