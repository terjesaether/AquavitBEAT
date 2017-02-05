namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedStuff : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Releases", "frontImageUrl");
            DropColumn("dbo.Releases", "backImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Releases", "backImageUrl", c => c.String());
            AddColumn("dbo.Releases", "frontImageUrl", c => c.String());
        }
    }
}
