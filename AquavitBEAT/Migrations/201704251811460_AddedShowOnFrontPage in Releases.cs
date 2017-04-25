namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShowOnFrontPageinReleases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Releases", "ShowOnFrontpage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Releases", "ShowOnFrontpage");
        }
    }
}
