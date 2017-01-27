namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongToRelease : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SongToReleases",
                c => new
                    {
                        SongToReleaseId = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        ReleaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongToReleaseId)
                .ForeignKey("dbo.Releases", t => t.ReleaseId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.ReleaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongToReleases", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongToReleases", "ReleaseId", "dbo.Releases");
            DropIndex("dbo.SongToReleases", new[] { "ReleaseId" });
            DropIndex("dbo.SongToReleases", new[] { "SongId" });
            DropTable("dbo.SongToReleases");
        }
    }
}
