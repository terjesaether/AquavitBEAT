namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongToArtistmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SongToArtists",
                c => new
                    {
                        SongToArtistId = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongToArtistId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongToArtists", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongToArtists", "ArtistId", "dbo.Artists");
            DropIndex("dbo.SongToArtists", new[] { "ArtistId" });
            DropIndex("dbo.SongToArtists", new[] { "SongId" });
            DropTable("dbo.SongToArtists");
        }
    }
}
