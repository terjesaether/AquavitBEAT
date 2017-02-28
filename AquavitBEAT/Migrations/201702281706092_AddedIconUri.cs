namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIconUri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialMedias", "IconUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SocialMedias", "IconUri");
        }
    }
}
