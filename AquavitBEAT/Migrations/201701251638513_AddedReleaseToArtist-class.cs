namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReleaseToArtistclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReleaseToArtists",
                c => new
                    {
                        ReleaseToArtistId = c.Int(nullable: false, identity: true),
                        ArtistId = c.Int(nullable: false),
                        ReleaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReleaseToArtistId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Releases", t => t.ReleaseId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.ReleaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReleaseToArtists", "ReleaseId", "dbo.Releases");
            DropForeignKey("dbo.ReleaseToArtists", "ArtistId", "dbo.Artists");
            DropIndex("dbo.ReleaseToArtists", new[] { "ReleaseId" });
            DropIndex("dbo.ReleaseToArtists", new[] { "ArtistId" });
            DropTable("dbo.ReleaseToArtists");
        }
    }
}
