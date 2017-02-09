namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongTORemixerDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SongToRemixers",
                c => new
                    {
                        SongToRemixerId = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongToRemixerId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongToRemixers", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongToRemixers", "ArtistId", "dbo.Artists");
            DropIndex("dbo.SongToRemixers", new[] { "ArtistId" });
            DropIndex("dbo.SongToRemixers", new[] { "SongId" });
            DropTable("dbo.SongToRemixers");
        }
    }
}
