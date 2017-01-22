namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        About = c.String(),
                        Mail = c.String(nullable: false),
                        Address = c.String(),
                        Country = c.String(),
                        ProfileImgUrl = c.String(),
                        Song_SongId = c.Int(),
                        Song_SongId1 = c.Int(),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Songs", t => t.Song_SongId)
                .ForeignKey("dbo.Songs", t => t.Song_SongId1)
                .Index(t => t.Song_SongId)
                .Index(t => t.Song_SongId1);
            
            CreateTable(
                "dbo.Artworks",
                c => new
                    {
                        ArtworkId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Comment = c.String(),
                        Image_ImageId = c.Int(),
                        TypeOfArtwork_ReleaseTypeId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(),
                        Release_ReleaseId = c.Int(),
                    })
                .PrimaryKey(t => t.ArtworkId)
                .ForeignKey("dbo.Images", t => t.Image_ImageId)
                .ForeignKey("dbo.ReleaseTypes", t => t.TypeOfArtwork_ReleaseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId)
                .ForeignKey("dbo.Releases", t => t.Release_ReleaseId)
                .Index(t => t.Image_ImageId)
                .Index(t => t.TypeOfArtwork_ReleaseTypeId)
                .Index(t => t.Artist_ArtistId)
                .Index(t => t.Release_ReleaseId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImgUrl = c.String(),
                        Author_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Artists", t => t.Author_ArtistId, cascadeDelete: true)
                .Index(t => t.Author_ArtistId);
            
            CreateTable(
                "dbo.ReleaseTypes",
                c => new
                    {
                        ReleaseTypeId = c.Int(nullable: false, identity: true),
                        ReleaseTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReleaseTypeId);
            
            CreateTable(
                "dbo.Releases",
                c => new
                    {
                        ReleaseId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseType_ReleaseTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReleaseId)
                .ForeignKey("dbo.ReleaseTypes", t => t.ReleaseType_ReleaseTypeId, cascadeDelete: true)
                .Index(t => t.ReleaseType_ReleaseTypeId);
            
            CreateTable(
                "dbo.ReleaseFormats",
                c => new
                    {
                        ReleaseFormatId = c.Int(nullable: false, identity: true),
                        FormatTypeId = c.Int(nullable: false),
                        Release_ReleaseId = c.Int(),
                    })
                .PrimaryKey(t => t.ReleaseFormatId)
                .ForeignKey("dbo.FormatTypes", t => t.FormatTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Releases", t => t.Release_ReleaseId)
                .Index(t => t.FormatTypeId)
                .Index(t => t.Release_ReleaseId);
            
            CreateTable(
                "dbo.FormatTypes",
                c => new
                    {
                        FormatTypeId = c.Int(nullable: false, identity: true),
                        FormatTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FormatTypeId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        RemixName = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        AudioUrl = c.String(nullable: false),
                        Artist_ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId)
                .Index(t => t.Artist_ArtistId);
            
            CreateTable(
                "dbo.ArtistSocialMedias",
                c => new
                    {
                        ArtistSocialMediaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Artist_ArtistId = c.Int(),
                    })
                .PrimaryKey(t => t.ArtistSocialMediaId)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId)
                .Index(t => t.Artist_ArtistId);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        SocialMediaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Prefix = c.String(),
                    })
                .PrimaryKey(t => t.SocialMediaId);
            
            CreateTable(
                "dbo.ReleaseArtists",
                c => new
                    {
                        Release_ReleaseId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Release_ReleaseId, t.Artist_ArtistId })
                .ForeignKey("dbo.Releases", t => t.Release_ReleaseId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId, cascadeDelete: true)
                .Index(t => t.Release_ReleaseId)
                .Index(t => t.Artist_ArtistId);
            
            CreateTable(
                "dbo.SongReleases",
                c => new
                    {
                        Song_SongId = c.Int(nullable: false),
                        Release_ReleaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongId, t.Release_ReleaseId })
                .ForeignKey("dbo.Songs", t => t.Song_SongId, cascadeDelete: true)
                .ForeignKey("dbo.Releases", t => t.Release_ReleaseId, cascadeDelete: true)
                .Index(t => t.Song_SongId)
                .Index(t => t.Release_ReleaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistSocialMedias", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Songs", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Releases", "ReleaseType_ReleaseTypeId", "dbo.ReleaseTypes");
            DropForeignKey("dbo.Artists", "Song_SongId1", "dbo.Songs");
            DropForeignKey("dbo.SongReleases", "Release_ReleaseId", "dbo.Releases");
            DropForeignKey("dbo.SongReleases", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.Artists", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.Artworks", "Release_ReleaseId", "dbo.Releases");
            DropForeignKey("dbo.ReleaseFormats", "Release_ReleaseId", "dbo.Releases");
            DropForeignKey("dbo.ReleaseFormats", "FormatTypeId", "dbo.FormatTypes");
            DropForeignKey("dbo.ReleaseArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ReleaseArtists", "Release_ReleaseId", "dbo.Releases");
            DropForeignKey("dbo.Artworks", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", "dbo.ReleaseTypes");
            DropForeignKey("dbo.Artworks", "Image_ImageId", "dbo.Images");
            DropForeignKey("dbo.Images", "Author_ArtistId", "dbo.Artists");
            DropIndex("dbo.SongReleases", new[] { "Release_ReleaseId" });
            DropIndex("dbo.SongReleases", new[] { "Song_SongId" });
            DropIndex("dbo.ReleaseArtists", new[] { "Artist_ArtistId" });
            DropIndex("dbo.ReleaseArtists", new[] { "Release_ReleaseId" });
            DropIndex("dbo.ArtistSocialMedias", new[] { "Artist_ArtistId" });
            DropIndex("dbo.Songs", new[] { "Artist_ArtistId" });
            DropIndex("dbo.ReleaseFormats", new[] { "Release_ReleaseId" });
            DropIndex("dbo.ReleaseFormats", new[] { "FormatTypeId" });
            DropIndex("dbo.Releases", new[] { "ReleaseType_ReleaseTypeId" });
            DropIndex("dbo.Images", new[] { "Author_ArtistId" });
            DropIndex("dbo.Artworks", new[] { "Release_ReleaseId" });
            DropIndex("dbo.Artworks", new[] { "Artist_ArtistId" });
            DropIndex("dbo.Artworks", new[] { "TypeOfArtwork_ReleaseTypeId" });
            DropIndex("dbo.Artworks", new[] { "Image_ImageId" });
            DropIndex("dbo.Artists", new[] { "Song_SongId1" });
            DropIndex("dbo.Artists", new[] { "Song_SongId" });
            DropTable("dbo.SongReleases");
            DropTable("dbo.ReleaseArtists");
            DropTable("dbo.SocialMedias");
            DropTable("dbo.ArtistSocialMedias");
            DropTable("dbo.Songs");
            DropTable("dbo.FormatTypes");
            DropTable("dbo.ReleaseFormats");
            DropTable("dbo.Releases");
            DropTable("dbo.ReleaseTypes");
            DropTable("dbo.Images");
            DropTable("dbo.Artworks");
            DropTable("dbo.Artists");
        }
    }
}
