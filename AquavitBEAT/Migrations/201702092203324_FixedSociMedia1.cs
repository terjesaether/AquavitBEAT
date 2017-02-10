namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedSociMedia1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistSocialMedias", "Artist_ArtistId", "dbo.Artists");
            DropIndex("dbo.ArtistSocialMedias", new[] { "Artist_ArtistId" });
            RenameColumn(table: "dbo.ArtistSocialMedias", name: "Artist_ArtistId", newName: "ArtistId");
            AddColumn("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", c => c.Int());
            AlterColumn("dbo.ArtistSocialMedias", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.ArtistSocialMedias", "ArtistId");
            CreateIndex("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId");
            AddForeignKey("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", "dbo.SocialMedias", "SocialMediaId");
            AddForeignKey("dbo.ArtistSocialMedias", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistSocialMedias", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", "dbo.SocialMedias");
            DropIndex("dbo.ArtistSocialMedias", new[] { "SocialMedia_SocialMediaId" });
            DropIndex("dbo.ArtistSocialMedias", new[] { "ArtistId" });
            AlterColumn("dbo.ArtistSocialMedias", "ArtistId", c => c.Int());
            DropColumn("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId");
            RenameColumn(table: "dbo.ArtistSocialMedias", name: "ArtistId", newName: "Artist_ArtistId");
            CreateIndex("dbo.ArtistSocialMedias", "Artist_ArtistId");
            AddForeignKey("dbo.ArtistSocialMedias", "Artist_ArtistId", "dbo.Artists", "ArtistId");
        }
    }
}
