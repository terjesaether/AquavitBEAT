namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongTORemixerDbStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SongToRemixers", "RemixName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SongToRemixers", "RemixName");
        }
    }
}
