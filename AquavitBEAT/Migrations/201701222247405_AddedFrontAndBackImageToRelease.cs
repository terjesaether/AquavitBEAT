namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFrontAndBackImageToRelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Releases", "frontImageUrl", c => c.String());
            AddColumn("dbo.Releases", "backImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Releases", "backImageUrl");
            DropColumn("dbo.Releases", "frontImageUrl");
        }
    }
}
