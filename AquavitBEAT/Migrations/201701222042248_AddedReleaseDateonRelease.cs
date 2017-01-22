namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReleaseDateonRelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Releases", "ReleaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Releases", "ReleaseDate");
        }
    }
}
