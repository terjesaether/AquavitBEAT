namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meh1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BuyUrls");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BuyUrls",
                c => new
                    {
                        BuyUrlId = c.Int(nullable: false, identity: true),
                        BuyLink = c.String(),
                        UrlName = c.String(),
                    })
                .PrimaryKey(t => t.BuyUrlId);
            
        }
    }
}
