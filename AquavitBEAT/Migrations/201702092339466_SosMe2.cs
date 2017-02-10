namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SosMe2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", "dbo.SocialMedias");
            DropIndex("dbo.ArtistSocialMedias", new[] { "SocialMedia_SocialMediaId" });
            DropColumn("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", c => c.Int());
            CreateIndex("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId");
            AddForeignKey("dbo.ArtistSocialMedias", "SocialMedia_SocialMediaId", "dbo.SocialMedias", "SocialMediaId");
        }
    }
}
