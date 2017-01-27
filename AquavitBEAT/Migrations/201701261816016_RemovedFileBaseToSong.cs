namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFileBaseToSong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Songs", "AudioUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Songs", "AudioUrl", c => c.String(nullable: false));
        }
    }
}
